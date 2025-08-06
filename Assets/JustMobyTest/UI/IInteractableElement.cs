using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractableElement
{
    public event Action<CubeConfig, Vector3, DragSourceType> onPutElement;
    bool TryPutElement(CubeConfig elementConfig, Vector3 elementPosition, DragSourceType dragSourceType);
}
