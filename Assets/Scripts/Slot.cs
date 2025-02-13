using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Slot : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Getting the object that collided with the slot
        //GameObject obj = col.gameObject;
        //Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();

        Score(500);
    }

    protected virtual void Score(int score)
    {
        GameManager.Instance.addScore(score);
    }
}
