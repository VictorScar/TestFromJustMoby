using System.Collections;
using System.Collections.Generic;
using ScarFramework.UI;
using UnityEngine;
using UnityEngine.Serialization;

public class Game : MonoBehaviour
{
    private TowerCubesConfig _cubesesConfig;

    [SerializeField] private ScrollbarConroller _scrollbarConroller;
    [SerializeField] private DragController dragController;
    [SerializeField] private TowerController _towerController;
    [SerializeField] private TowerData towerData;

    public void StartGame(TowerCubesConfig cubesConfig)
    {
        _cubesesConfig = cubesConfig;
        dragController.Init(cubesConfig, cubesConfig.CubeSize);
        var gameScreen = GameServices.I.UISystem.GetScreen<GameScreen>();
        gameScreen.Show();
        var towerView = gameScreen.TowerView;
        towerView.CubeSize = cubesConfig.CubeSize;
        _scrollbarConroller.Init(dragController, _cubesesConfig);

        _towerController.Init(cubesConfig, towerView, towerData, dragController);
    }
}