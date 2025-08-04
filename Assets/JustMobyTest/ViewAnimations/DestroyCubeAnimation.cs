using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DestroyCubeAnimation : CubeAnimation
{
    [SerializeField] private float _moveDuration = 2f;
    [SerializeField] private float _scaleDuration = 1f;
    [SerializeField] private Vector3 endRotation = new Vector3(0f, 0f, 90f);
    [SerializeField] private Vector2 endScale = new Vector2(1.5f, 1.5f);
    [SerializeField] private Vector2 endPoint;

    private RectTransform _cashedTransform;

    protected override Tween PlayInternal(RectTransform animatedObject, Vector3 targetPoint)
    {
        _cashedTransform = animatedObject;
        var correctedPoint = new Vector2(animatedObject.anchoredPosition.x + endPoint.x, endPoint.y);

        var sequence = DOTween.Sequence();
        return sequence
            .Append(animatedObject.DOAnchorPos(correctedPoint, _moveDuration))
            .Join(animatedObject.DOLocalRotate(endRotation, _moveDuration))
            .Append(animatedObject.DOScale(endScale, _scaleDuration))
            .SetEase(ease)
            .OnKill(OnAnimationEnded);
    }

    protected override void OnAnimationEnded()
    {
        _cashedTransform.anchoredPosition = endPoint;
        _cashedTransform.localRotation = Quaternion.Euler(endRotation);
        _cashedTransform.localScale = endScale;
    }
}