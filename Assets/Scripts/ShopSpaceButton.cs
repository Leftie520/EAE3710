using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using TMPro;

public class ShopSpaceButton : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{

    public GameObject itemForSale;

    public TMP_Text descriptionReference;

    public int cost;


    public void Start()
    {
        cost = 0;

        // generating random item for this button
        itemForSale = generateItem();
        
        SpriteRenderer sprt = GetComponent<SpriteRenderer>();

        
        sprt.sprite = itemForSale.GetComponent<SpriteRenderer>().sprite;
        sprt.transform.localScale = new Vector3(0.65f, 0.65f, 1f);
        sprt.color = Color.white;
    }

    private GameObject generateItem()
    {
        // note that for now we are specifically choosing bumpers because we don't have any items yet :`(
        int index = UnityEngine.Random.Range(0, PrefabDB.Instance.bumperTable.Count);

        // accessing a randomly generated bumper from the enum
        PrefabDB.Bumpers bumper = (PrefabDB.Bumpers)index;

        // converting the enum to a GO and returning it
        return PrefabDB.Instance.bumperTable[bumper];
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        Debug.Log("Purchased Something For " + cost + "!");
        Destroy(this.gameObject);

        CurrentLayout.Instance.currBumper = itemForSale;
        ShopManager.Instance.updateCurrLayoutUI();
        //Update Money Value
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptionReference.text = "test";
    }
}
