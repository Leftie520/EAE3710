using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabDB : MonoBehaviour
{

    private static PrefabDB instance;

    // I'll be honest I'm not entirely sure if we need different enums or not...
    public enum Balls
    {
        Basic
    }

    public enum Bumpers
    {
        Basic = 0,
        Moon = 1,
        Mercury = 2,
        Kepler = 3,
        Clone = 4,
        Moblize = 5
    }


    public Dictionary<Bumpers, GameObject> bumperTable;

    [SerializeField]
    private GameObject[] bumpersAsGOs;

    private void Awake()
    {
        //Debug.Log("DB created");

        DontDestroyOnLoad(this);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        bumperTable = new Dictionary<Bumpers, GameObject>();
        Debug.Log(bumpersAsGOs.Length);

        // populating the bumperTable
        for (int i = 0; i < bumpersAsGOs.Length; i++)
        {
            bumperTable.Add((Bumpers)i, bumpersAsGOs[i]);
            Debug.Log("Bumper: " + bumpersAsGOs[i].ToString());
        }

    }


    public static PrefabDB Instance
    {

        // accessing the game manager, creating one in the process if necessary.
        get
        {

            if (instance == null)
            {
                Debug.Log("PrefabDB doesn't exist");
            }

            return instance;
        }
    }



}
