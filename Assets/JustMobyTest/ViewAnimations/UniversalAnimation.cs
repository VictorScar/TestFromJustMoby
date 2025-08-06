using DG.Tweening;
using JustMobyTest.ViewAnimations.AnimationParts;
using ScarFramework.UI;
using UnityEngine;

namespace JustMobyTest.ViewAnimations
{
    public class UniversalAnimation : CubeAnimation
    {
        [SerializeField] private CubeAnimationPart[] animationParts;
    
        protected override Tween PlayInternal(UIView view, Vector3 targetPoint)
        {
            var sequence = DOTween.Sequence();

            foreach (var part in animationParts)
            {
                part.RunAnimation(sequence, view, targetPoint);
            }

            return sequence.OnKill(OnAnimationEnded);
        }

        protected override void OnAnimationEnded()
        {
     
        }
    }
}
