using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace JustMobyTest.Core
{
    public class Boot : MonoBehaviour
    {
        [Inject] private GameServices _gameServices;
        [SerializeField] private string saveDataKey = "gameDataKey";


        void Start()
        {
            Initialize();
        }

        private async UniTask Initialize()
        {
            if (_gameServices)
            {
                await _gameServices.Init(saveDataKey);
                _gameServices.SceneManageService.LoadGameScene();
            }
            else
            {
                Debug.LogError("Game service prefab is not assigned!");
            }
        }
    }
}