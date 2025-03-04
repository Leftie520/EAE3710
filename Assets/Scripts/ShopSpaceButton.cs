using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class ShopSpaceButton : MonoBehaviour, IPointerDownHandler
{

    public Sprite unusedSprite;

    public int cost;

    private Boolean isLock;

    public ShopSpaceButton(int cost)
    {
        this.cost = cost;
        isLock = false; 
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isLock)
        {
            Debug.Log("Purchased Something For " + cost + "!");
            this.GetComponent<SpriteRenderer>().sprite = unusedSprite;
            //Update Money Value
        }
    }
}
