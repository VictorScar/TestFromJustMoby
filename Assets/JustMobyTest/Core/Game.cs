using System.Collections;
using System.Collections.Generic;
using ScarFramework.UI;
using UnityEngine;

public class Game : MonoBehaviour
{
    private TowerCubeConfig _cubesConfig;
    [SerializeField] private ScrollbarConroller _scrollbarConroller;
    [SerializeField] private DragController _dragController;
    public void StartGame(TowerCubeConfig cubeConfig)
    {
        _dragController.Init();
        _cubesConfig = cubeConfig;
        GameServices.I.UISystem.GetScreen<GameScreen>().Show();
        _scrollbarConroller.Init(_dragController, _cubesConfig);
    }
}