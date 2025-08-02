using UnityEngine;

public class TowerController : MonoBehaviour
{
    private TowerView _towerView;
    private Tower _tower;

    public void Init(TowerView view, TowerData towerData)
    {
        _towerView = view;
        _tower = new Tower();
        _towerView.onPutElement += AddCube;
        _tower.onCubeAdded += AddView;

        _tower.Data = towerData;
    }

    private void AddView(TowerCubeType cubeType, Vector2 position)
    {
        var cubeView = _towerView.AddCubeView(cubeType, position);
    }

    private void AddCube(CubeData cubeData, Vector3 pos)
    {
        _tower.TryAddCube(cubeData.CubeType, pos);
    }

    private void RemoveCube()
    {
    }
}