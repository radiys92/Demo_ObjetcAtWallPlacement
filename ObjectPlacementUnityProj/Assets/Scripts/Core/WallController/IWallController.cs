using UnityEngine;

public interface IWallController
{
    void SetState(WallState state);
    IWallState GetCurrentState();
    void AddSpawnedObject(IObjectController obj);
    void ClearWall();
    RaycastHit[] CastScreenPoint(Vector2 point);
    bool IsSpawned(IObjectController controller);
    void NavigateToPoint(Transform target, Vector3 worldPoint);
    Vector3 ScreenToWorldPoint(Vector2 point);
    void NavigateToPivot(Transform target, Vector2 pivot);
    void SetCameraByControlsDriven(bool driven);
}