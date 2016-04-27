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
    private List<Transform> _spawnedObjects = new List<Transform>();

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
            if (_rotator == null)
            {
                WallView.RotateCamera = true;
                _rotator = g;
            }
            else
            {
                return;
            }
        }
    }
    private void OnGestureEnd(Gesture g)
    {
        if (_rotator == g)
        {
            WallView.RotateCamera = false;
            _rotator = null;
        }
    }

    public void SpawnObject()
    {
        Transform obj = Instantiate(App.Instance.SelectedPrefab).transform;
        _spawnedObjects.Add(obj);
        WallPhysics.SetToPivot(obj, new Vector2(.5f, .5f));
    }
}