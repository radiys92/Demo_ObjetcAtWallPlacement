using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager
{
    public void OpenShop()
    {
        SceneManager.LoadScene("Shop", LoadSceneMode.Additive);
        var wall = GameObject.FindObjectOfType<WallController>();
        wall.SetState(WallState.ObjectSelecting);
    }

    public void CloseShop()
    {
        SceneManager.UnloadScene("Shop");
        var wall = GameObject.FindObjectOfType<WallController>();
        wall.SetState(WallState.ObjectSpawn);
    }

    public void SetControlsState(bool isCameraMoving)
    {
        var wall = GameObject.FindObjectOfType<WallController>();
        wall.SetState(isCameraMoving?WallState.CameraMove : WallState.ObjectMove);
    }

    public void SetMagnetEnable(bool isMagnetEnable)
    {
        var wall = GameObject.FindObjectOfType<WallController>();
        wall.IsMagnetEnabled = isMagnetEnable;
    }

    public void ClearWall()
    {
        var wall = GameObject.FindObjectOfType<WallController>();
        wall.ClearWall();
    }

    public void SetInfoCaption(string caption)
    {
        InfoPanelController.SetCaption(caption);
    }
}