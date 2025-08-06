using UnityEngine;

namespace JustMobyTest.Configs
{
    public struct GameConfigData
    {
        public float MaxXCubeOffset;
        public float MinYCubeOffset;
        public float NotificationShowTime;
        public Vector2 CubeSize;

        public CubesConfigData CubesConfigData;
        public GameTextsConfig GameTextsConfig;
    }
}