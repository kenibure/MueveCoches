using Firebase.Database;
using UnityEngine;

//Esta clase es necesaria para poder enviar parámetros de una Escena a otra ya que no hay manera de hacerlo. Se utilizan estas variables estáticas para ese fin.
public class StaticUtilities
{
    public static InfoNivel infoNivelSeleccionado { get; set; }

    public static bool firebaseLazado { get; set; } = false;

    public static Firebase.FirebaseApp firebaseApp { get; set; }

    public static bool esPrimeraVez { get; set; } = true;

    public static void asignarNivel(InfoNivel infoNivel)
    {
        infoNivelSeleccionado = infoNivel;
    }

    //Este método debe lanzarse una única vez.
    public static void lanzarFirebase()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                firebaseApp = Firebase.FirebaseApp.DefaultInstance;
                firebaseLazado = true;
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.

                firebaseLazado = false;
            }
        });
    }

    //En caso de que Firebase esté lanzado y por tanto se pueda enviar el dato devolverá "TRUE", en caso contrario devolverá "FALSE". Si devuelve FALSE significa que no  se ha enviado.
    public static bool enviarDatoAFireBase(string json, string nombreTabla, string id)
    {
        //Hay que verificar que Firebase se haya lanzado.
        if(firebaseLazado)
        {
            DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

            reference.Child(nombreTabla).Child(id).SetRawJsonValueAsync(json);

            return true;
        } else
        {
            return false;
        }
    }

    //Este es un método de ejemplo que envía datos a FireBase
    public static void ejemploDeEnvioDeDatosAFirebase()
    {
        TestFirebaseInfo info = new TestFirebaseInfo();
        info.color = "verde";
        info.nombre = "Ruben";
        info.numero = 17;
        info.timestamp = DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss-fff");
        info.id = "ID_" + info.timestamp;
        //info.id = "valorID2";
        string json = JsonUtility.ToJson(info);


        enviarDatoAFireBase(json, "infoTabla4", info.id);
    }
}
