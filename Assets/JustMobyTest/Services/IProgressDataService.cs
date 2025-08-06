using Cysharp.Threading.Tasks;
using JustMobyTest._Model;
using JustMobyTest.Data;

public interface IProgressDataService
{
    public UniTask<TowerData> LoadData();
    public CubeData[] Data { set; }
    public void ResetSaves();
}
