using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class CometTrailSlot : Slot
{
    public float speedMultiplier = 2f;

    void OnTriggerEnter2D(Collider2D col)
    {
        Rigidbody2D rb = col.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // Apply speed boost
            rb.velocity *= speedMultiplier;
        }

        Score(score);
    }

    protected override void Score(int score)
    {
        GameManager.Instance.addScore(score);
    }
}
