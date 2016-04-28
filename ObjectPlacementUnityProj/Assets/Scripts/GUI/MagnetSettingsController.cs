using UnityEngine;
using UnityEngine.UI;

public class MagnetSettingsController : MonoBehaviour
{
    public Text MagnetStateLabel;

    private bool _isMagnetEnable = true;

    void Start()
    {
        UpdatetView();
    }

    public void SwitchStates()
    {
        _isMagnetEnable = !_isMagnetEnable;
        App.Instance.ScenesManager.SetMagnetEnable(_isMagnetEnable);
        UpdatetView();
    }

    private void UpdatetView()
    {
        MagnetStateLabel.text = "State: " + (_isMagnetEnable ? "magnet enable" : "magnet disable");
    }
}
