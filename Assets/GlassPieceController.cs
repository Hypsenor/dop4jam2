using UnityEngine;

public class GlassPieceController : MonoBehaviour
{
    public GameObject brokenVersion; // K�r�k cam objesi

    private bool isBroken = false;

    // Bu fonksiyon karakter temas�nda �a�r�lacak
    public void BreakGlass()
    {
        if (isBroken) return;

        isBroken = true;
        gameObject.SetActive(false);          // Sa�lam cam� kapat
        brokenVersion.SetActive(true);        // K�r�k cam� a�
    }
}
