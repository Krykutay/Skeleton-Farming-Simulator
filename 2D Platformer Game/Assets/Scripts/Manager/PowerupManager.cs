using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public static PowerupManager Instance { get; private set; }

    [SerializeField] float _durationDamage = 8f;
    [SerializeField] float _durationShield = 8f;
    [SerializeField] float _durationVaporize = 1.3f;

    [SerializeField] GameObject _damagePowerupCountdown;
    [SerializeField] GameObject _shieldPowerupCountdown;
    [SerializeField] GameObject _vaporizePowerupCountdown;

    SpriteRenderer _spriteRenderer;
    //PowerupCountdownBar _damagePowerupCountdownBar;
    //PowerupCountdownBar _shieldPowerupCountdownBar;
    //PowerupCountdownBar _vaporizePowerupCountdownBar;

    IEnumerator _deactivateDamagePowerup;
    IEnumerator _deactivateShieldPowerup;
    IEnumerator _deactivateVaporizePowerup;

    Color _color;
    Color _shieldPowerupPlayerColor;
    Color _vaporizePowerupPlayerColor;

    [SerializeField] SpriteRenderer[] _bodyParts;
    Animator _bodyAnims;

    void Awake()
    {
        Instance = this;

        _spriteRenderer = Player.Instance.GetComponent<SpriteRenderer>();
        //_damagePowerupCountdownBar = _damagePowerupCountdown.GetComponent<PowerupCountdownBar>();
        //_shieldPowerupCountdownBar = _shieldPowerupCountdown.GetComponent<PowerupCountdownBar>();
        //_vaporizePowerupCountdownBar = _vaporizePowerupCountdown.GetComponent<PowerupCountdownBar>();

        _bodyAnims = Player.Instance.transform.Find("BodyParts").GetComponent<Animator>();
    }

    void OnEnable()
    {
        _color = _spriteRenderer.color;
        _shieldPowerupPlayerColor = _color;
        _shieldPowerupPlayerColor.a = 0.35f;
        _vaporizePowerupPlayerColor = new Color(200f, 0f, 0f, 255f);
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
        foreach (var part in _bodyParts)
        {
            part.material.SetColor("_OutlineColor", new Color(1, .5f, 0f));
            part.material.SetFloat("_OutlineAlpha", 1f);
        }

        _bodyAnims.SetBool("damagePowerup", true);

        if (_deactivateDamagePowerup != null)
            StopCoroutine(_deactivateDamagePowerup);
        _deactivateDamagePowerup = DeactivateDamagePowerup();
        StartCoroutine(_deactivateDamagePowerup);
    }

    IEnumerator DeactivateDamagePowerup()
    {
        yield return new WaitForSeconds(_durationDamage);

        //_damagePowerupCountdownBar.gameObject.SetActive(false);
        _deactivateDamagePowerup = null;
        _bodyAnims.SetBool("damagePowerup", false);

        foreach (var part in _bodyParts)
        {
            part.material.SetFloat("_OutlineAlpha", 0f);
        }
    }

    public void ShieldPowerupCollected()
    {

    }

    public void VaporizePowerupCollected()
    {

    }

}
