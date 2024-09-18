using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public int tempDifficulty;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void StartNewRun()
    {
        StartCoroutine(LoadAndResetNewRun());
    }

    public void EndRun()
    {
        LoadScene("Main Menu Scene");
        FindObjectOfType<PlayerManager>().ResetRun();

        Destroy(FindObjectOfType<LevelManager>().gameObject);
        Destroy(FindObjectOfType<CombatHandler>().gameObject);
    }

    public IEnumerator LoadAndResetNewRun()
    {
        Debug.Log("starting new run");
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Overworld Scene");

        // Wait until the scene is fully loaded
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        Debug.Log("scene loaded");

        FindObjectOfType<PlayerManager>().ResetRun();
        FindObjectOfType<LevelManager>().ResetRun();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneAdditive(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    public void ReloadCurrentScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void EnableGameObjectsInScene(string sceneName)
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);
        if (scene.IsValid())
        {
            foreach (GameObject rootGameObject in scene.GetRootGameObjects())
            {
                rootGameObject.SetActive(true);
            }
        }
    }

    public void DisableGameObjectsInScene(string sceneName)
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);
        if (scene.IsValid())
        {
            foreach (GameObject rootGameObject in scene.GetRootGameObjects())
            {
                rootGameObject.SetActive(false);
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
