using UnityEngine;
using TMPro;

public class MerchantTalk1 : MonoBehaviour
{
    [SerializeField] UI_Assistant _uiAssistant;
    [SerializeField] TMP_Text _talkText;
    [SerializeField] UI_Shop _uiShop;

    IShopCustomer _shopCustomer;

    string[] _initialDialog;
    SoundManager.SoundTags[] _dialogSounds;
    float[] _typeSpeed;

    bool _isPlayerInRange;
    static bool _hasTalkedOnce;

    void OnEnable()
    {
        Player.Instance.inputHandler.OnTalkAction += PlayerTalkPressed;
        _uiAssistant.OnOpenShop += UIAssistant_OpenShop;
    }

    void OnDisable()
    {
        Player.Instance.inputHandler.OnTalkAction -= PlayerTalkPressed;
        _uiAssistant.OnOpenShop -= UIAssistant_OpenShop;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IShopCustomer>(out _shopCustomer))
        {
            _talkText.gameObject.SetActive(true);
            _talkText.text = "Shop (E)";
            _isPlayerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IShopCustomer>(out _shopCustomer))
        {
            _talkText.text = "";
            _isPlayerInRange = false;
            _talkText.gameObject.SetActive(false);
            _uiAssistant.gameObject.SetActive(false);
            _uiShop.Hide();
            _uiAssistant.StopTalkingSound();
        }
    }

    void PlayerTalkPressed()
    {
        if (!_isPlayerInRange)
            return;

        if (_hasTalkedOnce)
        {
            _uiShop.Show(_shopCustomer);
            return;
        }

        _initialDialog = new string[]
        {
            "Oh, hai! Unlike the blue dude over there, I don't know much of your language. Don't even know how he got the accent really.",
            "I sell swords, outfits and stimulates that enhance your character.",
            "Beware though, my bussines only concludes with the gems you collect from the mines!",
        };

        _dialogSounds = new SoundManager.SoundTags[]
        {
        SoundManager.SoundTags.MerchantTalk,
        SoundManager.SoundTags.MerchantTalk,
        SoundManager.SoundTags.MerchantTalk,
        };

        _typeSpeed = new float[]
        {
            0.05f,
            0.05f,
            0.05f,
        };

        _uiAssistant.gameObject.SetActive(true);
        _uiAssistant.NpcTalk(_initialDialog, _dialogSounds, _typeSpeed);

    }

    void UIAssistant_OpenShop()
    {
        _hasTalkedOnce = true;
        _uiShop.Show(_shopCustomer);
    }

}
