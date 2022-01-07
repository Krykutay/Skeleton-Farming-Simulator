using System;
using UnityEngine;

public class StartSpawns : MonoBehaviour
{
    public Action SpawnsTriggered; 

    void OnTriggerEnter2D(Collider2D collision)
    {
        SpawnsTriggered?.Invoke();
    }
}
