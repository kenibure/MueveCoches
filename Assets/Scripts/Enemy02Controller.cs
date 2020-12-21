using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy02Controller : MonoBehaviour {

    private float velocidadVertical = 3f; //positivo = arriba; negativo = abajo
    private float velocidadHorizontal = 1f; //positivo = derecha; negativo = izquierda


    private Rigidbody2D rb2d;
    public AudioClip playerCollisionSound;
    private GameObject gameController;

    // Start is called before the first frame update
    void Start() {
        rb2d = GetComponent<Rigidbody2D>();

        generarYAlmacenarVelocidadHorizontal(Random.Range(0, 2) == 1);
        asignarVelocidades();
    }

    //Este método es para que el Generator que los crea pueda meter aqui la info del Game Controller.
    public void ownSetGameControllerValue(GameObject gameController) {
        this.gameController = gameController;
    }

    //Esto detecta las colisiones contra elementos marcados como Trigger.
    private void OnTriggerEnter2D(Collider2D otherElement) {

        //Colisión contra una de las barras laterales, debe invertirse la X del movimiento.
        if (otherElement.gameObject.tag == "OwnTag_LeftBar" || otherElement.gameObject.tag == "OwnTag_RightBar") {
            velocidadHorizontal = velocidadHorizontal * -1;
            asignarVelocidades();
        }

        //Colisión contra la barra para eliminar.
        if (otherElement.gameObject.tag == "OwnTag_enemyDestroyer") {
            gameController.SendMessage("invocarEnemigo02"); //Esto hará que al destruirse se genere otro.
            Destroy(gameObject);
        }
    }

    //Esto detecta las colisiones contra elementos no marcados como Trigger.
    private void OnCollisionEnter2D(Collision2D collision) {

        //Colisión contra jugador
        if (collision.gameObject.tag == "OwnTag_player") {
            reproducirSonido(playerCollisionSound);
        }
    }

    //Recibe un AudioClip y lo envía al Controlador para que lo reproduzca.
    private void reproducirSonido(AudioClip audioClip) {
        gameController.SendMessage("reproducirSonidoUnaVez", audioClip);
    }

    //Devuelve un número entre 1 y 1.3.
    private float generarFactorMultiplicador() {
        int num1 = Random.Range(0, 3000);

        return 1 + (num1 / 1000);
    }


    private void generarYAlmacenarVelocidadHorizontal(bool esDerecha) {
        if(esDerecha) {
            velocidadHorizontal = generarFactorMultiplicador();
        } else {
            velocidadHorizontal = -(generarFactorMultiplicador());
        }
    }

    //Este método cambia el valor de "velocity" por el que tengan las velocidades.
    private void asignarVelocidades() {

        rb2d.velocity = new Vector2(velocidadHorizontal, -velocidadVertical);
    }
}
