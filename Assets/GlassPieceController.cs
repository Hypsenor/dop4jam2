using UnityEngine;

public class GlassPieceController : MonoBehaviour
{
    public GameObject brokenVersion; // Kýrýk cam objesi

    private bool isBroken = false;

    // Bu fonksiyon karakter temasýnda çaðrýlacak
    public void BreakGlass()
    {
        if (isBroken) return;

        isBroken = true;
        gameObject.SetActive(false);          // Saðlam camý kapat
        brokenVersion.SetActive(true);        // Kýrýk camý aç
    }
}
