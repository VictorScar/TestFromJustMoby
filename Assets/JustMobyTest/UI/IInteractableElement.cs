using System;
using JustMobyTest.Configs;
using JustMobyTest.Data;
using UnityEngine;

namespace JustMobyTest.UI
{
    public interface IInteractableElement
    {
        public event Action<CubeConfig, Vector3, DragSourceType> onPutElement;
        bool TryPutElement(CubeConfig elementConfig, Vector3 elementPosition, DragSourceType dragSourceType);
    }
}
