using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassManager : MonoBehaviour
{
    public static GlassManager Instance;

    [System.Serializable]
    public class GlassGroup
    {
        public GameObject[] glasses; // Her 3’lü cam grubu
    }

    public List<GlassGroup> glassGroups = new List<GlassGroup>();

    public Material normalMaterial;      // sağlam cam materyali
    public Material breakableMaterial;   // kırmızı cam materyali

    [HideInInspector]
    public List<GlassPieceController> breakableGlassControllers = new List<GlassPieceController>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DeactivateAllBrokenGlass();       // oyun başında kırıkları kapat
        AssignBreakableGlasses();         // rastgele 2 cam seç
        StartCoroutine(ShowBreakableGlassMaterial()); // 5 saniye kırmızı göster
    }

    void DeactivateAllBrokenGlass()
    {
        foreach (var group in glassGroups)
        {
            foreach (var glassObj in group.glasses)
            {
                GlassPieceController controller = glassObj.GetComponent<GlassPieceController>();
                if (controller != null && controller.brokenVersion != null)
                {
                    controller.brokenVersion.SetActive(false); // kırığı kapat
                    glassObj.SetActive(true);                  // sağlamı aç
                }
            }
        }
    }

    void AssignBreakableGlasses()
    {
        breakableGlassControllers.Clear();

        foreach (var group in glassGroups)
        {
            if (group.glasses.Length != 3)
            {
                Debug.LogWarning("Her grup 3 cam içermeli!");
                continue;
            }

            List<int> indices = new List<int> { 0, 1, 2 };

            // 3 camdan rastgele 2 tanesini kırılabilir seç
            for (int i = 0; i < 2; i++)
            {
                int randomIndex = Random.Range(0, indices.Count);
                int selected = indices[randomIndex];
                indices.RemoveAt(randomIndex);

                GameObject glassObj = group.glasses[selected];
                GlassPieceController controller = glassObj.GetComponent<GlassPieceController>();

                if (controller != null)
                {
                    breakableGlassControllers.Add(controller);
                }
                else
                {
                    Debug.LogError($"{glassObj.name} üzerinde GlassPieceController component’i yok!");
                }
            }
        }
    }

    IEnumerator ShowBreakableGlassMaterial()
    {
        // Kırılabilir camları 5 saniye boyunca kırmızı yap
        foreach (var glass in breakableGlassControllers)
        {
            SetMaterial(glass.gameObject, breakableMaterial);
        }

        yield return new WaitForSeconds(5f);

        // Materyalleri geri eski haline döndür
        foreach (var glass in breakableGlassControllers)
        {
            SetMaterial(glass.gameObject, normalMaterial);
        }
    }

    void SetMaterial(GameObject obj, Material mat)
    {
        Renderer rend = obj.GetComponent<Renderer>();
        if (rend != null)
        {
            rend.material = mat;
        }
    }
}
