using System;
using JustMobyTest.Data;
using UnityEngine;

namespace JustMobyTest.Configs
{
    [Serializable]
    public struct CubeConfig
    {
        public TowerCubeType CubeType;
        public Sprite Image;

        public CubeConfig(TowerCubeType cubeType, Sprite image = null)
        {
            CubeType = cubeType;
            Image = image;
        }

        public static CubeConfig Invalid => new CubeConfig(TowerCubeType.None);
        public bool IsInvalid => CubeType == TowerCubeType.None;
    }
}