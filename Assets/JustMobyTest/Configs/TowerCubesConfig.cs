using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameConfigs/CubeConfig", fileName = "CubesConfig")]
public class TowerCubesConfig : ScriptableObject
{
    [SerializeField] private CubeConfig[] cubeDatas;
    [SerializeField] private Vector2 cubesSize;

    public CubeConfig[] CubeDatas => cubeDatas;
    public Vector2 CubeSize => cubesSize;
    
    public bool TryGetData(TowerCubeType cubeType, out CubeConfig cubeConfig)
    {
        foreach (var data in cubeDatas)
        {
            if (data.CubeType == cubeType)
            {
                cubeConfig = data;
                return true;
            }
        }

        cubeConfig = new CubeConfig();
        return false;
    }
}