using UnityEngine;

public class EnemySound : MonoBehaviour
{
    public AudioClip enemyAudioClip;
    public float proximityRange = 10f;

    private AudioSource audioSource;
    private Transform player;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < proximityRange)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = enemyAudioClip;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    public void StopProximitySound()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
    }
}
