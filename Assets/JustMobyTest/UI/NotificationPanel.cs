using ScarFramework.UI;
using TMPro;
using UnityEngine;

namespace JustMobyTest.UI
{
    public class NotificationPanel : UIView
    {
        [SerializeField] private TMP_Text textField;

        public string Message
        {
            set => textField.text = value;
        }
    }
}