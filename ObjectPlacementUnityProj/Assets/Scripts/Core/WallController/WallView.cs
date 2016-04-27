using UnityEngine;

[RequireComponent(typeof(WallSettings))]
public class WallView : MonoBehaviour
{
    public Transform ViewTransform;
    public MouseOrbitImproved CameraControls;

    WallSettings _settings;

    public bool RotateCamera
    {
        get { return CameraControls.EnableMoving; }
        set { CameraControls.EnableMoving = value; }
    }

    void Start()
    {
        _settings = GetComponent<WallSettings>();
        if (CameraControls == null)
            CameraControls = FindObjectOfType<MouseOrbitImproved>();
        UpdateView();
    }

    public void UpdateView()
    {
        ViewTransform.localScale = new Vector3(_settings.Width, _settings.Height,1);
        var r = ViewTransform.GetComponent<Renderer>();
        if (_settings.WallMaterial != null)
            r.material = _settings.WallMaterial;
        CameraControls.distance = (Screen.width/Screen.height > _settings.Height / _settings.Width ? _settings.Height : _settings.Width)*1.5f;
        CameraControls.UpdatePosition();
    }
}