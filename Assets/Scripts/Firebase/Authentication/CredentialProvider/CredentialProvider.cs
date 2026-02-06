using UnityEngine;


public interface ICredentialProvider
{
    void GetToken();
}
public class CredentialProvider
{
    private readonly ICredentialProvider _credentialProvider;
    
    //Constructor
    public CredentialProvider(ICredentialProvider credentialProvider)//Inyeccion de dependencia por medio del constructor
    {
        this._credentialProvider = credentialProvider;  
    }
    public void GetToken()
    {
        _credentialProvider.GetToken();
    }
}
