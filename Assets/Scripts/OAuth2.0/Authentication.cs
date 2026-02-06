using UnityEngine;

public class Authentication :Singleton<Authentication>
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GoogleAuth()//inicia el flujo de autenticacion google
    {
        var googleFlow = GetComponent<UniWebViewAuthenticationFlowGoogle>();
        googleFlow.StartAuthenticationFlow();
    }

    public void SetGoogleAuth()
    {
        this.GoogleAuth();  
    }
}
