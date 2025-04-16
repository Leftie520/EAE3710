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

    public Dictionary<Bumpers, string> bumperDescs;

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
        bumperDescs = new Dictionary<Bumpers, string>();
        Debug.Log(bumpersAsGOs.Length);

        // populating the bumperTable
        for (int i = 0; i < bumpersAsGOs.Length; i++)
        {
            bumperTable.Add((Bumpers)i, bumpersAsGOs[i]);
            Debug.Log("Bumper: " + bumpersAsGOs[i].ToString());
        }

        bumperDescs.Add(Bumpers.Basic, "Basic Bumper:\n\nA basic bumper which scores 300 points when hit.");
        bumperDescs.Add(Bumpers.Moon, "Moon Bumper:\n\nA very large bumper which scores 100 points when hit");
        bumperDescs.Add(Bumpers.Mercury, "Mercury Bumper:\n\nA bumper which launches the ball very fast and scores 500 points when hit.");
        bumperDescs.Add(Bumpers.Kepler, "Kepler Bumper:\n\nA bumper which gives scores 200 points on its first hit, 400 on its second, and so on... nets 2000 points and explodes on its 5th hit.");
        bumperDescs.Add(Bumpers.Clone, "Clone Bumper:\n\nA bumper which spawns ghost balls when hit. Scores 100 points when hit.");
        bumperDescs.Add(Bumpers.Moblize, "Mobilize Bumper:\n\nA bumper which scores 150 points on its first hit, 250 on its second hit, and so on..." +
            ".");

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
