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
    private float startOffset = 30f;
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

        // var localPosition = cubeView.Rect.InverseTransformPoint(new Vector3(xPos, height));
        // cubeView.Rect.anchoredPosition = new Vector2(localPosition.x, _cubeSize.y * height + startOffset);
        cubeView.onBeginDrag += OnDragCube;
        return cubeView;
    }

    public void RemoveCubeView(TowerCubeView view)
    {
        _views.Remove(view);
        view.onBeginDrag -= OnDragCube;
        Destroy(view.gameObject);
    }

    public void SetCubePosition(TowerCubeView cubeView, Vector2 newPos)
    {
        var localPosition = rect.InverseTransformPoint(newPos);
        cubeView.SetPosition(newPos);
    }

    protected override void OnInit()
    {
        ClearViews();
        _config = GameServices.I.Config;
    }

    private void ClearViews()
    {
        for (int i = 0; i < _views.Count; i++)
        {
            RemoveCubeView(_views[i]);
        }
    }

    private void OnDragCube(PointerEventData eventData, UIDragable view)
    {
        onDragElement?.Invoke(view as TowerCubeView);
    }

    public bool TryPutElement(CubeConfigData elementConfigData, Vector3 elementPosition)
    {
        //var relativePosition = elementPosition - root.position;
        onPutElement?.Invoke(elementConfigData, elementPosition);
        Debug.Log("Put Cube!");
        return true;
    }
}