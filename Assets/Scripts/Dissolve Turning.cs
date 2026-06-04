using UnityEngine;

public class DissolveTurning : MonoBehaviour
{
    private Material shaderMaterial;

    [SerializeField] private bool isVisibleAtStart = true;
    [Space]

    public float dissolveTime = 5f;

    [SerializeField] private float lowerAlpha = 0f;
    [SerializeField] private float higherAlpha = 1f;

    private float alphaClip = 0f;
    private float targetAlpha = 0f;

    [SerializeField] private float colliderMargin = 0f;

    private Collider collider;

    void Start()
    {
        shaderMaterial = GetComponent<Renderer>().material;
        shaderMaterial.SetFloat("_AlphaClip", alphaClip);
        collider = gameObject.GetComponentInChildren<Collider>();

        if (isVisibleAtStart)
        {
            alphaClip = higherAlpha;
            targetAlpha = higherAlpha;
        }
        else
        {
            alphaClip = lowerAlpha;
            targetAlpha = lowerAlpha;
        }    
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            targetAlpha = targetAlpha == higherAlpha ? lowerAlpha : higherAlpha;
        }

        float speed = 1f / dissolveTime;

        alphaClip = Mathf.MoveTowards(alphaClip, targetAlpha, speed * Time.deltaTime);

        shaderMaterial.SetFloat("_AlphaClip", alphaClip);

        if (alphaClip <= (lowerAlpha + colliderMargin))
            collider.enabled = false;
        else if (alphaClip > (lowerAlpha + colliderMargin) && !collider.enabled)
        {
            collider.enabled = true;
        }
    }
}