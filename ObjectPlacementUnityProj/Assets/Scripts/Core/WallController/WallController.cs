using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WallSettings))]
[RequireComponent(typeof(WallPhysics))]
[RequireComponent(typeof(WallView))]
public class WallController : MonoBehaviour, IWallController
{
    public WallSettings WallSettings;
    public WallPhysics WallPhysics;
    public WallView WallView;


    private readonly List<IObjectController> _spawnedObjects = new List<IObjectController>();
    private readonly Dictionary<WallState, IWallState> _states = new Dictionary<WallState, IWallState>();
    private IWallState _currentState;

    
    public bool IsMagnetEnabled
    {
        get { return WallPhysics.IsMagnetEnable; }
        set { WallPhysics.IsMagnetEnable = value; }
    }

   
    public RaycastHit[] CastScreenPoint(Vector2 point)
    {
        return WallView.CastScreenPoint(point);
    }

    public bool IsSpawned(IObjectController controller)
    {
        return _spawnedObjects.Contains(controller);
    }

    public void NavigateToPoint(Transform target, Vector3 worldPoint)
    {
        WallPhysics.NavigateToPoint(target,worldPoint);
    }

    public Vector3 ScreenToWorldPoint(Vector2 point)
    {
        return WallView.ScreenToWorldPoint(point);
    }

    public void NavigateToPivot(Transform target, Vector2 pivot)
    {
        WallPhysics.NavigateToPivot(target,pivot);
    }

    public void SetCameraByControlsDriven(bool driven)
    {
        WallView.RotateCamera = driven;
    }

    private void Start()
    {
        if (WallSettings == null)
            WallSettings = GetComponent<WallSettings>();
        if (WallPhysics == null)
            WallPhysics = GetComponent<WallPhysics>();
        if (WallView == null)
            WallView = GetComponent<WallView>();

        GestureController.OnGestureStart += OnGestureStart;
        GestureController.OnGestureEnd += OnGestureEnd;

        _states.Add(WallState.CameraMove, new CameraMoveWallState(this));
        _states.Add(WallState.ObjectMove, new ObjectMoveWallState(this));
        _states.Add(WallState.ObjectSpawn, new ObjectSpawnWallState(this));
        SetState(WallState.ObjectMove);
    }

    public void OnDestroy()
    {
        GestureController.OnGestureStart -= OnGestureStart;
        GestureController.OnGestureEnd -= OnGestureEnd;
    }

    private void OnGestureStart(Gesture g)
    {
        GetCurrentState().OnGestureStart(g);
    }

    private void OnGestureEnd(Gesture g)
    {
        GetCurrentState().OnGestureEnd(g);
    }

    public void SetState(WallState state)
    {
        if (_currentState != null)
            _currentState.Release();
        _currentState = _states[state];
        _currentState.Start();
    }

    public IWallState GetCurrentState()
    {
        return _currentState;
    }

    public void AddSpawnedObject(IObjectController obj)
    {
        _spawnedObjects.Add(obj);
    }

    public void ClearWall()
    {
        foreach (var spawned in _spawnedObjects)
        {
            Destroy(spawned.GetTransform().gameObject);
        }
        _spawnedObjects.Clear();
    }
}