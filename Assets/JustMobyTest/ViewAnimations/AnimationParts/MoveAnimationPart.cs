using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[Serializable]
public class MoveAnimationPart : CubeAnimationPart
{
    [SerializeField] private Vector2 targetPosition;
    [SerializeField] private float speed;
 
    protected override Tween RunAnimationInternal(RectTransform transform, Vector3 targetVector)
    {
        if (speed < 1f) speed = 1f;
        var duration = Vector3.Distance(transform.anchoredPosition, targetVector) / speed;
        return transform.DOAnchorPos(targetVector, duration).OnKill(OnAnimationComplete);
    }

    protected override void OnAnimationComplete()
    {
        if (_cashedTransform != null)
        {
            _cashedTransform.anchoredPosition = _cashedValue;
        }
        
        base.OnAnimationComplete();
    }
}