using DG.Tweening;
using ScarFramework.Button;
using UnityEngine;

public abstract class CubeAnimation : MonoBehaviour
{
    [SerializeField] private CubeAnimationID animationID;
    [SerializeField] protected Ease ease;
    
    protected Sequence _animation;
    public CubeAnimationID AnimationID => animationID;

    public Tween Play(RectTransform animatedObject, Vector3 targetPoint)
    {
        _animation = DOTween.Sequence();
        return _animation.Append(PlayInternal(animatedObject, targetPoint));
    }

    protected abstract Tween PlayInternal(RectTransform animatedObject,
        Vector3 targetPoint);


    protected virtual void OnAnimationEnded()
    {
    }

    [Button("Kill")]
    public void KillAnimation()
    {
        Debug.Log("Kill animation");
        _animation?.Kill();
    }
}