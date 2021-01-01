using System.Collections;
using System.Collections.Generic;

//Esta clase es necesaria para poder enviar parámetros de una Escena a otra ya que no hay manera de hacerlo. Se utilizan estas variables estáticas para ese fin.
public class StaticUtilities
{
    public static InfoNivel infoNivelSeleccionado { get; set; }

    public static void asignarNivel(InfoNivel infoNivel)
    {
        infoNivelSeleccionado = infoNivel;
    }
}
