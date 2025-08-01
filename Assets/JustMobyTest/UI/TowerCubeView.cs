using System.Collections;
using System.Collections.Generic;
using ScarFramework.UI;
using UnityEngine;
using UnityEngine.UI;

public class TowerCubeView : UIView
{
    [SerializeField] private Image icon;
    
    private TowerCubeData _data;
    
    

    public TowerCubeData Data
    {
        set
        {
            _data = value;
            icon.sprite = value.Image;
        }
    }
}
