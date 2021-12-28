using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Shop : MonoBehaviour
{
    Transform _container;
    Transform _shopSkinTemplate;
    Transform _shopSwordTemplate;

    void Awake()
    {
        _container = transform.Find("container");
        _shopSkinTemplate = _container.Find("shopSkinTemplate");
        _shopSwordTemplate = _container.Find("shopSwordTemplate");

        _shopSkinTemplate.gameObject.SetActive(false);
        _shopSwordTemplate.gameObject.SetActive(false);
    }

    void Start()
    {
        CreateSkinButton(Items.GetSprite(Items.ItemType.DefaultSkin), "Default Skin", Items.GetCost(Items.ItemType.DefaultSkin), 0);
        CreateSkinButton(Items.GetSprite(Items.ItemType.BlueSkin), "Blue Skin", Items.GetCost(Items.ItemType.BlueSkin), 1);
        CreateSkinButton(Items.GetSprite(Items.ItemType.GreenSkin), "Green Skin", Items.GetCost(Items.ItemType.GreenSkin), 2);
        CreateSkinButton(Items.GetSprite(Items.ItemType.YellowSkin), "Yellow Skin", Items.GetCost(Items.ItemType.YellowSkin), 3);
        CreateSkinButton(Items.GetSprite(Items.ItemType.BrownSkin), "Brown Skin", Items.GetCost(Items.ItemType.BrownSkin), 4);

        CreateSwordButton(Items.GetSprite(Items.ItemType.DefaultSword), "Default Sword", Items.GetCost(Items.ItemType.DefaultSword), 0);
        CreateSwordButton(Items.GetSprite(Items.ItemType.BlueSword), "Blue Sword", Items.GetCost(Items.ItemType.BlueSword), 1);
        CreateSwordButton(Items.GetSprite(Items.ItemType.CyanSword), "Cyan Sword", Items.GetCost(Items.ItemType.CyanSword), 2);
        CreateSwordButton(Items.GetSprite(Items.ItemType.GreenSword), "Green Sword", Items.GetCost(Items.ItemType.GreenSword), 3);
        CreateSwordButton(Items.GetSprite(Items.ItemType.RedSword), "Red Sword", Items.GetCost(Items.ItemType.RedSword), 4);
        CreateSwordButton(Items.GetSprite(Items.ItemType.PurpleSword), "Purple Sword", Items.GetCost(Items.ItemType.PurpleSword), 5);
    }

    void CreateSkinButton(Sprite[] itemSprite, string itemName, int itemCost, int positionIndex)
    {
        Transform shopItemTransform = Instantiate(_shopSkinTemplate, _container);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();

        float shopItemHeight = 100f;
        shopItemRectTransform.anchoredPosition = new Vector2(-200, 300 + -shopItemHeight * positionIndex);

        shopItemTransform.Find("itemName").GetComponent<TextMeshProUGUI>().SetText(itemName);
        shopItemTransform.Find("costText").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString());

        shopItemTransform.Find("itemImage1").GetComponent<Image>().sprite = itemSprite[0];
        shopItemTransform.Find("itemImage2").GetComponent<Image>().sprite = itemSprite[1];

        shopItemTransform.gameObject.SetActive(true);
    }

    void CreateSwordButton(Sprite[] itemSprite, string itemName, int itemCost, int positionIndex)
    {
        Transform shopItemTransform = Instantiate(_shopSwordTemplate, _container);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();

        float shopItemHeight = 100f;
        shopItemRectTransform.anchoredPosition = new Vector2(200, 300 + -shopItemHeight * positionIndex);

        shopItemTransform.Find("itemName").GetComponent<TextMeshProUGUI>().SetText(itemName);
        shopItemTransform.Find("costText").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString());

        shopItemTransform.Find("itemImage1").GetComponent<Image>().sprite = itemSprite[0];
        shopItemTransform.Find("itemImage2").GetComponent<Image>().sprite = itemSprite[0];

        shopItemTransform.gameObject.SetActive(true);
    }
}
