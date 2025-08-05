using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractableElement
{
    public event Action<CubeConfigData, Vector3> onPutElement;
    bool TryPutElement(CubeConfigData elementConfigData, Vector3 elementPosition);
}
