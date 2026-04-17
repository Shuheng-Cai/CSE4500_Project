using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSounndManager : MonoBehaviour
{
    public static BossSounndManager instance;
    
    AudioSource audioSource;
    public AudioClip fireballSound;
    public AudioClip smallHitSound;
    public AudioClip largeHitSound;
    public AudioClip explosionSound;
    public AudioClip spinningSound;
    public AudioClip chargeSound;
    public AudioClip roarSound;

    void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayFireballSound() {
        audioSource.PlayOneShot(fireballSound);
    }

    public void PlaySmallHitSound() {
        audioSource.PlayOneShot(smallHitSound);
    }

    public void PlayLargeHitSound() {
        audioSource.PlayOneShot(largeHitSound);
    }
    
    public void PlaySpinningSound() {
        audioSource.PlayOneShot(spinningSound);
    }

    public void PlayExplosionSound() {
        audioSource.PlayOneShot(explosionSound);
    }
    
    public void  PlayChargeSound() {
        audioSource.PlayOneShot(chargeSound);
    }

    public void PlayRoarSound() {
        audioSource.PlayOneShot(roarSound);
    }
}
