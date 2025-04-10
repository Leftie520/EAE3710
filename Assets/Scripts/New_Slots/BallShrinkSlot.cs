using System.Collections;
using UnityEngine;
using UnityEngine.U2D;

public class BallShrinkSlot : Slot
{
    public float shrinkFactor = 0.5f;
    public float duration = 5f;

    void OnTriggerEnter2D(Collider2D col)
    {
        Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Transform ballTransform = rb.transform;
            ballTransform.localScale = Vector3.one * shrinkFactor;
            StartCoroutine(ResetScale(ballTransform));
        }

        Score(score);
    }

    private IEnumerator ResetScale(Transform ballTransform)
    {
        yield return new WaitForSeconds(duration);
        if (ballTransform != null)
        {
            ballTransform.localScale = Vector3.one;
        }
    }

    protected override void Score(int score)
    {
        GameManager.Instance.addScore(score);
    }
}