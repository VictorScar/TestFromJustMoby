using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using ScarFramework.UI;
using UnityEngine;
using UnityEngine.UI;

public class TowerCubeView : UIDragable
{
    [SerializeField] private Image icon;
    //animator

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
        }
        else
        {
        }


        rect.anchoredPosition = newPos;
    }

    public void Fall(string failtureReason)
    {
        Debug.Log("Fall! Reason is " + failtureReason);
       // rect.DOMove(rect.position - transform.up * 50f, 0.5f).OnComplete(DestroyCube);
       DestroyCube();
    }

    private void DestroyCube()
    {
        Destroy(gameObject);
    }
}