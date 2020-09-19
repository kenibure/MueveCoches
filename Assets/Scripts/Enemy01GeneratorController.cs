using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Este generador de enemigos se va a ir moviendo hacia los lados.
public class Enemy01GeneratorController : MonoBehaviour
{

    public GameObject enemy01Prefab;
    public float generatorTimer = 1f;
    public float leftLimit = -1.5f;
    public float rightLimit = 1.5f;
    public float landscapeSpeed = 0.02f;
    private CurrentCourse currentCourse;
    private CurrentCourse lastKnowedCourse; //Este se utiliza para cuando se le da a pausa que luego sepa para donde iba



    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CreateEnemy", 0f, generatorTimer);
        currentCourse = CurrentCourse.derecha;
    }

    // Update is called once per frame
    void Update()
    {
        landscapeMovement();
    }

    private void CreateEnemy() {
        Instantiate(enemy01Prefab, transform.position, Quaternion.identity);
    }

    //Realiza el movimiento horizontal
    private void landscapeMovement() {

        Vector3 currentPos = this.transform.position;
        Vector3 newPos = currentPos;

        switch(currentCourse) {
            case CurrentCourse.derecha:
                newPos.x = currentPos.x + landscapeSpeed;
                if(newPos.x > rightLimit) {
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
}
