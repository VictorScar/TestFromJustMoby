using DG.Tweening;
using ScarFramework.UI;
using UnityEngine;

namespace JustMobyTest.ViewAnimations.AnimationParts
{
    public abstract class CubeAnimationPart : MonoBehaviour
    {
        [SerializeField] protected Ease ease;
        [SerializeField] private AnimationPartTypeExecution executionType;

        protected UIView _cashedView;
        protected Vector3 _cashedValue;

        public AnimationPartTypeExecution TypeExecution => executionType;

        public Tween RunAnimation(Sequence sequence, UIView view, Vector3 targetValue)
        {
            _cashedView = view;
            _cashedValue = targetValue;
        
            switch (executionType)
            {
                case AnimationPartTypeExecution.Append:
                    return sequence.Append(RunAnimationInternal(view, targetValue).SetEase(ease));
                    break;
                case AnimationPartTypeExecution.Join:
                    return sequence.Join(RunAnimationInternal(view, targetValue).SetEase(ease));
                    break;
            }

            return null;
        }

        protected abstract Tween RunAnimationInternal(UIView view, Vector3 targetVector);

        protected virtual void OnAnimationComplete()
        {
            _cashedView = null;
            _cashedValue = new Vector3();
        }
    }
}