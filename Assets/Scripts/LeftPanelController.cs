using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class LeftPanelController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public bool buttonPressed;
    public GameObject playerController;

    public void OnPointerDown(PointerEventData eventData) {
        buttonPressed = true;
        playerController.SendMessage("activarMovimientoIzquierda");
    }

    public void OnPointerUp(PointerEventData eventData) {
        buttonPressed = false;
        playerController.SendMessage("desactivarMovimiento");
    }
}
