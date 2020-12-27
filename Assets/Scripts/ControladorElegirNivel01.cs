using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorElegirNivel01 : MonoBehaviour
{

    public GameObject listaDeNivelesEnCanvas; //Es la lista del Canvas donde habrá que ir agregando botones.

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject go in generarListado()) {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private List<GameObject> generarListado() {
        return null;
    }
}
