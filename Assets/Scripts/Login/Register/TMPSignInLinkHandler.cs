using System.Xml;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TMPSignInLinkHandler : MonoBehaviour, IPointerClickHandler
{
    #region EVENTS
    //Eventos personalizados
    public delegate void ClickOnSignInLinkEvent();
    public event ClickOnSignInLinkEvent OnClickedSignInLink;

    
    private TextMeshProUGUI signInLink;

    //Eventos estandar
    #endregion

    private void Awake()
    {
        signInLink = GetComponent<TextMeshProUGUI>();   
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log($"Texto clickeado: {signInLink.text}");
        UIController.Instance.ShowPanel(PanelType.login);
    }
}
