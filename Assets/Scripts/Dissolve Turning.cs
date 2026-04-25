using UnityEngine;

public class DissolveTurning : MonoBehaviour
{
    private Material shaderMaterial;

    public float dissolveTime = 5f;

    private float alphaClip = 0f;
    private float targetAlpha = 0f;

    void Start()
    {
        shaderMaterial = GetComponent<Renderer>().material;
        shaderMaterial.SetFloat("_AlphaClip", alphaClip);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            targetAlpha = targetAlpha == 1f ? 0f : 1f;
        }

        float speed = 1f / dissolveTime;

        alphaClip = Mathf.MoveTowards(alphaClip, targetAlpha, speed * Time.deltaTime);

        shaderMaterial.SetFloat("_AlphaClip", alphaClip);
    }
}