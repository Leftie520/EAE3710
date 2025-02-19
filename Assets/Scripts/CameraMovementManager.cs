using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    public Rigidbody2D ball;

    void Start()
    {
        this.transform.position = new Vector3(transform.position.x, ball.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        // since transform is a vector3, we can move the camera up and down by just not adjusting the x or z
        
        // TODO: this is called even during game-overs and other non-gameplay scenes, causes errors with nonexistant pinballs.
        
        if (ball.transform.position.y > -2.3)
            this.transform.position = new Vector3(transform.position.x, ball.position.y, transform.position.z);
    }
}
