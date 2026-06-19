using UnityEngine;

public abstract class BasePowerupData : ScriptableObject
{
    [SerializeField] protected float activeTime;
    public abstract void OnEquip(IPowerupUser user, PowerupInstance instance);
    public abstract void OnUnEquip(IPowerupUser user, PowerupInstance instance);
    public abstract void OnActivate(IPowerupUser user);
    public abstract void OnTick(IPowerupUser user);
    protected void ActivateTimer(PowerupInstance instance)
    {
        if (instance != null)
        {
            instance.SetUseTimer(true);
            instance.SetTimer(activeTime);
            instance.StartTimer();
        }
    }
    public PowerupInstance CreateInstance()
    {
        PowerupInstance newInstance = new();
        newInstance.SetPowerupData(this);
        return newInstance;
    }

}
