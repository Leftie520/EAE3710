using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabDB : MonoBehaviour
{
    private static PrefabDB instance;

    // Ball types 
    //I can work on new balls if we really have time left...
    public enum Balls
    {
        Basic
    }

    // Bumper types
    public enum Bumpers
    {
        Basic = 0,
        Moon = 1,
        Mercury = 2,
        Kepler = 3,
        Clone = 4,
        Moblize = 5,
        WhiteDwarf = 6,
        Venus = 7,
        Phobos = 8  
    }

    // Spinner types 
    public enum Spinners
    {
        Basic = 0,
        CometTrail = 1,
        DarkMatter = 2,
        Wormhole = 3
    }

    // Bumper dictionaries
    public Dictionary<Bumpers, GameObject> bumperTable;
    public Dictionary<Bumpers, string> bumperDescs;

    // Spinner dictionaries
    public Dictionary<Spinners, GameObject> spinnerTable;
    public Dictionary<Spinners, string> spinnerDescs;

    // Prefab arrays to populate the dictionaries (assigned in Inspector)
    [SerializeField]
    private GameObject[] bumpersAsGOs;

    [SerializeField]
    private GameObject[] spinnersAsGOs;

    private void Awake()
    {
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

        // Initialize bumper tables
        bumperTable = new Dictionary<Bumpers, GameObject>();
        bumperDescs = new Dictionary<Bumpers, string>();

        for (int i = 0; i < bumpersAsGOs.Length; i++)
        {
            bumperTable.Add((Bumpers)i, bumpersAsGOs[i]);
        }

        bumperDescs.Add(Bumpers.Basic, "Basic Bumper:\n\nA basic bumper which scores 300 points when hit.");
        bumperDescs.Add(Bumpers.Moon, "Moon Bumper:\n\nA very large bumper which scores 100 points when hit.");
        bumperDescs.Add(Bumpers.Mercury, "Mercury Bumper:\n\nA bumper which launches the ball very fast and scores 500 points when hit.");
        bumperDescs.Add(Bumpers.Kepler, "Kepler Bumper:\n\nA bumper which scores 200 points on its first hit, 400 on its second, up to 2000 points, and explodes on its 5th hit.");
        bumperDescs.Add(Bumpers.Clone, "Clone Bumper:\n\nA bumper which spawns ghost balls when hit. Scores 100 points when hit.");
        bumperDescs.Add(Bumpers.Moblize, "Mobilize Bumper:\n\nA bumper which scores 150 points on first hit, 250 on second hit, and so on.");
        bumperDescs.Add(Bumpers.WhiteDwarf, "WhiteDwarf Bumper:\n\nA bumper which scores 250 points and adds 0.5 seconds of extra time when hit.");
        bumperDescs.Add(Bumpers.Venus, "Venus Bumper:\n\nA bumper that scores 600 points and randomly changes the bounce angle when hit.");
        bumperDescs.Add(Bumpers.Phobos, "Phobos Bumper:\n\nA bumper that scores 400 points and slightly shifts its position when hit.");
        // Initialize spinner tables
        spinnerTable = new Dictionary<Spinners, GameObject>();
        spinnerDescs = new Dictionary<Spinners, string>();

        for (int i = 0; i < spinnersAsGOs.Length; i++)
        {
            spinnerTable.Add((Spinners)i, spinnersAsGOs[i]);
        }

        spinnerDescs.Add(Spinners.Basic, "Basic Spinner:\n\nA basic spinner which scores 500 points when hit.");
        spinnerDescs.Add(Spinners.CometTrail, "CometTrail Spinner:\n\nA spinner which scores 700 points and temporarily speeds up the ball when hit.");
        spinnerDescs.Add(Spinners.DarkMatter, "DarkMatter Spinner:\n\nA spinner Randomly grants 1000 points or deducts 500 points when hit.");
        spinnerDescs.Add(Spinners.Wormhole, "Wormhole Spinner:\n\nA spinner that teleports the ball to a random wormhole and scores 800 points when hit.");
    }

    public static PrefabDB Instance
    {
        get
        {
            if (instance == null)
            {
                //Debug.Log("PrefabDB doesn't exist");
            }
            return instance;
        }
    }
}
