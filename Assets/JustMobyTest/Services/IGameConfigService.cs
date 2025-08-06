using System.Threading;
using Cysharp.Threading.Tasks;
using JustMobyTest.Configs._Data;

namespace JustMobyTest.Services
{
    public interface IGameConfigService
    {
        public UniTask<GameConfigData> GetGameConfigData(CancellationToken token);
    }
}
