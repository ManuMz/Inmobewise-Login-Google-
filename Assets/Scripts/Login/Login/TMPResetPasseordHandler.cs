using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TMPResetPasseordHandler : MonoBehaviour, IPointerClickHandler
{
    private TextMeshProUGUI resetPasseordLink;

    private void Awake()
    {
        resetPasseordLink = GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UIController.Instance.ShowPanel(PanelType.resetPassword);
    }
}
