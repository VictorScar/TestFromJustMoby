using DG.Tweening;
using UnityEngine;

public abstract class CubeAnimation : MonoBehaviour
{
    [SerializeField] protected float speed = 1f;
    [SerializeField] private CubeAnimationID animationID;
    public CubeAnimationID AnimationID => animationID;

    public Tween Play(RectTransform animatedObject, Vector3 targetPoint)
    {
        return PlayInternal(animatedObject, targetPoint).OnComplete(OnAnimationEnded);
    }

    protected abstract Tween PlayInternal(RectTransform animatedObject,
        Vector3 targetPoint);


    protected virtual void OnAnimationEnded()
    {
    }
}