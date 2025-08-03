using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AditionAnimation : CubeAnimation
{
    [SerializeField] private Ease ease;
    [SerializeField] private float durationf = 5f;
    protected override Tween PlayInternal(RectTransform animatedObject, Vector3 targetPoint)
    {
        if (speed == 0f)
        {
            speed = 1f;
        }
        
        var duration = Vector3.Distance(targetPoint, animatedObject.position)/speed;
       
        return animatedObject.DOAnchorPos(targetPoint, duration).SetEase(ease);
    }

    protected override void OnAnimationEnded()
    {
        
    }
}
