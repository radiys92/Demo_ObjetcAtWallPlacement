using UnityEngine;
using UnityEngine.UI;

public class ControlsPanelController : MonoBehaviour
{
    public Text ControlsStateLabel;

    private bool _isMoveCameraState = false;

    void Start()
    {
        UpdatetView();
    }

    public void SwitchStates()
    {
        _isMoveCameraState = !_isMoveCameraState;
        App.Instance.ScenesManager.SetControlsState(_isMoveCameraState);
        UpdatetView();
    }

    private void UpdatetView()
    {
        ControlsStateLabel.text = "State: " + (_isMoveCameraState ? "camera moving" : "object moving");
    }
}
