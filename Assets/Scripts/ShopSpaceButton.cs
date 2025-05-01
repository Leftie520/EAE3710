using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;
using static PrefabDB;

public class ShopSpaceButton : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject itemForSale;
    public int shopItemIndex;

    public TMP_Text descriptionReference;
    public string description;

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
        int index = UnityEngine.Random.Range(0, PrefabDB.Instance.shopItemsTable.Count);
        shopItemIndex = -1;

        PrefabDB.ShopItems shopItem = (PrefabDB.ShopItems)index;
        description = PrefabDB.Instance.shopItemsDescs[shopItem];
        shopItemIndex = index;
        return PrefabDB.Instance.shopItemsTable[shopItem];

        //if (index < 8)
        //{
        //    // accessing a randomly generated bumper from the enum
        //    PrefabDB.Bumpers bumper = (PrefabDB.Bumpers)index;

        //    description = PrefabDB.Instance.bumperDescs[bumper];
        //    shopItemIndex = index;

        //    // converting the enum to a GO and returning it
        //    return PrefabDB.Instance.bumperTable[bumper];
        //}
        //else
        //{
        //    shopItemIndex = index;
        //    index = index - 7;

        //    // Accessing a randomly generated spinner from the enum
        //    PrefabDB.Spinners spinners = (PrefabDB.Spinners)index;

        //    description = PrefabDB.Instance.spinnerDescs[spinners];

        //    // converting the enum to a GO and returning it
        //    return PrefabDB.Instance.spinnerTable[spinners];
        //}
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        Debug.Log("Purchased Something For " + cost + "!");
        Destroy(this.gameObject);

        if (shopItemIndex < 8)
            CurrentLayout.Instance.currBumper = itemForSale;
        else
            CurrentLayout.Instance.currSpinner = itemForSale;

        ShopManager.Instance.updateCurrLayoutUI();
        //Update Money Value

        //resetting the item description field
        OnPointerExit(eventData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptionReference.text = description;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionReference.text = "";

    }
}
