using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    private EnumEstadoPartida estadoPartida = EnumEstadoPartida.enMarcha;

    public void cambiarEscena(string escenaDestino) {
        print("Cambiando a la escena " + escenaDestino);
        SceneManager.LoadScene(escenaDestino);
    }

    //Si el juego está en marcha lo pone en pausa, y si esta en pausa lo pone en marcha.
    public void cambiarEstadoPausa() {
        if(estadoPartida == EnumEstadoPartida.enMarcha) {
            ponerElJuegoEnPausa();
        } else {
            quitarPausa();
        }

    }

    private void ponerElJuegoEnPausa() {
        print("Se a a poner el juego en pausa.");
        estadoPartida = EnumEstadoPartida.pausa;
        asignarVelocidadJuego(0);
    }

    private void quitarPausa() {
        print("Se a a quitar pausa.");
        estadoPartida = EnumEstadoPartida.enMarcha;
        asignarVelocidadJuego(1);
    }

    public EnumEstadoPartida getEstadoPartida() {
        return estadoPartida;
    }

    //0 = juego parado
    //1 = velocidad normal
    //10 = 10 x velocidad normal
    private void asignarVelocidadJuego(float velocidad) {
        Time.timeScale = velocidad;
    }

    //Este método resetea todo lo que haga falta para que si se relanza el juego todo empiece. Debe lanzarse al salir.
    public void resetearTodo() {
        quitarPausa();
    }

}
