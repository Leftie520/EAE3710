using UnityEngine;

public class ChamberBlocker : MonoBehaviour
{

    private void OnTriggerExit2D(Collider2D collision)
    {
        // making sure the ball is moving out of the chamber and not back into it
        //if (collision.attachedRigidbody.linearVelocity.y > 0)
        //{
            this.GetComponent<BoxCollider2D>().isTrigger = false;
            GameManager.Instance.ballInField = true;
        //}
    }

}
