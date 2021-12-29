using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.EventSystems;

public class UI_Shop : MonoBehaviour
{
    [SerializeField] PlayerInventory _playerInventory;

    Transform _container;
    Transform _shopSkinTemplate;
    Transform _shopSwordTemplate;
    Transform _shopUpgradeTemplate;

    Image _currentSword;
    Image _currentOutfit;

    Color _unequippedItemColor;
    Color _equippedItemColor;

    IShopCustomer _shopCustomer;

    void Awake()
    {
        _container = transform.Find("container");
        _shopSkinTemplate = _container.Find("shopSkinTemplate");
        _shopSwordTemplate = _container.Find("shopSwordTemplate");
        _shopUpgradeTemplate = _container.Find("shopUpgradeTemplate");

        _unequippedItemColor = _shopSwordTemplate.Find("background").GetComponent<Image>().color;
        _equippedItemColor = new Color(0.36f, 80/255f, 0.17f);

        _shopSkinTemplate.gameObject.SetActive(false);
        _shopSwordTemplate.gameObject.SetActive(false);
        _shopUpgradeTemplate.gameObject.SetActive(false);
    }

    void Start()
    {
        foreach (int item in _playerInventory.inventory)
        {
            Items.SetCost((Items.ItemType)item);
        }

        CreateSkinButton(Items.ItemType.DefaultOutfit, Items.GetSprite(Items.ItemType.DefaultOutfit), "Default Outfit", Items.GetCost(Items.ItemType.DefaultOutfit), 0);
        CreateSkinButton(Items.ItemType.BlueOutfit, Items.GetSprite(Items.ItemType.BlueOutfit), "Blue Outfit", Items.GetCost(Items.ItemType.BlueOutfit), 1);
        CreateSkinButton(Items.ItemType.GreenOutfit, Items.GetSprite(Items.ItemType.GreenOutfit), "Green Outfit", Items.GetCost(Items.ItemType.GreenOutfit), 2);
        CreateSkinButton(Items.ItemType.YellowOutfit, Items.GetSprite(Items.ItemType.YellowOutfit), "Yellow Outfit", Items.GetCost(Items.ItemType.YellowOutfit), 3);
        CreateSkinButton(Items.ItemType.BrownOutfit, Items.GetSprite(Items.ItemType.BrownOutfit), "Brown Outfit", Items.GetCost(Items.ItemType.BrownOutfit), 4);

        CreateSwordButton(Items.ItemType.DefaultSword, Items.GetSprite(Items.ItemType.DefaultSword), "Default Swords", Items.GetCost(Items.ItemType.DefaultSword), 0);
        CreateSwordButton(Items.ItemType.BlueSword, Items.GetSprite(Items.ItemType.BlueSword), "Blue Swords", Items.GetCost(Items.ItemType.BlueSword), 1);
        CreateSwordButton(Items.ItemType.CyanSword, Items.GetSprite(Items.ItemType.CyanSword), "Cyan Swords", Items.GetCost(Items.ItemType.CyanSword), 2);
        CreateSwordButton(Items.ItemType.GreenSword, Items.GetSprite(Items.ItemType.GreenSword), "Green Swords", Items.GetCost(Items.ItemType.GreenSword), 3);
        CreateSwordButton(Items.ItemType.RedSword, Items.GetSprite(Items.ItemType.RedSword), "Red Swords", Items.GetCost(Items.ItemType.RedSword), 4);
        CreateSwordButton(Items.ItemType.PurpleSword, Items.GetSprite(Items.ItemType.PurpleSword), "Purple Swords", Items.GetCost(Items.ItemType.PurpleSword), 5);

        CreateUpgradeButton(Items.ItemType.DefenseBoost, Items.GetSprite(Items.ItemType.DefenseBoost), "Defense Boost", Items.GetCost(Items.ItemType.DefenseBoost), 0);
        CreateUpgradeButton(Items.ItemType.OffenseBoost, Items.GetSprite(Items.ItemType.OffenseBoost), "Offense Boost", Items.GetCost(Items.ItemType.OffenseBoost), 1);

    }

    void CreateSkinButton(Items.ItemType itemType, Sprite[] itemSprite, string itemName, int itemCost, int positionIndex)
    {
        Transform shopItemTransform = Instantiate(_shopSkinTemplate, _container);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();

        float shopItemHeight = 100f;
        shopItemRectTransform.anchoredPosition = new Vector2(-400 + (positionIndex % 3) * 400, 50 - shopItemHeight * Mathf.Floor(positionIndex / 3));

        shopItemTransform.Find("itemName").GetComponent<TextMeshProUGUI>().SetText(itemName);

        TextMeshProUGUI ShopItemCostText = shopItemTransform.Find("costText").GetComponent<TextMeshProUGUI>();
        ShopItemCostText.SetText(itemCost.ToString());

        shopItemTransform.Find("itemImage1").GetComponent<Image>().sprite = itemSprite[0];
        shopItemTransform.Find("itemImage2").GetComponent<Image>().sprite = itemSprite[1];

        shopItemTransform.gameObject.SetActive(true);

        Image image = shopItemTransform.Find("background").GetComponent<Image>();

        if ((int)itemType == _playerInventory.EquippedOutfit)
        {
            image.color = _equippedItemColor;
            _currentOutfit = image;
        }

        shopItemTransform.GetComponent<Button>().onClick.AddListener(() =>
        {
            TryBuyOutfitItem(itemType, ShopItemCostText, image);
        });
    }

    void CreateSwordButton(Items.ItemType itemType, Sprite[] itemSprite, string itemName, int itemCost, int positionIndex)
    {
        Transform shopItemTransform = Instantiate(_shopSwordTemplate, _container);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();

        float shopItemHeight = 100f;
        shopItemRectTransform.anchoredPosition = new Vector2(-400 + (positionIndex % 3) * 400, 300 - shopItemHeight * Mathf.Floor(positionIndex/3));

        shopItemTransform.Find("itemName").GetComponent<TextMeshProUGUI>().SetText(itemName);

        TextMeshProUGUI ShopItemCostText = shopItemTransform.Find("costText").GetComponent<TextMeshProUGUI>();
        ShopItemCostText.SetText(itemCost.ToString());

        shopItemTransform.Find("itemImage1").GetComponent<Image>().sprite = itemSprite[0];
        shopItemTransform.Find("itemImage2").GetComponent<Image>().sprite = itemSprite[0];

        shopItemTransform.gameObject.SetActive(true);

        Image image = shopItemTransform.Find("background").GetComponent<Image>();

        if ((int)itemType == _playerInventory.EquippedSwords)
        {
            image.color = _equippedItemColor;
            _currentSword = image;
        }

        shopItemTransform.GetComponent<Button>().onClick.AddListener(() =>
        {
            TryBuySwordItem(itemType, ShopItemCostText, image);
        });
    }

    void CreateUpgradeButton(Items.ItemType itemType, Sprite[] itemSprite, string itemName, int itemCost, int positionIndex)
    {
        Transform shopItemTransform = Instantiate(_shopUpgradeTemplate, _container);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();

        float shopItemHeight = 100f;
        shopItemRectTransform.anchoredPosition = new Vector2(-400 + (positionIndex % 3) * 400, -200 - shopItemHeight * Mathf.Floor(positionIndex / 3));

        shopItemTransform.Find("itemName").GetComponent<TextMeshProUGUI>().SetText(itemName);

        TextMeshProUGUI ShopItemCostText = shopItemTransform.Find("costText").GetComponent<TextMeshProUGUI>();
        ShopItemCostText.SetText(itemCost.ToString());

        shopItemTransform.Find("itemImage1").GetComponent<Image>().sprite = itemSprite[0];

        shopItemTransform.gameObject.SetActive(true);

        Image image = shopItemTransform.Find("background").GetComponent<Image>();

        shopItemTransform.GetComponent<Button>().onClick.AddListener(() =>
        {
            TryBuyBoostItem(itemType, ShopItemCostText, image);
        });
    }

    void TryBuyOutfitItem(Items.ItemType itemType, TextMeshProUGUI shopItemCostText, Image image)
    {
        if (_shopCustomer.TrySpendTokenAmount(Items.GetCost(itemType)))
        {
            if (_currentOutfit != null)
            {
                _currentOutfit.color = _unequippedItemColor;
            }
            _currentOutfit = image;
            _currentOutfit.color = _equippedItemColor;

            Items.SetCost(itemType);
            shopItemCostText.SetText("0");

            _shopCustomer.BoughtItem(itemType);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }


    void TryBuySwordItem(Items.ItemType itemType, TextMeshProUGUI shopItemCostText, Image image)
    {
        if (_shopCustomer.TrySpendTokenAmount(Items.GetCost(itemType)))
        {
            if (_currentSword != null)
            {
                _currentSword.color = _unequippedItemColor;
            }
            _currentSword = image;
            _currentSword.color = _equippedItemColor;

            Items.SetCost(itemType);
            shopItemCostText.SetText("0");

            _shopCustomer.BoughtItem(itemType);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    void TryBuyBoostItem(Items.ItemType itemType, TextMeshProUGUI shopItemCostText, Image image)
    {
        if (_shopCustomer.TrySpendTokenAmount(Items.GetCost(itemType)))
        {

            Items.SetCost(itemType);
            shopItemCostText.SetText("0");

            _shopCustomer.BoughtItem(itemType);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public void Show(IShopCustomer shopCustomer)
    {
        _shopCustomer = shopCustomer;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
