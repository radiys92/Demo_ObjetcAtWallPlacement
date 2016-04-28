using UnityEngine;

internal class ObjectSpawnWallState : IWallState
{
    private IWallController _controller;

    public ObjectSpawnWallState(IWallController wallController)
    {
        _controller = wallController;
    }

    public void Start()
    {
        Transform obj = GameObject.Instantiate(App.Instance.SelectedPrefab).transform;
        _controller.AddSpawnedObject(obj.GetComponent<IObjectController>());
        _controller.NavigateToPivot(obj, new Vector2(.5f, .5f));
        _controller.SetState(WallState.ObjectMove);
    }

    public void Release()
    {
    }

    public void OnGestureStart(Gesture g)
    {
    }

    public void OnGestureEnd(Gesture g)
    {
    }
}