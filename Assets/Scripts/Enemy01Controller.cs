using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01Controller : MonoBehaviour
{

    public float velocity = 5f;

    private Rigidbody2D rb2d;
    public AudioClip playerCollisionSound;
    private GameObject gameController;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = Vector2.down * velocity;
    }

    //Este método es para que el Generator que los crea pueda meter aqui la info del Game Controller.
    public void ownSetGameControllerValue(GameObject gameController) {
        this.gameController = gameController;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Esto detecta las colisiones contra elementos marcados como Trigger.
    private void OnTriggerEnter2D(Collider2D otherElement) {

        //Colisión contra la barra para eliminar.
        if(otherElement.gameObject.tag == "OwnTag_enemyDestroyer") {
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
}
