using System;
using ScarFramework.UI.ViewAnimators;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ScarFramework.UI
{
    public class UIClickableView : UIView, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler
    {
        [SerializeField] private UIAnimator onClickDownAnimator;
        [SerializeField] private UIAnimator onClickUpAnimator;
        public event Action<UIClickableView, PointerEventData> onClick;
        
        public void PointerClick(PointerEventData eventData)
        {
            onClick?.Invoke(this, eventData);
            OnPointerClick(eventData);
        }

        public void PointerUp(PointerEventData eventData)
        {
            onClickUpAnimator?.PlayAnimation(this);
            Debug.Log("PointerUp");
        }

        public void PointerDown(PointerEventData eventData)
        {
            onClickDownAnimator?.PlayAnimation(this);
            Debug.Log("PointerDown");
        }

        protected virtual void OnPointerClick(PointerEventData eventData)
        {
            
        }
        
        protected virtual void OnPointerUp(PointerEventData eventData)
        {
            
        }
        
        protected virtual void OnPointerDown(PointerEventData eventData)
        {
            
        }
    }
}
