using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTrigger : MonoBehaviour
{
    [Tooltip("Yüklenecek sahne adı")]
    public string targetSceneName = "Menu";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
