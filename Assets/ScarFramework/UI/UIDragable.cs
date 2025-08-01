using System;
using UnityEngine.EventSystems;

namespace ScarFramework.UI
{
    public class UIDragable : UIView, IBeginDragHandler, IEndDragHandler
    {
        public event Action onBeginDrag;
        public event Action onEndDrag;
    
        public void OnBeginDrag(PointerEventData eventData)
        {
            onBeginDrag?.Invoke();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            onEndDrag?.Invoke();
        }
    }
}
