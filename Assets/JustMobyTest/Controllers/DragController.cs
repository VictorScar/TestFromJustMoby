using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragController : MonoBehaviour
{
    private DragViewPanel _viewPanel;
    private EventSystem _eventSystem;

    private TowerCubeData _elementData;
    private bool _isDragging = false;
    private bool _isInitialized;

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

    public void Init()
    {
        _eventSystem = GameServices.I.UISystem.EventSystem;
        _viewPanel = GameServices.I.UISystem.GetScreen<GameScreen>().DragViewPanel;
        _isInitialized = true;
    }

    public void StartDrag(TowerCubeData cubeData)
    {
        _elementData = cubeData;
        _viewPanel.Icon = cubeData.Image;
        _isDragging = true;
    }

    public void EndDragElement()
    {
        _viewPanel.IsVisible = false;
        TryPutElement(_elementData);
        _elementData = TowerCubeData.Invalid;
        _isDragging = false;
    }


    private bool TryPutElement(TowerCubeData elementData)
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
                return interactable.TryPutElement(elementData);
            }
        }

        return false;
    }
}