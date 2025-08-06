using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using JustMobyTest.Configs._Data;
using JustMobyTest.Core;
using UnityEngine;
using Zenject;

public class Game : MonoBehaviour
{
    [SerializeField] private ScrollbarConroller scrollbarConroller;
    [SerializeField] private DragController dragController;
    [SerializeField] private TowerController towerController;
    [SerializeField] private HoleAreaController holeAreaController;
    [SerializeField] private NotificationController notificationController;

    [Inject] private GameServices _gameServices;

    private CancellationTokenSource _gameCancellationTokenSource;

    private void Start()
    {
        StartGame();
    }

    private async UniTask StartGame()
    {
        _gameCancellationTokenSource = new CancellationTokenSource();
        var gameConfigData =
            await _gameServices.GameConfigService.GetGameConfigData(_gameCancellationTokenSource.Token);

        var cubesConfigData = gameConfigData.CubesConfigData;
        dragController.Init(cubesConfigData, gameConfigData.CubeSize);
        var gameScreen = _gameServices.UISystem.GetScreen<GameScreen>();
        gameScreen.Show();
        var towerView = gameScreen.TowerView;
        towerView.CubeSize = gameConfigData.CubeSize;
        scrollbarConroller.Init(dragController, gameConfigData.CubesConfigData);
        holeAreaController.Init(cubesConfigData, gameConfigData.CubeSize, gameScreen.HoleArea);

        towerController.Init(gameConfigData, towerView, _gameServices.ProgressDataService, dragController);
        notificationController.Init(towerController, holeAreaController, gameScreen.NotificationPanel,
            gameConfigData.GameTextsConfig);
    }
}