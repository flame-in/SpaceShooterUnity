﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    [SerializeField]    private AudioClip _explosionClip;
    [SerializeField]    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject,3f);
        _audioSource = GetComponent<AudioSource>();
        if(_audioSource == null)
        {
            Debug.LogError("Error finding audio source. Setting audio clip manually");
            _audioSource.clip = _explosionClip;
        }
        _audioSource.Play();
    }
}
