using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "GameConfigs/GameConfig", fileName = "GameConfig")]
public class GameConfig : ScriptableObject
{
    [SerializeField] private TowerCubesConfig cubesConfig;
    [SerializeField] private GameTextsConfig gameTextsConfig;
    [SerializeField] private Vector2 cubeSize = new Vector2(50f, 50f);
    [SerializeField] private float maxCubeXOffset = 0.5f;
    [SerializeField] private float maxCubeYOffset = 0.5f;
    [SerializeField] private float notificationShowTime = 2f;

    public TowerCubesConfig CubesConfig => cubesConfig;
    public GameTextsConfig GameTextsConfig => gameTextsConfig;
    public Vector2 CubeSize => cubeSize;
    public float MaxCubeXOffset => maxCubeXOffset;
    public float MaxCubeYOffset => maxCubeYOffset;
    public float NotificationShowTime => notificationShowTime;
}