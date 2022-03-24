using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [Header("General Configuration Settings")]
    [Tooltip("How fast ship moves up and down.")] 
    [SerializeField] [Range(0f, 50f)] float _moveSpeed = 30f;
    
    [Tooltip("How far player moves horizontally")]
    [SerializeField] float _xRange = 20f;
    
    [Tooltip("How far player moves vertically")]
    [SerializeField] float _yRange = 12f;


    [Header("Pitch, Yaw, Roll")]
    [SerializeField] [Range(-5f, 0f)] float _pitchPositionFactor = -2f;
    [SerializeField] [Range(-30f, 0f)] float _pitchControlFactor = -10f;

    [SerializeField] float _yawPositionFactor = 2.5f;
    [SerializeField] float _rollControlFactor = -15f;


    [Header("Controller Settings")]
    [Tooltip("Vertical and horizontal movement settings mapped to player input")]
    [SerializeField] InputAction _movement;
    
    [Tooltip("Firing control settings mapped to player input")]
    [SerializeField] InputAction _firing;

    
    [Header("Weapons")]
    [Tooltip("Add all player ship lasers here")]
    [SerializeField] GameObject[] _lasers;
    float _xThrow, _yThrow;
    Vector3 _localPosition;

    void OnEnable ()
    {
        _movement.Enable();
        _firing.Enable();    
    }

    void OnDisable ()
    {
        _movement.Disable();
        _firing.Disable();
    }

    void Update ()
    {
        _localPosition = transform.localPosition;
        ProcessTranslation(_localPosition);
        ProcessRotation(_localPosition);
        ProcessFiring();
    }

    void ProcessTranslation (Vector3 localPosition)
    {
        _xThrow = _movement.ReadValue<Vector2>().x;
        _yThrow = _movement.ReadValue<Vector2>().y;

        float xOffset = _xThrow * _moveSpeed * Time.deltaTime;
        float rawXPos = localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -_xRange, _xRange);

        float yOffset = _yThrow * _moveSpeed * Time.deltaTime;
        float rawYPos = localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -_yRange, _yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, localPosition.z);
    }

    void ProcessRotation(Vector3 localPosition)
    {
        // pitch adjustments based on player position and controller input throw
        float pitchPosition = transform.localPosition.y * _pitchPositionFactor;
        float pitchControl = _yThrow * _pitchControlFactor;

        float pitch = pitchPosition + pitchControl;
        float yaw = localPosition.x * _yawPositionFactor;
        float roll = _xThrow * _rollControlFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessFiring() => SetLasersActive(Input.GetButton("Fire1") || _firing.ReadValue<float>() > 0.5);

    void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in _lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
}