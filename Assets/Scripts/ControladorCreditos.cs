using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorCreditos : MonoBehaviour
{
    string miPaypal = "https://paypal.me/rknb";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void lanzarPaypal() {
        print("Lanzando Paypal");
        Application.OpenURL(miPaypal);
    }
    public void cambiarEscena(string escenaDestino) {
        print("Cambiando a la escena " + escenaDestino);
        SceneManager.LoadScene(escenaDestino);
    }
}
