using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Este enemigo es una bola que va rebotando y arrastra al jugador. No está embuclada pero al destruirse se invoca que se genere una nueva. Está estipulado en GameController que aparezca una nueva cada 10 puntos a partir de 15.
public class Enemy02GeneratorController : GenericEnemyGeneratorController
{

    // Start is called before the first frame update
    void Start()
    {
        initEnemyGenerator();
    }

    //Son las acciones iniciales que deben hacerse. Esto no se pone directamente en el "Start()", para poder llamarlo desde fuera y asi resetear el elemento.
    public void initEnemyGenerator()
    {
        CancelInvoke("CreateEnemy");
        changePosition(initialPosition);

        currentCourse = CurrentCourse.izquierda;
    }

    //Este método debe ser público por que este enemigo no se genera en un bucle, si no que es a petición.
    public void generarEnemigo(InfoEnemigo02 infoEnemigo02)
    {
        if(infoEnemigo02.debeGenerarse)
        {
            CreateEnemy();
        }
    }
}
