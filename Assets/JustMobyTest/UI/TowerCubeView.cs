using DG.Tweening;
using ScarFramework.Button;
using ScarFramework.UI;
using UnityEngine;
using UnityEngine.UI;

public class TowerCubeView : UIDragable
{
    [SerializeField] private Image icon;
    [SerializeField] private TowerCubeAnimator animator;
    [SerializeField] private Vector2 targetPos;

    public Sprite Icon
    {
        set => icon.sprite = value;
    }

    public Vector2 Size
    {
        set => Rect.sizeDelta = value;
    }

    public void SetPosition(Vector3 newPos, bool immediately = true)
    {
        if (immediately)
        {
            rect.anchoredPosition = newPos;
        }
        else
        {
            animator.PlayAnimation(Rect, CubeAnimationID.Move, newPos);
        }
    }

    public void Fall(string failtureReason)
    {
        Debug.Log("Fall! Reason is " + failtureReason);
        animator.PlayAnimation(rect, CubeAnimationID.Destroy, new Vector2(rect.anchoredPosition.x + Random.Range(-20f,20f),0f)).OnKill(DestroyCube);
        //DestroyCube();
    }

    private void DestroyCube()
    {
        Destroy(gameObject);
    }

    [Button("SetPos")]
    public void SetPosTest()
    {
        SetPosition(targetPos, false);
    }

    public void Upload(Vector2 targetPos)
    {
        animator.PlayAnimation(rect, CubeAnimationID.Upload, targetPos).OnKill(DestroyCube);
    }
}