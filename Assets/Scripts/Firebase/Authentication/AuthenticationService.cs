using UnityEngine;

public class AuthenticationService
{
    //Forma de autenticacion (cliente)
    private readonly IAuthenticationService _authenticationService;
    //Constructor
    public AuthenticationService(IAuthenticationService authenticationService)//Inyeccion de dependencia por medio del constructor
    {
        this._authenticationService = authenticationService;    
    }

    public void Authenticate(string token)
    {
        _authenticationService.Authenticate(token);
    }
}
