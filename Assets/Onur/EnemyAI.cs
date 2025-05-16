using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public GameObject jumpscareImage;
    public AudioSource jumpscareAudio;
    public EnemySound enemySound;       // proximity sound ref

    [Header("Timings")]
    public float preSoundDelay = 0.2f;
    public float menuDelay = 1.0f;

    [Header("Flying Chase")]
    public bool flyAlways = true;       // true: hep u�sun
    public float flySpeed = 6f;
    public float rotateSpeed = 5f;

    private UnityEngine.AI.NavMeshAgent agent;
    private float fixedY;               // sabit y�kseklik

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (flyAlways && agent != null)
            agent.enabled = false;      // NavMesh�i kapat�yoruz

        fixedY = transform.position.y;  // ba�lang��taki y�kseklik

        if (jumpscareImage != null)
            jumpscareImage.SetActive(false);
    }

    void Update()
    {
        if (player == null) return;

        if (flyAlways)
        {
            // Hedef pozisyonu: player'�n x,z'si, bizim fixedY'miz
            Vector3 target = new Vector3(player.position.x, fixedY, player.position.z);
            // Y�nelim
            Vector3 dir = (target - transform.position).normalized;
            if (dir.sqrMagnitude > 0.001f)
            {
                Quaternion look = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * rotateSpeed);
            }
            // �lerleme
            transform.position += transform.forward * flySpeed * Time.deltaTime;
        }
        else
        {
            // Geleneksel NavMesh yakla��m� (eski kod)
            agent.SetDestination(player.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            StartCoroutine(JumpscareSequence());
    }

    private IEnumerator JumpscareSequence()
    {
        if (enemySound != null)
            enemySound.StopProximitySound();

        if (jumpscareImage != null)
            jumpscareImage.SetActive(true);

        yield return new WaitForSeconds(preSoundDelay);

        if (jumpscareAudio != null)
            jumpscareAudio.Play();

        yield return new WaitForSeconds(menuDelay);

        if (player != null)
            Destroy(player.gameObject);

        SceneManager.LoadScene("Menu");
    }
}
