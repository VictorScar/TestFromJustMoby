using DG.Tweening;
using ScarFramework.UI;
using UnityEngine;

namespace JustMobyTest.ViewAnimations.AnimationParts
{
    public class ScaleAnimationPart : CubeAnimationPart
    {
        [SerializeField] private float duration;
        [SerializeField] private Vector2 endScale;
        protected override Tween RunAnimationInternal(UIView view, Vector3 targetVector)
        {
            return view.Rect.DOScale(endScale, duration).OnKill(OnAnimationComplete);;
        }

        protected override void OnAnimationComplete()
        {
            if (_cashedView != null)
            {
                _cashedView.Rect.localScale = endScale;
            }
            base.OnAnimationComplete();
        }
    }
}
