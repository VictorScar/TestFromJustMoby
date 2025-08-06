using System;
using JustMobyTest.Data;
using UnityEngine;

namespace JustMobyTest._Model
{
    [Serializable]
    public class TowerCube
    {
        public TowerCubeType CubeType;
        public float XPos;
        public int Height;

        public static TowerCube Invalid =>
            new()
            {
                CubeType = TowerCubeType.None
            };

        public bool IsInvalid => CubeType == TowerCubeType.None;

        public CubeData CubeData => new CubeData
        {
            CubeType = CubeType,
            Position = new Vector2(XPos, Height)
        };

        public static bool operator ==(TowerCube a, TowerCube b)
        {
            return a.CubeType == b.CubeType;
        }

        public static bool operator !=(TowerCube a, TowerCube b)
        {
            return !(a == b);
        }
    }
}