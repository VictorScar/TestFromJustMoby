using ScarFramework.UI;
using UnityEngine;
using UnityEngine.UI;

namespace JustMobyTest.UI
{
    public class DragViewPanel : UIView
    {
        [SerializeField] private Image icon;

        public Sprite Icon
        {
            set
            {
                icon.sprite = value;
                IsVisible = value != null;
            }
        }

        public bool IsVisible
        {
            set => icon.gameObject.SetActive(value);
        }
    }
}