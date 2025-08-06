using ScarFramework.UI;
using TMPro;
using UnityEngine;

namespace JustMobyTest.UI
{
    public class LoadingScreen : UIScreen
    {
        [SerializeField] private TMP_Text screenHeaderLabel;

        public string ScreenHeader
        {
            set => screenHeaderLabel.text = value;
        }
    }
}
