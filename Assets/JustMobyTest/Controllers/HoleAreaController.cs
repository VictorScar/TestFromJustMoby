using System;
using JustMobyTest.Configs;
using JustMobyTest.Data;
using UnityEngine;

namespace JustMobyTest.Controllers
{
    public class HoleAreaController : MonoBehaviour
    {
        private HoleViewArea _holeArea;
        private HoleView _hole;
        private CubesConfigData _cubesConfigData;
        public event Action<bool> onUploadCube;
        public event Action onWrongDragSource;

        public void Init(CubesConfigData cubesConfigData, Vector2 cubeSize, HoleViewArea holeViewArea)
        {
            _cubesConfigData = cubesConfigData;
            _holeArea = holeViewArea;
            _holeArea.CubeSize = cubeSize;
            _hole = _holeArea.Hole;
            _holeArea.onPutElement += CreateFallingCube;
            _hole.onPutElement += UploadCube;
        }

        private void CreateFallingCube(CubeConfig config, Vector3 elementPos, DragSourceType dragSourceType)
        {
            if (dragSourceType == DragSourceType.FromTower)
            {
                if (_cubesConfigData.TryGetData(config.CubeType, out var cubeData))
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

        private void UploadCube(CubeConfig config, Vector3 elementPos,DragSourceType dragSourceType)
        {
            if (dragSourceType == DragSourceType.FromTower)
            {
                if (_cubesConfigData.TryGetData(config.CubeType, out var cubeData))
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
}