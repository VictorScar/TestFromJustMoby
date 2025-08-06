using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManageService : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "Game";
    public void LoadGameScene()
    {
        SceneManager.LoadSceneAsync(gameSceneName);
    }
}