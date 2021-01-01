using System.Collections;
using System.Collections.Generic;

public class InfoEnemigo01
{
    public bool debeGenerarse { get; set; } //TRUE = debe existir. FALSE = no debe generarse.
    public float startGeneratorTimer { get; set; } //El tiempo que hay INICIALMENTE entre cada creación de enemigos. (Se irá reduciendo al ir consiguiendo puntos).

    public float generatorSpeedIncrementorFactor { get; set; }
    public float generatorSpeedLimiter { get; set; } //Es la velocidad límite a la que se pueden generar enemigos más rápido que esto (número más bajo). Una vez llega ahí el incremento de la velocidad es mucho más lento.

}
