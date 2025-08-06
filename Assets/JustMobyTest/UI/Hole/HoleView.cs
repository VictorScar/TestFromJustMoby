using System;
using System.Collections;
using System.Collections.Generic;
using JustMobyTest.Configs;
using JustMobyTest.Data;
using JustMobyTest.UI;
using ScarFramework.UI;
using UnityEngine;

public class HoleView : UIView, IInteractableElement
{
    [SerializeField] private RectTransform uploadPoint;
    public event Action<CubeConfig, Vector3, DragSourceType> onPutElement;
    public Vector2 UploadPoint => uploadPoint.anchoredPosition;

    public bool TryPutElement(CubeConfig elementConfig, Vector3 elementPosition, DragSourceType dragSourceType)
    {
        if (dragSourceType == DragSourceType.FromTower)
        {
            onPutElement?.Invoke(elementConfig, elementPosition, dragSourceType);
            return true;
        }

        return false;
    }
}