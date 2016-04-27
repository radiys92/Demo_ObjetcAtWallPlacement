using UnityEngine;

[RequireComponent(typeof(WallSettings))]
[RequireComponent(typeof(WallPhysics))]
[RequireComponent(typeof(WallView))]
public class WallController : MonoBehaviour
{
    public WallSettings WallSettings;
    public WallPhysics WallPhysics;
    public WallView WallView;

    private void Start()
    {
        if (WallSettings == null)
            WallSettings = GetComponent<WallSettings>();
        if (WallPhysics == null)
            WallPhysics = GetComponent<WallPhysics>();
        if (WallView == null)
            WallView = GetComponent<WallView>();
    }
}