using System;
using UnityEngine;
using UnityEngine.Serialization;


[Serializable]
public struct TowerData
{
    [FormerlySerializedAs("CubesData")] public CubeDataInfo[] CubesInfo;
    public Vector2 CubeSize;
}
