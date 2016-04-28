using System.Collections.Generic;
using UnityEngine;

public class WallPhysics : MonoBehaviour
{
    public Collider WallCollider;
    public bool IsMagnetEnable = false;

    public void NavigateToPivot(Transform target, Vector2 pivot)
    {
        var objBounds = GetBounds(target);
        var wallBounds = WallCollider.bounds;
        wallBounds.min += objBounds.extents;
        wallBounds.max -= objBounds.extents;
        var offset = wallBounds.size;
        offset.x *= pivot.x;
        offset.y *= pivot.y;
        var pos = wallBounds.min + offset;
        pos.z = wallBounds.min.z - objBounds.size.z;
        target.position = pos;
    }

    public void NavigateToPoint(Transform target, Vector3 point)
    {
        var objBounds = GetBounds(target);
        var wallBounds = WallCollider.bounds;
        wallBounds.min += objBounds.extents;
        wallBounds.max -= objBounds.extents;
        var pos = wallBounds.ClosestPoint(point);
        if (IsMagnetEnable)
        {
            pos = DoMagnet(wallBounds, pos);
        }
        pos.z = wallBounds.min.z - objBounds.size.z;
        target.position = pos;
    }

    private Vector3 DoMagnet(Bounds bounds, Vector3 pos)
    {
        var c = bounds.center;
        c.z = pos.z;
        var e = bounds.extents;
        List<Vector3> anchors = new List<Vector3>(new Vector3[]
        {
            c + new Vector3(0, 0, 0),
            c + new Vector3(e.x, e.y, 0),
            c + new Vector3(e.x, -e.y, 0), 
            c + new Vector3(-e.x, e.y, 0),
            c + new Vector3(-e.x, -e.y, 0),
            c + new Vector3(e.x, 0, 0),
            c + new Vector3(-e.x, 0, 0),
            c + new Vector3(0, e.y, 0),
            c + new Vector3(0, -e.y, 0),
        });
        anchors.Sort((a, b) => (int) (Vector3.SqrMagnitude(a - pos) - Vector3.SqrMagnitude(b - pos)));
        return anchors[0];
    }

    private Bounds GetBounds(Transform target)
    {
        var cols = new List<Collider>(target.GetComponentsInChildren<Collider>());
        cols.AddRange(target.GetComponents<Collider>());
        if (cols.Count == 0) return new Bounds(transform.position, Vector3.zero);
        if (cols.Count == 1) return cols[0].bounds;
        var b = cols[0].bounds;
        for (var i = 1; i < cols.Count; i++)
            b.Encapsulate(cols[i].bounds);
        return b;
    }

    public bool IsWall(Transform target)
    {
        var cols = new List<Collider>(target.GetComponentsInChildren<Collider>());
        cols.AddRange(target.GetComponents<Collider>());
        foreach (var col in cols)
        {
            if (col == WallCollider) return true;
        }
        return false;
    }
}