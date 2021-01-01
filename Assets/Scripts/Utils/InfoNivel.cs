using System.Collections;
using System.Collections.Generic;

//Esta clase contiene la información necesaria para un nivel.
public class InfoNivel
{

    public InfoEnemigo01 enemigo01 { get; set; }
    public InfoEnemigo02 enemigo02 { get; set; }
    public float velocidadDeJuego { get; set; }

    //En el constructor por defecto deja todos los enemigos a FALSE, de modo que si no se añaden no se generarán.
    public InfoNivel()
    {
        enemigo01 = new InfoEnemigo01();
        enemigo01.debeGenerarse = false;

        enemigo02 = new InfoEnemigo02();
        enemigo02.debeGenerarse = false;
    }

}
