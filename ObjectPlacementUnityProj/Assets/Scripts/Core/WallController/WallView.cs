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
        CameraControls.Distance = (Screen.width/Screen.height > _settings.Height / _settings.Width ? _settings.Height : _settings.Width)*1.5f;
        CameraControls.UpdatePosition();
    }

    public RaycastHit[] CastScreenPoint(Vector2 screentPoint)
    {
        var cam = CameraControls.GetCamera();
        var ray = cam.ScreenPointToRay(screentPoint);
        return Physics.BoxCastAll(ray.origin, Vector3.one/4, ray.direction);
    }

    public Vector3 ScreenToWorldPoint(Vector2 screentPoint)
    {
        var cam = CameraControls.GetCamera();
        var ray = cam.ScreenPointToRay(screentPoint);
        var pos = ray.origin - ray.direction*(ray.origin.z/ray.direction.z);
        return pos;
    }
}