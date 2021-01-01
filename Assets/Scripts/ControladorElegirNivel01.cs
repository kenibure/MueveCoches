using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorElegirNivel01 : MonoBehaviour
{

    public GameObject listaDeNivelesEnCanvas; //Es la lista del Canvas donde habrá que ir agregando botones.
    public GameObject prefabButton;

    // Start is called before the first frame update
    void Start()
    {
        generarListado();
    }

    //Este método va a devolver un listado con los "Button" que deben mostrarse.
    private void generarListado() {
        List < GameObject > lista = new List<GameObject>();
        GameObject newObject1 = Instantiate(prefabButton, transform.position, Quaternion.identity) as GameObject;
        newObject1.transform.parent = listaDeNivelesEnCanvas.transform;
        newObject1.SendMessage("setEscenaDeDestino", "EscenaInicial");
        newObject1.SendMessage("setValorLabelBoton", "Inicio");
        lista.Add(newObject1);
        GameObject newObject2 = Instantiate(prefabButton, transform.position, Quaternion.identity) as GameObject;
        newObject2.transform.parent = listaDeNivelesEnCanvas.transform;
        newObject2.SendMessage("setEscenaDeDestino", "EscenaCreditos");
        newObject2.SendMessage("setValorLabelBoton", "Creditos");
        lista.Add(newObject2);
        GameObject newObject3 = Instantiate(prefabButton, transform.position, Quaternion.identity) as GameObject;
        newObject3.transform.parent = listaDeNivelesEnCanvas.transform;
        newObject3.SendMessage("setEscenaDeDestino", "EscenaJuego_nvl01");
        newObject3.SendMessage("setValorLabelBoton", "Nivel 1");
        newObject3.SendMessage("setInfoNivel", ListadoNiveles.nivel01());
        lista.Add(newObject3);
        GameObject newObject4 = Instantiate(prefabButton, transform.position, Quaternion.identity) as GameObject;
        newObject4.transform.parent = listaDeNivelesEnCanvas.transform;
        newObject4.SendMessage("setEscenaDeDestino", "EscenaJuego_nvl01");
        newObject4.SendMessage("setValorLabelBoton", "Nivel 2");
        newObject4.SendMessage("setInfoNivel", ListadoNiveles.nivel02());
        lista.Add(newObject4);
    }
}
