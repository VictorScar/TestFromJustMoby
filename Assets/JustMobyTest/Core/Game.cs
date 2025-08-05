using System.Collections;
using System.Collections.Generic;
using ScarFramework.UI;
using UnityEngine;
using UnityEngine.Serialization;

public class Game : MonoBehaviour
{
    private TowerCubesConfig _cubeesConfig;

    [SerializeField] private GameTextConfig gameTextConfig;
    [SerializeField] private ScrollbarConroller scrollbarConroller;
    [SerializeField] private DragController dragController;
    [SerializeField] private TowerController towerController;
    [SerializeField] private HoleAreaController holeAreaController;
    [SerializeField] private NotificationController notificationController;
    [SerializeField] private TowerData towerData;

    public void StartGame(TowerCubesConfig cubesConfig)
    {
        _cubeesConfig = cubesConfig;
        dragController.Init(cubesConfig, cubesConfig.CubeSize);
        var gameScreen = GameServices.I.UISystem.GetScreen<GameScreen>();
        gameScreen.Show();
        var towerView = gameScreen.TowerView;
        towerView.CubeSize = cubesConfig.CubeSize;
        scrollbarConroller.Init(dragController, _cubeesConfig);
        holeAreaController.Init(cubesConfig, gameScreen.HoleArea);

        towerController.Init(cubesConfig, towerView, towerData, dragController);
        notificationController.Init(towerController, holeAreaController, gameScreen.NotificationPanel, gameTextConfig);
    }
}