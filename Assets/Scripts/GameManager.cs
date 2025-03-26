using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameManager;

public class GameManager: MonoBehaviour
{

    /// <summary>
    /// private storage for the Instance property.
    /// </summary>
    public static GameManager instance;

    /// <summary>
    /// Private storage for the shop instance
    /// </summary>

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
    //public int currentLevel;

    /// <summary>
    /// the index of the pinball that will be played next from the lineup (what is 'on deck')
    /// </summary>
    private int ballLineupIndex;

    /// <summary>
    /// stores the current score; will be reset to zero at the start/end of each round.
    /// </summary>
    //private int score;

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
    ///     Checks to see if the player is currently at the shop. This would prevent the rest of the code running.
    /// </summary>
    public Boolean currentlyAtShop;


    /// <summary>
    /// initialize your game manager here. Do not reference to GameObjects here (i.e. GameObject.Find etc.)
    /// because the game manager will be created before the objects
    /// </summary>
    public GameManager()
    {
        //Debug.Log("Game Manager Constructor is Ran");
        timer = 45f;
        //timerText.text = "Time: 45";
        ballInField = false;

        StaticData.score = 0;
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
    }

    /// <summary>
    /// Because the GameManager is a singleton, this accesses the single gameManager instance.
    /// </summary>
    public static GameManager Instance
    {
        // accessing the game manager, creating one in the process if necessary.
        get
        {
            //Debug.Log("Instance is Ran");
            //Debug.Log("Attempting to access GM");
            if (instance == null)
            {
                Debug.Log("GameManager DNE");
            }

            return instance;
        }
    }

    void Awake()
    {
        //Debug.Log("Awake Method Is Ran");

        if (instance == null)
        {
            DontDestroyOnLoad(this);
            //DontDestroyOnLoad(blocker);
            instance = this;

            //Ensuring Camera is properly loaded
            Camera camera = Camera.main;
            if (camera != null)
            {
                DontDestroyOnLoad(camera.gameObject);
            }
            else
            {
                Debug.Log("Main Camera Not Found");
            }
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        Camera[] cameras = FindObjectsByType<Camera>(FindObjectsSortMode.None);
        foreach (Camera cam in cameras)
        {
            AudioListener listener = cam.GetComponent<AudioListener>();
            if (listener != null && cam != Camera.main)
            {
                listener.enabled = false;
            }
        }

        // Find all EventSystem objects in the scene
        EventSystem[] eventSystems = FindObjectsByType<EventSystem>(FindObjectsSortMode.None);

        // If there are more than one EventSystems, disable the extra ones
        if (eventSystems.Length > 1)
        {
            for (int i = 1; i < eventSystems.Length; i++) // Start from 1 to keep the first one active
            {
                eventSystems[i].gameObject.SetActive(false);
            }
        }

        timer = 30f;
        timerText.text = "Time: 30";
        scoreTargetText.text = "Score to Beat: " + levelScoreTargets[StaticData.currentLevel].ToString();
        currentLevelText.text = "Level: " + (StaticData.currentLevel + 1).ToString();
        gameOverText.text = "";
        //shopInstance = ShopManager.Instance;
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

        if (timer <= 0)
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
        //Debug.Log("Add Score Method is ran");
        StaticData.score += additive;
        Debug.Log(StaticData.score  + " is the new score.");

        // updating the score text field
        scoreText.text = "Current Score: " + StaticData.score;
        
    }

    /// <summary>
    /// as the name implies, this shuts down the flippers
    /// </summary>
    private void setFlippersEnabled(bool enabled)
    {
        //Debug.Log("Set Flippers Enabled Method is ran");
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
        //Debug.Log("Current Score: " + StaticData.score);
        //Debug.Log("Level Score Target: " + levelScoreTargets[StaticData.currentLevel]);
        Debug.Log("Next Level: " + levelScoreTargets[StaticData.currentLevel].ToString());
        Debug.Log("Current Level: " + StaticData.currentLevel);

        if (StaticData.score >= levelScoreTargets[StaticData.currentLevel])
        {
            Debug.Log("Score reached! Heading to shop.");
            headToShop();
            return;
        }
        else
        {
            Debug.Log("Score not reached yet. Keep playing.");
        }

        //Debug.Log("Spawn Ball Method is ran");
        //if (StaticData.score >= levelScoreTargets[StaticData.currentLevel])
        //{
        //    //StartCoroutine(headToShop());
        //    headToShop();
        //    //prepareForLevelUp();
        //    return;
        //}

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

        //if(blocker == null)
        //{
        //    Debug.Log("Blocker does not exist");
        //}

        GameObject ballAsGO = (GameObject)Resources.Load(ball.prefabPath, typeof(GameObject));
        ballAsGO = GameObject.Instantiate(ballAsGO, new Vector3(7.82f, -4f, 0), new Quaternion());

        Camera camera = Camera.main; // This ensures you're using the main camera
        if (camera != null)
        {
            CameraMovementManager cmm = camera.GetComponent<CameraMovementManager>();
            if (cmm != null)
            {
                cmm.ball = ballAsGO.GetComponent<Rigidbody2D>();
            }
            else
            {
                Debug.LogError("CameraMovementManager not found on the camera.");
            }
        }
        else
        {
            Debug.LogError("Main camera not found.");
        }

        // syncing the ball up with the cameraMovementManager 
        //Camera camera = GameObject.FindAnyObjectByType<Camera>();
        //CameraMovementManager cmm = camera.GetComponent<CameraMovementManager>();
        //cmm.ball = ballAsGO.GetComponent<Rigidbody2D>();


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
        //Debug.Log("End Game method is ran");
        // Insert code to state GAME OVER on Screen
        gameOverText.text = "GAME OVER";
    }

    private void headToShop()
    {
        //Debug.Log("attempting to load shop");
        prepareForLevelUp();

        Scene gameScene = SceneManager.GetSceneByName("GameScene");
        if (gameScene.isLoaded)
        {
            foreach (GameObject obj in gameScene.GetRootGameObjects())
            {
                if (obj != this.gameObject) // Don't disable GameManager
                    obj.SetActive(false);
            }
        }

        SceneManager.LoadScene("ShopScene", LoadSceneMode.Additive);

        Debug.Log("Starting shop");
        ShopManager.Instance.StartShop();
    }

    private void prepareForLevelUp()
    {
        Debug.Log("levelling up");

        if (StaticData.score < levelScoreTargets[StaticData.currentLevel])
        {
            endGame();
        }

        // reseting a bunch of values for when the next level starts
        StaticData.currentLevel++;
        Debug.Log("new level is " + StaticData.currentLevel);
        StaticData.score = 0;
        timer = 30f;
        ballLineupIndex = 0;
        timerText.text = "Time: " + timer.ToString("0.0");
        scoreTargetText.text = "Score to Beat: " + levelScoreTargets[StaticData.currentLevel].ToString();
        currentLevelText.text = "Level: " + (StaticData.currentLevel + 1).ToString();
        scoreText.text = "Current Score: " + StaticData.score;
        // making the game-based information invisible since it's not needed in the shop
        //timerText.enabled = false;
        //currentLevelText.enabled = false;
        //scoreText.enabled = false;
        //scoreTargetText.enabled = false;
    }

    public void calculateNextScoreToBeat()
    {
        int randomScoreIncrease = (int)(UnityEngine.Random.Range(0, StaticData.currentLevel) * (1 - 0.5) + 0.5);
        StaticData.scoreToBeat = StaticData.score + randomScoreIncrease;
    }

}
