using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float playerSpeed = 1000;
    [SerializeField] float powerupStrength = 15;
    [SerializeField] GameObject powerupIndicator;
    [SerializeField] Vector3 powerUpIndicatorOffset = new(0, -0.5f, 0);

    private Rigidbody playerRb;
    private InputSystem_Actions controls;
    private GameObject focalPoint;

    [SerializeField] bool hasPowerUp = false;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerUp = true;
            if (powerupIndicator != null) powerupIndicator.SetActive(true);
            StartCoroutine(PowerupCountDownRoutine());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") &&
            hasPowerUp)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.transform.position - transform.position);

            if (enemyRb != null) enemyRb.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }

    IEnumerator PowerupCountDownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        if (powerupIndicator != null) powerupIndicator.SetActive(false);
    }
}
