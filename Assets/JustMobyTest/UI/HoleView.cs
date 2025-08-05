using System;
using System.Collections;
using System.Collections.Generic;
using ScarFramework.UI;
using UnityEngine;

public class HoleView : UIView, IInteractableElement
{
    [SerializeField] private RectTransform uploadPoint;
    public event Action<CubeConfigData, Vector3> onPutElement;
    public Vector2 UploadPoint => uploadPoint.anchoredPosition;

    public bool TryPutElement(CubeConfigData elementConfigData, Vector3 elementPosition)
    {
        onPutElement?.Invoke(elementConfigData, elementPosition);
        return true;
    }
}
