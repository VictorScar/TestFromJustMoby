using System.Collections;
using System.Collections.Generic;
using ScarFramework.Button;
using ScarFramework.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class TowerView : UIClickableView
{
    [SerializeField] private RectTransform root;
    [SerializeField]private TowerCubeView cubeViewPrefab;
    private TowerCubeConfig _config;
    
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
}