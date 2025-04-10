using System.Collections;
using UnityEngine;

public class Moon : MonoBehaviour
{
    public int score;

    private Vector3 startPos;
    private Rigidbody2D rb;

    [Header("Movement Settings")]
    public float moveForce = 0.3f; 
    public float drag = 3f;
    public float resetDelay = 1f;
    public float maxSpeed = 1.2f; 

    void Start()
    {
        
        startPos = transform.position;

        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }

        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.linearDamping = drag;
    }

    void Update()
    {
       
        if (rb != null && rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        GameObject obj = col.gameObject;
        Rigidbody2D ballRb = obj.GetComponent<Rigidbody2D>();

        if (ballRb != null)
        {
           
            Vector3 newVel = new Vector3(ballRb.position.x, ballRb.position.y) - transform.position;
            newVel.Normalize();
            newVel *= 16;
            ballRb.linearVelocity = newVel;

           
            Vector2 pushDir = (transform.position - obj.transform.position).normalized;
            rb.AddForce(pushDir * moveForce, ForceMode2D.Force);

            Score(score);
        }
    }

    protected virtual void Score(int score)
    {
        Debug.Log("Bumper Add Score");
        GameManager.Instance.addScore(score);
    }

    public void ResetPosition()
    {
        // Called at the beginning of each level to reset the bumper's position.  Might need to be updated in furture

        StartCoroutine(ResetAfterDelay());
    }

    private IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(resetDelay);
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        transform.position = startPos;
    }
}
