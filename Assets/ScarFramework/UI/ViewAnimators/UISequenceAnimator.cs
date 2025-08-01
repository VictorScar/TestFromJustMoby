using DG.Tweening;
using UnityEngine;

namespace ScarFramework.UI.ViewAnimators
{
    [CreateAssetMenu(menuName = "UI/Animators/Sequence", fileName = "SequenceUI")]
    public class UISequenceAnimator : UIAnimator
    {
        [SerializeField] private UIAnimator[] animators;

        protected override void OnInit(UIView view)
        {
            if (animators != null)
            {
                foreach (var animator in animators)
                {
                    animator.Init(view);
                }
            }
        }
       

        protected override Tween AnimateInternal(UIView view)
        {
            if (animators != null)
            {
                var animationInternal = DOTween.Sequence();

                foreach (var animator in animators)
                {
                    animationInternal.Append(animator.PlayAnimation(view));
                }

                animationInternal.Play();
              
                return animationInternal;
            }

            return null;
        }

        protected override void OnStartAnimation(UIView view)
        {
        
        }

        protected override void OnEndAnimation()
        {
           
        }
    }
}
