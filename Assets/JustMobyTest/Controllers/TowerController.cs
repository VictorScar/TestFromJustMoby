using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using JustMobyTest._Model;
using JustMobyTest._Model.Validators;
using JustMobyTest.Configs;
using JustMobyTest.Data;
using UnityEngine;

namespace JustMobyTest.Controllers
{
    public class TowerController : MonoBehaviour
    {
        [SerializeField] private Tower _tower;
        private ICubeValidator[] _validators;

        private Vector2 _cubeSize;
        private TowerView _towerView;
        private DragController _dragController;
        private List<TowerCubePair> _cubePairs = new List<TowerCubePair>();
        private CubesConfigData _cubesConfigData;
        private IProgressDataService _localProgressDataService;
        public event Action onCubeAdded;
        public event Action onCubeRemoved;
        public event Action<FailureReason> onCubeFall;

        private void OnDestroy()
        {
            if (_towerView)
            {
                _towerView.onDragElement -= OnDragCube;
                _towerView.onPutElement -= TryAddCube;
                _tower.onDataUpdated -= OnTowerDataUpdated;
            }

            ClearCubes();
        }

        public async UniTask Init(GameConfigData gameConfigData, TowerView view,
            IProgressDataService localProgressDataService, DragController dragController)
        {
            _cubesConfigData = gameConfigData.CubesConfigData;
            _cubeSize = gameConfigData.CubeSize;
            _towerView = view;
            _dragController = dragController;
            _localProgressDataService = localProgressDataService;

            var towerData = await _localProgressDataService.LoadData();

            _validators = new ICubeValidator[]
            {
                new TowerHeightValidator(gameConfigData.CubeSize.y, _towerView.Rect.rect.size.y),
                new XPositionValidator(gameConfigData.CubeSize.x),
                new HeightCubeValidator(gameConfigData.MinYCubeOffset)
            };

            _tower = new Tower(_validators);

            _towerView.onPutElement += TryAddCube;
            _towerView.onDragElement += OnDragCube;
            _tower.onDataUpdated += OnTowerDataUpdated;

            SetStartCubeData(towerData);
        }


        private void OnTowerDataUpdated(List<TowerCube> towerCubes)
        {
            if (towerCubes != null)
            {
                var cubesData = new List<CubeData>();

                foreach (var cube in towerCubes)
                {
                    cubesData.Add(cube.CubeData);
                }

                _localProgressDataService.Data = cubesData.ToArray();
            }
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
                        Position = new Vector2(info.XPosition, i)
                    };

                    if (_cubesConfigData.TryGetData(data.CubeType, out var cubeConfigData))
                    {
                        var view = AddView(cubeConfigData.CubeType);
                        /*var view = _towerView.AddCubeView(cubeConfigData.Image);
                    view.Size = _cubeSize;*/


                        if (_tower.TryAddCube(data, out var newCube, out var failtureReason))
                        {
                            AddCubePair(newCube, view);
                            view.SetPosition(new Vector2(newCube.XPos, newCube.Height * _cubeSize.y));
                        }
                    }
                }
            }
        }

        private void TryAddCube(CubeConfig cubeConfig, Vector3 pos, DragSourceType dragSourceType)
        {
            var view = AddView(cubeConfig.CubeType);
            var relativePosition = _towerView.Rect.InverseTransformPoint(pos);
            var cubeData = new CubeData
            {
                CubeType = cubeConfig.CubeType,
                Position = new Vector2(relativePosition.x, relativePosition.y / _cubeSize.y)
            };

            if (view)
            {
                view.SetPosition(relativePosition);

                if (_tower.TryAddCube(cubeData, out var cube, out var reason))
                {
                    AddCubePair(cube, view);
                    onCubeAdded?.Invoke();
                    view.SetPosition(new Vector2(cube.XPos, cube.Height * _cubeSize.y), false);
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


                if (!_tower.ValidateAddition(validatedCubeData, out var reason, isNewCube: false))
                {
                    RemoveCube(_cubePairs[removeCubeHeight]);
                    var view = AddView(validatedCubeData.CubeType);
                    view.SetPosition(new Vector2(validatedCubeData.Position.x,
                        validatedCubeData.Position.y * _cubeSize.y));
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
                    _cubePairs[i].Model.Height * _cubeSize.y), false);
            }
        }

        private TowerCubeView AddView(TowerCubeType cubeType)
        {
            if (_cubesConfigData.TryGetData(cubeType, out var cubeConfig))
            {
                var view = _towerView.AddCubeView(cubeConfig.Image);
                view.Size = _cubeSize;

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
    }
}