using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
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

    /// <summary>
    /// stores how much time is remaining for that ball
    /// </summary>
    public float timer;

    /// <summary>
    /// stores whether or not we are in control of the flippers (i.e. if the timer is going)
    /// </summary>
    public bool ballInField;

    [SerializeField]
    /// <summary>
    /// a reference to the text GO that displays the current round score.
    /// </summary>
    public TMP_Text scoreText;

    [SerializeField]
    /// <summary>
    /// a reference to the text GO that displays the current round time.
    /// </summary>
    public TMP_Text timerText;

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
        timer = 30f;
        ballInField = false;

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
    /// called every frame
    /// </summary>
    private void Update()
    {
        if (ballInField)
        {
            Debug.Log("test");
            timer -= Time.deltaTime;
            timerText.text = "Time: " + timer.ToString("0.0");
            if (timer < 0)
            {
                ballInField = false;
                timer = 0;
                setFlippersEnabled(false);
            }
        }

        
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
    /// as the name implies, this shuts down the flippers
    /// </summary>
    private void setFlippersEnabled(bool enabled)
    {
        // in theory this is accessing all flippers
        Flipper[] flippers = GameObject.FindObjectsByType<Flipper>(new FindObjectsSortMode());

        // disabling each flipper
        foreach (Flipper flipper in flippers)
        {
            flipper.isControllable = enabled;
        }
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

        // syncing the ball up with the cameraMovementManager 
        Camera camera = GameObject.FindAnyObjectByType<Camera>();
        CameraMovementManager cmm = camera.GetComponent<CameraMovementManager>();
        cmm.ball = ballAsGO.GetComponent<Rigidbody2D>();

        ballLineupIndex++;

        // allowing the next ball to go through the full chamber and not get stopped.
        blocker.GetComponent<BoxCollider2D>().isTrigger = true;

        setFlippersEnabled(true);
        timer = 30f;
        timerText.text = "Time: 30";
        ballInField = false;

        
    }



}
