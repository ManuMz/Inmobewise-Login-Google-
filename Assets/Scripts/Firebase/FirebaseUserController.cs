using System;
using Cysharp.Threading.Tasks;
using UnityEngine;


[Serializable]
public class FirebaseUserData //MODELO
{
    //Propiedades automaticas 
    public string DisplayName { get; set; }
    public string Email { get; set; }   
    public string PhoneNumber { get; set; }
    public System.Uri PhotoUrl { get; set; }
    public string UserId { get; set; }

    //Constructor
    //public FirebaseUserData(string displayName, string email, string phoneNumber, System.Uri photoUrl,
    //    string userId)
    //{
    //    DisplayName = displayName;
    //    Email = email;
    //    PhoneNumber = phoneNumber;
    //    PhotoUrl = photoUrl;
    //    UserId = userId;    
    //}
}


public class FirebaseUserController : MonoBehaviour
{
    //La clase FirebaseUserController es un suscriptor de los eventos
    //

    //private const string urlServices = "https://arvispace.com/InmobewiseApp/servicios/";/*https://arvispace.com/InmobewiseApp/services/ */ Produccion

    private string urlServices = "http://localhost/InmobewiseApp/services/ ";//Local


    //reduccion de acoplamiento de clase FirebaseManager
    private IFirebaseServices _signInService;

    //Constructor de la clase FirebaseController
    //Inyeccion de dependencias por Constructor
    public FirebaseUserController(IFirebaseServices signInService)
    {
       this._signInService = signInService;
    }


    private void Awake()
    {
        //Suscripcion a Evento FirebaseUserStateChanged
        FirebaseManager.Instance.FirebaseUserStateChanged += FirebaseManager_FirebaseUserStateChanged;
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }
    void Start()
    {
        
    }

    private void SuscribeEvents()
    {

    }
    private void DesuscribeEvents()
    {

    }
    /// <summary>
    /// Metodo que se ejecuta cuando ocurre el evento FirebaseUserStateChanged
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void FirebaseManager_FirebaseUserStateChanged(object sender, bool state)
    {
        
        Debug.Log($"Firebaser User State ha cambiado a {state} ");
        
        this.GetFirebaseUser();
    }

    private void ValidateUserSigned()
    {
        //Si está registrado 

        //Si el usuario ingresa por primera vez -> se completa su registro en la BD -> pasa a la secuencia de carga de la escena buildingScene 

        //Si ya está registrado, pasa a la secuencia de carga de la escena BuildingScene 
    }

    private void GetFirebaseUser()
    {
        var firebaseCurrentUser = this._signInService.GetUser();

        Debug.Log("Datos del usuario actual");
        Debug.Log($"Nombre:{firebaseCurrentUser.DisplayName} ");
        Debug.Log($"Email:{firebaseCurrentUser.Email} ");
        Debug.Log($"Telefono:{firebaseCurrentUser.PhoneNumber} ");
    }


    #region Async Methods
    private async UniTask ValidateFirebaseUserSigned()
    {
        string path = urlServices + "";
        FirebaseUserData firebaseUserData = new FirebaseUserData();
        WWWForm form = new WWWForm();
        form.AddField("email", firebaseUserData.Email);

        await RESTApi.HTTPGET(path);
    }

    private async UniTask RegisterFirebaseUser()
    {
        string path = urlServices + "";
        FirebaseUserData firebaseUserData = new FirebaseUserData();
        WWWForm form = new WWWForm();
        form.AddField("displayName", firebaseUserData.DisplayName);
        form.AddField("email", firebaseUserData.Email);
        form.AddField("phoneNumber", firebaseUserData.PhoneNumber);
        form.AddField("userId", firebaseUserData.UserId);

        await RESTApi.HTTPPOST(path, form);

    }
    #endregion

    private void OnDestroy()
    {   
        //Desuscribir evento
        FirebaseManager.Instance.FirebaseUserStateChanged -= FirebaseManager_FirebaseUserStateChanged;
    }

}
