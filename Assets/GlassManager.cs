using UnityEngine;
using System.Collections.Generic;

public class GlassManager : MonoBehaviour
{
    public GameObject[] allGlassPanels;
    public Light overheadSpotlight; // Tek ışık objesi

    private List<int> safeGlassIndices = new List<int>();
    private List<Transform> breakableTargets = new List<Transform>();

    private float timer = 0f;
    private bool lightActive = true;

    void Start()
    {
        int count = allGlassPanels.Length;

        if (count % 3 != 0)
        {
            Debug.LogError("Cam sayısı 3'ün katı olmalı!");
            return;
        }

        for (int i = 0; i < count; i += 3)
        {
            List<int> group = new List<int> { i, i + 1, i + 2 };
            int safeIndex = Random.Range(0, 3);
            int safeGlobal = group[safeIndex];
            safeGlassIndices.Add(safeGlobal);

            for (int j = 0; j < 3; j++)
            {
                int index = group[j];

                if (index != safeGlobal)
                {
                    // Kırılabilir cam → hedef listesine ekle
                    breakableTargets.Add(allGlassPanels[index].transform);
                }
            }
        }

        PositionLightToCoverTargets();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (lightActive && timer >= 5f)
        {
            if (overheadSpotlight != null)
                overheadSpotlight.enabled = false;

            lightActive = false;
        }
    }

    void PositionLightToCoverTargets()
    {
        if (overheadSpotlight == null || breakableTargets.Count == 0) return;

        // Ortalamayı al
        Vector3 center = Vector3.zero;
        foreach (Transform t in breakableTargets)
        {
            center += t.position;
        }
        center /= breakableTargets.Count;

        // Işığı yukarıdan bu merkeze yönlendir
        Vector3 lightPosition = center + Vector3.up * 10f; // yukarıdan bakacak
        overheadSpotlight.transform.position = lightPosition;
        overheadSpotlight.transform.LookAt(center);
    }
}

