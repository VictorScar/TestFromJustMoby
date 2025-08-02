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
        _tower = new Tower();
        _towerView.onPutElement += TryAddCube;
        _towerView.onDragElement += OnDragCube;
        // _tower.onCubeAdded += AddCubePair;

        SetStartCubeData(towerData);
    }

    private void SetStartCubeData(TowerData towerData)
    {
        var cubesData = towerData.CubesData;

        if (cubesData != null)
        {
            // _tower.Data = towerData;

            for (var i = 0; i < cubesData.Length; i++)
            {
                var data = cubesData[i];
                if (_cubesConfig.TryGetData(data.CubeType, out var cubeConfigData))
                {
                    var view = _towerView.AddCubeView(cubeConfigData.Image);
                    view.Size = _cubesConfig.CubeSize;
                    _towerView.SetCubePosition(view, new Vector2(data.XPos, data.Height * _cubesConfig.CubeSize.y));
                    //view.SetPosition(new Vector2(data.XPos, data.Height*_cubesConfig.CubeSize.y));

                    if (_tower.TryAddCube(data.CubeType, new Vector2(data.XPos, data.Height), out var newCube))
                    {
                        AddCubePair(newCube, view);
                    }
                }
            }
        }
    }

    private void TryAddCube(CubeData cubeData, Vector3 pos)
    {
        var view = AddView(cubeData.CubeType);

        var relativePosition = _towerView.Rect.InverseTransformPoint(pos);

        if (view)
        {
            view.SetPosition(pos);

            if (_tower.TryAddCube(cubeData.CubeType, relativePosition, out var cube))
            {
                AddCubePair(cube, view);
                _towerView.SetCubePosition(view, new Vector2(cube.XPos, cube.Height * _cubesConfig.CubeSize.y));
                //view.SetPosition(pos, false);
            }
            else
            {
                view.Fall();
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