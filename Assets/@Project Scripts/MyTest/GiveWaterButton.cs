using UnityEngine;
using UnityEngine.UI;

public class GiveWaterButton : MonoBehaviour
{
    Button _button;

    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(GiveWaterButtons);
    }
    
    void GiveWaterButtons()
    {
        Managers.Game.GiveWaterUI(1);
    }
}
