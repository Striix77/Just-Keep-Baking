using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI promptText;
    void Start()
    {

    }

    public void UpdateText(string newText)
    {
        promptText.text = newText;
    }
}
