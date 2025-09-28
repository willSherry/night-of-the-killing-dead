using System;
using UnityEngine;

public enum SoundType
{
    PlayerStep,
    PlayerShoot,
    EnemyHit,
    EnemyDeath,
    PlayerHit,
    PlayerDeath,
    Skelefy,
    SkeletonAttack
}

[RequireComponent (typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundList;
    private static SoundManager instance;
    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType sound, float volume = 1.0f)
    {
        instance.audioSource.PlayOneShot(instance.soundList[(int)sound], volume);
    }

}