using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameConfigs/CubeConfig", fileName = "CubesConfig")]
public class TowerCubesConfig : ScriptableObject
{
    [SerializeField] private CubeConfigData[] cubeDatas;
    [SerializeField] private Vector2 cubesSize;

    public CubeConfigData[] CubeDatas => cubeDatas;
    public Vector2 CubeSize => cubesSize;
    
    public bool TryGetData(TowerCubeType cubeType, out CubeConfigData cubeConfigData)
    {
        foreach (var data in cubeDatas)
        {
            if (data.CubeType == cubeType)
            {
                cubeConfigData = data;
                return true;
            }
        }

        cubeConfigData = new CubeConfigData();
        return false;
    }
}