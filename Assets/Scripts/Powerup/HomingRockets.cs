using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/HomingRocket")]
public class HomingRockets : BasePowerupData
{
    [SerializeField] Rocket rocketPrefab;
    [SerializeField] float shootInterval = 1;
    [SerializeField] float detectionRadius = 10f;

    private float timer = 0;
    public override void OnActivate(IPowerupUser user)
    {
    }

    public override void OnEquip(IPowerupUser user, PowerupInstance instance)
    {
        ActivateTimer(instance);
        user.ActivatePowerupIndicator();
    }

    public override void OnTick(IPowerupUser user)
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Shoot(user);
            timer = Mathf.Max(0.01f, shootInterval);
        }
    }

    public override void OnUnEquip(IPowerupUser user, PowerupInstance instance)
    {
        user.DeactivatePowerupIndicator();
    }

    // Not implement Object Pooling yet!!!
    private void Shoot(IPowerupUser user)
    {
        if (rocketPrefab == null) return;
        var colliders = Physics.OverlapSphere(user.GetUser().transform.position, detectionRadius);
        foreach(var collider in colliders)
        {
            if (collider.GetComponent<Enemy>() != null)
            {
                var newRocket = Instantiate(rocketPrefab, user.GetUser().transform.position, rocketPrefab.transform.rotation);
                if (newRocket == null) Debug.LogWarning("Failed to spawn rocket");
                else
                {
                    newRocket.SetTarget(collider.gameObject);
                }
            }
        }
    }
}
