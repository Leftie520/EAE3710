//using NUnit.Framework;
//using UnityEngine;
//using System.Collections;
//using System;
//using System.Collections.Generic;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement;

//public class PinballGameManager : MonoBehaviour
//{
//    public static PinballGameManager instance;

//    public GameManager gameInstance;

//    private List<Pinball> ballLineup;

//    private int ballLineupIndex;

//    public bool ballInField;

//    public ChamberBlocker blocker;

//    private PinballGameManager()
//    {
//        Debug.Log("Running the Pinball Game Manager");

//        gameInstance = new GameManager();

//        StaticData.timer = 45f;

//        ballInField = false;
//        StaticData.score = 0;

//        ballLineup = new List<Pinball>();
//        for (int i = 0; i < 3; i++)
//        {
//            ballLineup.Add(new Pinball());
//        }

//        StaticData.currentLevel = 0;
//    }

//    /// <summary>
//    /// Because the GameManager is a singleton, this accesses the single gameManager instance.
//    /// </summary>
//    public static PinballGameManager Instance
//    {
//        // accessing the game manager, creating one in the process if necessary.
//        get
//        {
//            Debug.Log("PGM Instance is Ran");
//            //Debug.Log("Attempting to access GM");
//            if (instance == null)
//            {
//                Debug.Log("PinballGameManager DNE");
//            }

//            return instance;
//        }
//    }

//    private void Awake()
//    {
//        Debug.Log("Pinball Game Manager Awake is Ran");

//        if (instance == null)
//        {
//            DontDestroyOnLoad(this);
//            instance = this;
//        }
//        else
//        {
//            Destroy(gameObject);
//            return;
//        }

//        StaticData.timer = 30f;
//    }

//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (ballInField)
//        {
//            StaticData.timer -= Time.deltaTime;
//            String timerData = StaticData.timer.ToString("0.0");
//            GameManager.instance.timerText.text = "Time: " + timerData;
//            if (StaticData.timer <= 0)
//            {
//                ballInField = false;
//                StaticData.timer = 0;
//                setFlippersEnabled(false);
//            }
//        }
//    }

//    /// <summary>
//    /// adds a value to the current score
//    /// </summary>
//    /// <param name="additive"> the score being added to the total </param>
//    public void addScore(int additive)
//    {
//        Debug.Log("Add Score Method is ran");
//        StaticData.score += additive;
//        Debug.Log(StaticData.score + " is the new score.");

//        // updating the score text field
//        GameManager.instance.scoreText.text = "Current Score: " + StaticData.score;

//    }

//    /// <summary>
//    /// as the name implies, this shuts down the flippers
//    /// </summary>
//    private void setFlippersEnabled(bool enabled)
//    {
//        Debug.Log("Set Flippers Enabled Method is ran");
//        // in theory this is accessing all flippers
//        Flipper[] flippers = GameObject.FindObjectsByType<Flipper>(new FindObjectsSortMode());

//        // disabling each flipper
//        foreach (Flipper flipper in flippers)
//        {
//            flipper.isControllable = enabled;
//        }
//    }

//    public void SpawnBall()
//    {
//        Debug.Log("Spawn Ball Method is ran");
//        if (StaticData.score >= StaticData.scoreToBeat)
//        {
//            headToShop();
//            //prepareForLevelUp();
//            return;
//        }

//        Pinball ball;

//        // getting the next ball in the sequence, calls a game over if there aren't any left.
//        try
//        {
//            ball = ballLineup[ballLineupIndex];
//        }
//        catch (Exception)
//        {
//            endGame();
//            return;
//        }

//        GameObject ballAsGO = (GameObject)Resources.Load(ball.prefabPath, typeof(GameObject));
//        ballAsGO = GameObject.Instantiate(ballAsGO, new Vector3(7.82f, -4f, 0), new Quaternion());

//        // syncing the ball up with the cameraMovementManager 
//        Camera camera = GameObject.FindAnyObjectByType<Camera>();
//        CameraMovementManager cmm = camera.GetComponent<CameraMovementManager>();
//        cmm.ball = ballAsGO.GetComponent<Rigidbody2D>();

//        ballLineupIndex++;

//        // allowing the next ball to go through the full chamber and not get stopped.
//        blocker.GetComponent<BoxCollider2D>().isTrigger = true;

//        setFlippersEnabled(true);

//        // Hey this makes the timer respective to each ball
//        StaticData.timer = 30f;
//        gameInstance.timerText.text = "Time: 30";
//        ballInField = false;
//    }

//    /// <summary>
//    /// Has the game manager end the game
//    /// </summary>
//    private void endGame()
//    {
//        Debug.Log("End Game method is ran");
//        // Insert code to state GAME OVER on Screen
//        gameInstance.gameOverText.enabled = true;
//    }

//    private IEnumerator headToShop()
//    {
//        Debug.Log("attempting to load shop");
//        prepareForLevelUp();
//        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("ShopScene", LoadSceneMode.Single);
//        yield return new WaitUntil(() => asyncLoad.isDone);
//    }

//    private void prepareForLevelUp()
//    {
//        Debug.Log("levelling up");

//        if (StaticData.score < StaticData.scoreToBeat)
//        {
//            endGame();
//        }

//        // reseting a bunch of values for when the next level starts
//        StaticData.currentLevel++;
//        StaticData.score = 0;
//        StaticData.timer = 30f;
//        ballLineupIndex = 0;
//        gameInstance.timerText.text = "Time: " + StaticData.timer.ToString("0.0");
//        gameInstance.scoreTargetText.text = "Score to Beat: " + StaticData.scoreToBeat;
//        gameInstance.currentLevelText.text = "Level: " + (StaticData.currentLevel + 1).ToString();
//        gameInstance.scoreText.text = "Current Score: " + StaticData.score;
//        // making the game-based information invisible since it's not needed in the shop
//        //timerText.enabled = false;
//        //currentLevelText.enabled = false;
//        //scoreText.enabled = false;
//        //scoreTargetText.enabled = false;
//    }

//    public void calculateNextScoreToBeat()
//    {
//        int randomScoreIncrease = (int)(UnityEngine.Random.Range(0, StaticData.currentLevel) * (1 - 0.5) + 0.5);
//        StaticData.scoreToBeat = StaticData.score + randomScoreIncrease;
//    }
//}
