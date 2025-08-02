using System.Collections;
using System.Collections.Generic;
using ScarFramework.UI;
using UnityEngine;
using UnityEngine.UI;

public class TowerCubeView : UIDragable
{
    [SerializeField] private Image icon;
    
    private CubeData _data;
    
    

    public CubeData Data
    {
        set
        {
            _data = value;
            icon.sprite = value.Image;
        }
    }

    public Vector2 Size
    {
        set => Rect.sizeDelta = value;
    }
}
