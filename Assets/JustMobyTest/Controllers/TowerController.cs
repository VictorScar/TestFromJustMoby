using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] private Tower _tower;
    [SerializeField] private float minVerticalOffset = 0.5f;
    private ICubeValidator[] _validators;

    private TowerView _towerView;
    private DragController _dragController;
    private List<TowerCubePair> _cubePairs = new List<TowerCubePair>();
    private TowerCubesConfig _cubesConfig;
    public event Action onCubeAdded;
    public event Action onCubeRemoved;
    public event Action<FailureReason> onCubeFall;

    public void Init(TowerCubesConfig cubesConfig, TowerView view, TowerData towerData, DragController dragController)
    {
        _cubesConfig = cubesConfig;
        _towerView = view;
        _dragController = dragController;

        _validators = new ICubeValidator[]
        {
            new TowerHeightValidator(_cubesConfig.CubeSize.y, _towerView.Rect.rect.size.y),
            new XPositionValidator(_cubesConfig.CubeSize.x),
            new HeightCubeValidator(minVerticalOffset)
        };

        _tower = new Tower(_cubesConfig.CubeSize, _validators);

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
                    Position = new Vector2(info.Position.x, i)
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

    private void TryAddCube(CubeConfigData cubeConfigData, Vector3 pos, DragSourceType dragSourceType)
    {
        var view = AddView(cubeConfigData.CubeType);
        var relativePosition = _towerView.Rect.InverseTransformPoint(pos);
        var cubeData = new CubeData
        {
            CubeType = cubeConfigData.CubeType,
            Position = new Vector2(relativePosition.x, relativePosition.y / _cubesConfig.CubeSize.y)
        };

        if (view)
        {
            view.SetPosition(relativePosition);

            if (_tower.TryAddCube(cubeData, out var cube, out var reason))
            {
                AddCubePair(cube, view);
                onCubeAdded?.Invoke();
                view.SetPosition(new Vector2(cube.XPos, cube.Height * _cubesConfig.CubeSize.y), false);
            }
            else
            {
                view.Fall();
                onCubeFall?.Invoke(reason);
                Debug.Log("Failure! Reason is" + reason.ToString());
            }
        }
    }

    private void RemoveCube(TowerCubePair cubePair)
    {
        var removeCubeHeight = cubePair.Model.Height;
        _tower.RemoveCube(cubePair.Model);
        _towerView.RemoveCubeView(cubePair.View);
        _cubePairs.Remove(cubePair);

        onCubeRemoved?.Invoke();
        
        UpdateCubesStates(removeCubeHeight);
        UpdateViewsPositions(removeCubeHeight);
    }

    private void UpdateCubesStates(int removeCubeHeight)
    {
        if (removeCubeHeight > 0 && removeCubeHeight < _cubePairs.Count)
        {
            var cube = _cubePairs[removeCubeHeight].Model;
            var validatedCubeData = new CubeData
            {
                CubeType = cube.CubeType,
                Position = new Vector2(cube.XPos,
                    cube.Height)
            };


            if (!_tower.ValidateAddition(validatedCubeData, out var reason, isNewCube:false))
            {
                RemoveCube(_cubePairs[removeCubeHeight]);
                var view = AddView(validatedCubeData.CubeType);
                view.SetPosition(new Vector2(validatedCubeData.Position.x,
                    validatedCubeData.Position.y * _cubesConfig.CubeSize.y));
                view.Fall();
                onCubeFall?.Invoke(reason);
            }
        }
    }

    private void UpdateViewsPositions(int removeCubeHeight)
    {
        for (int i = removeCubeHeight; i < _cubePairs.Count; i++)
        {
            var view = _cubePairs[i].View;
            view.SetPosition(new Vector2(view.Rect.anchoredPosition.x,
                _cubePairs[i].Model.Height * _cubesConfig.CubeSize.y), false);
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
            _dragController.StartDrag(cubePair.Model.CubeType, DragSourceType.FromTower);

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
        _cubePairs.Clear();
        _towerView?.ClearViews();
    }

    private void OnDestroy()
    {
        if (_towerView)
        {
            _towerView.onDragElement -= OnDragCube;
            _towerView.onPutElement -= TryAddCube;
        }

        ClearCubes();
    }
}