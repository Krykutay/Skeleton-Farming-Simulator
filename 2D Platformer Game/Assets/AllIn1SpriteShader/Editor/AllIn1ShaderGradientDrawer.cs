using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class AllIn1ShaderGradientDrawer : MaterialPropertyDrawer
{
	private int resolution;
	private Texture2D textureAsset;

	public AllIn1ShaderGradientDrawer()
	{
		resolution = 64;
	}

	public AllIn1ShaderGradientDrawer(float res)
	{
		resolution = (int)res;
	}

	private static bool IsPropertyTypeSuitable(MaterialProperty prop)
	{
		return prop.type == MaterialProperty.PropType.Texture;
	}

	public string TextureName(MaterialProperty prop) => $"{prop.name}Tex";

	public override void OnGUI(Rect position, MaterialProperty prop, GUIContent label, MaterialEditor editor)
	{
		if (!IsPropertyTypeSuitable(prop))
		{
			EditorGUI.HelpBox(position, $"[Gradient] used on non-texture property \"{prop.name}\"", MessageType.Error);
			return;
		}

		if (!AssetDatabase.Contains(prop.targets.FirstOrDefault()))
		{
			EditorGUI.HelpBox(position, "Save Material To Folder to use this effect. Or use the regular Color Ramp instead", MessageType.Error);
			return;
		}

		string textureName = TextureName(prop);

		Gradient currentGradient = null;
		if (prop.targets.Length == 1)
		{
			Material target = (Material)prop.targets[0];
			string path = AssetDatabase.GetAssetPath(target);
			textureAsset = GetTextureAsset(path, textureName);
			if (textureAsset != null)
				currentGradient = DecodeGradient(prop, textureAsset.name);
			if (currentGradient == null)
				currentGradient = new Gradient() { };

			EditorGUI.showMixedValue = false;
		}
		else
		{
			EditorGUI.showMixedValue = true;
		}

		using (EditorGUI.ChangeCheckScope changeScope = new EditorGUI.ChangeCheckScope())
		{
			currentGradient = EditorGUILayout.GradientField(label, currentGradient, GUILayout.Height(15));

			if (changeScope.changed)
			{
				string encodedGradient = EncodeGradient(currentGradient);
				string fullAssetName = textureName + encodedGradient;
				foreach (Object target in prop.targets)
				{
					if (!AssetDatabase.Contains(target)) continue;

					string path = AssetDatabase.GetAssetPath(target);
					Texture2D textureAsset = GetTexture(path, textureName);
					Undo.RecordObject(textureAsset, "Change Material Gradient");
					textureAsset.name = fullAssetName;
					BakeGradient(currentGradient, textureAsset);
					EditorUtility.SetDirty(textureAsset);

					Material material = (Material)target;
					material.SetTexture(prop.name, textureAsset);
				}
			}
		}
		EditorGUI.showMixedValue = false;
	}

	private Texture2D GetTexture(string path, string name)
	{
		textureAsset = GetTextureAsset(path, name);
		if (textureAsset == null) CreateTexture(path, name);
		if (textureAsset.width != resolution) textureAsset.Resize(resolution, 1);
		return textureAsset;
	}

	private void CreateTexture(string path, string name = "unnamed texture")
	{
		textureAsset = new Texture2D(resolution, 1, TextureFormat.RGBA32, false);
		textureAsset.wrapMode = TextureWrapMode.Clamp;
		textureAsset.filterMode = FilterMode.Bilinear;
		textureAsset.name = name;
		AssetDatabase.AddObjectToAsset(textureAsset, path);
		AssetDatabase.Refresh();
	}

	private string EncodeGradient(Gradient gradient)
	{
		if (gradient == null) return null;
		return JsonUtility.ToJson(new GradientRepresentation(gradient));
	}

	private Gradient DecodeGradient(MaterialProperty prop, string name)
	{
		string json = name.Substring(TextureName(prop).Length);
		try
		{
			return JsonUtility.FromJson<GradientRepresentation>(json).ToGradient();
		}
		catch (Exception)
		{
			return null;
		}
	}

	private Texture2D GetTextureAsset(string path, string name)
	{
		return AssetDatabase.LoadAllAssetsAtPath(path).FirstOrDefault(asset => asset.name.StartsWith(name)) as Texture2D;
	}

	private void BakeGradient(Gradient gradient, Texture2D texture)
	{
		if (gradient == null) return;
		for (int x = 0; x < texture.width; x++)
		{
			Color color = gradient.Evaluate((float)x / (texture.width - 1));
			for (int y = 0; y < texture.height; y++) texture.SetPixel(x, y, color);
		}
		texture.Apply();
	}

	[MenuItem("Assets/AllIn1Shader/Remove All Gradient Textures")]
	static void RemoveAllSubassets()
	{
		foreach (Object asset in Selection.GetFiltered<Object>(SelectionMode.Assets))
		{
			string path = AssetDatabase.GetAssetPath(asset);
			foreach (Object subAsset in AssetDatabase.LoadAllAssetRepresentationsAtPath(path))
			{
				Object.DestroyImmediate(subAsset, true);
			}
			AssetDatabase.ImportAsset(path);
		}
	}

	class GradientRepresentation
	{
		public GradientMode mode;
		public ColorKey[] colorKeys;
		public AlphaKey[] alphaKeys;

		public GradientRepresentation() { }

		public GradientRepresentation(Gradient source)
		{
			FromGradient(source);
		}

		public void FromGradient(Gradient source)
		{
			mode = source.mode;
			colorKeys = source.colorKeys.Select(key => new ColorKey(key)).ToArray();
			alphaKeys = source.alphaKeys.Select(key => new AlphaKey(key)).ToArray();
		}

		public void ToGradient(Gradient gradient)
		{
			gradient.mode = mode;
			gradient.colorKeys = colorKeys.Select(key => key.ToGradientKey()).ToArray();
			gradient.alphaKeys = alphaKeys.Select(key => key.ToGradientKey()).ToArray();
		}

		public Gradient ToGradient()
		{
			Gradient gradient = new Gradient();
			ToGradient(gradient);
			return gradient;
		}

		[Serializable]
		public struct ColorKey
		{
			public Color color;
			public float time;

			public ColorKey(GradientColorKey source)
			{
				color = default;
				time = default;
				FromGradientKey(source);
			}

			public void FromGradientKey(GradientColorKey source)
			{
				color = source.color;
				time = source.time;
			}

			public GradientColorKey ToGradientKey()
			{
				GradientColorKey key;
				key.color = color;
				key.time = time;
				return key;
			}
		}

		[Serializable]
		public struct AlphaKey
		{
			public float alpha;
			public float time;

			public AlphaKey(GradientAlphaKey source)
			{
				alpha = default;
				time = default;
				FromGradientKey(source);
			}

			public void FromGradientKey(GradientAlphaKey source)
			{
				alpha = source.alpha;
				time = source.time;
			}

			public GradientAlphaKey ToGradientKey()
			{
				GradientAlphaKey key;
				key.alpha = alpha;
				key.time = time;
				return key;
			}
		}
	}
}