using UnityEngine;
using System.Collections;

/// <summary>
/// From http://wiki.unity3d.com/index.php?title=MouseOrbitImproved
/// </summary>
[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class MouseOrbitImproved : MonoBehaviour
{
    public bool EnableMoving;
    public Transform Target;
    public float Distance = 5.0f;
    public float XSpeed = 50.0f;
    public float YSpeed = 50.0f;

    public float YMinLimit = -20f;
    public float YMaxLimit = 20f;

    public float XMinLimit = -60;
    public float XMaxLimit = 60f;

    private Rigidbody _rigidbody;

    float x = 0.0f;
    float y = 0.0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        _rigidbody = GetComponent<Rigidbody>();

        // Make the rigid body not change rotation
        if (_rigidbody != null)
        {
            _rigidbody.freezeRotation = true;
        }
    }

    void LateUpdate()
    {
        if (!Target || !EnableMoving) return;

        x += Input.GetAxis("Mouse X") * XSpeed * Distance * 0.02f;
        y -= Input.GetAxis("Mouse Y") * YSpeed * 0.02f;
        x = ClampAngle(x, XMinLimit, XMaxLimit);
        y = ClampAngle(y, YMinLimit, YMaxLimit);
        var rotation = Quaternion.Euler(y, x, 0);
        UpdatePosition(rotation);
    }

    public void UpdatePosition()
    {
        UpdatePosition(transform.rotation);
    }

    private void UpdatePosition(Quaternion rotation)
    {
        var negDistance = new Vector3(0.0f, 0.0f, -Distance);
        var position = rotation * negDistance + Target.position;

        transform.rotation = rotation;
        transform.position = position;
    }

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

    public Camera GetCamera()
    {
        return GetComponent<Camera>();
    }
}