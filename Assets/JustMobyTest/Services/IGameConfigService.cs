using System.Threading;
using Cysharp.Threading.Tasks;
using JustMobyTest.Configs;

namespace JustMobyTest.Services
{
    public interface IGameConfigService
    {
        public UniTask<GameConfigData> GetGameConfigData(CancellationToken token);
    }
}
