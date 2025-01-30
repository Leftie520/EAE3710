using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FlipperR : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private GameObject pivot;

    private Vector3 pivotPos;

    private float rotationSpeed;


    // Start is called before the first frame update
    void Start()
    {
        pivotPos = pivot.transform.position;
        rotationSpeed = 900;
    }

    // Update is called once per frame
    void Update()
    {
        updateRotation(Input.GetKey(KeyCode.RightArrow));
    }


    private void updateRotation(bool rightKey)
    {
        float currAngle = transform.rotation.eulerAngles.z;
        float deltaAngle;
        Debug.Log(transform.rotation.eulerAngles.z);

        if (rightKey)
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
