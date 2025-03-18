using UnityEngine;

public class CurrentLayout : MonoBehaviour
{

    private static CurrentLayout instance;

    // we can add other fields for the other enums once we have items to put in the enums...
    public GameObject currBumper;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currBumper = PrefabDB.Instance.bumperTable[PrefabDB.Bumpers.Basic];
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
    }

    public static CurrentLayout Instance
    {

        get
        {

            if (instance == null)
            {
                Debug.Log("CurrentLayout DNE");
            }

            return instance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
