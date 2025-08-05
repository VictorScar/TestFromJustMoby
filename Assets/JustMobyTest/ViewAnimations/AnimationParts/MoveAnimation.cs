using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using ScarFramework.Button;
using UnityEngine;

public class MoveAnimation : CubeAnimation
{
    [SerializeField] private float speed = 1f;
    
    private Vector3 _cashedPoint;
    private RectTransform _cashedObject;

    protected override Tween PlayInternal(RectTransform animatedObject, Vector3 targetPoint)
    {
        _cashedObject = animatedObject;
        _cashedPoint = targetPoint;

        if (speed == 0f) speed = 1f;

        var duration = Vector3.Distance(targetPoint, animatedObject.position) / speed;
        var sequence = DOTween.Sequence();
        return sequence.Append(animatedObject.DOAnchorPos(targetPoint, duration).SetEase(ease)).OnKill(OnAnimationEnded);
    }

    protected override void OnAnimationEnded()
    {
        if (_cashedObject != null)
        {
            _cashedObject.anchoredPosition = _cashedPoint;
            _cashedObject = null;
        }

        Debug.Log("Moving ended!");
    }
    

}