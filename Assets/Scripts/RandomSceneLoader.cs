using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using JetBrains.Annotations;

public class RandomSceneLoader : MonoBehaviour
{

    public static RandomSceneLoader Instance { get; private set; }


    public static int[] sceneIndexes = new int[]{1, 2, 3};
    private static List<int> loadedScenes = new List<int>();
    private static int roundCount = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }


    // Start is called before the first frame update
    public void StartGame()
    {
        Debug.Log("StartGame() was called!");
        roundCount = 0;
        loadedScenes.Clear();
        LoadNextScene();


    }

    public static void LoadNextScene()
    {
        if (sceneIndexes.Length == 0)
        {
            Debug.LogError("No scenes were ever assigned!!!");
        return;
        }

        List<int> availableScenes = new List<int>(sceneIndexes);

        // Remove already played levels
        foreach (int loaded in loadedScenes)
        {
            availableScenes.Remove(loaded);
        }

        if (availableScenes.Count == 0)
        {
            loadedScenes.Clear();
            availableScenes = new List<int>(sceneIndexes);
        }

        int nextScene = availableScenes[Random.Range(0, availableScenes.Count)];
        loadedScenes.Add(nextScene);

        SceneManager.LoadScene(nextScene);
    }

    public void QuitToMainMenu()
    {
        loadedScenes.Clear(); // Reset round tracking
        SceneManager.LoadScene("Title Page"); // Load your main menu scene
    }
}
