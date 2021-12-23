using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public static PowerupManager Instance { get; private set; }
    public bool isDamagePowerupActive { get; private set; }
    public bool isShieldPowerupActive { get; private set; }

    [SerializeField] float _durationDamage = 8f;
    [SerializeField] float _durationShield = 8f;
    [SerializeField] float _durationVaporize = 1.3f;

    [SerializeField] GameObject _damagePowerupCountdown;
    [SerializeField] GameObject _shieldPowerupCountdown;
    [SerializeField] GameObject _vaporizePowerupCountdown;

    [SerializeField] SpriteRenderer[] _bodyParts;

    //PowerupCountdownBar _damagePowerupCountdownBar;
    //PowerupCountdownBar _shieldPowerupCountdownBar;
    //PowerupCountdownBar _vaporizePowerupCountdownBar;

    IEnumerator _deactivateDamagePowerup;
    IEnumerator _deactivateShieldPowerup;
    IEnumerator _deactivateVaporizePowerup;

    Color _resetColor;
    Color _damagePowerupPlayerColor;
    Color _shieldPowerupPlayerColor;
    Color _damageAndShieldPowerupPlayerColor;
    Color _vaporizePowerupPlayerColor;

    Animator _bodyAnims;

    void Awake()
    {
        Instance = this;

        //_damagePowerupCountdownBar = _damagePowerupCountdown.GetComponent<PowerupCountdownBar>();
        //_shieldPowerupCountdownBar = _shieldPowerupCountdown.GetComponent<PowerupCountdownBar>();
        //_vaporizePowerupCountdownBar = _vaporizePowerupCountdown.GetComponent<PowerupCountdownBar>();

        _bodyAnims = Player.Instance.transform.Find("BodyParts").GetComponent<Animator>();
    }

    void OnEnable()
    {
        _resetColor = new Color(0f, 0f, 0f);
        _damagePowerupPlayerColor = new Color(1f, 0.5f, 0f);
        _shieldPowerupPlayerColor = new Color(0f, 0f, 1f);
        _damageAndShieldPowerupPlayerColor = new Color(1f, 0.5f, 1f);
        _vaporizePowerupPlayerColor = new Color(0.8f, 0f, 0f);
    }

    void Start()
    {
        //_damagePowerupCountdownBar.gameObject.SetActive(false);
        //_shieldPowerupCountdownBar.gameObject.SetActive(false);
        //_vaporizePowerupCountdownBar.gameObject.SetActive(false);
    }

    public void HealthPowerupCollected()
    {
        Player.Instance.SetCurrentHealth();
    }

    public void DamagePowerupCollected()
    {
        isDamagePowerupActive = true;

        if (isShieldPowerupActive)
        {
            foreach (var part in _bodyParts)
            {
                part.material.SetColor("_OutlineColor", _damageAndShieldPowerupPlayerColor);
                part.material.SetFloat("_OutlineAlpha", 1f);
                part.material.SetColor("_InnerOutlineColor", _damageAndShieldPowerupPlayerColor);
            }
        }
        else
        {
            foreach (var part in _bodyParts)
            {
                part.material.SetColor("_OutlineColor", _damagePowerupPlayerColor);
                part.material.SetFloat("_OutlineAlpha", 1f);
                part.material.SetColor("_InnerOutlineColor", _damagePowerupPlayerColor);
            }
        }

        _bodyAnims.SetBool("powerupGlow", true);

        if (_deactivateDamagePowerup != null)
            StopCoroutine(_deactivateDamagePowerup);
        _deactivateDamagePowerup = DeactivateDamagePowerup();
        StartCoroutine(_deactivateDamagePowerup);
    }

    IEnumerator DeactivateDamagePowerup()
    {
        yield return new WaitForSeconds(_durationDamage);

        isDamagePowerupActive = false;
        //_damagePowerupCountdownBar.gameObject.SetActive(false);
        _deactivateDamagePowerup = null;

        if (isShieldPowerupActive)
        {
            foreach (var part in _bodyParts)
            {
                part.material.SetColor("_OutlineColor", _shieldPowerupPlayerColor);
                part.material.SetColor("_InnerOutlineColor", _shieldPowerupPlayerColor);
            }
        }
        else
        {
            foreach (var part in _bodyParts)
            {
                part.material.SetFloat("_OutlineAlpha", 0f);
                part.material.SetColor("_InnerOutlineColor", _resetColor);
            }
            _bodyAnims.SetBool("powerupGlow", false);
        }
    }

    public void ShieldPowerupCollected()
    {
        isShieldPowerupActive = true;

        if (isDamagePowerupActive)
        {
            foreach (var part in _bodyParts)
            {
                part.material.SetColor("_OutlineColor", _damageAndShieldPowerupPlayerColor);
                part.material.SetFloat("_OutlineAlpha", 1f);
                part.material.SetColor("_InnerOutlineColor", _damageAndShieldPowerupPlayerColor);
            }
        }
        else
        {
            foreach (var part in _bodyParts)
            {
                part.material.SetColor("_OutlineColor", _shieldPowerupPlayerColor);
                part.material.SetFloat("_OutlineAlpha", 1f);
                part.material.SetColor("_InnerOutlineColor", _shieldPowerupPlayerColor);
            }
        }

        _bodyAnims.SetBool("powerupGlow", true);

        if (_deactivateShieldPowerup != null)
            StopCoroutine(_deactivateShieldPowerup);
        _deactivateShieldPowerup = DeactivateShieldPowerup();
        StartCoroutine(_deactivateShieldPowerup);
    }

    IEnumerator DeactivateShieldPowerup()
    {
        yield return new WaitForSeconds(_durationShield);

        isShieldPowerupActive = false;
        //_damagePowerupCountdownBar.gameObject.SetActive(false);
        _deactivateShieldPowerup = null;

        if (isDamagePowerupActive)
        {
            foreach (var part in _bodyParts)
            {
                part.material.SetColor("_OutlineColor", _damagePowerupPlayerColor);
                part.material.SetColor("_InnerOutlineColor", _damagePowerupPlayerColor);
            }
        }
        else
        {
            foreach (var part in _bodyParts)
            {
                part.material.SetFloat("_OutlineAlpha", 0f);
                part.material.SetColor("_InnerOutlineColor", _resetColor);
            }
            _bodyAnims.SetBool("powerupGlow", false);
        }
    }

    public void VaporizePowerupCollected()
    {

    }

}
