using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy02GeneratorController : GenericEnemyGeneratorController {

    // Start is called before the first frame update
    void Start() {
        initialPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        changePosition(initialPosition);
        currentCourse = CurrentCourse.derecha;

        CreateEnemy();

    }
}
