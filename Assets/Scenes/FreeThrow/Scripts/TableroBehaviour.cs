using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableroBehaviour : MonoBehaviour
{

    [SerializeField] private AudioClip reboundSound;


    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }



    private void OnCollisionEnter(Collision collision)
    {
        audioSource.PlayOneShot(reboundSound);
    }
}
