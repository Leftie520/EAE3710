using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Flipper : MonoBehaviour
{

    /// <summary>
    /// the rigidbody associated with this flipper
    /// </summary>
    [SerializeField]
    private Rigidbody2D rb;

    /// <summary>
    /// the pivot point GO for this flipper
    /// </summary>
    [SerializeField]
    private GameObject pivot;

    /// <summary>
    /// true if the flipper's pivot is on the left side, false if on the right.
    /// </summary>
    [SerializeField]
    private bool isLeftFlipper;

    /// <summary>
    /// the position of the pivot GO
    /// </summary>
    private Vector3 pivotPos;

    /// <summary>
    /// the rotation speed used when calculating the flipper's new pos
    /// </summary>
    private float rotationSpeed;

    /// <summary>
    /// the input keycode required to flip this flipper
    /// </summary>
    private KeyCode input;

    /// <summary>
    /// stores whether or not the launch angle was calculated for the pinball most recently collided with (Note: may run into errors with multiball)
    /// </summary>
    private bool launchedBallRecently;

    /// <summary>
    /// whether or not pressing the keycode will activate the flipper
    /// </summary>
    public bool isControllable;

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

        isControllable = false;

    }

    // Update is called once per frame
    void Update()
    {
        updateRotation(Input.GetKey(input));
    }

    /// <remarks>
    /// ngl this method was heavily simplified and it feels a lot better; the code is still commented out for now in case we want to adjust the 
    /// working again later, but for now, it just adds an upwards force and that feels super intuitive...
    /// </remarks>
    private void OnCollisionStay2D(Collision2D collision)
    {
        // preventing a ball launch if the flipper is disabled.
        if (!isControllable) return;


        Debug.Log("ball has been launched");

        // we don't want to override a previous calculation of the ball trajectory, but we need to use
        // collisionStay for when the player waits a moment to launch the ball.
        if (launchedBallRecently)
            return;

        // determining how far away the ball is from the pivot
        float distFromPivot = Mathf.Sqrt(Mathf.Pow(collision.transform.position.x - pivotPos.x, 2) + Mathf.Pow(collision.transform.position.y - pivotPos.y - 0.4f, 2));

        // modifying the strength of the flippers; """"should"""" not impact accuracy
        float exitVel = distFromPivot * 18;

        // determining the exit angle
        Vector3 exitDir = Vector3.zero;
        float launchAngle = transform.rotation.eulerAngles.z + 45;

        if (!isLeftFlipper)
            launchAngle += 90;

        //exitDir.x = Mathf.Cos(Mathf.Deg2Rad * launchAngle);
        exitDir.y = Mathf.Sin(Mathf.Deg2Rad * launchAngle);


        // normalizing since this vector should only modify the direction of the launch, NOT the power
        // (I'll be so honest we definitely don't need this line but idc...)
        exitDir = exitDir.normalized;

        // final launch values
        exitDir *= exitVel;

        // not launching the ball if the flipper is at either end of its range (i.e. not moving)
        if (isLeftFlipper)
        {
            if (Mathf.Abs(transform.rotation.eulerAngles.z - 15) == 0f || Math.Abs(transform.rotation.eulerAngles.z - 75) == 0f || !Input.GetKey(KeyCode.LeftArrow))
                return;
        }
        else
        {
            if (Mathf.Abs(transform.rotation.eulerAngles.z - 345) == 0f || Math.Abs(transform.rotation.eulerAngles.z - 285) == 0f || !Input.GetKey(KeyCode.RightArrow))
                return;
        }

        //// if the ball is too far from the flipper, this will make it act like a weak hit
        //if (distFromPivot > 1.8)
        //{
        //    // the 5 * allows the velocity so smoothly scale to 0
        //    exitDir.y *= 5 * (2 - distFromPivot);
        //    // making sure it can't be negative.
        //    exitDir.y = Mathf.Max(0f, exitDir.y);
        //}

        exitDir.x *= 0.8f;


        collision.rigidbody.linearVelocity = new Vector2(collision.rigidbody.linearVelocity.x, exitDir.y);

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
        if ((keyHeld && isControllable) ^ isLeftFlipper)
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
