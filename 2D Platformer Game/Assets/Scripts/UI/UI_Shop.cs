using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Shop : MonoBehaviour
{
    Transform _container;

    void Awake()
    {
        _container = transform.Find("container");
    }

    void Start()
    {

    }

}
