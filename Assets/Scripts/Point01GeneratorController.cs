using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point01GeneratorController : MonoBehaviour {

    public GameObject point01prefab;
    public GameObject gameController;
    public float startGeneratorTimer = 2.5f;
    public float leftLimit = -1.5f;
    public float rightLimit = 1.5f;
    public float landscapeSpeed = 0.05f;
    private CurrentCourse currentCourse;
    private CurrentCourse lastKnowedCourse; //Este se utiliza para cuando se le da a pausa que luego sepa para donde iba

    private Vector3 initialPosition;


    // Start is called before the first frame update
    void Start() {
        initialPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        initPointGenerator();
    }

    //Son las acciones iniciales que deben hacerse. Esto no se pone directamente en el "Start()", para poder llamarlo desde fuera y asi resetear el elemento.
    public void initPointGenerator() {
        CancelInvoke("CreatePoint");
        transform.position = initialPosition;

        generateLoopCreatePoints(startGeneratorTimer);
        currentCourse = CurrentCourse.derecha;
    }

    // Update is called once per frame
    void Update() {
        landscapeMovement();
    }

    //Son las posibles direcciones en las que se puede mover este generador de puntos.
    private enum CurrentCourse {
        derecha, izquierda, parado
    }


    //Realiza el movimiento horizontal
    private void landscapeMovement() {

        Vector3 currentPos = this.transform.position;
        Vector3 newPos = currentPos;

        switch (currentCourse) {
            case CurrentCourse.derecha:
                newPos.x = currentPos.x + landscapeSpeed;
                if (newPos.x > rightLimit) {
                    newPos.x = rightLimit;
                    currentCourse = CurrentCourse.izquierda;
                }
                changePosition(newPos);
                break;
            case CurrentCourse.izquierda:
                newPos.x = currentPos.x - landscapeSpeed;
                if (newPos.x < leftLimit) {
                    newPos.x = leftLimit;
                    currentCourse = CurrentCourse.derecha;
                }
                changePosition(newPos);
                break;
        }
    }

    //Se le pasa un vector y coloca el GameObject en ese vector.
    private void changePosition(Vector3 destino) {
        this.transform.position = destino;
    }

    public void pauseLandscapeMovement() {
        lastKnowedCourse = currentCourse;
        currentCourse = CurrentCourse.parado;
    }

    public void resumeLandscapeMovement() {
        currentCourse = lastKnowedCourse;
    }

    private void generateLoopCreatePoints(float timeBetweenAction) {
        InvokeRepeating("CreatePoint", startGeneratorTimer, timeBetweenAction);
    }

    //Esto hace falta asi por que tiene un parámetro de entrada (el Game Controller)
    private void CreatePoint() {
        GameObject newObject = Instantiate(point01prefab, transform.position, Quaternion.identity) as GameObject;
        newObject.SendMessage("ownSetGameControllerValue", gameController);
    }
}
