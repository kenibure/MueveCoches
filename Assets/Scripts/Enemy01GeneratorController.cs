using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01GeneratorController : MonoBehaviour
{

    public GameObject enemy01Prefab;
    private float generatorTimer = 2f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CreateEnemy", 0f, generatorTimer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateEnemy() {
        Instantiate(enemy01Prefab, transform.position, Quaternion.identity);
    }
}
