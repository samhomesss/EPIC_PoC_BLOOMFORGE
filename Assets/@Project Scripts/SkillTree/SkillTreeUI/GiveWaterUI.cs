using UnityEngine;
using UnityEngine.UI;

public class GiveWaterUI : MonoBehaviour
{
    Button _button;
    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnWaterButtonPressed);
    }

    public void OnWaterButtonPressed()
    {
        FindAnyObjectByType<TreeManager>()?.WaterTree();
    }

}
