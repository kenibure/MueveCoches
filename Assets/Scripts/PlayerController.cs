using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 10; //Se pone public para que aparezca fuera, en el Unity y sea más cómodo.
    public float padding = 1f; //Esto es por que hay que añadir un pequeño margen a cada extremo, por que si no al hacer el CLAMP, si que deja salir la mitad del objeto.
    public float defaultYposition = -2f; //Posición Y por defecto. Es donde el player va a tender a ir.

    private Animator animator;

    private Rigidbody2D rigidBody2D;

    private enum Direccion {
        quieto,
        derecha,
        izquierda
    }

    private Direccion direccionActual;

    // Start is called before the first frame update
    void Start()
    {
        direccionActual = Direccion.quieto;
        animator = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();

        asignarEjeYPorDefecto();
    }

    // Update is called once per frame
    void Update() {


        moverCoche(direccionActual);

        movimientoPorDefecto();
    }

    private void moverCoche(Direccion direccion) {
        switch(direccion) {
            case Direccion.derecha:
                //Debug.Log("Se hace movimiento a la derecha");
                this.transform.position += new Vector3(Time.deltaTime * speed, 0, 0);
                // Clamping, para que no se salga de los margenes. Clamp recibe 3 números, si el primero no está entre los límites de después, lo pone al límite
                /*float newX = Mathf.Clamp(this.transform.position.x, -10 + padding, 10 - padding); //Esto no es por píxeles, es por unidades. Fuera en Unity, en la cámara, el campo "size", son las unidades a cada lado, es decir si es "10", sería 20x20 (se multiplica el size por 2)
                this.transform.position = new Vector3(newX, this.transform.position.y, this.transform.position.z);*/
                break;
            case Direccion.izquierda:
                //Debug.Log("Se hace movimiento a la izquierda");
                this.transform.position += new Vector3(-1 * Time.deltaTime * speed, 0, 0);
                // Clamping, para que no se salga de los margenes. Clamp recibe 3 números, si el primero no está entre los límites de después, lo pone al límite
                float newX2 = Mathf.Clamp(this.transform.position.x, -10 + padding, 10 - padding); //Esto no es por píxeles, es por unidades. Fuera en Unity, en la cámara, el campo "size", son las unidades a cada lado, es decir si es "10", sería 20x20 (se multiplica el size por 2)
                this.transform.position = new Vector3(newX2, this.transform.position.y, this.transform.position.z);
                break;
            default:
                break;
        }
    }

    //Este método debe ser llamado desde fuera, y es el que activa el movimiento.
    public void activarMovimientoDerecha() {
        Debug.Log("Se activa movimiento derecha (aa01)");
        direccionActual = Direccion.derecha;
        animator.SetTrigger("MoverseDerecha");
        animator.ResetTrigger("MoverseArriba");
        animator.ResetTrigger("MoverseIzquierda");
    }

    //Este método debe ser llamado desde fuera, y es el que activa el movimiento.
    public void activarMovimientoIzquierda() {
        Debug.Log("Se activa movimiento izquierda");
        direccionActual = Direccion.izquierda;
        animator.SetTrigger("MoverseIzquierda");
        animator.ResetTrigger("MoverseArriba");
        animator.ResetTrigger("MoverseDerecha");
    }

    //Este método debe ser llamado desde fuera, y es el que detiene el movimiento.
    public void desactivarMovimiento() {
        Debug.Log("Se desactiva movimiento.");
        direccionActual = Direccion.quieto;
        animator.SetTrigger("MoverseArriba");
        animator.ResetTrigger("MoverseDerecha");
        animator.ResetTrigger("MoverseIzquierda");
    }

    //En caso de que no esté en la posición del eje Y, por defecto se mueve hacia allá.
    private void movimientoPorDefecto() {
        rigidBody2D.velocity = Vector2.zero;

        float factorVelocidad = Time.deltaTime * speed / 10;

        if (this.transform.position.y > defaultYposition) {
            if(this.transform.position.y - factorVelocidad < defaultYposition) {
                asignarEjeYPorDefecto();
            } else {
                moverEjeY(-factorVelocidad);
            }
        }

        if (this.transform.position.y < defaultYposition) {
            if (this.transform.position.y + factorVelocidad > defaultYposition) {
                asignarEjeYPorDefecto();
            } else {
                moverEjeY(factorVelocidad);
            }
        }
    }

    //Deja los ejes X y Z como estén, pero el Y lo pone al valor por defecto.
    private void asignarEjeYPorDefecto() {
        this.transform.position = new Vector3(this.transform.position.x, defaultYposition, this.transform.position.z);
    }

    //Mover en el eje Y
    private void moverEjeY(float cantidad) {
        this.transform.position += new Vector3(0, +cantidad, 0);
    }
    private void OnTriggerEnter2D(Collider2D otherElement) {
        if (otherElement.gameObject.tag == "OwnTag_bottomBar") {
            Destroy(gameObject);
            Debug.Log("MUERTO");
        }
    }
}
