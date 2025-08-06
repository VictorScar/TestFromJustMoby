using System;
using DG.Tweening;
using ScarFramework.UI;
using UnityEngine;

namespace JustMobyTest.ViewAnimations.AnimationParts
{
    [Serializable]
    public class MoveAnimationPart : CubeAnimationPart
    {
        [SerializeField] private Vector2 targetPosition;
        [SerializeField] private float speed;
 
        protected override Tween RunAnimationInternal(UIView view, Vector3 targetVector)
        {
            if (speed < 1f) speed = 1f;
            var duration = Vector3.Distance(view.Rect.anchoredPosition, targetVector) / speed;
            return view.Rect.DOAnchorPos(targetVector, duration).OnKill(OnAnimationComplete);
        }

        protected override void OnAnimationComplete()
        {
            if (_cashedView != null)
            {
                _cashedView.Rect.anchoredPosition = _cashedValue;
            }
        
            base.OnAnimationComplete();
        }
    }
}