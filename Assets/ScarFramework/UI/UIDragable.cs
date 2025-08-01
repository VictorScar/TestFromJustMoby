using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ScarFramework.UI
{
    public class UIDragable : UIView, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        public event Action<PointerEventData, UIDragable> onBeginDrag;
        public event Action onEndDrag;

        public void OnBeginDrag(PointerEventData eventData)
        {
            onBeginDrag?.Invoke(eventData, this);
            //Debug.Log("onDragCube!");
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            onEndDrag?.Invoke();
        }

        public void OnDrag(PointerEventData eventData)
        {
           
        }
    }
}