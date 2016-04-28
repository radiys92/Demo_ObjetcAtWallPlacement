internal class ObjectMoveWallState : IWallState
{
    private IWallController _controller;
    private Gesture _mover;
    private IObjectController _moveTarget;

    public ObjectMoveWallState(IWallController wallController)
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
        if (_mover != null) return;
        var something = _controller.CastScreenPoint(g.StartPoint);
        foreach (var targets in something)
        {
            var controller = targets.collider.transform.GetComponentInParent<IObjectController>();
            if (!_controller.IsSpawned(controller)) continue;
            _moveTarget = controller;
            _moveTarget.IsSelected = true;
            _mover = g;
            g.OnGestureStay += OnMoveGestureUpdate;
            return;
        }
    }
    private void OnMoveGestureUpdate(Gesture g)
    {
        _moveTarget.IsMoving = true;
        _controller.NavigateToPoint(_moveTarget.GetTransform(), _controller.ScreenToWorldPoint(g.EndPoint));
    }

    public void OnGestureEnd(Gesture g)
    {
        if (_mover != g) return;
        _moveTarget.IsSelected = false;
        _moveTarget.IsMoving = false;
        _moveTarget = null;
        _mover = null;
    }
}