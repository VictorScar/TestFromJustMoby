using System.Collections;
using System.Collections.Generic;
using ScarFramework.UI;
using UnityEngine;

public class Game : MonoBehaviour
{
    private TowerCubeConfig _cubesConfig;
    
    [SerializeField] private ScrollbarConroller _scrollbarConroller;
    [SerializeField] private DragController _dragController;
    [SerializeField] private TowerController _towerController;
    public void StartGame(TowerCubeConfig cubeConfig)
    {
        _dragController.Init();
        _cubesConfig = cubeConfig;
        var gameScreen = GameServices.I.UISystem.GetScreen<GameScreen>();
        gameScreen.Show();
        var towerView = gameScreen.TowerView;
        _scrollbarConroller.Init(_dragController, _cubesConfig);
        _towerController.Init(towerView);
    }
}