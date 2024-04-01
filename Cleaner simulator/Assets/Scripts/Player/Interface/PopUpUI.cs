using TMPro;
using UnityEngine;

public class PopUpUI : MonoBehaviour
{
    public string CollectingText = "Press E to collect ";

    private void ShowCollectingItemText()
    {
        CollectingText.text = $"Press E to collect {objName}";
    }

}
