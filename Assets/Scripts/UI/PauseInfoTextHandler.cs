using TMPro;
using UnityEngine;

public class PauseInfoTextHandler : MonoBehaviour
{
    private TMP_Text menuTitle;
    private string _deadText = "�� �������";

    private void Awake()
    {
        menuTitle = GetComponent<TMP_Text>();
        EventManager.PlayerDead.AddListener(SetDeathInfoText);
        EventManager.GamePaused.AddListener(SetPauseInfoText);
    }

    public void SetDeathInfoText()
    {
        menuTitle.text = $"<color=red>{_deadText}</color>";
    }

    public void SetPauseInfoText()
    {
        menuTitle.text = "�����";
    }
}
