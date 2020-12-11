﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    private EnumEstadoPartida estadoPartida = EnumEstadoPartida.enMarcha;
    public GameObject enemyGenerator01;
    public GameObject point01Generator;
    public GameObject botonPausa;
    public GameObject menuPausa;
    public GameObject menuFinDelJuego;
    public GameObject player;
    public GameObject leftPannelColor; //Esta variable contiene el color del PANEL IZQUIERDO. Debe activarse al empezar la partida y al llegar la puntuación a "3" se desactivará.
    public GameObject rightPannelColor; //Esta variable contiene el color del PANEL DERECHO. Debe activarse al empezar la partida y al llegar la puntuación a "3" se desactivará.
    public Text labelPuntuation;
    public Text labelPuntuationFinal; //Esto es en el cartel de Fin del Juego.

    private int puntuacion = 0;


    void Start() {
        puntuacion = 0;
        asignarPuntuacionAlLabelNormal();
        asignarEstadoAPanelesDeColores(true);
    }

    public void cambiarEscena(string escenaDestino) {
        print("Cambiando a la escena " + escenaDestino);
        SceneManager.LoadScene(escenaDestino);
    }

    //Si el juego está en marcha lo pone en pausa, y si esta en pausa lo pone en marcha.
    public void cambiarEstadoPausa() {
        if(estadoPartida == EnumEstadoPartida.enMarcha) {
            lanzarMenuPausa();
        } else {
            cerrarMenuPausa();
        }

    }

    //Esto para el juego y lanza el menu de pausa.
    private void lanzarMenuPausa() {
        print("Se a a poner el juego en pausa.");

        //Desactiva el boton de pausa, por que ya se va a abrir el menu.
        desactivarBotonPausa();

        //Se activa el panel con el menu visual de pausa.
        menuPausa.SetActive(true);

        //Se paraliza todo.
        pausarTodo();

    }

    //Esto reanuda el juego y cierra el menu de pausa.
    private void cerrarMenuPausa() {

        //Se activa el boton de pausa, por que ya se va a cerrar el menu.
        activarBotonPausa();

        //Se desactiva el panel con el menu visual de pausa.
        menuPausa.SetActive(false);

        //Se reanuda todo
        reanudarTodo();

    }

    //Se encarga de dejar en pausa, pero NO lanza visualmente un menu de pausa, solo lo paraliza todo.
    private void pausarTodo() {
        estadoPartida = EnumEstadoPartida.pausa;
        enemyGenerator01.SendMessage("pauseLandscapeMovement");
        point01Generator.SendMessage("pauseLandscapeMovement");
        asignarVelocidadJuego(0);
    }

    //Se encarga de quitar la pausa, pero NO cierra visualmente un menu de pausa, solo lo reanuda todo.
    private void reanudarTodo() {
        estadoPartida = EnumEstadoPartida.enMarcha;
        enemyGenerator01.SendMessage("resumeLandscapeMovement");
        point01Generator.SendMessage("resumeLandscapeMovement");
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

    //Este método resetea todo para que el juego vuelva al estado inicial.
    public void resetearTodo() {
        menuPausa.SetActive(false);
        menuFinDelJuego.SetActive(false);
        activarBotonPausa();
        estadoPartida = EnumEstadoPartida.enMarcha;
        player.SendMessage("initPlayer");
        enemyGenerator01.SendMessage("initEnemyGenerator");
        point01Generator.SendMessage("initPointGenerator");
        puntuacion = 0;
        asignarPuntuacionAlLabelNormal();
        asignarEstadoAPanelesDeColores(true);

        asignarVelocidadJuego(1);
    }

    //Se llama a este metodo cuando se ha conseguido un punto.
    public void pointWon() {
        puntuacion++;
        asignarPuntuacionAlLabelNormal();
        enemyGenerator01.SendMessage("incrementGeneratorSpeed");
        if(puntuacion == 3) {
            asignarEstadoAPanelesDeColores(false);
        }
    }

    public void finDelJuego() {
        print("Fin del juego.");
        pausarTodo();
        desactivarBotonPausa();
        dejarLabelPuntuacionVacio();
        estadoPartida = EnumEstadoPartida.finDelJuego;
        eliminarElementosPorTag("OwnTag_enemy01");
        eliminarElementosPorTag("OwnTag_point01");
        asignarPuntuacionAlLabelDeFinDeJuego();

        menuFinDelJuego.SetActive(true);
    }

    public void reiniciarJuego() {
        resetearTodo();
    }

    private void activarBotonPausa() {
        botonPausa.SetActive(true);
    }

    private void desactivarBotonPausa() {
        botonPausa.SetActive(false);
    }

    private void eliminarElementosPorTag(string tag) {

        var clones = GameObject.FindGameObjectsWithTag(tag);
        foreach (var clone in clones) {
            Destroy(clone);
        }
    }

    private void asignarPuntuacionAlLabelNormal() {
        labelPuntuation.text = puntuacion + "";
    }

    private void asignarPuntuacionAlLabelDeFinDeJuego() {
        labelPuntuationFinal.text = puntuacion + "";
    }

    //Este método deja el Label de la puntuación vacío. La puntuación se mantiene en la variable "puntuacion".
    private void dejarLabelPuntuacionVacio() {
        labelPuntuation.text = "";
    }

    //Estos paneles son los que aparecen a la derecha e izquierda marcando donde debe pulsar el usuario para mover al personaje. La idea es que se desactiven al cambio de X puntos.
    private void asignarEstadoAPanelesDeColores(bool nuevoEstado) {
        leftPannelColor.SetActive(nuevoEstado);
        rightPannelColor.SetActive(nuevoEstado);
    }
}
