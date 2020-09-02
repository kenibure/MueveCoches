using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class RightPanelController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public bool buttonPressed;
    public PlayerController playerController;

    public void OnPointerDown(PointerEventData eventData) {
        buttonPressed = true;
        Debug.Log(" -->  Mover derecha.");
        playerController.activarMovimientoDerecha();
    }

    public void OnPointerUp(PointerEventData eventData) {
        buttonPressed = false;
        Debug.Log(" -->| Parar movimiento derecha.");
        playerController.desactivarMovimiento();
    }
}
