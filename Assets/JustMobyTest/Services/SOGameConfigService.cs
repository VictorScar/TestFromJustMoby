using System.Threading;
using Cysharp.Threading.Tasks;
using JustMobyTest.Configs._Data;
using UnityEngine;

namespace JustMobyTest.Services
{
    public class SOGameConfigService : MonoBehaviour, IGameConfigService
    {
        [SerializeField] private GameConfig gameConfig;

        public UniTask<GameConfigData> GetGameConfigData(CancellationToken token)
        {
            var configData = new GameConfigData()
            {
                CubesConfigData = GetCubesConfigData(),
                GameTextsConfig = gameConfig.GameTextsConfig,
                MaxXCubeOffset = gameConfig.MaxCubeXOffset,
                MinYCubeOffset = gameConfig.MaxCubeYOffset,
                CubeSize = gameConfig.CubeSize
            
            };

            return UniTask.FromResult(configData);
        }

        private CubesConfigData GetCubesConfigData()
        {
            var cubesConfig = gameConfig.CubesConfig;

            return new CubesConfigData
            {
                CubeConfigs = cubesConfig.CubeDatas
            };
        }
    }
}