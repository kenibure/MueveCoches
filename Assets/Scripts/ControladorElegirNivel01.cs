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
        foreach (GameObject go in generarListado()) {
        }
        print("Fin del start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Este método va a devolver un listado con los "Button" que deben mostrarse.
    private List<GameObject> generarListado() {
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
        lista.Add(newObject3);
        /*GameObject newObject4 = Instantiate(prefabButton, transform.position, Quaternion.identity) as GameObject;
        newObject4.transform.parent = listaDeNivelesEnCanvas.transform;
        newObject4.SendMessage("setEscenaDeDestino", "EscenaJuego_nvl01");
        newObject4.SendMessage("setValorLabelBoton", "Nivel 2");
        GameObject newObject5 = Instantiate(prefabButton, transform.position, Quaternion.identity) as GameObject;
        newObject5.transform.parent = listaDeNivelesEnCanvas.transform;
        newObject5.SendMessage("setEscenaDeDestino", "EscenaJuego_nvl01");
        newObject5.SendMessage("setValorLabelBoton", "Nivel 3");
        GameObject newObject6 = Instantiate(prefabButton, transform.position, Quaternion.identity) as GameObject;
        newObject6.transform.parent = listaDeNivelesEnCanvas.transform;
        newObject6.SendMessage("setEscenaDeDestino", "EscenaJuego_nvl01");
        newObject6.SendMessage("setValorLabelBoton", "Nivel 4");
        GameObject newObject7 = Instantiate(prefabButton, transform.position, Quaternion.identity) as GameObject;
        newObject7.transform.parent = listaDeNivelesEnCanvas.transform;
        newObject7.SendMessage("setEscenaDeDestino", "EscenaJuego_nvl01");
        newObject7.SendMessage("setValorLabelBoton", "Nivel 5");
        GameObject newObject8 = Instantiate(prefabButton, transform.position, Quaternion.identity) as GameObject;
        newObject8.transform.parent = listaDeNivelesEnCanvas.transform;
        newObject8.SendMessage("setEscenaDeDestino", "EscenaJuego_nvl01");
        newObject8.SendMessage("setValorLabelBoton", "Nivel 6");
        GameObject newObject9 = Instantiate(prefabButton, transform.position, Quaternion.identity) as GameObject;
        newObject9.transform.parent = listaDeNivelesEnCanvas.transform;
        newObject9.SendMessage("setEscenaDeDestino", "EscenaJuego_nvl01");
        newObject9.SendMessage("setValorLabelBoton", "Nivel 7");
        GameObject newObject10 = Instantiate(prefabButton, transform.position, Quaternion.identity) as GameObject;
        newObject10.transform.parent = listaDeNivelesEnCanvas.transform;
        newObject10.SendMessage("setEscenaDeDestino", "EscenaJuego_nvl01");
        newObject10.SendMessage("setValorLabelBoton", "Nivel 8");
        GameObject newObject11 = Instantiate(prefabButton, transform.position, Quaternion.identity) as GameObject;
        newObject11.transform.parent = listaDeNivelesEnCanvas.transform;
        newObject11.SendMessage("setEscenaDeDestino", "EscenaJuego_nvl01");
        newObject11.SendMessage("setValorLabelBoton", "Nivel 9");
        GameObject newObject12 = Instantiate(prefabButton, transform.position, Quaternion.identity) as GameObject;
        newObject12.transform.parent = listaDeNivelesEnCanvas.transform;
        newObject12.SendMessage("setEscenaDeDestino", "EscenaJuego_nvl01");
        newObject12.SendMessage("setValorLabelBoton", "Nivel 10");
        lista.Add(newObject4);
        lista.Add(newObject5);
        lista.Add(newObject6);
        lista.Add(newObject7);
        lista.Add(newObject8);
        lista.Add(newObject9);
        lista.Add(newObject10);
        lista.Add(newObject11);
        lista.Add(newObject12);*/
        return lista;
    }
}
