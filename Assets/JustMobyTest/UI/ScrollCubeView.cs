using System.Collections;
using System.Collections.Generic;
using ScarFramework.UI;
using UnityEngine;
using UnityEngine.UI;

public class ScrollCubeView : UIDragable
{
    [SerializeField] private Image icon;

    public Sprite Icon
    {
        set => icon.sprite = value;
    }
}
