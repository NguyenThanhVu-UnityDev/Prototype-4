using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float enemySpeed = 2.0f;

    private Rigidbody enemyRb;
    private GameObject player;

    private void Awake()
    {
        enemyRb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (player == null || enemyRb == null) return;

        Vector3 lookDirection = (player.transform.position - transform.position).normalized;

        enemyRb.AddForce(lookDirection * enemySpeed);

        if (transform.position.y < -10) Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            OnInteractWithPlayer(collision.gameObject);
        }
    }

    protected virtual void OnInteractWithPlayer(GameObject player) { }
}
