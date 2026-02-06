using UnityEngine;
using UnityEngine.UI;

public class FirebaseAuthFormController : MonoBehaviour
{
    
    [Header("InputFields")]
    [SerializeField] 
    private InputField fullnameInput;
    [SerializeField]
    private InputField usernameInput;
    [SerializeField]
    private InputField phoneNumberInput;
    [SerializeField]
    private InputField emailInput;

    private void Awake()
    {
        
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

    //Establecer el contenido de los campos fullname, username
    private void SetInputFieldsContent()
    {

    }
    //Bloquear campo Email para que no sea editable
}
