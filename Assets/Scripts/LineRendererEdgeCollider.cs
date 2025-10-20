using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]

public class LineRendererEdgeCollider : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider;
    private readonly List<Vector2> _edgePoints = new List<Vector2>();
    void OnEnable()
    {
        lineRenderer = GetComponent<LineRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();
    }

    private void OnValidate()
    {
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
        if (edgeCollider == null)
        {
            edgeCollider = GetComponent<EdgeCollider2D>();
        }
        ApplyColliderToLineRenderer();
    }
    void Update()
    {
        ApplyColliderToLineRenderer();
    }
    public void ApplyColliderToLineRenderer()
    {
        if (lineRenderer.positionCount < 2)
        {
            edgeCollider.enabled = false;
            return;
        }
        _edgePoints.Clear();
        if (_edgePoints.Capacity < lineRenderer.positionCount)
        {
            _edgePoints.Capacity = lineRenderer.positionCount;
        }
        for (int i = 0; i < lineRenderer.positionCount; i++) // considerado que lineRenderer usa world space
        {
            Vector3 lrPos = lineRenderer.GetPosition(i);
            _edgePoints.Add(new Vector2(lrPos.x, lrPos.y));
        }
        if (_edgePoints.Count >= 2)
        {
            edgeCollider.SetPoints(_edgePoints);
            edgeCollider.enabled = true;
        }
        else
        {
            edgeCollider.enabled = false;
        }

    }
}
