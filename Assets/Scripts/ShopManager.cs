using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{

    /// <summary>
    /// private storage for the Instance property.
    /// </summary>
    private static ShopManager instance;

    /// <summary>
    ///     Displays the next level the user would be going to
    /// </summary>
    public int currentLevel;

    [SerializeField]
    public TMP_Text nextLevelText;
    /// <summary>
    /// storage for the startButton as a GameObject
    /// </summary>
    [SerializeField]
    public SpriteRenderer startButton;

    /// <summary>
    /// storage for the rerollButton as a GameObject
    /// </summary>
    [SerializeField]
    public SpriteRenderer rerollButton;

    [SerializeField]
    public TMP_Text descriptionText;

    /// <summary>
    /// the spriteRenderer showing the currently equipped bumper prefab.
    /// </summary>
    [SerializeField]
    public SpriteRenderer currentBumper;

    /// <summary>
    /// the spriteRenderer showing the currently equipped spinner prefab.
    /// </summary>
    [SerializeField]
    public SpriteRenderer currentSpinner;

    /// <summary>
    /// the spriteRenderer showing the currently equipped flipper prefab.
    /// </summary>
    [SerializeField]
    public SpriteRenderer currentFlipper;

    /// <summary>
    /// the spriteRenderer showing the currently equipped ball prefab.
    /// </summary>
    [SerializeField]
    public SpriteRenderer currentBall;

    //private List<ShopSpaceButton> gameObjectsOnSale;
    private List<GameObject> gameObjectsOnSale;


    /// <summary>
    /// called when the manager is first created
    /// </summary>
    private void Awake()
    {



        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this);
            //AudioListener[] listeners = UnityEngine.Object.FindObjectsOfType<AudioListener>();
            //if (listeners.Length > 1)
            //{
            //    Debug.LogWarning("Multiple AudioListeners found. Removing extras...");
            //    for (int i = 1; i < listeners.Length; i++)
            //    {
            //        Destroy(listeners[i]); // Keep the first one, remove others
            //    }
            //}
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //gameObjectsOnSale = new List<ShopSpaceButton>();
        gameObjectsOnSale = new List<GameObject>();
        StartShop();
    }


    /// <summary>
    /// Because the ShopManager is a singleton, this accesses the single gameManager instance.
    /// </summary>
    public static ShopManager Instance
    {

        get
        {

            if (instance == null)
            {
                Debug.Log("ShopManager DNE");
            }

            return instance;
        }
    }



    public void StartShop()
    {
        // it works but I hate it
        //await SceneManager.LoadSceneAsync("ShopScene", LoadSceneMode.Single);
        //StartCoroutine(LoadShopScene());

        currentLevel = StaticData.currentLevel;
        Debug.Log("new level is in shop: " + currentLevel);
        nextLevelText.text = "Next Level: " + currentLevel.ToString();
        gameObjectsOnSale = new List<GameObject>();

        rollShop();
        updateCurrLayoutUI();
    }

    private IEnumerator LoadShopScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("ShopScene", LoadSceneMode.Single);
        yield return new WaitUntil(() => asyncLoad.isDone);

        for (int i = 0; i < 3; i++)
        {
            GameObject newShopItem = (GameObject)Resources.Load("Prefabs/ShopSpace", typeof(GameObject));
            newShopItem = GameObject.Instantiate(newShopItem, new Vector3(5f * (i - 1), 2f, 0), Quaternion.identity);
        }
    }

    public void rollShop()
    {
        foreach (ShopSpaceButton button in FindObjectsOfType<ShopSpaceButton>())
        {
            Destroy(button.gameObject);
        }

        GameObject shopParent = GameObject.Find("ShopSpaceParent"); // Make sure this exists in the ShopScene
        int saleCount = gameObjectsOnSale.Count;
        for(int i = 0; i < saleCount; i++)
        {
            Destroy(gameObjectsOnSale[i]);
        }
        gameObjectsOnSale.Clear();

        for (int i = 0; i < 3; i++)
        {
            //ShopSpaceButton newShopItem = (ShopSpaceButton)Resources.Load("Prefabs/ShopSpace", typeof(ShopSpaceButton));
            GameObject newShopItem = (GameObject)Resources.Load("Prefabs/ShopSpace", typeof(GameObject));
            newShopItem = GameObject.Instantiate(newShopItem, new Vector3(5f * (i - 1), 2f, 0), new Quaternion());
            newShopItem.transform.SetParent(shopParent.transform);
            gameObjectsOnSale.Add(newShopItem);

            //newShopItem.descriptionReference = descriptionText;
            
        }
    }

    public void updateCurrLayoutUI()
    {
        currentBumper.sprite = CurrentLayout.Instance.currBumper.GetComponent<SpriteRenderer>().sprite;
        currentBumper.transform.localScale = new Vector3(0.65f, 0.65f, 1f);
    }

    /// <summary>
    ///     HeadToGame has the player resume playing pinball, by going onto the next level.
    /// </summary>
    public void headToGame()
    {
        for (int i = 0; i < gameObjectsOnSale.Count; i++)
        {
            Destroy(gameObjectsOnSale[i]);
        }
        gameObjectsOnSale.Clear();

        Debug.Log("attempting to load game");
        //GameManager.instance.currentlyAtShop = false;
        Scene gameScene = SceneManager.GetSceneByName("GameScene");
        if (gameScene.isLoaded)
        {
            GameObject table = null;

            // turning on all text fields
            foreach (GameObject obj in gameScene.GetRootGameObjects())
            {
                obj.SetActive(true);
                if (obj.name.Equals("Table"))
                    table = obj;
            }

            List<Transform> newBumpersPos = new List<Transform>();

            // replacing every old bumper with an instance of the currently selected bumper.
            foreach (Transform child in table.transform)
            {
                GameObject obj = child.gameObject;
                if (obj.GetComponent<Bumper>() != null)
                {

                    Transform oldTransform = obj.transform;
                    Destroy(obj);

                    newBumpersPos.Add(oldTransform);

                }

            }

            foreach (Transform transform in newBumpersPos)
            {
                GameObject obj = Instantiate(CurrentLayout.Instance.currBumper, table.transform);
                obj.transform.localPosition = transform.localPosition;

                obj.transform.localScale = new Vector3(.65f, .65f, .65f);
                if (CurrentLayout.Instance.currBumper == PrefabDB.Instance.bumperTable[PrefabDB.Bumpers.Moon])
                {
                    obj.transform.localScale = obj.transform.localScale /.65f;

                }




            }
            //GameManager.instance.SpawnBall();

            SceneManager.UnloadSceneAsync("ShopScene");
            //SceneManager.LoadScene("GameScene");
            //yield return new WaitUntil(() => asyncLoad.isDone);
            //AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync("ShopScene");
            //yield return new WaitUntil(() => asyncLoad.isDone);
        }
    }
}
