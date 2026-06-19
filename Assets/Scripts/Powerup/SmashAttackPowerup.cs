using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/SmashAttack")]
public class SmashAttackPowerup : BasePowerupData
{
    [SerializeField] float jumpForce = 50f;
    [SerializeField] float smashForce = 100f;
    [SerializeField] float jumpTime = 1f;
    [SerializeField] float smashRadius = 20f;
    [SerializeField] float ultrasonicWaveForce = 20f;

    private float timer = 0;
    private bool ableToSmash = false;
    public override void OnActivate(IPowerupUser user)
    {
        if (timer > 0) return;

        if (user.GetUser().TryGetComponent(out Rigidbody userRb))
        {
            timer = jumpTime;
            userRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            ableToSmash = false;
        }
    }

    public override void OnEquip(IPowerupUser user, PowerupInstance instance)
    {
        ActivateTimer(instance);
        user.ActivatePowerupIndicator();

        instance.OnUserCollisionEnter += OnUserCollisionEnter;
    }

    public override void OnTick(IPowerupUser user)
    {
        if (timer <= 0) return;

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (user.GetUser().TryGetComponent(out Rigidbody userRb))
            {
                userRb.AddForce(Vector3.down * smashForce, ForceMode.Impulse);
                ableToSmash = true;
            }
        }
    }

    public override void OnUnEquip(IPowerupUser user, PowerupInstance instance)
    {
        user.DeactivatePowerupIndicator();
        instance.OnUserCollisionEnter -= OnUserCollisionEnter;
    }

    public void OnUserCollisionEnter(IPowerupUser user, Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && ableToSmash)
        {
            Smash(user);
            ableToSmash = false;
        } 
    }

    private void Smash(IPowerupUser user)
    {
        var colliders = Physics.OverlapSphere(user.GetUser().transform.position, smashRadius);
        foreach (var collider in colliders)
        {
            if (collider.gameObject.TryGetComponent(out Enemy enemy))
            {
                if (enemy.TryGetComponent(out Rigidbody enemyRb))
                {
                    var distance = Vector3.Distance(user.GetUser().transform.position, enemy.transform.position);
                    var applyRate = 1.0f - Mathf.Clamp(distance / smashRadius, 0, 0.99f);
                    Vector3 awayFromUser = (enemy.transform.position - user.GetUser().transform.position).normalized;
                    enemyRb.AddForce(awayFromUser * applyRate * ultrasonicWaveForce, ForceMode.Impulse);
                }
            }
        }
    }
}
