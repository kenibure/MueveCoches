using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericEnemyGeneratorController : MonoBehaviour {
    public GameObject gameController;
    protected CurrentCourse currentCourse;
    protected CurrentCourse lastKnowedCourse; //Este se utiliza para cuando se le da a pausa que luego sepa para donde iba
    public GameObject enemyPrefab;
    protected float landscapeSpeed = 0.02f;
    protected float leftLimit = -1.5f;
    protected float rightLimit = 1.5f;
    protected Vector3 initialPosition;



    // Update is called once per frame
    void Update() {
        landscapeMovement();
    }

    //Son las posibles direcciones en las que se puede mover este generador de enemigos.
    protected enum CurrentCourse {
        derecha, izquierda, parado
    }

    //Se le pasa un vector y coloca el GameObject en ese vector.
    protected void changePosition(Vector3 destino) {

        this.transform.position = destino;
    }

    protected void CreateEnemy() {
        GameObject newObject = Instantiate(enemyPrefab, transform.position, Quaternion.identity) as GameObject;
        newObject.SendMessage("ownSetGameControllerValue", gameController);
        print("Generando enemigo");
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
    public void pauseLandscapeMovement() {
        lastKnowedCourse = currentCourse;
        currentCourse = CurrentCourse.parado;
    }

    public void resumeLandscapeMovement() {
        currentCourse = lastKnowedCourse;
    }

    protected void generateLoopCreateEnemies(float timeBetweenAction) {
        InvokeRepeating("CreateEnemy", 0f, timeBetweenAction);
    }

    protected void cancelLoopCreateEnemies() {
        CancelInvoke("CreateEnemy");
    }

    //Esto es para añadir aleatoriedad a las partidas.
    protected void cambiarSentidoDelGenerator() {
        if (currentCourse == CurrentCourse.derecha) {
            currentCourse = CurrentCourse.izquierda;
        }

        if (currentCourse == CurrentCourse.izquierda) {
            currentCourse = CurrentCourse.derecha;
        }
    }
}
