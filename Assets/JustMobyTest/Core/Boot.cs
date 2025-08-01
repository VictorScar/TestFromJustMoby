using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boot : MonoBehaviour
{
    [SerializeField] private GameServices gameServices;
    [SerializeField] private Game game;
    [SerializeField] private TowerCubeConfig cubesConfig;
    void Start()
    {
        gameServices.Init(cubesConfig);
        DontDestroyOnLoad(gameServices.gameObject);
        
        game.StartGame(cubesConfig);
    }

}
