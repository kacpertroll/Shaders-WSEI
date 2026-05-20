using TMPro;
using UnityEngine;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine.Rendering.Universal;

public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction Instance { get; private set; }

    [SerializeField] private CinemachineCamera playerCam;
    [SerializeField] private float range = 3f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private UniversalRendererData rendererData;
    private ScriptableRendererFeature grayscaleFeature;

    void Awake()
    {
        foreach (var f in rendererData.rendererFeatures)
        {
            if (f.name == "Grayscale")
            {
                grayscaleFeature = f;
                break;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            ScanForInteractable();
        }
    }

    void ScanForInteractable()
    {
        Ray ray = new Ray(playerCam.transform.position, playerCam.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, range, interactableLayer))
        {
            if (grayscaleFeature != null)
                grayscaleFeature.SetActive(true);

            Destroy(hit.collider.gameObject);
        }
    }
}