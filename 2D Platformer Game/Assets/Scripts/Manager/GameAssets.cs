using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public static GameAssets Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    public Sprite[] s_DefaultSword;
    public Sprite[] s_BlueSword;
    public Sprite[] s_CyanSword;
    public Sprite[] s_GreenSword;
    public Sprite[] s_RedSword;
    public Sprite[] s_PurpleSword;
    public Sprite[] s_DefaultSkin;
    public Sprite[] s_BlueSkin;
    public Sprite[] s_GreenSkin;
    public Sprite[] s_YellowSkin;
    public Sprite[] s_BrownSkin;
    public Sprite[] s_DefenseBoost;
    public Sprite[] s_OffenseBoost;
}
