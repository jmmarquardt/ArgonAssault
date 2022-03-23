using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float _loadDelay;
    [SerializeField] ParticleSystem _crashVFX;

    PlayerController _playerController;
    MeshRenderer _meshRenderer;
    BoxCollider _boxCollider;

    void Awake ()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _playerController = GetComponent<PlayerController>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    void OnTriggerEnter(Collider other) => StartCrashSequence();
    void StartCrashSequence()
    {
        _crashVFX.Play();
        _meshRenderer.enabled = false;
        _playerController.enabled = false;
        _boxCollider.enabled = false;
        Invoke("ReloadLevel", _loadDelay);
    }

    void ReloadLevel()
    {   
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}