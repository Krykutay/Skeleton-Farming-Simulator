using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceHit : MonoBehaviour
{
    float _damageFrequency = 1f;
    float _trigerEnterTime;

    Dictionary<Collider2D, bool> collisions;

    void Awake()
    {
        collisions = new Dictionary<Collider2D, bool>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        _trigerEnterTime = Time.time;
        if (collision.TryGetComponent<Entity>(out Entity entity))
            entity.DamageBySurface();
        else
            Player.Instance.DamageBySurface();

        collisions[collision] = true;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (Time.time >= _trigerEnterTime + _damageFrequency)
        {
            foreach (Collider2D key in collisions.Keys)
            {
                if (!collisions[key])
                    continue;

                if (key.TryGetComponent<Entity>(out Entity entity))
                    entity.DamageBySurface();
                else
                    Player.Instance.DamageBySurface();
            }
            _trigerEnterTime = Time.time;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        collisions[collision] = false;

        bool isAllLeft = false;
        foreach (bool value in collisions.Values)
        {
            if (value)
                isAllLeft = true;
        }

        if (!isAllLeft)
        {
            StopCoroutine(ClearCollisions());
            StartCoroutine(ClearCollisions());
        }
    }

    IEnumerator ClearCollisions()
    {
        yield return new WaitForSeconds(0.5f);

        collisions.Clear();
    }
}
