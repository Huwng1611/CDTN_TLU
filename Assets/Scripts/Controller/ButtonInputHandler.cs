using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Hungdv.InputController
{
    public class ButtonInputHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        public UnityAction OnPointerEnterEvent;
        public UnityAction OnPointerExitEvent;
        public UnityAction OnPointerDownEvent;
        public UnityAction OnPointerUpEvent;

        private void OnDestroy()
        {
            OnPointerEnterEvent = null;
            OnPointerExitEvent = null;
            OnPointerDownEvent = null;
            OnPointerUpEvent = null;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnPointerEnterEvent?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnPointerExitEvent?.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnPointerDownEvent?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnPointerUpEvent?.Invoke();
        }
    }
}