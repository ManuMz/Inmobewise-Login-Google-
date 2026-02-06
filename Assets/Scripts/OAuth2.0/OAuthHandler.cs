using UnityEngine;

public class OAuthHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnGoogleTokenReceived(UniWebViewAuthenticationGoogleToken token)
    {
        Debug.Log("Token received: " + token.AccessToken);
    }

    public void OnGoogleAuthError(long errorCode, string errorMessage)
    {
        Debug.Log("Error happened: " + errorCode + " " + errorMessage);
    }
}
