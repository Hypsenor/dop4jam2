using UnityEngine;
using System.Collections.Generic;

public class GlassManager : MonoBehaviour
{
    public GameObject[] allGlassPanels; // 3'erli s�ral� camlar
    public Material redGlassMaterial;

    private Material[] originalMaterials;
    private Renderer[] glassRenderers;

    private List<int> safeGlassIndices = new List<int>();
    private float timer = 0f;
    private bool isRedActive = true;

    void Start()
    {
        int count = allGlassPanels.Length;

        if (count % 3 != 0)
        {
            Debug.LogError("Cam say�s� 3'�n kat� olmal�!");
            return;
        }

        glassRenderers = new Renderer[count];
        originalMaterials = new Material[count];

        for (int i = 0; i < count; i += 3)
        {
            // Grup: i, i+1, i+2
            List<int> groupIndices = new List<int> { i, i + 1, i + 2 };

            // 1 sa�lam cam se�
            int safeIndexInGroup = Random.Range(0, 3);
            int safeGlobalIndex = groupIndices[safeIndexInGroup];
            safeGlassIndices.Add(safeGlobalIndex);

            for (int j = 0; j < 3; j++)
            {
                int index = i + j;

                glassRenderers[index] = allGlassPanels[index].GetComponentInChildren<Renderer>();

                if (glassRenderers[index] != null)
                {
                    originalMaterials[index] = glassRenderers[index].material;

                    // E�er bu cam sa�lam de�ilse, k�rm�z�ya boya
                    if (index != safeGlobalIndex)
                    {
                        glassRenderers[index].material = redGlassMaterial;
                    }
                }
                else
                {
                    Debug.LogWarning("Renderer eksik: " + allGlassPanels[index].name);
                }
            }
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (isRedActive && timer >= 1f)
        {
            // K�rm�z� camlar� geri eski rengine d�nd�r
            for (int i = 0; i < glassRenderers.Length; i++)
            {
                if (!safeGlassIndices.Contains(i) && glassRenderers[i] != null)
                {
                    glassRenderers[i].material = originalMaterials[i];
                }
            }

            isRedActive = false;
        }
    }

    public List<int> GetSafeGlassIndices()
    {
        return safeGlassIndices;
    }
}
