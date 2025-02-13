using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public class GameManager: MonoBehaviour
{

    /// <summary>
    /// private storage for the Instance property.
    /// </summary>
    private static GameManager instance;

    /// <summary>
    /// A list of pinball objects storing the list the player currently has access to (similar to a joker lineup from Balatro.)
    /// </summary>
    private List<Pinball> ballLineup;

    /// <summary>
    /// the index of the pinball that will be played next from the lineup (what is 'on deck')
    /// </summary>
    private int ballLineupIndex;

    /// <summary>
    /// stores the current score; will be reset to zero at the start/end of each round.
    /// </summary>
    private int score;


    [SerializeField]
    /// <summary>
    /// a reference to the text GO that displays the current round score.
    /// </summary>
    public TMP_Text scoreText;

    [SerializeField]
    /// <summary>
    /// a reference to the GO that blocks the chamber.
    /// </summary>
    public ChamberBlocker blocker;

    /// <summary>
    /// initialize your game manager here. Do not reference to GameObjects here (i.e. GameObject.Find etc.)
    /// because the game manager will be created before the objects
    /// </summary>
    private GameManager()
    {

        score = 0;
        ballLineup = new List<Pinball>();

        // creating the default lineup of three plain pinballs.
        for (int i = 0; i < 3; i++)
        {
            ballLineup.Add(new Pinball());
        }
    }

    /// <summary>
    /// Because the gameManager is a singleton, this accesses the single gameManager instance.
    /// </summary>
    public static GameManager Instance
    {
        
        // accessing the game manager, creating one in the process if necessary.
        get
        {
            //Debug.Log("Attempting to access GM");
            if (instance == null)
            {
                Debug.Log("shit");
            }

            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }


    /// <summary>
    /// adds a value to the current score
    /// </summary>
    /// <param name="additive"> the score being added to the total </param>
    public void addScore(int additive)
    {
        this.score += additive;
        Debug.Log(score  + " is the new score.");
        // updating the score text field
        scoreText.text = "Current Score: " + score;
        
    }


    /// <summary>
    /// spawns the next ball in the lineup; called whenever a round starts or the current ball falls off screen.
    /// </summary>
    public void SpawnBall()
    {
        Pinball ball;
            
        // getting the next ball in the sequence, calls a game over if there aren't any left.
        try
        {
            ball = ballLineup[ballLineupIndex];
        } 
        catch (Exception)
        {
            Debug.Log("game over!");
            return;
        }

        GameObject ballAsGO = (GameObject)Resources.Load(ball.prefabPath, typeof(GameObject));
        ballAsGO = GameObject.Instantiate(ballAsGO, new Vector3(6f, -4f, 0), new Quaternion());

        // syncing the ball up with the cameraMovementManager (THIS CODE FEELS SO YUCKY WHY DOES IT WORK LMAO)
        Camera camera = GameObject.FindAnyObjectByType<Camera>();
        CameraMovementManager cmm = camera.GetComponent<CameraMovementManager>();
        cmm.ball = ballAsGO.GetComponent<Rigidbody2D>();

        ballLineupIndex++;

        // allowing the next ball to go through the full chamber and not get stopped.
        blocker.GetComponent<BoxCollider2D>().isTrigger = true;

        
    }



}
