using System;
using System.Collections;
using System.Collections.Generic;
using ScarFramework.Button;
using ScarFramework.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class TowerView : UIView, IInteractableElement
{
    [SerializeField] private RectTransform root;
    [SerializeField] private TowerCubeView cubeViewPrefab;
    private TowerCubesConfig _config;
    private Vector2 _cubeSize;
    private List<TowerCubeView> _views = new List<TowerCubeView>();

    public event Action<TowerCubeView> onDragElement;
    public event Action<CubeConfigData, Vector3> onPutElement;

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
        _views.Remove(view);
        view.onBeginDrag -= OnDragCube;
        Destroy(view.gameObject);
    }

    protected override void OnInit()
    {
        ClearViews();
        _config = GameServices.I.Config;
    }

    

    private void OnDragCube(PointerEventData eventData, UIDragable view)
    {
        onDragElement?.Invoke(view as TowerCubeView);
    }

    public bool TryPutElement(CubeConfigData elementConfigData, Vector3 elementPosition, DragSourceType dragSourceType)
    {
        onPutElement?.Invoke(elementConfigData, elementPosition);
      //  Debug.Log("Put Cube!");
        return true;
    }
}