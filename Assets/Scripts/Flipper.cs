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

    [SerializeField]
    private HingeJoint2D joint;

    private Vector3 pivotPos;

    private float rotationSpeed;

    private KeyCode input;

    private bool launchedBallRecently;

    // Start is called before the first frame update
    void Start()
    {
        pivotPos = pivot.transform.position;
        rotationSpeed = 600;

        if (isLeftFlipper)
            input = KeyCode.LeftArrow;
        else 
            input = KeyCode.RightArrow;

        launchedBallRecently = false;

    }

    // Update is called once per frame
    void Update()
    {
        updateRotation(Input.GetKey(input));
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        Debug.Log("ball has been launched");

        // we don't want to override a previous calculation of the ball trajectory, but we need to use
        // collisionStay for when the player waits a moment to launch the ball.
        if (launchedBallRecently)
            return;

        // determining how far away the ball is from the pivot
        float distFromPivot = Mathf.Sqrt(Mathf.Pow(collision.transform.position.x - pivotPos.x, 2) + Mathf.Pow(collision.transform.position.y - pivotPos.y, 2));

        // modifying the strength of the flippers; """"should"""" not impact accuracy
        float exitVel = distFromPivot * 18;

        // determining the exit angle
        Vector3 exitDir = Vector3.zero;
        float launchAngle = transform.rotation.eulerAngles.z + 45;

        if (!isLeftFlipper)
            launchAngle += 90;

        exitDir.x = Mathf.Cos(Mathf.Deg2Rad * launchAngle);
        exitDir.y = Mathf.Sin(Mathf.Deg2Rad * launchAngle);


        // normalizing since this vector should only modify the direction of the launch, NOT the power
        // (I'll be so honest we definitely don't need this line but idc...)
        exitDir = exitDir.normalized;

        // final launch values
        exitDir *= exitVel;

        // not launching the ball if the flipper is at either end of its range (i.e. not moving)
        if (isLeftFlipper)
        {
            if (Mathf.Abs(transform.rotation.eulerAngles.z - 15) < 0f || Math.Abs(transform.rotation.eulerAngles.z - 75) < 0f || !Input.GetKey(KeyCode.LeftArrow))
                return;
        }
        else
        {
            if (Mathf.Abs(transform.rotation.eulerAngles.z - 345) < 0f || Math.Abs(transform.rotation.eulerAngles.z - 285) < 0f || !Input.GetKey(KeyCode.RightArrow))
                return;
        }

        // if the ball is too far from the flipper, this will make it act like a weak hit
        if (distFromPivot > 1.8)
        {
            // the 5 * allows the velocity so smoothly scale to 0
            exitDir.y *= 5 * (2 - distFromPivot);
            // making sure it can't be negative.
            exitDir.y = Mathf.Max(0f, exitDir.y);
        }

        exitDir.x *= 0.8f;

        collision.rigidbody.linearVelocity = exitDir;

        launchedBallRecently = true;

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        launchedBallRecently = false;
        Debug.Log("ball has left flipper");
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


        // making sure all left flippers are between 15 and 75 degrees
        if (isLeftFlipper)
        {
            if (newAngle < 15)
            {
                deltaAngle = 15 - currAngle;
            }
            else if (newAngle > 75)
            {
                deltaAngle = 75 - currAngle;
            }
        }
        else

        // making sure all right flippers are between -15 and -75 degrees
        {
            if (newAngle > 345)
            {
                deltaAngle = 345 - currAngle;
            }
            else if (newAngle < 285)
            {
                deltaAngle = 285 - currAngle;
            }
        }

        rb.transform.RotateAround(pivotPos, new Vector3(0, 0, 1), deltaAngle);

        

        



    }
}
