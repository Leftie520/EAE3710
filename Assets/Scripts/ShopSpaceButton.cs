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

    public TMP_Text descriptionReference;
    public string description;

    public int cost;

    // new fields to track item type
    public bool isBumper;
    public int itemIndex;

    public void Start()
    {
        cost = 0;

        // generating random item for this button
        int bumperCount = PrefabDB.Instance.bumperTable.Count;
        int spinnerCount = PrefabDB.Instance.spinnerTable.Count;
        int totalCount = bumperCount + spinnerCount;
        int index = UnityEngine.Random.Range(0, totalCount);

        if (index < bumperCount)
        {
            isBumper = true;
            itemIndex = index;
            Bumpers bumper = (Bumpers)itemIndex;
            itemForSale = PrefabDB.Instance.bumperTable[bumper];
            description = PrefabDB.Instance.bumperDescs[bumper];
        }
        else
        {
            isBumper = false;
            itemIndex = index - bumperCount;
            Spinners spinner = (Spinners)itemIndex;
            itemForSale = PrefabDB.Instance.spinnerTable[spinner];
            description = PrefabDB.Instance.spinnerDescs[spinner];
        }

        SpriteRenderer sprt = GetComponent<SpriteRenderer>();

        sprt.sprite = itemForSale.GetComponent<SpriteRenderer>().sprite;
        sprt.transform.localScale = new Vector3(0.65f, 0.65f, 1f);
        sprt.color = Color.white;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        Debug.Log("Purchased Something For " + cost + "!");
        Destroy(this.gameObject);

        if (isBumper)
        {
            CurrentLayout.Instance.currBumper = itemForSale;
        }
        else
        {
            CurrentLayout.Instance.currSpinner = itemForSale;
        }

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
