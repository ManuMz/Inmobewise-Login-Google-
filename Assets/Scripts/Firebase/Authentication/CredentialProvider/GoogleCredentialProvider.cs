using System;
using UnityEngine;

[Serializable]
public class GoogleCredentialData
{
    //Propiedades automaticas
    public string IdToken { get; set; } //Lectura & escritura

}

[Serializable]
public class ExceptionData
{

    public string Type { get; set; }
    public string Message { get; set; }

}

public class GoogleCredentialProvider: MonoBehaviour, ICredentialProvider
{
    private GoogleCredentialData googleCredentialData;

    //private const string gameObjectName = "GoogleCredentialProvider";
    private const string successMethodName = "OnSucessUserData";
    private const string exceptionMethodName = "OnExceptionUserData";
    private const string libraryName = "com.example.googlesignin.GoogleSignInActivity";
    private const string libraryMethod = "GetUserDataUnity";

    public GoogleCredentialData GoogleCredentialData => googleCredentialData;

    #region Java JavaBridge - por el momento sin uso
    //private const string unityClassObject = "com.unity3d.player.UnityPlayer";
    //private const string unityActivityObject = "currentActivity";
    //private const string callbackObject = "com.example.googlesignin.UserDataCallback";

    //traer Data para su uso en Unity
    //Implementar interfaces Java ->JavaBridge
    //class UserDataCallback : AndroidJavaProxy
    //{
    //    public UserDataCallback() : base(callbackObject) { }
    //    public void OnUserDataCallback(String data)
    //    {
    //        Debug.Log("Data recibida desde Java: " + data);
    //    }

    //}
    #endregion

    private void CreateGoogleCredential()
    {
        try
        {
            //Intancia del plugin Java con el que se obtiene GoogleCredential
            AndroidJavaObject library = new AndroidJavaObject(libraryName);

            //Envio de parametros al metodo java 
            //string[] param = new string[]{gameObjectName,successMethodName, exceptionMethodName};   

            library.Call(libraryMethod, gameObject.name, successMethodName, exceptionMethodName);

        }
        catch (Exception e)
        {
            Debug.LogError($"Error en CreateGoogleSignInRequest{e.Message}");
        }
    }

    public void OnSucessUserData(String data)
    {
        Debug.Log("Data recibida desde plugin Java: " + data);
    }

    public void OnExceptionUserData(String data)
    {
        Debug.Log("Error-excepcion recibido desde plugin Java: " + data);
        //Conversion del JSON
    }

    public void GetToken()
    {
        this.CreateGoogleCredential();
    }
}
