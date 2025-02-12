using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Flipper : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private GameObject pivot;

    [SerializeField]
    private bool isLeftFlipper;

    private Vector3 pivotPos;

    private float rotationSpeed;

    private KeyCode input; 


    // Start is called before the first frame update
    void Start()
    {
        pivotPos = pivot.transform.position;
        rotationSpeed = 600;

        if (isLeftFlipper)
            input = KeyCode.LeftArrow;
        else 
            input = KeyCode.RightArrow;

    }

    // Update is called once per frame
    void Update()
    {
        updateRotation(Input.GetKey(input));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {


        // determining how far away the ball is from the pivot
        float distFromPivot = Mathf.Sqrt(Mathf.Pow(collision.transform.position.x - pivotPos.x, 2) + Mathf.Pow(collision.transform.position.y - pivotPos.y, 2));

        float exitVel = distFromPivot * 8;


        // these numbers feel ok; the larger distFromPivot is, the more the velocity is x-bound and the smaller, the more it's
        // y-bound
        distFromPivot /= 2f;
        distFromPivot -= 0.1f;


        // determining the exit angle
        Vector3 exitDir = Vector3.zero;
        exitDir.x = Mathf.Cos(Mathf.Deg2Rad * 60 * (distFromPivot + 1));
        exitDir.y = Mathf.Sin(Mathf.Deg2Rad * 60 * (distFromPivot + 1));

        if (isLeftFlipper)
            exitDir.x *= -1;

        Debug.Log("X: " + exitDir.x);
        Debug.Log("Y: " + exitDir.y);


        exitDir = exitDir.normalized;

        // final launch values
        exitDir *= exitVel;


        if (isLeftFlipper)
        {
            if (Mathf.Abs(transform.rotation.eulerAngles.z - 60) < 2f || Math.Abs(transform.rotation.eulerAngles.z - 120) < 2f || !Input.GetKey(KeyCode.LeftArrow))
                return;
        }
        else
        {
            if (Mathf.Abs(transform.rotation.eulerAngles.z - 60) < 2f || Math.Abs(transform.rotation.eulerAngles.z - 120) < 2f || !Input.GetKey(KeyCode.RightArrow))
                return;
        }

        collision.rigidbody.linearVelocity = exitDir;

    }

    private void updateRotation(bool keyHeld)
    {
        float currAngle = transform.rotation.eulerAngles.z;
        float deltaAngle;

        
        // using an XOR operator so that not holding a left flipper turns it clockwise and not holding a right flipper
        // turns it counterclockwise
        if (keyHeld ^ isLeftFlipper)
        {
            deltaAngle = -rotationSpeed * Time.deltaTime;
        }
        else
        {
            deltaAngle = rotationSpeed * Time.deltaTime;
        }

        float newAngle = currAngle + deltaAngle;
        
        if (newAngle < 60)
        {
            deltaAngle = 60 - currAngle;
        } 
        else if (newAngle > 120)
        {
            deltaAngle = 120 - currAngle;
        }

        rb.transform.RotateAround(pivotPos, new Vector3(0, 0, 1), deltaAngle);



    }
}
