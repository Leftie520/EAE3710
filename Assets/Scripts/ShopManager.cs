using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{

    /// <summary>
    /// private storage for the Instance property.
    /// </summary>
    private static ShopManager instance;

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


    /// <summary>
    /// called when the manager is first created
    /// </summary>
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



    public async void StartShop()
    {     
        // it works but I hate it
        await SceneManager.LoadSceneAsync("ShopScene", LoadSceneMode.Single);

        for (int i = 0; i < 3; i++)
        {
            GameObject newShopItem = (GameObject)Resources.Load("Prefabs/ShopSpace", typeof(GameObject));
            newShopItem = GameObject.Instantiate(newShopItem, new Vector3(5f*(i-1), 2f, 0), new Quaternion());
        
        }

    }

    public void updateCurrLayoutUI()
    {
        currentBumper.sprite = CurrentLayout.Instance.currBumper.GetComponent<SpriteRenderer>().sprite;
    }
}
