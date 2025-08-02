using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractableElement
{
    bool TryPutElement(CubeData elementData, Vector3 elementPosition);
}
