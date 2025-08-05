using DG.Tweening;
using UnityEngine;

public class ScaleAnimationPart : CubeAnimationPart
{
    [SerializeField] private float duration;
    [SerializeField] private Vector2 endScale;
    protected override Tween RunAnimationInternal(RectTransform transform, Vector3 targetVector)
    {
        return transform.DOScale(endScale, duration).OnKill(OnAnimationComplete);;
    }

    protected override void OnAnimationComplete()
    {
        if (_cashedTransform != null)
        {
            _cashedTransform.localScale = endScale;
        }
        base.OnAnimationComplete();
    }
}
