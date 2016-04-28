using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[RequireComponent(typeof(WallSettings))]
[RequireComponent(typeof(WallPhysics))]
[RequireComponent(typeof(WallView))]
public class WallController : MonoBehaviour
{
    public WallSettings WallSettings;
    public WallPhysics WallPhysics;
    public WallView WallView;
    public bool RotateCamera;

    private Gesture _rotator;
    private List<IObjectController> _spawnedObjects = new List<IObjectController>();
    private IObjectController _moveTarget;
    private Gesture _mover = null;

    public bool IsMagnetEnabled
    {
        get { return WallPhysics.IsMagnetEnable; }
        set { WallPhysics.IsMagnetEnable = value; }
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
    }

    public void OnDestroy()
    {
        GestureController.OnGestureStart -= OnGestureStart;
        GestureController.OnGestureEnd -= OnGestureEnd;
    }

    private void OnGestureStart(Gesture g)
    {
        if (RotateCamera)
        {
            if (_rotator != null) return;
            WallView.RotateCamera = true;
            _rotator = g;
            return;
        }

        if (_mover == null)
        {
            var something = WallView.CastScreenPoint(g.StartPoint);
            foreach (var targets in something)
            {
                var controller = targets.collider.transform.GetComponentInParent<IObjectController>();
                if (!_spawnedObjects.Contains(controller)) continue;
                _moveTarget = controller;
                _moveTarget.IsSelected = true;
                _mover = g;
                g.OnGestureStay += OnMoveGestureUpdate;
                return;
            }
        }
    }

    private void OnMoveGestureUpdate(Gesture g)
    {
        _moveTarget.IsMoving = true;
        WallPhysics.NavigateToPoint(_moveTarget.GetTransform(), WallView.ScreenToWorldPoint(g.EndPoint));
    }

    private void OnGestureEnd(Gesture g)
    {
        if (_rotator == g)
        {
            WallView.RotateCamera = false;
            _rotator = null;
        }
        if (_mover == g)
        {
            _moveTarget.IsSelected = false;
            _moveTarget.IsMoving = false;
            _moveTarget = null;
            _mover = null;
        }
    }

    public void SpawnObject()
    {
        Transform obj = Instantiate(App.Instance.SelectedPrefab).transform;
        _spawnedObjects.Add(obj.GetComponent<IObjectController>());
        WallPhysics.NavigateToPivot(obj, new Vector2(.5f, .5f));
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