using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class RightPanelController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

        public bool buttonPressed;

        public void OnPointerDown(PointerEventData eventData) {
            buttonPressed = true;
        Debug.Log(" -->  Mover derecha.");
        }

        public void OnPointerUp(PointerEventData eventData) {
            buttonPressed = false;
        Debug.Log(" -->| Parar movimiento derecha.");
    }
    }
