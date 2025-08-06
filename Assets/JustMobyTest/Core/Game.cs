using JustMobyTest.Configs._Data;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private ScrollbarConroller scrollbarConroller;
    [SerializeField] private DragController dragController;
    [SerializeField] private TowerController towerController;
    [SerializeField] private HoleAreaController holeAreaController;
    [SerializeField] private NotificationController notificationController;

    private GameServices _gameServices;
    
    
    public void StartGame(GameConfigData gameConfigData)
    {
        var cubesConfigData = gameConfigData.CubesConfigData;
        dragController.Init(cubesConfigData, gameConfigData.CubeSize);
        var gameScreen = GameServices.I.UISystem.GetScreen<GameScreen>();
        gameScreen.Show();
        var towerView = gameScreen.TowerView;
        towerView.CubeSize = gameConfigData.CubeSize;
        scrollbarConroller.Init(dragController, gameConfigData.CubesConfigData);
        holeAreaController.Init(cubesConfigData, gameConfigData.CubeSize, gameScreen.HoleArea);

        towerController.Init(gameConfigData, towerView, _gameServices.ProgressDataService, dragController);
        notificationController.Init(towerController, holeAreaController, gameScreen.NotificationPanel, gameConfigData.GameTextsConfig);
    }
}