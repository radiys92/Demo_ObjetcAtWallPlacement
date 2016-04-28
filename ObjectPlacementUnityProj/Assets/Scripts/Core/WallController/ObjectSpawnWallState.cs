using UnityEngine;

internal class ObjectSpawnWallState : IWallState
{
    private IWallController _controller;

    private Vector2[] _corners = new Vector2[2];
    private int _puttedPoints = 0;

    public ObjectSpawnWallState(IWallController wallController)
    {
        _controller = wallController;
    }

    public void Start()
    {
        _puttedPoints = 0;
        Debug.Log("Place point 1");
    }

    public void Release()
    {
    }

    public void OnGestureStart(Gesture g)
    {
        _corners[_puttedPoints] = g.StartPoint;
        _puttedPoints++;
        if (_puttedPoints > 1)
        {
            PlaceObject();
        }
        else
        {
            Debug.Log("Place point 2");
        }
    }

    private void PlaceObject()
    {
        Transform obj = GameObject.Instantiate(App.Instance.SelectedPrefab).transform;
        var controller = obj.GetComponent<IObjectController>();
        _controller.AddSpawnedObject(controller);
        var start = _controller.ScreenToWorldPoint(_corners[0]);
        var fin = _controller.ScreenToWorldPoint(_corners[1]);
        _controller.NavigateToPoint(obj, (start + fin)/2);
        controller.SetScale(new Vector2(Mathf.Abs(fin.x - start.x), Mathf.Abs(fin.y - start.y)));
        _controller.SetState(WallState.ObjectMove);
    }

    public void OnGestureEnd(Gesture g)
    {
    }
}