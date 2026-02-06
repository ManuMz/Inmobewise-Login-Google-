using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GoogleAuthenticationService : IAuthenticationService
{
    Firebase.Auth.FirebaseAuth auth;
    private void InitializeAuth()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;//Acceso a la API
    }

    public void Authenticate(string token)
    {
        try
        {
            InitializeAuth();
            var credential = Firebase.Auth.GoogleAuthProvider.GetCredential(token, null);

            auth.SignInAndRetrieveDataWithCredentialAsync(credential).ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("SignInAndRetrieveDataWithCredentialAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("SignInAndRetrieveDataWithCredentialAsync encountered an error: " + task.Exception);
                    return;
                }

                Firebase.Auth.AuthResult result = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                    result.User.DisplayName, result.User.UserId); //FirebaseUser
            });
        }
        catch (Exception e)
        {
            Debug.LogError("Excepcion en metodo Authenticate de GoogleAuthService: " + e.Message);
        }
    }
}
