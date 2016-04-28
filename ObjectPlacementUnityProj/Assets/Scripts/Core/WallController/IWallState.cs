public interface IWallState
{
    void Start();
    void Release();
    void OnGestureStart(Gesture g);
    void OnGestureEnd(Gesture g);
}