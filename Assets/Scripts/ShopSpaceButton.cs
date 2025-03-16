using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class ShopSpaceButton : MonoBehaviour, IPointerDownHandler
{

    public Sprite unusedSprite;

    public int cost;


    public ShopSpaceButton(int cost)
    {
        this.cost = cost;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

            Debug.Log("Purchased Something For " + cost + "!");
            this.GetComponent<SpriteRenderer>().sprite = unusedSprite;
            //Update Money Value
    }
}
