using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControladorBotonElegirNivel : MonoBehaviour {

    public Text labelBoton;
    private string escenaDestino = "EscenaInicial"; //Contiene la escena que debe lanzarse al hacer click en el botón. Por defecto siempre será la "EscenaInicial".
    private string valorLabelBoton = "default";

    // Start is called before the first frame update
    void Start() {
        cambiarValorLabelBoton();
    }

    public void setEscenaDeDestino(string escenaDestino) {
        this.escenaDestino = escenaDestino;
    }

    public void setValorLabelBoton(string valorLabelBoton) {
        this.valorLabelBoton = valorLabelBoton;
        cambiarValorLabelBoton();
    }

    //Este método asigna el valor al Label del boton.
    private void cambiarValorLabelBoton() {
        labelBoton.text = valorLabelBoton;
    }

    public void cambiarEscena() {
        print("Cambiando a la escena " + escenaDestino);
        SceneManager.LoadScene(escenaDestino);
    }
}
