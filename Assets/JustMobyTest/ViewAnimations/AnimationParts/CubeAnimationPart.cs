using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class CubeAnimationPart : MonoBehaviour
{
    [SerializeField] protected Ease ease;
    [SerializeField] private AnimationPartTypeExecution executionType;

    protected RectTransform _cashedTransform;
    protected Vector3 _cashedValue;

    public AnimationPartTypeExecution TypeExecution => executionType;

    public Tween RunAnimation(Sequence sequence, RectTransform transform, Vector3 targetValue)
    {
        _cashedTransform = transform;
        _cashedValue = targetValue;
        
        switch (executionType)
        {
            case AnimationPartTypeExecution.Append:
                return sequence.Append(RunAnimationInternal(transform, targetValue).SetEase(ease));
                break;
            case AnimationPartTypeExecution.Join:
                return sequence.Join(RunAnimationInternal(transform, targetValue).SetEase(ease));
                break;
        }

        return null;
    }

    protected abstract Tween RunAnimationInternal(RectTransform transform, Vector3 targetVector);

    protected virtual void OnAnimationComplete()
    {
        _cashedTransform = null;
        _cashedValue = new Vector3();
    }
}