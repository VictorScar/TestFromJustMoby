using System;
using System.Collections;
using System.Collections.Generic;
using ScarFramework.UI;
using UnityEngine;

public class HoleViewArea : UIView, IInteractableElement
{
    [SerializeField] private TowerCubeView cubeViewPrefab;
    [SerializeField] private RectTransform holeMask;
    [SerializeField] private HoleView hole;
    [SerializeField] private RectTransform root;

    private Vector2 _cubeSize;
    private List<TowerCubeView> _views = new List<TowerCubeView>();

    public event Action<CubeConfigData, Vector3, DragSourceType> onPutElement;

    public HoleView Hole => hole;

    public Vector2 CubeSize
    {
        set => _cubeSize = value;
    }

    public RectTransform Mask => holeMask;

    public TowerCubeView AddCubeView(Sprite image)
    {
        var cubeView = Instantiate(cubeViewPrefab, root);

        cubeView.Icon = image;
        cubeView.Size = _cubeSize;
        return cubeView;
    }

    public void RemoveCubeView(TowerCubeView view)
    {
        _views.Remove(view);
        Destroy(view.gameObject);
    }

    protected override void OnInit()
    {
        ClearViews();
    }

    private void ClearViews()
    {
        for (int i = 0; i < _views.Count; i++)
        {
            RemoveCubeView(_views[i]);
        }
    }

    public bool TryPutElement(CubeConfigData elementConfigData, Vector3 elementPosition, DragSourceType dragSourceType)
    {
        Debug.Log("Try put in Hole");
        onPutElement?.Invoke(elementConfigData, elementPosition, dragSourceType);
        return false;
    }
}