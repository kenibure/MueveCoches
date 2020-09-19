using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point01Controller : MonoBehaviour {

    public float velocity = 4.5f;

    private Rigidbody2D rb2d;
    private GameObject gameController;

    //Este método es para que el Generator que los crea pueda meter aqui la info del Game Controller.
    public void ownSetGameControllerValue(GameObject gameController) {
        this.gameController = gameController;
    }


    // Start is called before the first frame update
    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = Vector2.down * velocity;
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D otherElement) {
        if (otherElement.gameObject.tag == "OwnTag_player") {
            gameController.SendMessage("pointWon");
            Destroy(gameObject);
        }
        if (otherElement.gameObject.tag == "OwnTag_enemyDestroyer") {
            Destroy(gameObject);
        }
    }
}
