using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Pinball : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb;

    public string prefabPath = "Prefabs/Balls/Pinball";

    // Bonus value for when we hit a object that we don't care about
    public int bonus;
    
    // Start is called before the first frame update
    // A Virus has been detected
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.transform.position.y < -8)
        {
            //Debug.Log("Pinball update");
            GameManager.Instance.SpawnBall();
            Debug.Log("Ball dying");
            Destroy(this.gameObject);
        }
            

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("moblizeBumper"))
        {
            GameManager.Instance.addScore(bonus);
            bonus = 0;
        }
    }

}
