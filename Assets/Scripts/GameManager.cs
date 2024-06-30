using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);            
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void OnLevelSuccess()
    {
        Debug.Log("Level Success");
        EndGameScreenController.Instance.ShowSuccessScreen();
    }
    public void OnLevelFail()
    {
        Debug.Log("Level Fail");
        EndGameScreenController.Instance.ShowFailScreen();
    }
    public void ReloadScene(bool isSuccess)
    {
        if (isSuccess)
        {
            SaveLoadManager.IncreaseLevel();
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("LevelScene");
        }
    }
    public void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelScene");
    }
}
