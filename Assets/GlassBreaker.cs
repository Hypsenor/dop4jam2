using UnityEngine;

public class GlassBreaker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Temas etti�i objede GlassPieceController var m�?
        GlassPieceController glass = other.GetComponent<GlassPieceController>();

        if (glass != null)
        {
            // Bu cam, k�r�labilir listede mi?
            if (GlassManager.Instance.breakableGlassControllers.Contains(glass))
            {
                glass.BreakGlass();
                Debug.Log("Cam k�r�ld�: " + glass.name);
            }
        }
    }
}
