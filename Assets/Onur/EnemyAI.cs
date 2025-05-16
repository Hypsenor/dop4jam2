using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public GameObject jumpscareImage;
    public AudioSource jumpscareAudio;   

    [Header("Timings")]
    public float preSoundDelay = 0.2f;
    public float menuDelay = 1.0f;

    private NavMeshAgent agent;
    private EnemySound enemySound;      // EnemySound referans

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemySound = GetComponent<EnemySound>();    

        if (jumpscareImage != null)
            jumpscareImage.SetActive(false);
    }

    void Update()
    {
        if (player != null)
            agent.SetDestination(player.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Sahurcu seni yakaladý!");
            StartCoroutine(JumpscareSequence());
        }
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
