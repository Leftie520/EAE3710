using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    /// A list that tracks the specific scores required to beat this level
    /// </summary>
    private List<int> levelScoreTargets;

    /// <summary>
    /// The current level the player is on
    /// </summary>
    private int currentLevel;

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
    /// A text to display the score to beat to pass to the next level
    /// </summary>
    [SerializeField]
    public TMP_Text scoreTargetText;

    /// <summary>
    /// A text to display the score to beat to pass to the next level
    /// </summary>
    [SerializeField]
    public TMP_Text currentLevelText;

    /// <summary>
    /// States the game is over
    /// </summary>
    [SerializeField]
    public TMP_Text gameOverText;


    /// <summary>
    /// initialize your game manager here. Do not reference to GameObjects here (i.e. GameObject.Find etc.)
    /// because the game manager will be created before the objects
    /// </summary>
    private GameManager()
    {
        timer = 45f;
        //timerText.text = "Time: 45";
        ballInField = false;

        score = 0;
        ballLineup = new List<Pinball>();

        // creating the default lineup of three plain pinballs.
        for (int i = 0; i < 3; i++)
        {
            ballLineup.Add(new Pinball());
        }

        levelScoreTargets = new List<int>();
        levelScoreTargets.Add(100);
        levelScoreTargets.Add(2500);
        levelScoreTargets.Add(4000);
        levelScoreTargets.Add(5500);
        levelScoreTargets.Add(7000);
        levelScoreTargets.Add(10000);
        levelScoreTargets.Add(15000);
        levelScoreTargets.Add(20000);
        levelScoreTargets.Add(30000);

        currentLevel = 0;
    }

    /// <summary>
    /// Because the GameManager is a singleton, this accesses the single gameManager instance.
    /// </summary>
    public static GameManager Instance
    {

        // accessing the game manager, creating one in the process if necessary.
        get
        {
            //Debug.Log("Attempting to access GM");
            if (instance == null)
            {
                Debug.Log("GameManager DNE");
            }

            return instance;
        }
    }

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


        timer = 30f;
        timerText.text = "Time: 45";
        scoreTargetText.text = "Score to Beat: " + levelScoreTargets[currentLevel].ToString();
        currentLevelText.text = "Level: " + (currentLevel + 1).ToString();
        gameOverText.text = "GAME OVER";
        gameOverText.enabled = false;
    }


    /// <summary>
    /// called every frame
    /// </summary>
    private void Update()
    {
        if (ballInField)
        {
            timer -= Time.deltaTime;
            timerText.text = "Time: " + timer.ToString("0.0");
            if (timer < 0)
            {
                ballInField = false;
                timer = 0;
                setFlippersEnabled(false);
            }
        }

        if(timer <= 0)
        {
            setFlippersEnabled(false);
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
        if (score >= levelScoreTargets[currentLevel])
        {
            prepareForLevelUp();
            return;
        }

        Pinball ball;
            
        // getting the next ball in the sequence, calls a game over if there aren't any left.
        try
        {
            ball = ballLineup[ballLineupIndex];
        } 
        catch (Exception)
        {
            endGame();
            return;
        }

        GameObject ballAsGO = (GameObject)Resources.Load(ball.prefabPath, typeof(GameObject));
        ballAsGO = GameObject.Instantiate(ballAsGO, new Vector3(7.82f, -4f, 0), new Quaternion());

        // syncing the ball up with the cameraMovementManager 
        Camera camera = GameObject.FindAnyObjectByType<Camera>();
        CameraMovementManager cmm = camera.GetComponent<CameraMovementManager>();
        cmm.ball = ballAsGO.GetComponent<Rigidbody2D>();

        ballLineupIndex++;

        // allowing the next ball to go through the full chamber and not get stopped.
        blocker.GetComponent<BoxCollider2D>().isTrigger = true;

        setFlippersEnabled(true);

        // Hey this makes the timer respective to each ball
        timer = 30f;
        timerText.text = "Time: 30";
        ballInField = false;

        
    }

    /// <summary>
    /// Has the game manager end the game
    /// </summary>
    private void endGame()
    {
        // Insert code to state GAME OVER on Screen
        gameOverText.enabled = true;
    }

    private void prepareForLevelUp()
    {
        Debug.Log("levelling up");

        if (score < levelScoreTargets[currentLevel])
        {
            endGame();
        }

        // reseting a bunch of values for when the next level starts
        currentLevel++;
        score = 0;
        timer = 30f;
        ballLineupIndex = 0;
        timerText.text = "Time: " + timer.ToString("0.0");
        scoreTargetText.text = "Score to Beat: " + levelScoreTargets[currentLevel].ToString();
        currentLevelText.text = "Level: " + (currentLevel + 1).ToString();
        scoreText.text = "Current Score: " + score;

        // making the game-based information invisible since it's not needed in the shop
        timerText.enabled = false;
        currentLevelText.enabled = false;
        scoreText.enabled = false;
        scoreTargetText.enabled = false;

        //starting the shop
        Debug.Log("attempting to load shop");
        ShopManager.Instance.StartShop();
    }

}
