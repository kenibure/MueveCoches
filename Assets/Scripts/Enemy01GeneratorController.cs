using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Este generador de enemigos se va a ir moviendo hacia los lados.
public class Enemy01GeneratorController : MonoBehaviour {

    public GameObject enemy01Prefab;
    public float startGeneratorTimer = 1f;
    public float leftLimit = -1.5f;
    public float rightLimit = 1.5f;
    public float landscapeSpeed = 0.02f;
    private CurrentCourse currentCourse;
    private CurrentCourse lastKnowedCourse; //Este se utiliza para cuando se le da a pausa que luego sepa para donde iba

    private float currentGeneratorTimer;

    private Vector3 initialPosition;


    // Start is called before the first frame update
    void Start() {
        initialPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        initEnemyGenerator();
    }

    // Update is called once per frame
    void Update() {
        landscapeMovement();
    }

    //Son las acciones iniciales que deben hacerse. Esto no se pone directamente en el "Start()", para poder llamarlo desde fuera y asi resetear el elemento.
    public void initEnemyGenerator() {
        CancelInvoke("CreateEnemy");
        changePosition(initialPosition);

        currentCourse = CurrentCourse.derecha;
        currentGeneratorTimer = startGeneratorTimer;
        generateLoopCreateEnemies(startGeneratorTimer);
    }

    private void CreateEnemy() {
        Instantiate(enemy01Prefab, transform.position, Quaternion.identity);
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

    //Son las posibles direcciones en las que se puede mover este generador de enemigos.
    private enum CurrentCourse {
        derecha, izquierda, parado
    }

    public void pauseLandscapeMovement() {
        lastKnowedCourse = currentCourse;
        currentCourse = CurrentCourse.parado;
    }

    public void resumeLandscapeMovement() {
        currentCourse = lastKnowedCourse;
    }

    //Con esto se incrementa la velocidad a la que se generan los enemigos.
    public void incrementGeneratorSpeed() {
        float generatorSpeedIncrementorFactor = 0.01f;
        currentGeneratorTimer = currentGeneratorTimer - generatorSpeedIncrementorFactor;

        cancelLoopCreateEnemies();
        generateLoopCreateEnemies(currentGeneratorTimer);
    }

    private void generateLoopCreateEnemies(float timeBetweenAction) {
        cambiarSentidoDelGenerator();
        InvokeRepeating("CreateEnemy", 0f, timeBetweenAction);
    }

    private void cancelLoopCreateEnemies() {
        CancelInvoke("CreateEnemy");
    }

    //Esto es para añadir aleatoriedad a las partidas.
    private void cambiarSentidoDelGenerator() {
        if (currentCourse == CurrentCourse.derecha) {
            currentCourse = CurrentCourse.izquierda;
        }

        if (currentCourse == CurrentCourse.izquierda) {
            currentCourse = CurrentCourse.derecha;
        }
    }
}
