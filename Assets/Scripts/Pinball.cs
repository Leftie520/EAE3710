using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinball : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb;

    public string prefabPath = "Prefabs/Pinball";
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.transform.position.y < -8)
        {
            GameManager.Instance.SpawnBall();
            Debug.Log("Ball dying");
            Destroy(this.gameObject);
        }
            

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
    }

}
