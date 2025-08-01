using System.Collections;
using System.Collections.Generic;
using ScarFramework.UI;
using UnityEngine;

public class GameServices : MonoBehaviour
{
    [SerializeField] private UISystem uiSystem;
    
    private TowerCubeConfig _cubeConfig;

    public static GameServices I { get; private set; }
    public UISystem UISystem => uiSystem;
    public TowerCubeConfig Config => _cubeConfig;

    public void Init(TowerCubeConfig cubesConfig)
    {
        if (!I)
        {
            I = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _cubeConfig = cubesConfig;
        uiSystem.Init();
    }
}