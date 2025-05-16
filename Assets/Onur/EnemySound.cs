using UnityEngine;

public class EnemySound : MonoBehaviour
{
    public AudioClip enemyAudioClip;  // Assign the audio clip in the Unity Editor
    public float proximityRange = 10f;  // Set the desired proximity range

    private AudioSource audioSource;
    private Transform player;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Check the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if the player is within the proximity range
        if (distanceToPlayer < proximityRange)
        {
            // If the audio is not playing, start playing it
            if (!audioSource.isPlaying)
            {
                audioSource.clip = enemyAudioClip;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
        else
        {
            // If the player is outside the proximity range, stop the audio
            audioSource.Stop();
        }
    }
}
