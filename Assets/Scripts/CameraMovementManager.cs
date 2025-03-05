using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementManager : MonoBehaviour
{

    /// <summary>
    /// references the pinball currently in play.
    /// </summary>
    [SerializeField]
    public Rigidbody2D ball;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector3(transform.position.x, -2.3f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        // since transform is a vector3, we can move the camera up and down by just not adjusting the x or z
        
        // TODO: this is called even during game-overs and other non-gameplay scenes, causes errors with nonexistant pinballs.
        
        if (ball && ball.transform.position.y > -2.3)
            this.transform.position = new Vector3(transform.position.x, ball.position.y, transform.position.z);
    }
}
