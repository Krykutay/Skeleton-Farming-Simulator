using System.Collections;
using UnityEngine;

public class DropLootGreen : DropLoot
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isFollowing)
            StartCoroutine(DelayBeforeDisappear());
    }

    IEnumerator DelayBeforeDisappear()
    {
        yield return new WaitForSeconds(0.25f);
        ScoreManager.Instance.Orb_Collected();
        DropLootGreenPool.Instance.ReturnToPool(this);
    }
}
