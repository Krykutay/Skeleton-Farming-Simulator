using UnityEngine;

public class Items
{
    static int _defaultSwordCost = 0;
    static int _blueSwordCost = 12;
    static int _cyanSwordCost = 12;
    static int _greenSwordCost = 12;
    static int _redSwordCost = 12;
    static int _purpleSwordCost = 12;
    
    static int _defaultOutfitCost = 0;
    static int _blueOutfitCost = 8;
    static int _greenOutfitCost = 8;
    static int _yellowOutfitCost = 8;
    static int _brownOutfitCost = 8;
    
    static int _defenseBoostCost = 30;
    static int _offenseBoostCost = 30;

    public enum ItemType
    {
        DefaultSword,
        BlueSword,
        CyanSword,
        GreenSword,
        RedSword,
        PurpleSword,
        DefaultOutfit,
        BlueOutfit,
        GreenOutfit,
        YellowOutfit,
        BrownOutfit,
        DefenseBoost,
        OffenseBoost
    }

    public static int GetCost(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.DefaultSword:
                return _defaultSwordCost;
            case ItemType.BlueSword:
                return _blueSwordCost;
            case ItemType.CyanSword:
                return _cyanSwordCost;
            case ItemType.GreenSword:
                return _greenSwordCost;
            case ItemType.RedSword:
                return _redSwordCost;
            case ItemType.PurpleSword:
                return _purpleSwordCost;
            case ItemType.DefaultOutfit:
                return _defaultOutfitCost;
            case ItemType.BlueOutfit:
                return _blueOutfitCost;
            case ItemType.GreenOutfit:
                return _greenOutfitCost;
            case ItemType.YellowOutfit:
                return _yellowOutfitCost;
            case ItemType.BrownOutfit:
                return _brownOutfitCost;
            case ItemType.DefenseBoost:
                return _defenseBoostCost;
            case ItemType.OffenseBoost:
                return _offenseBoostCost;
        }
    }

    public static void SetCost(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.DefaultSword:
                _defaultSwordCost = 0;
                break;
            case ItemType.BlueSword:
                _blueSwordCost = 0;
                break;
            case ItemType.CyanSword:
                _cyanSwordCost = 0;
                break;
            case ItemType.GreenSword:
                _greenSwordCost = 0;
                break;
            case ItemType.RedSword:
                _redSwordCost = 0;
                break;
            case ItemType.PurpleSword:
                _purpleSwordCost = 0;
                break;
            case ItemType.DefaultOutfit:
                _defaultOutfitCost = 0;
                break;
            case ItemType.BlueOutfit:
                _blueOutfitCost = 0;
                break;
            case ItemType.GreenOutfit:
                _greenOutfitCost = 0;
                break;
            case ItemType.YellowOutfit:
                _yellowOutfitCost = 0;
                break;
            case ItemType.BrownOutfit:
                _brownOutfitCost = 0;
                break;
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
            case ItemType.DefaultOutfit:
                return GameAssets.Instance.s_DefaultOutfit;
            case ItemType.BlueOutfit:
                return GameAssets.Instance.s_BlueOutfit;
            case ItemType.GreenOutfit:
                return GameAssets.Instance.s_GreenOutfit;
            case ItemType.YellowOutfit:
                return GameAssets.Instance.s_YellowOutfit;
            case ItemType.BrownOutfit:
                return GameAssets.Instance.s_BrownOutfit;
            case ItemType.DefenseBoost:
                return GameAssets.Instance.s_DefenseBoost;
            case ItemType.OffenseBoost:
                return GameAssets.Instance.s_OffenseBoost;
        }
    }

    public static Color[] GetTrailColor(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.DefaultSword:
                return GameAssets.Instance.c_DefaultTrail;
            case ItemType.BlueSword:
                return GameAssets.Instance.c_BlueTrail;
            case ItemType.CyanSword:
                return GameAssets.Instance.c_CyanTrail;
            case ItemType.GreenSword:
                return GameAssets.Instance.c_GreenTrail;
            case ItemType.RedSword:
                return GameAssets.Instance.c_RedTrail;
            case ItemType.PurpleSword:
                return GameAssets.Instance.c_PurpleTrail;
        }
    }

}
