using DG.Tweening;
using UnityEngine;

namespace ScarFramework.UI.ViewAnimators
{
    [CreateAssetMenu(menuName = "UI/Animators/Rotate", fileName = "RotateUI")]
    public class RotateUI : UIAnimator
    {
        [SerializeField] private Vector3 startValue;
        [SerializeField] private Vector3 endValue;
        
        protected override Tween AnimateInternal(UIView view)
        {
            var animationInternal = DOTween.Sequence();
            animationInternal
                .Append(view.Rect.DORotate(startValue, duration))
                .Append(view.Rect.DORotate(endValue, duration))
                .SetLoops(-1);

            return animationInternal;
        }

  

        protected override void OnStartAnimation(UIView view)
        {
        }

        protected override void OnEndAnimation()
        {
        }


    }
}