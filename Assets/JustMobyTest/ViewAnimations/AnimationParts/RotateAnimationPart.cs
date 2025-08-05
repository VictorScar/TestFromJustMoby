using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RotateAnimationPart : CubeAnimationPart
{
    [SerializeField] private float duration;
    [SerializeField] private Vector3 endRotation;
    protected override Tween RunAnimationInternal(RectTransform transform, Vector3 targetVector)
    {
        return transform.DOLocalRotate(endRotation, duration).OnKill(OnAnimationComplete);;
    }

    protected override void OnAnimationComplete()
    {
        if (_cashedTransform != null)
        {
            _cashedTransform.localRotation = Quaternion.Euler(endRotation);
        }
        base.OnAnimationComplete();
    }
}
