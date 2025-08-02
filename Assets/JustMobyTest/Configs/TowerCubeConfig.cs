using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameConfigs/CubeConfig", fileName = "CubesConfig")]
public class TowerCubeConfig : ScriptableObject
{
    [SerializeField] private CubeData[] cubeDatas;
    [SerializeField] private Vector2 cubesSize;

    public CubeData[] CubeDatas => cubeDatas;
    public Vector2 CubeSize => cubesSize;
    
    public bool GetData(TowerCubeType cubeType, out CubeData cubeData)
    {
        foreach (var data in cubeDatas)
        {
            if (data.CubeType == cubeType)
            {
                cubeData = data;
                return true;
            }
        }

        cubeData = new CubeData();
        return false;
    }
}