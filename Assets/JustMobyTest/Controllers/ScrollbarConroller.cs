using System;
using System.Collections;
using System.Collections.Generic;
using ScarFramework.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollbarConroller : MonoBehaviour
{
    private TowerCubesConfig _cubesesConfig;
    private ScrollPanelView _scrollPanel;
    private DragController _dragController;

    private Dictionary<UIDragable, CubeData> _scrollCubes = new Dictionary<UIDragable, CubeData>();

    public void Init(DragController dragController, TowerCubesConfig cubesesConfig)
    {
        _dragController = dragController;
        _scrollPanel = GameServices.I.UISystem.GetScreen<GameScreen>().ScrollViewPanel;
        ClearScrollViews();
        CreateScrollElements(cubesesConfig.CubeDatas);
    }

    private void CreateScrollElements(CubeData[] cubeDatas)
    {
        if (cubeDatas != null)
        {
            foreach (var data in cubeDatas)
            {
                CreateViewElement(data);
            }
        }
    }

    private void CreateViewElement(CubeData elementData)
    {
        var view = _scrollPanel.CreateView(elementData.Image);
        view.onBeginDrag += OnCubeStartDragging;
        _scrollCubes.Add(view, elementData);
    }

    private void OnCubeStartDragging(PointerEventData eventData, UIDragable view)
    {
        if (TryGetCubeData(view, out var data))
        {
            _dragController.StartDrag(data.CubeType);
        }
    }

    private bool TryGetCubeData(UIDragable view, out CubeData cubeData)
    {
        if (_scrollCubes.TryGetValue(view, out var data))
        {
            cubeData = data;
            return true;
        }

        cubeData = CubeData.Invalid;
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