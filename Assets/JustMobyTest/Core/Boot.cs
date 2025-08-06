using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

public class Boot : MonoBehaviour
{
    [SerializeField] private GameServices gameServices;
    [SerializeField] private Game game;

    private CancellationTokenSource _gameCancellationToken;
   
    void Start()
    {
        InitGameServices();
       
    }

    private async UniTask InitGameServices()
    {
        gameServices.Init();
        DontDestroyOnLoad(gameServices.gameObject);

        var gameConfigData = await gameServices.GameConfigService.GetGameConfigData(_gameCancellationToken.Token);
        game.StartGame(gameConfigData);
    }

}
