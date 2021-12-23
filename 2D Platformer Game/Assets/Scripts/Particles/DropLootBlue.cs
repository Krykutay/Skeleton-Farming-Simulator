using System.Collections;
using UnityEngine;

public class DropLootBlue : DropLoot
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isFollowing)
            StartCoroutine(DelayBeforeDisappear());
    }

    IEnumerator DelayBeforeDisappear()
    {
        yield return new WaitForSeconds(0.25f);
        DropLootBluePool.Instance.ReturnToPool(this);
    }
}
