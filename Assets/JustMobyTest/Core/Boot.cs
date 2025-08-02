using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Boot : MonoBehaviour
{
    [SerializeField] private GameServices gameServices;
    [SerializeField] private Game game;
    [FormerlySerializedAs("cubesConfig")] [SerializeField] private TowerCubesConfig cubesesConfig;
    void Start()
    {
        gameServices.Init(cubesesConfig);
        DontDestroyOnLoad(gameServices.gameObject);
        
        game.StartGame(cubesesConfig);
    }

}
