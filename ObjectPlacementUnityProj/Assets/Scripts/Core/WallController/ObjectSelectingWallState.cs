internal class ObjectSelectingWallState : IWallState
{
    private IWallController _controller;

    public ObjectSelectingWallState(IWallController wallController)
    {
        _controller = wallController;
    }

    public void Start()
    {
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