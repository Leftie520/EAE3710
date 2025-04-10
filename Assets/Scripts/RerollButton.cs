using UnityEngine;
using UnityEngine.EventSystems;

public class RerollButton : MonoBehaviour, IPointerDownHandler
{
    //// Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Let's go gambling for new items");
        //StartCoroutine(ShopManager.instance.headToGame());
        ShopManager.Instance.rollShop();
    }
}
