using Cysharp.Threading.Tasks;

public interface IProgressDataService
{
    public UniTask<TowerData> LoadData();
    public CubeData[] Data { set; }
    public void ResetSaves();
}
