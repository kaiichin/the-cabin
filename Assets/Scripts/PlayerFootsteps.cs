using System.Collections;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioClip[] footstepSFX;   
    public AudioClip[] cabinSFX;      
    public float stepInterval = 0.4f;
    public float footstepVolume = 0.4f;
    public LayerMask groundLayer;
    public LayerMask cabinLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(PlayFootsteps());
    }

    IEnumerator PlayFootsteps()
    {
        while (true)
        {
            float flatSpeed = new Vector2(rb.linearVelocity.x, rb.linearVelocity.z).magnitude;
            if (flatSpeed > 0.3f && IsOnGround())
            {
                AudioClip[] clips = GetSurfaceClips();
                if (clips != null && clips.Length > 0)
                    AudioManager.instance.PlaySFX(clips[Random.Range(0, clips.Length)], footstepVolume);
            }
            yield return new WaitForSeconds(stepInterval);
        }
    }

    private AudioClip[] GetSurfaceClips()
    {
        RaycastHit hit;
        if (groundCheck != null && Physics.Raycast(groundCheck.position, Vector3.down, out hit, 1.0f))
            if (((1 << hit.collider.gameObject.layer) & cabinLayer) != 0)
                return cabinSFX;
        return footstepSFX;
    }

    private bool IsOnGround()
    {
        if (groundCheck != null)
            return Physics.Raycast(groundCheck.position, Vector3.down, 1.5f, groundLayer | cabinLayer);
        return false;
    }
}
