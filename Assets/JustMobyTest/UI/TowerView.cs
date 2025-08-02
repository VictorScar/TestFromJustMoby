using System;
using System.Collections;
using System.Collections.Generic;
using ScarFramework.Button;
using ScarFramework.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class TowerView : UIClickableView, IInteractableElement
{
    [SerializeField] private RectTransform root;
    [SerializeField] private TowerCubeView cubeViewPrefab;
    private float startOffset = 30f;
    private TowerCubeConfig _config;
    private Vector2 _cubeSize;

    public Vector2 CubeSize
    {
        set => _cubeSize = value;
    }

    protected override void OnInit()
    {
        _config = GameServices.I.Config;
    }

    public event Action<CubeData, Vector3> onPutElement;

    public TowerCubeView AddCubeView(TowerCubeType cubeType, Vector2 position)
    {
        var cube = Instantiate(cubeViewPrefab, root);

        if (_config.GetData(cubeType, out var cubeData))
        {
            cube.Data = cubeData;
            cube.Size = _cubeSize;
        }

        var localPosition = cube.Rect.InverseTransformPoint(position);
        cube.Rect.anchoredPosition = new Vector2(localPosition.x, _cubeSize.y * position.y + startOffset);
        return cube;
    }

    protected override void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Tower View Clicked!");
    }

    public bool TryPutElement(CubeData elementData, Vector3 elementPosition)
    {
        //var relativePosition = elementPosition - root.position;
        onPutElement?.Invoke(elementData, elementPosition);
        Debug.Log("Put Cube!");
        return true;
    }
}