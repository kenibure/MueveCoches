using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public void generarEnemigo()
    {
        CreateEnemy();
    }
}
