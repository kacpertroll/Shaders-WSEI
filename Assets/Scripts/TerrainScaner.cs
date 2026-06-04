using UnityEngine;
using UnityEngine.InputSystem;

public class TerrainScaner : MonoBehaviour
{
    [SerializeField] private Terrain terrain;
    [SerializeField] private float scanSpeed;

    private float scanDistance;
    private Material terrainMaterial;

    void Start()
    {
        terrainMaterial = terrain.materialTemplate;
        scanDistance = 2000f;
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                terrainMaterial.SetVector("_OriginPoint", hit.point);
                scanDistance = 0f;
            }
        }

        scanDistance += Time.deltaTime * scanSpeed;
        terrainMaterial.SetFloat("_ScanDistance", scanDistance);
    }
}
