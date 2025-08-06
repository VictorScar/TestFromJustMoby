using System.Collections.Generic;
using JustMobyTest.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class DragController : MonoBehaviour
{
    [SerializeField] private DragSourceType _dragSourceType;

    private DragViewPanel _viewPanel;
    private EventSystem _eventSystem;
    private CubeConfig _elementConfig;
    private bool _isDragging = false;
    private bool _isInitialized;
    private CubesConfigData _cubesConfigData;

    [Inject] private GameServices _gameServices;
    
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

    public void Init(CubesConfigData cubesConfigData, Vector2 cubeSize)
    {
        _cubesConfigData = cubesConfigData;
        _eventSystem = _gameServices.UISystem.EventSystem;
        _viewPanel = _gameServices.UISystem.GetScreen<GameScreen>().DragViewPanel;
        _viewPanel.Rect.sizeDelta = cubeSize;
        _isInitialized = true;
    }

    public void StartDrag(TowerCubeType cubeType, DragSourceType dragSourceType)
    {
        if (_cubesConfigData.TryGetData(cubeType, out var cubeData))
        {
            _dragSourceType = dragSourceType;
            _elementConfig = cubeData;
            _viewPanel.Icon = cubeData.Image;
            _isDragging = true;
        }
    }

    public void EndDragElement()
    {
        _viewPanel.IsVisible = false;
        TryPutElement(_elementConfig, Input.mousePosition, _dragSourceType);
        _elementConfig = CubeConfig.Invalid;
        _isDragging = false;
        _dragSourceType = DragSourceType.None;
    }


    private bool TryPutElement(CubeConfig elementConfig, Vector3 elementPosition, DragSourceType dragSourceType)
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
                return interactable.TryPutElement(elementConfig, elementPosition, dragSourceType);
            }
        }

        return false;
    }
}