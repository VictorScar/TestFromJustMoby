using System.Collections.Generic;
using JustMobyTest.Configs;
using JustMobyTest.Core;
using JustMobyTest.Data;
using JustMobyTest.UI;
using ScarFramework.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace JustMobyTest.Controllers
{
    public class ScrollbarConroller : MonoBehaviour
    {
        private TowerCubesConfig _cubesesConfig;
        private ScrollPanelView _scrollPanel;
        private DragController _dragController;

        [Inject] private GameServices _gameServices;

        private Dictionary<UIDragable, CubeConfig> _scrollCubes = new Dictionary<UIDragable, CubeConfig>();

        public void Init(DragController dragController, CubesConfigData cubesConfigData)
        {
            _dragController = dragController;
            _scrollPanel = _gameServices.UISystem.GetScreen<GameScreen>().ScrollViewPanel;
            ClearScrollViews();
            CreateScrollElements(cubesConfigData.CubeConfigs);
        }

        private void CreateScrollElements(CubeConfig[] cubeDatas)
        {
            if (cubeDatas != null)
            {
                foreach (var data in cubeDatas)
                {
                    CreateViewElement(data);
                }
            }
        }

        private void CreateViewElement(CubeConfig elementConfig)
        {
            var view = _scrollPanel.CreateView(elementConfig.Image);
            view.onBeginDrag += OnCubeStartDragging;
            _scrollCubes.Add(view, elementConfig);
        }

        private void OnCubeStartDragging(PointerEventData eventData, UIDragable view)
        {
            if (TryGetCubeData(view, out var data))
            {
                _dragController.StartDrag(data.CubeType, DragSourceType.FromScroll);
            }
        }

        private bool TryGetCubeData(UIDragable view, out CubeConfig cubeConfig)
        {
            if (_scrollCubes.TryGetValue(view, out var data))
            {
                cubeConfig = data;
                return true;
            }

            cubeConfig = CubeConfig.Invalid;
            return false;
        }

        private void ClearScrollViews()
        {
            if (_scrollCubes != null)
            {
                foreach (var cubeData in _scrollCubes)
                {
                    cubeData.Key.onBeginDrag -= OnCubeStartDragging;
                }
            
                _scrollCubes.Clear();
            }

            _scrollPanel.Clear();
        }
    }
}