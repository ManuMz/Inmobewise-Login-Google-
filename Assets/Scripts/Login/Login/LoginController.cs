using System.Collections;
using System.Globalization;
using System.Text.RegularExpressions;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{
    #region Events

    #endregion

    [Header("BOTONES")]
    [SerializeField]
    private Button signInButton;
    [SerializeField]
    private Button signInGoogleButton;
    [SerializeField]
    private Button backButton;
    [Header("Colores SignInButton")]
    [SerializeField]
    private string enabledSingInButtonColor;
    [SerializeField]
    private string disabledSignInButtonColor;

    private InputField userInput;//email o phoneNumber
    private InputField passwordInput;

    //Flags
    private bool supressValidation = false;
    private bool isValidUser = false;
    private bool isValidPassword = false;

    //Estados de animacion en InputFields
    //Password
    private const string IDLE_PASSWORDINPUTFIELD = "IdlePasswordInputField";
    private const string MESSAGE_PASSWORDINPUTFIELD = "MessagePasswordInputField";
    //User
    private const string IDLE_USERINPUTFIELD = "IdleUserInputField";
    private const string MESSAGE_USERINPUTFIED = "MessageUserInputField";


    //Dependencias 
    private ICredentialProvider credentialProvider; 


    private void Awake()
    {
        SetComponents();
        SetDependencies();
    }

    private void OnEnable()
    {
        ResetInputFields();
        CheckLogin();   
    }
    private void OnDisable()
    {
        UnChecklogin(); 
    }

    void Start()
    {
        SetStart();
    }

    void Update()
    {
        
    }
    private void SetStart()
    {
        signInButton.onClick.AddListener(() =>
        {
            ForceSignInValidation();
            if (isValidUser && isValidPassword)
            {
                SignIn();
            }
      
        });

        signInGoogleButton.onClick.AddListener(()
            => SignInGoogle()
            );
        backButton.onClick.AddListener(()
            =>RegisterHere()
            );
        //CredentialManager.OnLoginSucess.AddListener(OnLoginSuccess);
        //CredentialManager.OnLoginFailed.AddListener(OnLoginFailed);
    }
    private void SetComponents()
    {
        userInput = GetInputField(InputFieldType.loginUser);
        passwordInput = GetInputField(InputFieldType.loginPassword);
    }

    private void SetDependencies()
    {
        
        Initialize();
    }


    private void Initialize()
    {
        this.credentialProvider = new GameObject("GoogleCredentialProvider").AddComponent<GoogleCredentialProvider>(); 
    }

    private InputField GetInputField(InputFieldType type)
    {
        var inputFields = GetComponentsInChildren<InputFieldController>();

        foreach (InputFieldController inputField in inputFields)
        {
            if (inputField.InputFieldType == type)
            {
                //return inputField.GetInputValue();//el texto/valor del inputField
                return inputField.GetInputField();//obtener el objeto
            }
        }
        return null;
    }

    private void ResetInputFields()
    {
        supressValidation = true;
        var inputFields = GetComponentsInChildren<InputFieldController>();
        foreach (var inputField in inputFields)
        {
            inputField.SetInputValue(string.Empty);
        }

        supressValidation = false;
    }

    private void CheckLogin()
    {
        userInput.onValueChanged.AddListener(
            ValidateLoginUser
            );
        passwordInput.onValueChanged.AddListener(
            ValidateLoginPassword
            );
    }

    private void UnChecklogin()
    {
        userInput.onValueChanged.RemoveListener(
            ValidateLoginUser
            );
        passwordInput.onValueChanged.RemoveListener(
            ValidateLoginPassword
            );
    }

    private void ForceSignInValidation()
    {
        ValidateLoginUser(userInput.text);
        ValidateLoginPassword(passwordInput.text);
        OnSignInButtonEnabled();
    }

    private void ValidateLoginUser(string user) 
    {
        if (supressValidation) return;

        if (string.IsNullOrEmpty(user))
        {
            UIController.Instance.SetInputFieldMessage(
                InputFieldType.loginUser, "Campo requerido",
                MESSAGE_USERINPUTFIED);

            isValidUser = false;
        }
        else
        {
            UIController.Instance.SetInputFieldMessage(
                InputFieldType.loginUser, "",
                IDLE_USERINPUTFIELD);

            isValidUser = true;
        }
        OnSignInButtonEnabled();
    }

    private void ValidateLoginPassword(string password)
    {
        if (supressValidation) return;

        //requerimientos de contaseña segura

        string simplePattern = "^.{9,}$";
        //string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+}{"";'/?><.,])(?!.*\s).{9,}$";

        string advancedPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{9,}$";
        if (string.IsNullOrEmpty(password))
        {
            UIController.Instance.SetInputFieldMessage(
                InputFieldType.loginPassword, "Campo requerido",
                MESSAGE_PASSWORDINPUTFIELD);

            isValidPassword = false;
        }
        //else if (password.Length < 9)
        //{
        //    UIController.Instance.SetInputFieldMessage(
        //     InputFieldType.loginPassword, "Debe tener minimo 9 caracteres",
        //     MESSAGE_PASSWORDINPUTFIELD);
        //}
        else if (!Regex.IsMatch(password,simplePattern))
        {
            UIController.Instance.SetInputFieldMessage(
                InputFieldType.loginPassword, "Formato inválido",
                MESSAGE_PASSWORDINPUTFIELD);

            isValidPassword = false;
        }
        else
        {
            UIController.Instance.SetInputFieldMessage(
                InputFieldType.loginPassword, "",
                IDLE_PASSWORDINPUTFIELD);
            
            isValidPassword = true; 
        }
        OnSignInButtonEnabled();
    }

    //Ambos campos: User y Password sean TRUE
    private void OnSignInButtonEnabled()
    {
        bool isEnabled = isValidUser && isValidPassword;

        if (!isEnabled)
        {
            SignInButtonState(false,disabledSignInButtonColor);
        }
        else
        {
            SignInButtonState(true, enabledSingInButtonColor);
        }
    }

    private void SignInButtonState(bool param, string hexColor)
    {
        signInButton.interactable = param;

        if (UnityEngine.ColorUtility.TryParseHtmlString(hexColor,out Color imgColorHex)) {

            signInButton.GetComponent<Image>().color = imgColorHex;
        }
    }
    
    public void SignIn()
    {
        Debug.Log("Accion SignIn HABILITADA");
        //Consultar que el correo o numero existe 
        //Comprobar que la contraseña relacionada al usuario sea la correcta 
    }
    private void SignInGoogle()
    {
        this.GetToken();    

    }
    private void GetToken() //OBTENER TOKEN
    {
        credentialProvider.GetToken();
    }

    private void Authenticate()//Autenticar usuario
    {

    }

    public void RegisterHere()
    {
        UIController.Instance.ShowPanel(PanelType.register);
    }
}
