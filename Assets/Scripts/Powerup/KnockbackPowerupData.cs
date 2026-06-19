using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Knockback")]
public class KnockbackPowerupData : BasePowerupData
{
    [SerializeField] float powerupStrength = 15;
    public override void OnActivate(IPowerupUser user)
    {
        
    }

    public override void OnEquip(IPowerupUser user, PowerupInstance instance)
    {
        user.ActivatePowerupIndicator();
        if (instance != null) instance.OnUserCollisionEnter += OnUserCollisionEnter;
        ActivateTimer(instance);
    }

    public override void OnTick(IPowerupUser user)
    {
        
    }

    public override void OnUnEquip(IPowerupUser user, PowerupInstance instance)
    {
        user.DeactivatePowerupIndicator();
        if (instance != null) instance.OnUserCollisionEnter -= OnUserCollisionEnter;
    }

    private void OnUserCollisionEnter(IPowerupUser user, Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.transform.position - user.GetUser().transform.position);

            if (enemyRb != null) enemyRb.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }
}
