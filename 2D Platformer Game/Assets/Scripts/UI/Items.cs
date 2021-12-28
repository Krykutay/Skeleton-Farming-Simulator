using UnityEngine;

public class Items
{
    public enum ItemType
    {
        DefaultSword,
        BlueSword,
        CyanSword,
        GreenSword,
        RedSword,
        PurpleSword,
        DefaultSkin,
        BlueSkin,
        GreenSkin,
        YellowSkin,
        BrownSkin,
        ExtraHealth
    }

    public static int GetCost(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.DefaultSword:
                return 0;
            case ItemType.BlueSword:
                return 10;
            case ItemType.CyanSword:
                return 10;
            case ItemType.GreenSword:
                return 10;
            case ItemType.RedSword:
                return 10;
            case ItemType.PurpleSword:
                return 10;
            case ItemType.DefaultSkin:
                return 0;
            case ItemType.BlueSkin:
                return 5;
            case ItemType.GreenSkin:
                return 5;
            case ItemType.YellowSkin:
                return 5;
            case ItemType.BrownSkin:
                return 5;
            case ItemType.ExtraHealth:
                return 30;
        }
    }

    public static Sprite[] GetSprite(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.DefaultSword:
                return GameAssets.Instance.s_DefaultSword;
            case ItemType.BlueSword:
                return GameAssets.Instance.s_BlueSword;
            case ItemType.CyanSword:
                return GameAssets.Instance.s_CyanSword;
            case ItemType.GreenSword:
                return GameAssets.Instance.s_GreenSword;
            case ItemType.RedSword:
                return GameAssets.Instance.s_RedSword;
            case ItemType.PurpleSword:
                return GameAssets.Instance.s_PurpleSword;
            case ItemType.DefaultSkin:
                return GameAssets.Instance.s_DefaultSkin;
            case ItemType.BlueSkin:
                return GameAssets.Instance.s_BlueSkin;
            case ItemType.GreenSkin:
                return GameAssets.Instance.s_GreenSkin;
            case ItemType.YellowSkin:
                return GameAssets.Instance.s_YellowSkin;
            case ItemType.BrownSkin:
                return GameAssets.Instance.s_BrownSkin;
            case ItemType.ExtraHealth:
                return GameAssets.Instance.s_DefaultSkin;
        }
    }

}
