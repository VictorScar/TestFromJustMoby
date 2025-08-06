using System;
using System.Collections;
using System.Collections.Generic;
using JustMobyTest.Configs;
using JustMobyTest.Data;
using JustMobyTest.UI;
using ScarFramework.Button;
using ScarFramework.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class TowerView : UIView, IInteractableElement
{
    [SerializeField] private RectTransform root;
    [SerializeField] private TowerCubeView cubeViewPrefab;
    private Vector2 _cubeSize;
    private List<TowerCubeView> _views = new List<TowerCubeView>();

    public event Action<TowerCubeView> onDragElement;
    public event Action<CubeConfig, Vector3, DragSourceType> onPutElement;

    public Vector2 CubeSize
    {
        set => _cubeSize = value;
    }

    public TowerCubeView AddCubeView(Sprite image)
    {
        var cubeView = Instantiate(cubeViewPrefab, root);

        cubeView.Icon = image;
        cubeView.Size = _cubeSize;
        cubeView.onBeginDrag += OnDragCube;
        _views.Add(cubeView);
        return cubeView;
    }

    public void ClearViews()
    {
        for (int i = 0; i < _views.Count; i++)
        {
            RemoveCubeView(_views[i]);
        }
    }

    public void RemoveCubeView(TowerCubeView view)
    {
        if (view)
        {
            _views.Remove(view);
            view.onBeginDrag -= OnDragCube;
            Destroy(view.gameObject);
        }
   
    }

    protected override void OnInit()
    {
        ClearViews();
        }


    private void OnDragCube(PointerEventData eventData, UIDragable view)
    {
        onDragElement?.Invoke(view as TowerCubeView);
    }

    public bool TryPutElement(CubeConfig elementConfig, Vector3 elementPosition, DragSourceType dragSourceType)
    {
        onPutElement?.Invoke(elementConfig, elementPosition, dragSourceType);
        return true;
    }
}