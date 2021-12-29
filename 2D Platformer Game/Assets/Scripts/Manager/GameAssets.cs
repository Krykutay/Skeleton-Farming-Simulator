using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public static GameAssets Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    [Header("Sprite Swords")]
    public Sprite[] s_DefaultSword;
    public Sprite[] s_BlueSword;
    public Sprite[] s_CyanSword;
    public Sprite[] s_GreenSword;
    public Sprite[] s_RedSword;
    public Sprite[] s_PurpleSword;

    [Header("Sprite Outfits")]
    public Sprite[] s_DefaultOutfit;
    public Sprite[] s_BlueOutfit;
    public Sprite[] s_GreenOutfit;
    public Sprite[] s_YellowOutfit;
    public Sprite[] s_BrownOutfit;

    [Header("Sprite Boosts")]
    public Sprite[] s_DefenseBoost;
    public Sprite[] s_OffenseBoost;

    [Header("Color SwordTrailEffects")]
    public Color[] c_DefaultTrail;
    public Color[] c_BlueTrail;
    public Color[] c_CyanTrail;
    public Color[] c_GreenTrail;
    public Color[] c_RedTrail;
    public Color[] c_PurpleTrail;
}
