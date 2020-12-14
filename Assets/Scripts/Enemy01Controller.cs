using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01Controller : MonoBehaviour
{

    public float velocity = 5f;

    private Rigidbody2D rb2d;
    public AudioClip playerCollisionSound;

    private AudioSource audioSource; //Este elemento está creado como "Component" en el "Controller". Se asigna en el "Start()".


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = Vector2.down * velocity;
        audioSource = GetComponent<AudioSource>();
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

    //Recibe un AudioClip y lo reproduce.
    private void reproducirSonido(AudioClip audioClip) {
        audioSource.clip = audioClip;
        audioSource.Play();

    }
}
