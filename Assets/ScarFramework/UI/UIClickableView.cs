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
        
        public virtual void PointerClick(PointerEventData eventData)
        {
            onClick?.Invoke(this, eventData);
            OnPointerClick(eventData);
        }

        public virtual void PointerUp(PointerEventData eventData)
        {
            onClickUpAnimator?.PlayAnimation(this);
            Debug.Log("PointerUp");
        }

        public virtual void PointerDown(PointerEventData eventData)
        {
            onClickDownAnimator?.PlayAnimation(this);
            Debug.Log("PointerDown");
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            PointerClick(eventData);
        }
        
        public  void OnPointerUp(PointerEventData eventData)
        {
            PointerUp(eventData);
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            PointerDown(eventData);
        }
    }
}
