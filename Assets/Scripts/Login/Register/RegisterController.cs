using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

public class RegisterController : MonoBehaviour
{

    #region EVENTS
    //Eventos personalizados
    //Eventos estandar
    #endregion

    //[Header("INPUTFIELDS")]
    //Variables Globales de la clase referentes a InputFields
    private InputField fullNameInput;
    private InputField userNameInput;
    private InputField emailInput;
    private InputField phoneNumberInput;
    private InputField passwordInput;
    private InputField confirmPasswordInput;

    //Estados de animacion en InputFields
    //FullName
    private const string IDLE_FULLNAMEINPUTFIELD = "IdleFullnameInputField";
    private const string MESSAGE_FULLNAMEINPUTFIELD = "MessageFullnameInputField";
    //UserName
    private const string IDLE_USERNAMEINPUTFIELD = "IdleUserNameInputField";
    private const string MESSAGE_USERNAMEDINPUTFIELD = "MessageUserNameInputField";
    //Email
    private const string IDLE_EMAILINPUTFIELD = "IdleEmailInputField";
    private const string MESSAGE_EMAILINPUTFIELD = "MessageEmailInputField";
    //PhoneNumber
    private const string IDLE_PHONENUMBERINPUTFIELD = "IdlePhoneNumberInputField";
    private const string MESSAGE_PHONENUMBERINPUTFIELD = "MessagePhoneNumberInputField";
    //Password
    private const string IDLE_PASSWORDINPUTFIELD = "IdlePasswordInputField";
    private const string MESSAGE_PASSWORDINPUTFIELD = "MessagePasswordInputField";
    //ResetPassword
    private const string IDLE_CONFIRMPASSWORDINPUTFIELD = "IdleConfirmPasswordInputField";
    private const string MESSAGE_CONFIRMPASSWORDINPUTFIELD = "MessageConfirmPasswordInputField";

    //Flags
    private bool supressValidation = false;

    private void Awake()
    {
        SetComponents();
    }

    private void OnEnable()
    {
       ResetInputFields();
        CheckRegister();    
    }
    void Start()
    {
        
    }

    void Update()
    {

    }

    private void SetStart()
    {
       
    }

    private void SetComponents()
    {
        fullNameInput = GetInputField(
            InputFieldType.fullNameRegister);
        userNameInput = GetInputField(
            InputFieldType.userNameRegister);
        emailInput = GetInputField(
            InputFieldType.emailRegister);
        //phoneNumberInput = GetInputField(
        //    InputFieldType.phoneNumberRegister);
        //passwordInput = UIController.Instance.GetInputField(
        //    InputFieldType.passwordRegister);
        //confirmPasswordInput = UIController.Instance.GetInputField(
        //    InputFieldType.confirmPasswordRegister
        //    );
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

    private void CheckRegister()
    {
        fullNameInput.onValueChanged.AddListener(
                ValidateFullName
            );
        userNameInput.onValueChanged.AddListener(
            ValidateUserName
            );
        emailInput.onValueChanged.AddListener(
            ValidateEmail
            );
        //phoneNumberInput.onValueChanged.AddListener(
        //    ValidatePhoneNumber
        //    );
    }
    private void UnCheckRegister()
    {
        fullNameInput.onValueChanged.RemoveListener(
                ValidateFullName
            );
        userNameInput.onValueChanged.RemoveListener(
            ValidateUserName
            );
        emailInput.onValueChanged.RemoveListener(
            ValidateEmail
            );
        //phoneNumberInput.onValueChanged.RemoveListener(
        //    ValidatePhoneNumber
        //    );
    }

    private void ValidateFullName(string fullName)
    {
        if (supressValidation) return;

        if (string.IsNullOrEmpty(fullName)) 
        {
            UIController.Instance.SetInputFieldMessage(
              InputFieldType.fullNameRegister, "Campo requerido",
              MESSAGE_FULLNAMEINPUTFIELD);
            //return false;
        }
        else
        {
            UIController.Instance.SetInputFieldMessage(
             InputFieldType.fullNameRegister, "",
             IDLE_FULLNAMEINPUTFIELD);
            //return true;
        }
    }
    private void ValidateUserName(string userName)
    {
        if (supressValidation) return;

        if (string.IsNullOrEmpty(userName))
        {
            UIController.Instance.SetInputFieldMessage(
               InputFieldType.userNameRegister, "Campo requerido",
               MESSAGE_USERNAMEDINPUTFIELD);
            //return false;

        }
        else if (userName.Length<4)
        {
            UIController.Instance.SetInputFieldMessage(
              InputFieldType.userNameRegister, "Debe tener minimo 4 caracteres",
              MESSAGE_USERNAMEDINPUTFIELD);
            //return false;
        }
        else
        {
            UIController.Instance.SetInputFieldMessage(
              InputFieldType.userNameRegister, "",
              IDLE_USERNAMEINPUTFIELD);
            //return true;
        }
    }
    private void ValidateEmail(string email)
    {
        if (supressValidation) return;

        var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        var regex = new Regex(pattern, RegexOptions.IgnoreCase);


        if (!regex.IsMatch(email))
        {

            UIController.Instance.SetInputFieldMessage(
               InputFieldType.emailRegister, "Formato inválido",//$"{email} is not a valid email address."
               MESSAGE_EMAILINPUTFIELD
               );
        }

        else if (string.IsNullOrEmpty(email))
        {
            UIController.Instance.SetInputFieldMessage(
                InputFieldType.emailRegister, "Campo requerido",
                MESSAGE_EMAILINPUTFIELD
                );
        }
        else
        {
            UIController.Instance.SetInputFieldMessage(
                InputFieldType.emailRegister, "",
                IDLE_EMAILINPUTFIELD
                );
        }


    }

    private void ValidatePhoneNumber(string phoneNumber)
    {
        //var letters = phoneNumber.Split(',');
        var letters = Regex.Replace(phoneNumber, @"\d", "");
        
        if (letters.Length>0)
        {
            UIController.Instance.SetInputFieldMessage(
                 InputFieldType.phoneNumberRegister, "Este campo no permite letras",
                 MESSAGE_PHONENUMBERINPUTFIELD
                 );
        }
        else if (phoneNumber.Length<10)
        {
            //Debe tener 10 caracteres
            UIController.Instance.SetInputFieldMessage(
                 InputFieldType.phoneNumberRegister, "Debe tener 10 caracteres",
                 MESSAGE_PHONENUMBERINPUTFIELD
                 );
        }
        else if (phoneNumber.Length>10)
        {
            UIController.Instance.SetInputFieldMessage(
                 InputFieldType.phoneNumberRegister, "No puedes tener mas de 10 caracteres",
                 MESSAGE_PHONENUMBERINPUTFIELD
                 );
        }
        else if (string.IsNullOrEmpty(phoneNumber))
        {
            UIController.Instance.SetInputFieldMessage(
                 InputFieldType.phoneNumberRegister, "Campo requerido",
                 MESSAGE_PHONENUMBERINPUTFIELD
                 );
        }
        else
        {
            UIController.Instance.SetInputFieldMessage(
                InputFieldType.phoneNumberRegister, "",
                IDLE_PHONENUMBERINPUTFIELD
                );
        }
    }

    private void ValidatePassword(string password)
    {

    }
    private void ConfirmPassword(string confirmpassword)
    {

    }

    private void OnDisable()
    {
        UnCheckRegister();
    }
}
