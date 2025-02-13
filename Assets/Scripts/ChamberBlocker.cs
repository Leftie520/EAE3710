using UnityEngine;

public class ChamberBlocker : MonoBehaviour
{

    private void OnTriggerExit2D(Collider2D collision)
    {
        this.GetComponent<BoxCollider2D>().isTrigger = false;
    }

}
