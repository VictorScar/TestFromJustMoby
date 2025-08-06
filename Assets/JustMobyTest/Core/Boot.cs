using System.Threading;
using Cysharp.Threading.Tasks;
using JustMobyTest.UI;
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


                var loadingTextID = new GameTextID
                {
                    CategoryID = TextCategoryID.General,
                    TextID = TextID.Loading
                };

                var gameData = await _gameServices.GameConfigService
                    .GetGameConfigData(CancellationToken.None);

                if (gameData.GameTextsConfig.TryGetTextByID(loadingTextID, out var loadingText))
                {
                    var loadingScreen = _gameServices.UISystem.GetScreen<LoadingScreen>();
                    loadingScreen.ScreenHeader = loadingText;
                    loadingScreen.Show(true);
                }

                _gameServices.SceneManageService.LoadGameScene();
            }
            else
            {
                Debug.LogError("Game service prefab is not assigned!");
            }
        }
    }
}