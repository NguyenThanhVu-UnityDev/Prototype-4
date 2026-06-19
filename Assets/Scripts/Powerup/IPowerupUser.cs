using UnityEngine;

public interface IPowerupUser
{
    public GameObject GetUser();
    public bool OnReceivePowerup(PowerupInstance powerup);
    public void OnUnequipPowerup(PowerupInstance powerup);
    public void ActivatePowerupIndicator();
    public void DeactivatePowerupIndicator();
    public void UpdatePowerups();
}
