using UnityEngine;

public class EmissionController : MonoBehaviour
{
    public Material targetMaterial; // Assign your material in the Inspector
    public Color litColor = Color.cyan; // Default glowing color
    private Color originalColor;

    void Start()
    {
        if (targetMaterial != null)
        {
            // Save the original emission color
            originalColor = targetMaterial.GetColor("_EmissionColor");

            // Ensure emission is enabled
            targetMaterial.EnableKeyword("_EMISSION");
        }
        else
        {
            Debug.LogWarning("Target Material is not assigned in the Inspector.");
        }
    }

    public void ActivateGlow()
    {
        if (targetMaterial != null)
        {
            targetMaterial.SetColor("_EmissionColor", litColor);
        }
    }

    public void DeactivateGlow()
    {
        if (targetMaterial != null)
        {
            targetMaterial.SetColor("_EmissionColor", originalColor);
        }
    }
}
