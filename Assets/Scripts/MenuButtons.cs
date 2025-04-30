using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    [SerializeField]
    int status; //0 -> exit game, 1 -> start game

    public void Clicked()
    {
        switch(status)
        {
            case 0:
                Application.Quit(); 
                break;
            case 1:
                SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
                break;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
