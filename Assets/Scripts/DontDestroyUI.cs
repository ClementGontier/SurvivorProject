using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;  

public class DontDestroyUI : MonoBehaviour
{
    public static DontDestroyUI instance; 
    public GameObject gameOverMenu; 
    public TMP_Text healthUI;
    void Awake()
    {       

        if (instance == null)
        {
            instance = this; 
            DontDestroyOnLoad(gameObject);           
        }
        else if (instance != this)
        {
            Destroy(gameObject); 
        }    
        this.gameObject.SetActive(false);   
        SceneManager.LoadSceneAsync("MainMenu");    
    }


    public GameObject GetGameOverMenu()
    {
        return gameOverMenu;    
    }

    public TMP_Text GetHealthUI()
    {
        return healthUI;    
    }   

    public void LoadMainMenu()
    {
        DontDestroyUI.instance.gameObject.SetActive(false);   
        SceneManager.LoadSceneAsync("MainMenu");
        Time.timeScale = 0f;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
