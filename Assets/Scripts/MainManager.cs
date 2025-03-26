//using JetBrains.Annotations;
//using UnityEngine;

//public class MainManager : MonoBehaviour
//{
//    /// <summary>
//    /// private storage for the Instance property.
//    /// </summary>
//    public static MainManager instance;

//    public int currentLevel;
//    public int money;
//    public int score;
//    public int scoreToBeat;
//    GameManager gameManager = GameManager.Instance;

//    void Start()
//    {
//        currentLevel = 1;
//        money = 0;
//        score = 0;
//        scoreToBeat = 500;

//        if (gameManager == null)
//        {
//            Debug.LogError("GameManager instance not found.");
//            return;
//        }

//        gameManager.timer = 30f;
//        gameManager.timerText.text = "Time: 30";
//        gameManager.scoreTargetText.text = "Score to Beat: " + scoreToBeat.ToString();
//        gameManager.currentLevelText.text = "Level: " + (currentLevel).ToString();
//        gameManager.gameOverText.text = "GAME OVER";
//        gameManager.gameOverText.enabled = false;
//    }

//    //// Update is called once per frame
//    //void Update()
//    //{

//    //}

//    /// <summary>
//    /// Because the MainManager is a singleton, this accesses the single gameManager instance.
//    /// </summary>
//    public static MainManager Instance
//    {

//        // accessing the game manager, creating one in the process if necessary.
//        get
//        {
//            //Debug.Log("Attempting to access GM");
//            if (instance == null)
//            {
//                Debug.Log("MainManager DNE");
//            }

//            return instance;
//        }
//    }

//public void calculateNextScoreToBeat()
//{
//    int randomScoreIncrease = (int)(Random.Range(0, currentLevel) * (1 - 0.5) + 0.5);
//    scoreToBeat = score + randomScoreIncrease;
//}

//    public void moveToNextLevel()
//    {
//        calculateNextScoreToBeat();
//        currentLevel++;
//        score = 0;
//    }


//    private int getScoreOverflow()
//    {
//        return score - scoreToBeat;
//    }
//}
