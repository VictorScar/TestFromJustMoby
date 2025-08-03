using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractableElement
{
    bool TryPutElement(CubeConfigData elementConfigData, Vector3 elementPosition);
}
