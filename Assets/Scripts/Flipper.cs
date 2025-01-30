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
        rotationSpeed = 900;

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


    private void updateRotation(bool keyHeld)
    {
        float currAngle = transform.rotation.eulerAngles.z;
        float deltaAngle;
        Debug.Log(transform.rotation.eulerAngles.z);

        
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
