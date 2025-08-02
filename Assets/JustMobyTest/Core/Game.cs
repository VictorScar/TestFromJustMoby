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
    [SerializeField] private TowerData towerData;
    public void StartGame(TowerCubeConfig cubeConfig)
    {
        _cubesConfig = cubeConfig;
        _dragController.Init(cubeConfig.CubeSize);
        var gameScreen = GameServices.I.UISystem.GetScreen<GameScreen>();
        gameScreen.Show();
        var towerView = gameScreen.TowerView;
        towerView.CubeSize = cubeConfig.CubeSize;
        _scrollbarConroller.Init(_dragController, _cubesConfig);
             
        _towerController.Init(towerView, towerData);
    }
}