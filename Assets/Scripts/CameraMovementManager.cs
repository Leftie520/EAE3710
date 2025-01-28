using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    Rigidbody2D ball;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // since transform is a vector3, we can move the camera up and down by just not adjusting the x or z
        if (ball.transform.position.y > 0)
            this.transform.position = new Vector3(transform.position.x, ball.position.y, transform.position.z);
    }
}
