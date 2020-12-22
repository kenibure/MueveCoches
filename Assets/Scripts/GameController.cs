using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class GameController : MonoBehaviour {

    private EnumEstadoPartida estadoPartida = EnumEstadoPartida.enMarcha;
    public GameObject enemyGenerator01;
    public GameObject enemyGenerator02;
    public GameObject point01Generator;
    public GameObject botonPausa;
    public GameObject menuPausa;
    public GameObject menuFinDelJuego;
    public GameObject player;
    public GameObject leftPannelColor; //Esta variable contiene el color del PANEL IZQUIERDO. Debe activarse al empezar la partida y al llegar la puntuación a "3" se desactivará.
    public GameObject rightPannelColor; //Esta variable contiene el color del PANEL DERECHO. Debe activarse al empezar la partida y al llegar la puntuación a "3" se desactivará.
    public Text labelPuntuation;
    public Text labelPuntuationFinal; //Esto es en el cartel de Fin del Juego.
    public Text labelRecordEnFinDelJuego; //Esto es en el cartel de Fin del Juego.
    public AudioClip pointSound;
    public AudioClip deathSound;
    public Text labelRecordEnPausa; //Esto es en el menu de pausa.

    private AudioSource audioSource; //Este elemento está creado como "Component" en el "Controller". Se asigna en el "Start()".

    private BannerView bannerViewPausa;
    private BannerView bannerViewFinDelJuego;
    private BannerView bannerViewInicioPartida;
    

    private int puntuacion = 0;

    private string playerPrefs_PuntuacionMaxima = "Puntuacion_Maxima";
    private string playerPrefs_SonidosActivos = "Sonidos_Activos_1=TRUE;0=FALSE";
    private string tag_ToggleSound = "OwnTag_ToggleSound";

    private string adMobsApp_miAplicacion = "ca-app-pub-8067831116754417~1698628271";
    private string adMobsApp_anuncioPausa = "ca-app-pub-8067831116754417/4655237756";
    private string adMobsApp_anuncioFinDelJuego = "ca-app-pub-8067831116754417/4655237756";
    private string adMobsApp_anuncioInicioPartida = "ca-app-pub-8067831116754417/6836115323";


    void Start() {
        incializadoresDePublicidad();
        lanzarBannerPublicidadInicioPartida();
        puntuacion = 0;
        asignarPuntuacionAlLabelNormal();
        asignarEstadoAPanelesDeColores(true);
        audioSource = GetComponent<AudioSource>();
        if (sonidosActivos()) {
            activarSonidos();
        } else {
            mutearSonidos();
        }
    }

    public void cambiarEscena(string escenaDestino) {
        print("Cambiando a la escena " + escenaDestino);
        cerrarTodasPublicidades();
        SceneManager.LoadScene(escenaDestino);
    }

    //Si el juego está en marcha lo pone en pausa, y si esta en pausa lo pone en marcha.
    public void cambiarEstadoPausa() {
        if (estadoPartida == EnumEstadoPartida.enMarcha) {
            lanzarMenuPausa();
        } else {
            cerrarMenuPausa();
        }

    }

    //Esto para el juego y lanza el menu de pausa.
    private void lanzarMenuPausa() {
        print("Se a a poner el juego en pausa.");

        lanzarBannerPublicidadEnPausa();

        //Desactiva el boton de pausa, por que ya se va a abrir el menu.
        desactivarBotonPausa();

        //Se activa el panel con el menu visual de pausa.
        menuPausa.SetActive(true);

        //Se recarga el valor de la puntuación máxima.
        asignarPuntuacionMaximaAlLabelDelRecord();

        //Se asigna el estado al Toggle del sonido.
        GameObject gameObject = GameObject.FindGameObjectsWithTag(tag_ToggleSound)[0];
        gameObject.GetComponent<Toggle>().isOn = sonidosActivos();


        //Se paraliza todo.
        pausarTodo();

    }

    //Esto reanuda el juego y cierra el menu de pausa.
    private void cerrarMenuPausa() {

        //Se activa el boton de pausa, por que ya se va a cerrar el menu.
        activarBotonPausa();

        //Se desactiva el panel con el menu visual de pausa.
        menuPausa.SetActive(false);

        eliminarAnuncioPausa();

        //Se reanuda todo
        reanudarTodo();

    }

    //Se encarga de dejar en pausa, pero NO lanza visualmente un menu de pausa, solo lo paraliza todo.
    private void pausarTodo() {
        estadoPartida = EnumEstadoPartida.pausa;
        enemyGenerator01.SendMessage("pauseLandscapeMovement");
        enemyGenerator02.SendMessage("pauseLandscapeMovement");
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
        enemyGenerator02.SendMessage("initEnemyGenerator");
        point01Generator.SendMessage("initPointGenerator");
        puntuacion = 0;
        asignarPuntuacionAlLabelNormal();
        asignarEstadoAPanelesDeColores(true);
        playMusicaEnCurso();
        cerrarTodasPublicidades();

        asignarVelocidadJuego(1);
    }

    //Se llama a este metodo cuando se ha conseguido un punto.
    public void pointWon() {
        reproducirSonidoUnaVez(pointSound);
        puntuacion++;
        asignarPuntuacionAlLabelNormal();
        enemyGenerator01.SendMessage("incrementGeneratorSpeed");
        if (puntuacion == 3) {
            asignarEstadoAPanelesDeColores(false);
        }

        if(puntuacion >= 15) {
            if(puntuacion % 10 != 0 && puntuacion % 5 == 0) {
                invocarEnemigo02(); //Hay que tener en cuenta que el enemigo02 no está embuclado pero justo al destruirse invoca a otro por lo que es como un bucle. Creará uno de estos bucles cada 10 puntos a partir de 15.
            }
        }

        if(puntuacion == 15) {
            eliminarAnuncioInicioPartida();
        }
    }

    //Esto se lanza cuando se acaba la partida.
    public void finDelJuego() {
        print("Fin del juego.");
        cerrarTodasPublicidades();
        lanzarBannerPublicidadInicioPartida();
        lanzarBannerPublicidadEnFinDelJuego();
        reproducirSonidoUnaVez(deathSound);
        pausarTodo();
        desactivarBotonPausa();
        dejarLabelPuntuacionVacio();
        estadoPartida = EnumEstadoPartida.finDelJuego;
        eliminarElementosPorTag("OwnTag_enemy01");
        eliminarElementosPorTag("OwnTag_enemy02");
        eliminarElementosPorTag("OwnTag_point01");
        guardarResultadoSiEsMejor(puntuacion);
        asignarPuntuacionAlLabelDeFinDeJuego();
        stopMusicaEnCurso();


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
        labelRecordEnFinDelJuego.text = recuperarPuntuacionMaxima() + "";
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

    //Recibe un AudioClip y lo reproduce una sola vez. De este modo no se machaca el sonido de fondo.
    public void reproducirSonidoUnaVez(AudioClip audioClip) {
        if (sonidosActivos()) {
            audioSource.PlayOneShot(audioClip);
        }
    }

    //Al llamar a este método se activan o desactivan los sonidos. Recibe como parámetro el TAG del Toggle que marca si deben estar activos o no.
    public void activarDesactivarSonidos(string toggleTag) {
        GameObject gameObject = GameObject.FindGameObjectsWithTag(toggleTag)[0];
        bool soundOn = gameObject.GetComponent<Toggle>().isOn;

        if (soundOn == true) {
            activarSonidos();
        } else {
            mutearSonidos();
        }
    }

    //Coloca a TRUE la variable que marca si los sonidos deben reproducirse o no.
    private void activarSonidos() {
        audioSource.mute = false;
        PlayerPrefs.SetInt(playerPrefs_SonidosActivos, 1);
    }

    //Coloca a FALSE la variable que marca si los sonido deben reproducirse o nos del PlayerPrefs y detiene los posibles sonidos que se estén reproduciendo.
    private void mutearSonidos() {
        audioSource.mute = true;
        PlayerPrefs.SetInt(playerPrefs_SonidosActivos, 0);
    }

    //Para la música que se está reproduciendo, pero no mutea nada.
    private void stopMusicaEnCurso() {
        audioSource.Stop();
    }

    //Le da "Play" al reproductor de audio.
    private void playMusicaEnCurso() {

        if (!audioSource.isPlaying) {
            audioSource.Play();
        }
        if (!sonidosActivos()) {
            mutearSonidos();
        }
    }

    //Recibe el número de puntos (nuevoResultado), y si es superior al último récord lo guarda como al.
    private void guardarResultadoSiEsMejor(int nuevoResultado) {
        if (nuevoResultado > recuperarPuntuacionMaxima()) {
            PlayerPrefs.SetInt(playerPrefs_PuntuacionMaxima, nuevoResultado);
        }
    }

    //Devuelve la puntuación máxima que hay guardada.
    private int recuperarPuntuacionMaxima() {
        return PlayerPrefs.GetInt(playerPrefs_PuntuacionMaxima, 0);
    }

    //Recupera la puntuación máxima y la asigna al Label que hay dentro del Menu pausa.
    private void asignarPuntuacionMaximaAlLabelDelRecord() {
        labelRecordEnPausa.text = recuperarPuntuacionMaxima().ToString();
    }

    private bool sonidosActivos() {
        if (PlayerPrefs.GetInt(playerPrefs_SonidosActivos, 1) == 1) {
            return true;
        } else {
            return false;
        }
    }

    private void lanzarBannerPublicidadEnPausa() {

        bannerViewPausa = new BannerView(adMobsApp_anuncioPausa, AdSize.Banner, AdPosition.Bottom);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerViewPausa.LoadAd(request);
    }

    private void lanzarBannerPublicidadEnFinDelJuego() {

        bannerViewFinDelJuego = new BannerView(adMobsApp_anuncioFinDelJuego, AdSize.Banner, AdPosition.Bottom);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerViewFinDelJuego.LoadAd(request);
    }

    private void lanzarBannerPublicidadInicioPartida() {

        bannerViewInicioPartida = new BannerView(adMobsApp_anuncioInicioPartida, AdSize.Banner, AdPosition.Top);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerViewInicioPartida.LoadAd(request);
    }

    //Esto debe llamarse al comienzo.
    private void incializadoresDePublicidad() {

        MobileAds.Initialize(initStatus => { });

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(adMobsApp_miAplicacion);

    }

    private void eliminarAnuncioPausa() {
        if (bannerViewPausa != null) {
            bannerViewPausa.Destroy();
        }
    }

    private void eliminarAnuncioFinDelJuego() {
        if (bannerViewFinDelJuego != null) {
            bannerViewFinDelJuego.Destroy();
        }
    }

    private void eliminarAnuncioInicioPartida() {
        if (bannerViewInicioPartida != null) {
            bannerViewInicioPartida.Destroy();
        }
    }

    //Este método destruye manualmente una a una todos los banners.
    private void cerrarTodasPublicidades() {
        eliminarAnuncioPausa();
        eliminarAnuncioFinDelJuego();
        eliminarAnuncioInicioPartida();
    }

    //El Enemigo02 no está enbuclado si no que debe ser invocado. El propio Enemigo02 tiene dentro la llamada al siguiente Enemigo02 cuando se destruya, por lo que se irán generando infinitamente.
    public void invocarEnemigo02() {
        enemyGenerator02.SendMessage("generarEnemigo");
    }
}
