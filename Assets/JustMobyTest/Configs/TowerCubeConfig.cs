using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameConfigs/CubeConfig", fileName = "CubesConfig")]
public class TowerCubeConfig : ScriptableObject
{
    [SerializeField] private TowerCubeData[] cubeDatas;

    public TowerCubeData[] CubeDatas => cubeDatas;

    public bool GetData(TowerCubeType cubeType, out TowerCubeData cubeData)
    {
        foreach (var data in cubeDatas)
        {
            if (data.CubeType == cubeType)
            {
                cubeData = data;
                return true;
            }
        }

        cubeData = new TowerCubeData();
        return false;
    }
}