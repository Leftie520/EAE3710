using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plunger : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    private float topHeight;
    private float lowHeight;

    private float power;


    // Start is called before the first frame update
    void Start()
    {
        topHeight = this.transform.position.y;
        lowHeight = topHeight - 1;
        power = 45;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        float deltaY = 0;

        if (Input.GetKey(KeyCode.DownArrow))
        {
            deltaY = -10 * Time.deltaTime;
            power = UnityEngine.Random.Range(35, 45);
        }
        else if (rb.position.y != topHeight)
        {
            deltaY = 55 * Time.deltaTime;
        }

        float newY = Mathf.Clamp(rb.position.y + deltaY, lowHeight, topHeight);
        rb.MovePosition(new Vector2(rb.position.x, newY));
    }
}
