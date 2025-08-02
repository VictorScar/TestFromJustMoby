using System.Collections;
using System.Collections.Generic;
using ScarFramework.UI;
using UnityEngine;

public class GameServices : MonoBehaviour
{
    [SerializeField] private UISystem uiSystem;
    
    private TowerCubesConfig _cubesConfig;

    public static GameServices I { get; private set; }
    public UISystem UISystem => uiSystem;
    public TowerCubesConfig Config => _cubesConfig;

    public void Init(TowerCubesConfig cubesesConfig)
    {
        if (!I)
        {
            I = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _cubesConfig = cubesesConfig;
        uiSystem.Init();
    }
}