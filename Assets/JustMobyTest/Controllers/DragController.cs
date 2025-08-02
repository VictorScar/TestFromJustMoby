using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragController : MonoBehaviour
{
    private DragViewPanel _viewPanel;
    private EventSystem _eventSystem;

    private CubeData _elementData;
    private bool _isDragging = false;
    private bool _isInitialized;
    private TowerCubesConfig _towerCubesConfig;

    private void Update()
    {
        if (!_isInitialized) return;
        
        if (_isDragging)
        {
            if (Input.GetMouseButton(0))
            {
                _viewPanel.Rect.position = Input.mousePosition;
            }
            else
            {
                EndDragElement();
            }
        }
       
    }

    public void Init(TowerCubesConfig towerCubesConfig, Vector2 cubeSize)
    {
        _towerCubesConfig = towerCubesConfig;
        _eventSystem = GameServices.I.UISystem.EventSystem;
        _viewPanel = GameServices.I.UISystem.GetScreen<GameScreen>().DragViewPanel;
        _viewPanel.Rect.sizeDelta = cubeSize;
        _isInitialized = true;
    }

    public void StartDrag(TowerCubeType cubeType)
    {
        if (_towerCubesConfig.TryGetData(cubeType, out var cubeData))
        {
            _elementData = cubeData;
            _viewPanel.Icon = cubeData.Image;
            _isDragging = true;
        }
    }

    public void EndDragElement()
    {
        _viewPanel.IsVisible = false;
        TryPutElement(_elementData, Input.mousePosition);
        _elementData = CubeData.Invalid;
        _isDragging = false;
    }


    private bool TryPutElement(CubeData elementData, Vector3 elementPosition)
    {
        var pointerData = new PointerEventData(_eventSystem)
        {
            position = Input.mousePosition
        };

        var results = new List<RaycastResult>();
        _eventSystem.RaycastAll(pointerData, results);

        foreach (var result in results)
        {
            if (result.gameObject.TryGetComponent<IInteractableElement>(out var interactable))
            {
                return interactable.TryPutElement(elementData, elementPosition);
            }
        }

        return false;
    }
}