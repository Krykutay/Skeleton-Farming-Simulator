using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceHit : MonoBehaviour
{
    BoxCollider2D _collider;
    List<Collider2D> collisions;
    [SerializeField] LayerMask _playerAndEnemy;

    Coroutine _hitCollisions = null;

    void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        collisions = new List<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        collisions.Add(collision);

        if (collisions.Count == 1)
            _hitCollisions = StartCoroutine(HitCollisions());
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        collisions.Remove(collision);

        if (collisions.Count == 0)
            StopCoroutine(_hitCollisions);
    }

    IEnumerator HitCollisions()
    {
        while (collisions.Count > 0)
        {
            Collider2D[] collision = Physics2D.OverlapBoxAll(_collider.bounds.center, _collider.bounds.size, 0f, _playerAndEnemy);
            foreach (var col in collision)
            {
                if (col.TryGetComponent<Entity>(out Entity entity))
                    entity.DamageBySurface();
                else
                    Player.Instance.DamageBySurface();
            }
            
            yield return new WaitForSeconds(1f);
        }
    }
}
