using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Este generador de enemigos se va a ir moviendo hacia los lados.
public class Enemy01GeneratorController : GenericEnemyGeneratorController {

    public float startGeneratorTimer = 1f; //El tiempo que hay INICIALMENTE entre cada creación de enemigos. (Se irá reduciendo al ir consiguiendo puntos).
    private float generatorSpeedIncrementorFactor = 0.01f;
    private float generatorSpeedLimiter = 0.73f; //Es la velocidad límite a la que se pueden generar enemigos más rápido que esto (número más bajo). Una vez llega ahí el incremento de la velocidad es mucho más lento.

    private float currentGeneratorTimer; //Time between an Enemy is generated.


    // Start is called before the first frame update
    void Start() {
        initialPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        initEnemyGenerator();
    }

    //Son las acciones iniciales que deben hacerse. Esto no se pone directamente en el "Start()", para poder llamarlo desde fuera y asi resetear el elemento.
    public void initEnemyGenerator() {
        CancelInvoke("CreateEnemy");
        changePosition(initialPosition);

        currentCourse = CurrentCourse.derecha;
        currentGeneratorTimer = startGeneratorTimer;
        generateLoopCreateEnemies(startGeneratorTimer);
    }

    //Con esto se incrementa la velocidad a la que se generan los enemigos.
    public void incrementGeneratorSpeed() {
        cancelLoopCreateEnemies();
        generateLoopCreateEnemies(currentGeneratorTimer);
        if (currentGeneratorTimer > generatorSpeedLimiter) {
            currentGeneratorTimer = currentGeneratorTimer - generatorSpeedIncrementorFactor;
        } else {
            currentGeneratorTimer = currentGeneratorTimer - (generatorSpeedIncrementorFactor/50);
        }


        //Esto hace que haya un 10% de posibilidades de que no cambie el sentido para darle aleatoriedad.
        if (Random.Range(0,10) != 1) {
            cambiarSentidoDelGenerator();
        }
    }
}
