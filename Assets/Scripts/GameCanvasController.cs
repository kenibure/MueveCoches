using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameCanvasController : MonoBehaviour {

    private float parallaxSpeed = 0.2f;
    public RawImage background; //Desde Unity hay que asignarle cual es.

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
        moverFondo(finalSpeed);
        //moverPlataforma(finalSpeed);
    }

    private void moverFondo(float speed) {
        Rect newRect = new Rect(background.uvRect.x, background.uvRect.y + speed, background.uvRect.width, background.uvRect.height);
        moverRawImage(background, newRect);
    }
    private void moverRawImage(RawImage rawImage, Rect rect) {
        rawImage.uvRect = rect;
    }
}
