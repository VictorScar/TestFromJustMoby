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
    [SerializeField]private TowerCubeView cubeViewPrefab;
    
    private TowerCubeConfig _config;

    protected override void OnInit()
    {
        _config = GameServices.I.Config;
    }

    public event Action<TowerCubeData, Vector3> onPutElement;
    
    public TowerCubeView AddCubeView(TowerCubeType cubeType)
    {
        var cube = Instantiate(cubeViewPrefab, root);
        
         if(_config.GetData(cubeType, out var cubeData))
         {
             cube.Data = cubeData;
         }
        return cube;
    }

    protected override void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Tower View Clicked!");
    }

    public bool TryPutElement(TowerCubeData elementData)
    {
        onPutElement?.Invoke(elementData, Input.mousePosition);
        Debug.Log("Create Cube!");
        return true;
    }
}