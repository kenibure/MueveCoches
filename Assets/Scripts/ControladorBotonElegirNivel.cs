using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControladorBotonElegirNivel : MonoBehaviour {

    public Text labelBoton;
    private string escenaDestino; //Contiene la escena que debe lanzarse al hacer click en el botón.
    private string escenaPorDefecto = "EscenaInicial";
    private string valorLabelBoton = "default";

    // Start is called before the first frame update
    void Start() {
        escenaDestino = escenaPorDefecto;
        cambiarValorLabelBoton();
    }

    // Update is called once per frame
    void Update() {

    }

    public void setEscenaDeDestino(string escenaDeDestino) {
        this.escenaDestino = escenaDeDestino;
    }

    public void setValorLabelBoton(string valorLabelBoton) {
        this.valorLabelBoton = valorLabelBoton;
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
