using UnityEngine;

public class GlassBreaker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Temas ettiði objede GlassPieceController var mý?
        GlassPieceController glass = other.GetComponent<GlassPieceController>();

        if (glass != null)
        {
            // Bu cam, kýrýlabilir listede mi?
            if (GlassManager.Instance.breakableGlassControllers.Contains(glass))
            {
                glass.BreakGlass();
                Debug.Log("Cam kýrýldý: " + glass.name);
            }
        }
    }
}
