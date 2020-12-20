using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class Controlador_EscenaInicial : MonoBehaviour {

    BannerView bannerView;

    public void Start() {
        lanzarBanner();
    }
    public void cambiarEscena(string escenaDestino) {
        print("Cambiando a la escena " + escenaDestino);
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
