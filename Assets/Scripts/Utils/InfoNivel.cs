using System.Collections;
using System.Collections.Generic;

//Esta clase contiene la información necesaria para un nivel.
public class InfoNivel
{
    public string idNivel { get; set; }
    public string labelNivel { get; set; }
    public float velocidadDeJuego { get; set; }

    public bool colocarPanelesDeColores { get; set; } //Esto es para mostrar los paneles de colores a los lados al inicio de la partida.

    public InfoEnemigo01 enemigo01 { get; set; }
    public InfoEnemigo02 enemigo02 { get; set; }

    //En el constructor por defecto deja todos los enemigos a FALSE, de modo que si no se añaden no se generarán.
    public InfoNivel()
    {
        idNivel = null;
        labelNivel = null;
        velocidadDeJuego = 1;
        colocarPanelesDeColores = true;

        enemigo01 = new InfoEnemigo01();
        enemigo01.debeGenerarse = false;

        enemigo02 = new InfoEnemigo02();
        enemigo02.debeGenerarse = false;
    }

}
