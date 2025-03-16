using TMPro;
using UnityEngine;

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
    /// storage for the first item button as a GameObject
    /// </summary>
    [SerializeField]
    public SpriteRenderer shopSpace1;

    /// <summary>
    /// storage for the second item button as a GameObject
    /// </summary>
    [SerializeField]
    public SpriteRenderer shopSpace2;

    /// <summary>
    /// storage for the third item button as a GameObject
    /// </summary>
    [SerializeField]
    public SpriteRenderer shopSpace3;

    /// <summary>
    /// Jake what do these do? :|
    /// 
    /// Like if we're replacing each button with a 'lock button', then shouldn't we just override the current button space?
    /// If so, why do we need these?!
    /// </summary>
    [SerializeField]
    public SpriteRenderer lock1;

    /// <summary>
    /// read above
    /// </summary>
    [SerializeField]
    public SpriteRenderer lock2;

    /// <summary>
    /// read slightly further above
    /// </summary>
    [SerializeField]
    public SpriteRenderer lock3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    /// <summary>
    /// called when the manager is first created
    /// </summary>
    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Because the ShopManager is a singleton, this accesses the single gameManager instance.
    /// </summary>
    public static ShopManager Instance
    {

        // accessing the game manager, creating one in the process if necessary.
        get
        {
            
            if (instance == null)
            {
                Debug.Log("shit");
            }

            return instance;
        }
    }



    //public void StartShop()
    //{


    //    // Turn on cursor
    //    // Switch to Shop UI
    //    // Set Values
    //}
}
