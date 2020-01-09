using UnityEngine;
using UnityEngine.UI;
using GameFramework.Dialogue;
using GameFramework;
using TMPro;

public class SimpleDialogueView : MonoBehaviour
{
    [SerializeField]
    private Image avatarUI;
    [SerializeField]
    private TextMeshProUGUI speakerName;
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private GameObject nextLine;

    public void ShowLine( DialogueLine dialogueLine) {
        avatarUI.sprite = dialogueLine.GetAvatar();
        speakerName.text = dialogueLine.GetSpeakerName();
        text.text = dialogueLine.GetDialogueLine();
    }

    public void ShowNextButton() {
        nextLine.SetActive(true);
    }

    public void HideNextButton() {
        nextLine.SetActive(false);
    }

    public void OnButtonClick() {
        GameEventSystem.OnNextDialogueLine();
    }

}
