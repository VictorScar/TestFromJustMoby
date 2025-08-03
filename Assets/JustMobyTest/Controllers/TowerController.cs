using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] private Tower _tower;
    private TowerView _towerView;
    private DragController _dragController;
    private List<TowerCubePair> _cubePairs = new List<TowerCubePair>();
    private TowerCubesConfig _cubesConfig;

    public void Init(TowerCubesConfig cubesConfig, TowerView view, TowerData towerData, DragController dragController)
    {
        _cubesConfig = cubesConfig;
        _towerView = view;
        _dragController = dragController;
        _tower = new Tower(_cubesConfig.CubeSize);
        _towerView.onPutElement += TryAddCube;
        _towerView.onDragElement += OnDragCube;

        SetStartCubeData(towerData);
    }

    private void SetStartCubeData(TowerData towerData)
    {
        var cubesInfo = towerData.CubesInfo;

        if (cubesInfo != null)
        {
            for (var i = 0; i < cubesInfo.Length; i++)
            {
                var info = cubesInfo[i];
                var data = new CubeData
                {
                    CubeType = info.CubeType,
                    Position = new Vector2(info.Position.x, i * _cubesConfig.CubeSize.y)
                };

                if (_cubesConfig.TryGetData(data.CubeType, out var cubeConfigData))
                {
                    var view = _towerView.AddCubeView(cubeConfigData.Image);
                    view.Size = _cubesConfig.CubeSize;
                   

                    if (_tower.TryAddCube(data, out var newCube, out var failtureReason))
                    {
                        AddCubePair(newCube, view);
                        view.SetPosition(new Vector2(newCube.XPos, newCube.Height * _cubesConfig.CubeSize.y));
                    }
                }
            }
        }
    }

    private void TryAddCube(CubeConfigData cubeConfigData, Vector3 pos)
    {
        var view = AddView(cubeConfigData.CubeType);
        var relativePosition = _towerView.Rect.InverseTransformPoint(pos);
        var cubeData = new CubeData
        {
            CubeType = cubeConfigData.CubeType,
            Position = relativePosition
        };

        if (view)
        {
            view.SetPosition(pos);

            if (_tower.TryAddCube(cubeData, out var cube, out var reason))
            {
                AddCubePair(cube, view);
                _towerView.SetCubePosition(view, new Vector2(cube.XPos, cube.Height * _cubesConfig.CubeSize.y));
                //view.SetPosition(pos, false);
            }
            else
            {
                view.Fall(reason);
            }
        }
    }

    private void RemoveCube(TowerCubePair cubePair)
    {
        var removeCubeHeight = cubePair.Model.Height;
        _tower.RemoveCube(cubePair.Model);
        _towerView.RemoveCubeView(cubePair.View);
        _cubePairs.Remove(cubePair);

        UpdateViewsPositions(removeCubeHeight);
    }

    private void UpdateViewsPositions(int removeCubeHeight)
    {
        for (int i = removeCubeHeight; i < _cubePairs.Count; i++)
        {
            var view = _cubePairs[i].View;
            view.SetPosition(new Vector2(view.Rect.anchoredPosition.x,
                view.Rect.anchoredPosition.y - _cubesConfig.CubeSize.y));
        }
    }

    private TowerCubeView AddView(TowerCubeType cubeType)
    {
        if (_cubesConfig.TryGetData(cubeType, out var cubeConfig))
        {
            var view = _towerView.AddCubeView(cubeConfig.Image);
            view.Size = _cubesConfig.CubeSize;

            return view;
        }

        return null;
    }

    private void OnDragCube(TowerCubeView cubeView)
    {
        if (TryGetCubePair(cubeView, out var cubePair))
        {
            _dragController.StartDrag(cubePair.Model.CubeType);

            RemoveCube(cubePair);
        }
    }

    private bool TryGetCubePair(TowerCubeView cubeView, out TowerCubePair cubePair)
    {
        foreach (var pair in _cubePairs)
        {
            if (pair.View == cubeView)
            {
                cubePair = pair;
                return true;
            }
        }

        cubePair = null;
        return false;
    }

    private bool TryGetCubePair(TowerCube cubeData, out TowerCubePair cubePair)
    {
        foreach (var pair in _cubePairs)
        {
            if (pair.Model == cubeData)
            {
                cubePair = pair;
                return true;
            }
        }

        cubePair = null;
        return false;
    }


    private void AddCubePair(TowerCube cube, TowerCubeView view)
    {
        var cubePair = new TowerCubePair(cube, view);
        _cubePairs.Add(cubePair);
    }

    private void ClearCubes()
    {
    }
}