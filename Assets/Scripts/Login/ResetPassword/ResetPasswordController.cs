using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ResetPasswordController : MonoBehaviour
{
    [Header("BOTONES")]
    [SerializeField]
    private Button validateButton;
    [SerializeField]
    private Button cancelButton;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetStart();
    }

    private void SetStart()
    {
        validateButton.onClick.AddListener(()
            =>ValidateParam());
        cancelButton.onClick.AddListener(()
            => Cancel()
        );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Cancel()
    {
        UIController.Instance.ShowPanel(PanelType.login);
    }

    private void ValidateParam()
    {

    
    }
}
