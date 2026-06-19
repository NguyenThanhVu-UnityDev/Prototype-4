using UnityEngine;

public class BouncyEnemy : Enemy
{
    [SerializeField] float bouncyForce = 10f;
    protected override void OnInteractWithPlayer(GameObject player)
    {
        if (player.TryGetComponent<Rigidbody>(out Rigidbody playerRb))
        {
            Vector3 awayFromEnemy = (player.transform.position - transform.position).normalized;
            playerRb.AddForce(awayFromEnemy * bouncyForce, ForceMode.Impulse);
        }
    }
}
