using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
using System;

public class Controlador_EscenaInicial : MonoBehaviour {

    BannerView bannerView;

    public void Start() {

        //Por aqui solo entrará la primera vez que se invoque la Escena Inicial.
        if(StaticUtilities.esPrimeraVez)
        {
            StaticUtilities.lanzarFirebase();
            StaticUtilities.ejemploDeEnvioDeDatosAFirebase();
            StaticUtilities.esPrimeraVez = false;
        }
        lanzarBanner();
    }
    public void cambiarEscena(string escenaDestino) {
        print("Cambiando a la escena " + escenaDestino);
        StaticUtilities.asignarNivel(ListadoNiveles.nivel01());
        eliminarAnuncio();
        SceneManager.LoadScene(escenaDestino);
    }

    //Este método se encarga de cerrar la aplicación.
    public void salirDelJuego() {
        print("Cerrando el juego.");
        eliminarAnuncio();
        Application.Quit();
    }

    //Este método lanza un banner de publicidad de AdMob que se quedará aunque se cambie de escena.
    private void lanzarBanner() {
        string adMobsApp_miAplicacion = "ca-app-pub-8067831116754417~1698628271";
        string adMobsApp_anuncio01 = "ca-app-pub-8067831116754417/7932670611";

        MobileAds.Initialize(initStatus => { });

        bannerView = new BannerView(adMobsApp_anuncio01, AdSize.Banner, AdPosition.Bottom);

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(adMobsApp_miAplicacion);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);
    }

    private void eliminarAnuncio() {
        bannerView.Destroy();
    }


}
