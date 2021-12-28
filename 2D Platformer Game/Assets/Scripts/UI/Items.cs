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
    
    static int _defenseBoostCount = 0;
    static int _offenseBoostCount = 0;

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
            case ItemType.DefaultSkin:
                return _defaultOutfitCost;
            case ItemType.BlueSkin:
                return _blueOutfitCost;
            case ItemType.GreenSkin:
                return _greenOutfitCost;
            case ItemType.YellowSkin:
                return _yellowOutfitCost;
            case ItemType.BrownSkin:
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
            case ItemType.DefaultSkin:
                _defaultOutfitCost = 0;
                break;
            case ItemType.BlueSkin:
                _blueOutfitCost = 0;
                break;
            case ItemType.GreenSkin:
                _greenOutfitCost = 0;
                break;
            case ItemType.YellowSkin:
                _yellowOutfitCost = 0;
                break;
            case ItemType.BrownSkin:
                _brownOutfitCost = 0;
                break;
            case ItemType.DefenseBoost:
                if (_defenseBoostCount >= 3)
                {
                    // Adjust later
                    break;
                }
                _defenseBoostCount++;
                break;
            case ItemType.OffenseBoost:
                if (_offenseBoostCount >= 3)
                {
                    // Adjust later
                    break;
                }
                _offenseBoostCount++;
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
            case ItemType.DefenseBoost:
                return GameAssets.Instance.s_DefenseBoost;
            case ItemType.OffenseBoost:
                return GameAssets.Instance.s_OffenseBoost;
        }
    }

}
