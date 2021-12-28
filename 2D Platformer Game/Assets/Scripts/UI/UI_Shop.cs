using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Shop : MonoBehaviour
{
    Transform _container;
    Transform _shopSkinTemplate;
    Transform _shopSwordTemplate;
    Transform _shopUpgradeTemplate;

    Image _currentSword;
    Image _currentOutfit;

    Color _initialItemColor;
    Color _onUseItemColor;

    IShopCustomer _shopCustomer;

    void Awake()
    {
        _container = transform.Find("container");
        _shopSkinTemplate = _container.Find("shopSkinTemplate");
        _shopSwordTemplate = _container.Find("shopSwordTemplate");
        _shopUpgradeTemplate = _container.Find("shopUpgradeTemplate");

        _initialItemColor = _shopSwordTemplate.Find("background").GetComponent<Image>().color;
        _onUseItemColor = new Color(125/255f, 112/255f, 65/255f);

        _shopSkinTemplate.gameObject.SetActive(false);
        _shopSwordTemplate.gameObject.SetActive(false);
        _shopUpgradeTemplate.gameObject.SetActive(false);
    }

    void Start()
    {
        CreateSkinButton(Items.ItemType.DefaultSkin, Items.GetSprite(Items.ItemType.DefaultSkin), "Default Outfit", Items.GetCost(Items.ItemType.DefaultSkin), 0);
        CreateSkinButton(Items.ItemType.BlueSkin, Items.GetSprite(Items.ItemType.BlueSkin), "Blue Outfit", Items.GetCost(Items.ItemType.BlueSkin), 1);
        CreateSkinButton(Items.ItemType.GreenSkin, Items.GetSprite(Items.ItemType.GreenSkin), "Green Outfit", Items.GetCost(Items.ItemType.GreenSkin), 2);
        CreateSkinButton(Items.ItemType.YellowSkin, Items.GetSprite(Items.ItemType.YellowSkin), "Yellow Outfit", Items.GetCost(Items.ItemType.YellowSkin), 3);
        CreateSkinButton(Items.ItemType.BrownSkin, Items.GetSprite(Items.ItemType.BrownSkin), "Brown Outfit", Items.GetCost(Items.ItemType.BrownSkin), 4);

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

    void TryBuySwordItem(Items.ItemType itemType, TextMeshProUGUI shopItemCostText, Image image)
    {
        if (_shopCustomer.TrySpendTokenAmount(Items.GetCost(itemType)))
        {
            if (_currentSword != null)
            {
                _currentSword.color = _initialItemColor;
            }
            _currentSword = image;
            _currentSword.color = _onUseItemColor;

            Items.SetCost(itemType);
            shopItemCostText.SetText("0");

            _shopCustomer.BoughtItem(itemType);
        }
    }

    void TryBuyOutfitItem(Items.ItemType itemType, TextMeshProUGUI shopItemCostText, Image image)
    {
        if (_shopCustomer.TrySpendTokenAmount(Items.GetCost(itemType)))
        {
            if (_currentOutfit != null)
            {
                _currentOutfit.color = _initialItemColor;
            }
            _currentOutfit = image;
            _currentOutfit.color = _onUseItemColor;

            Items.SetCost(itemType);
            shopItemCostText.SetText("0");

            _shopCustomer.BoughtItem(itemType);
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
