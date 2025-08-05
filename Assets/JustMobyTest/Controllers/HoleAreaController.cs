using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleAreaController : MonoBehaviour
{
    private HoleViewArea _holeArea;
    private HoleView _hole;
    private TowerCubesConfig _cubesConfig;
    public event Action<bool> onUploadCube;
    public event Action onWrongDragSource;

    public void Init(TowerCubesConfig cubesConfig, HoleViewArea holeViewArea)
    {
        _cubesConfig = cubesConfig;
        _holeArea = holeViewArea;
        _holeArea.CubeSize = cubesConfig.CubeSize;
        _hole = _holeArea.Hole;
        _holeArea.onPutElement += CreateFallingCube;
        _hole.onPutElement += UploadCube;
    }

    private void CreateFallingCube(CubeConfigData configData, Vector3 elementPos, DragSourceType dragSourceType)
    {
        if (dragSourceType == DragSourceType.FromTower)
        {
            if (_cubesConfig.TryGetData(configData.CubeType, out var cubeData))
            {
                var view = _holeArea.AddCubeView(cubeData.Image);
                var relativePosition = _holeArea.Rect.InverseTransformPoint(elementPos);
                view.SetPosition(relativePosition);
                view.Fall();
                onUploadCube?.Invoke(false);
            }
        }
        else
        {
            onWrongDragSource?.Invoke();
        }
    }

    private void UploadCube(CubeConfigData configData, Vector3 elementPos,DragSourceType dragSourceType)
    {
        if (dragSourceType == DragSourceType.FromTower)
        {
            if (_cubesConfig.TryGetData(configData.CubeType, out var cubeData))
            {
                var view = _holeArea.AddCubeView(cubeData.Image);
                var relativePosition = _holeArea.Rect.InverseTransformPoint(elementPos);
                view.SetPosition(relativePosition);
                view.Rect.SetParent(_holeArea.Mask);
                view.Upload(_hole.UploadPoint);
                onUploadCube?.Invoke(true);
            }
        }
        else
        {
            onWrongDragSource?.Invoke();
        }
    }
}