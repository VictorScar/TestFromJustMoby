using UnityEngine;

namespace ScarFramework.UI
{
    public class UIScreen : UIView
    {
        [SerializeField] private bool showOnInit = false;
        [SerializeField] private Canvas canvas;
        
        public Canvas Canvas => canvas;
        
        protected override void OnInit()
        {
            gameObject.SetActive(showOnInit);
        }
        
        
    }
}
