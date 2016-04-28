internal class CameraMoveWallState : IWallState
{
    private IWallController _controller;
    private Gesture _rotator;

    public CameraMoveWallState(IWallController wallController)
    {
        _controller = wallController;
    }

    public void Start()
    {
    }

    public void Release()
    {
        _controller.SetCameraByControlsDriven(false);
        _rotator = null;
    }

    public void OnGestureStart(Gesture g)
    {
        if (_rotator != null) return;
        _controller.SetCameraByControlsDriven(true);
        _rotator = g;
    }

    public void OnGestureEnd(Gesture g)
    {
        if (_rotator != g) return;
        _controller.SetCameraByControlsDriven(false);
        _rotator = null;
    }
}