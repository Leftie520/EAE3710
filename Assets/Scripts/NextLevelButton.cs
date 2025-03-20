using UnityEngine;
using UnityEngine.EventSystems;

public class NextLevelButton : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Attempting to resume to game");
        StartCoroutine(ShopManager.instance.headToGame());
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
