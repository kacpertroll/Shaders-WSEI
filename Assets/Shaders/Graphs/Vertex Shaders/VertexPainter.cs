using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VertexPainter : MonoBehaviour
{
    [SerializeField] private Color _color = Color.red;
    [SerializeField, Range(0f, 1f)] private float _strength = 0.2f;
    [SerializeField] private float _brushSize = 0.2f;
    
    private Camera _camera;
    private Mesh _mesh;
    private float _timer;
    
    private void Start()
    {
        _camera = Camera.main;
    }
    
    private void Update()
    {
        if (!Mouse.current.leftButton.isPressed)
            return;
        
        _timer -= Time.deltaTime;
        if (_timer > 0)
            return;

        Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            PaintMesh(hit);
            _timer = 0.05f;
        }
    }

    private void PaintMesh(RaycastHit hit)
    {
        _mesh = hit.collider.gameObject.GetComponent<MeshFilter>().mesh;

        if (_mesh == null)
            _mesh = hit.collider.gameObject.GetComponentInChildren<MeshFilter>().mesh;
        
        if (_mesh == null)
            return;
        
        var verticesToPaint = FindVerticesToPaint(hit.point, hit.transform);
        if (_mesh.colors == null || _mesh.colors.Length == 0)
            _mesh.colors = new Color[_mesh.vertexCount];

        var vertexColors = _mesh.colors;

        foreach (var vertex in verticesToPaint)
            vertexColors[vertex] = Color.Lerp(vertexColors[vertex], _color, _strength);
        
        _mesh.colors = vertexColors;
    }

    private List<int> FindVerticesToPaint(Vector3 hitPoint, Transform targetTransform)
    {
        var result = new List<int>();
        var vertices = _mesh.vertices;

        for (int i = 0; i < _mesh.vertexCount; i++)
        {
            if (Vector3.Distance(targetTransform.TransformPoint(vertices[i]), hitPoint) < _brushSize)
                result.Add(i);
        }

        return result;
    }
}
