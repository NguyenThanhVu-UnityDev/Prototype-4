using System;
using UnityEngine;

public class PowerupInstance
{
    [SerializeField] BasePowerupData powerupData;
    [SerializeField] IPowerupUser powerupUser;
    public Action<IPowerupUser, Collision> OnUserCollisionEnter;
    public Action<IPowerupUser, Collider> OnUserTriggerEnter;

    private bool useTimer = true;
    private bool isCountingDown = true;
    private float timer = 0;

    public void SetUseTimer(bool value) => useTimer = value;
    public void SetTimer(float time) => timer = time;
    public void StartTimer() => isCountingDown = true;
    public void StopTimer() => isCountingDown = false;

    public bool ComparePowerUp(BasePowerupData data)
    {
        if (powerupData == null) return false;
        if (powerupData == data) return true;
        return false;
    }

    public void SetPowerupData(BasePowerupData data)
    {
        if (data == null) return;
        powerupData = data;
    }

    public void OnEquip(IPowerupUser user)
    {
        if (powerupData != null) powerupData.OnEquip(user, this);
        powerupUser = user;
    }
    public void OnUnEquip()
    {
        if (powerupData != null) powerupData.OnUnEquip(powerupUser, this);
        if (powerupUser != null) powerupUser.OnUnequipPowerup(this);
        powerupData = null;
        powerupUser = null;
    }
    public void OnActivate()
    {
        if (powerupData != null) powerupData.OnActivate(powerupUser);
    }
    public void OnTick()
    {
        if (powerupData != null) powerupData.OnTick(powerupUser);
        if (useTimer && isCountingDown && timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                if (powerupData != null) powerupData.OnUnEquip(powerupUser, this);
                if (powerupUser != null) powerupUser.OnUnequipPowerup(this);

                timer = 0;
                isCountingDown = false;
            } 
        }
    }
}
