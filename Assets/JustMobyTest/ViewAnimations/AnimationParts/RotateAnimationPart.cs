using DG.Tweening;
using ScarFramework.UI;
using UnityEngine;

namespace JustMobyTest.ViewAnimations.AnimationParts
{
    public class RotateAnimationPart : CubeAnimationPart
    {
        [SerializeField] private float duration;
        [SerializeField] private Vector3 endRotation;
        protected override Tween RunAnimationInternal(UIView view, Vector3 targetVector)
        {
            return view.Rect.DOLocalRotate(endRotation, duration).OnKill(OnAnimationComplete);;
        }

        protected override void OnAnimationComplete()
        {
            if (_cashedView != null)
            {
                _cashedView.Rect.localRotation = Quaternion.Euler(endRotation);
            }
            base.OnAnimationComplete();
        }
    }
}
