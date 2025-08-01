using System.Collections;
using System.Collections.Generic;
using ScarFramework.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerController : MonoBehaviour
{
    private TowerView _view;
    private Tower _tower;

    public void Init(TowerView view)
    {
        _tower = new Tower();
        _view = view;
        _view.onPutElement += AddCube;
        _tower.onCubeAdded += AddView;
    }

    private void AddView(TowerCubeType cubeType, int heightLevel)
    {
        var cubeView = _view.AddCubeView(cubeType);
        cubeView.Rect.position = Input.mousePosition;
    }

    private void AddCube(TowerCubeData cubeData, Vector3 pos)
    {
        _tower.AddCube(cubeData.CubeType, pos);
    }
}
