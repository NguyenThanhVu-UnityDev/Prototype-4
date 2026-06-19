using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IPowerupUser
{
    [SerializeField] float playerSpeed = 1000;
    [SerializeField] GameObject powerupIndicator;
    [SerializeField] Vector3 powerUpIndicatorOffset = new(0, -0.5f, 0);

    private Rigidbody playerRb;
    private InputSystem_Actions controls;
    private GameObject focalPoint;

    private PowerupInstance powerupInstance = null;

    private void Awake()
    {
        controls = new();
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
    }

    private void OnEnable()
    {
        if (controls != null) controls.Player.Enable();
    }
    private void OnDisable()
    {
        if (controls != null) controls.Player.Disable();
    }

    private void Update()
    {
        UpdatePowerups();
        Move();
        UpdatePowerupIndicatorVisual();
    }

    private void Move()
    {
        if (controls == null || focalPoint == null || playerRb == null) return;
        Vector2 moveInput = controls.Player.Move.ReadValue<Vector2>();
        float forwardInput = moveInput.y;
        playerRb.AddForce(focalPoint.transform.forward * playerSpeed * forwardInput * Time.deltaTime);
    }

    private void UpdatePowerupIndicatorVisual()
    {
        if (powerupIndicator != null) powerupIndicator.transform.position = transform.position + powerUpIndicatorOffset;
    }

    public void UpdatePowerups()
    { 
        if (powerupInstance == null) return;
        powerupInstance.OnTick();
        if (powerupInstance == null) return;
        if (controls != null && controls.Player.Jump.triggered) powerupInstance.OnActivate(); 
    }


    private void OnTriggerEnter(Collider other)
    {
        PlayerEvents.RaisePlayerTriggerEnter(other);
        if (powerupInstance == null) return;
            powerupInstance.OnUserTriggerEnter?.Invoke(this, other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerEvents.RaisePlayerCollisionEnter(collision);
        if (powerupInstance == null) return;
        powerupInstance.OnUserCollisionEnter?.Invoke(this, collision);
    }

    public void ActivatePowerupIndicator()
    {
        if (powerupIndicator != null) powerupIndicator.SetActive(true);
    }

    public void DeactivatePowerupIndicator()
    {
        if (powerupIndicator != null) powerupIndicator.SetActive(false);
    }

    public bool OnReceivePowerup(PowerupInstance powerup)
    {
        if (powerup == null) return false;
        if (powerupInstance != null) return false;
        if (powerupInstance == powerup) return false;
        powerup.OnEquip(this);
        powerupInstance = powerup;
        return true;
    }

    public GameObject GetUser()
    {
        return gameObject;
    }

    public void OnUnequipPowerup(PowerupInstance powerup)
    {
        if (powerup == null || powerup != powerupInstance) return;
        powerupInstance = null;
    }
}
