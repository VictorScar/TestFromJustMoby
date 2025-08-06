using Cysharp.Threading.Tasks;
using JustMobyTest.Services;
using ScarFramework.UI;
using UnityEngine;

namespace JustMobyTest.Core
{
    public class GameServices : MonoBehaviour
    {
        [SerializeField] private UISystem uiSystem;
        [SerializeField] private SOGameConfigService gameConfigService;
        [SerializeField] private SceneManageService sceneManageService;
        private IProgressDataService _progressDataService;

        public UISystem UISystem => uiSystem;
        public SceneManageService SceneManageService => sceneManageService;
        public IGameConfigService GameConfigService => gameConfigService;

        public IProgressDataService ProgressDataService => _progressDataService;

        public UniTask Init(string saveDataKey)
        {
            uiSystem.Init();
            _progressDataService = new LocalProgressDataService(saveDataKey);
        
            return UniTask.CompletedTask;
        }
    }
}