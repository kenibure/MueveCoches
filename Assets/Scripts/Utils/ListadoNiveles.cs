using System.Collections;
using System.Collections.Generic;

public class ListadoNiveles
{
    public static InfoNivel nivel00()
    {
        InfoEnemigo01 infoEnemigo01 = new InfoEnemigo01();
        infoEnemigo01.debeGenerarse = false;

        InfoEnemigo02 infoEnemigo02 = new InfoEnemigo02();
        infoEnemigo02.debeGenerarse = false;

        InfoNivel infoNivel = new InfoNivel();
        infoNivel.velocidadDeJuego = 1;
        infoNivel.enemigo01 = infoEnemigo01;
        infoNivel.enemigo02 = infoEnemigo02;

        return infoNivel;
    }
    public static InfoNivel nivel01()
    {
        InfoEnemigo01 infoEnemigo01 = new InfoEnemigo01();
        infoEnemigo01.debeGenerarse = true;
        infoEnemigo01.startGeneratorTimer = 1f;
        infoEnemigo01.generatorSpeedIncrementorFactor = 0.01f;
        infoEnemigo01.generatorSpeedLimiter = 0.75f;

        InfoEnemigo02 infoEnemigo02 = new InfoEnemigo02();
        infoEnemigo02.debeGenerarse = true;

        InfoNivel infoNivel = new InfoNivel();
        infoNivel.velocidadDeJuego = 1;
        infoNivel.enemigo01 = infoEnemigo01;
        infoNivel.enemigo02 = infoEnemigo02;

        return infoNivel;
    }
    public static InfoNivel nivel02()
    {
        InfoEnemigo01 infoEnemigo01 = new InfoEnemigo01();
        infoEnemigo01.debeGenerarse = true;
        infoEnemigo01.startGeneratorTimer = 1f;
        infoEnemigo01.generatorSpeedIncrementorFactor = 0.03f;
        infoEnemigo01.generatorSpeedLimiter = 0.73f;

        InfoEnemigo02 infoEnemigo02 = new InfoEnemigo02();
        infoEnemigo02.debeGenerarse = true;

        InfoNivel infoNivel = new InfoNivel();
        infoNivel.velocidadDeJuego = 1.5f;
        infoNivel.enemigo01 = infoEnemigo01;
        infoNivel.enemigo02 = infoEnemigo02;

        return infoNivel;
    }
}
