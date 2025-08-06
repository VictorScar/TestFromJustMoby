using System.Collections;
using System.Collections.Generic;
using JustMobyTest.Services;
using ScarFramework.UI;
using UnityEngine;

public class GameServices : MonoBehaviour
{
    [SerializeField] private UISystem uiSystem;
    private IProgressDataService _progressDataService;
    private IGameConfigService _gameConfigService;

    public static GameServices I { get; private set; }
    public UISystem UISystem => uiSystem;
    public IGameConfigService GameConfigService => _gameConfigService;
   
    public IProgressDataService ProgressDataService => _progressDataService;

    public void Init()
    {
        if (!I)
        {
            I = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        uiSystem.Init();
        _progressDataService = new LocalProgressDataService("");
    }
}