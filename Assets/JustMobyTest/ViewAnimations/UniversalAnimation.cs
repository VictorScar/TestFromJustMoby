using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class UniversalAnimation : CubeAnimation
{
    [SerializeField] private CubeAnimationPart[] animationParts;
    
    protected override Tween PlayInternal(RectTransform animatedObject, Vector3 targetPoint)
    {
        var sequence = DOTween.Sequence();

        foreach (var part in animationParts)
        {
            part.RunAnimation(sequence, animatedObject, targetPoint);
        }

        return sequence.OnKill(OnAnimationEnded);
    }

    protected override void OnAnimationEnded()
    {
     
    }
}
