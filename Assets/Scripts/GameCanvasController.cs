using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameCanvasController : MonoBehaviour {

    private float parallaxSpeed = 0.2f;
    public RawImage background; //Carretera, el de encima
    public RawImage bottomBackground; //Agua, el de abajo

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {

        efectoParallax();
    }
    private void efectoParallax() { //Es el efecto de que el fondo se mueva más lento que la plataforma para dar sensación de produndidad.
        float finalSpeed = parallaxSpeed * Time.deltaTime; //Debe calcularse para que se adecúe a la pantalla, por eso no puede usarse directamente parallaxSpeed.
        moverBackground(finalSpeed);
        moverBottomBackground(finalSpeed);
    }

    private void moverBackground(float speed) {
        Rect newRect = new Rect(background.uvRect.x, background.uvRect.y + speed, background.uvRect.width, background.uvRect.height);
        moverRawImage(background, newRect);
    }

    private void moverBottomBackground(float speed) {
        Rect newRect = new Rect(bottomBackground.uvRect.x, bottomBackground.uvRect.y + (speed/2), bottomBackground.uvRect.width, bottomBackground.uvRect.height);
        moverRawImage(bottomBackground, newRect);
    }
    private void moverRawImage(RawImage rawImage, Rect rect) {
        rawImage.uvRect = rect;
    }
}
