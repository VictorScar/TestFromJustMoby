using System;
using System.Collections;
using System.Collections.Generic;
using ScarFramework.UI;
using UnityEngine;

public class HoleView : UIView, IInteractableElement
{
    [SerializeField] private RectTransform uploadPoint;
    public event Action<CubeConfigData, Vector3, DragSourceType> onPutElement;
    public Vector2 UploadPoint => uploadPoint.anchoredPosition;

    public bool TryPutElement(CubeConfigData elementConfigData, Vector3 elementPosition, DragSourceType dragSourceType)
    {
        if (dragSourceType == DragSourceType.FromTower)
        {
            onPutElement?.Invoke(elementConfigData, elementPosition, dragSourceType);
            return true;
        }

        return false;
    }
}