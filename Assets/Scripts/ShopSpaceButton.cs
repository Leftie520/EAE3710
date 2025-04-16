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

        description = PrefabDB.Instance.bumperDescs[bumper];

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
        descriptionReference.text = description;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionReference.text = "";

    }
}
