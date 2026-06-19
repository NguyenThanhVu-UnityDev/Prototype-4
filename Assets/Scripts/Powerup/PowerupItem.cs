using UnityEngine;

public class PowerupItem : MonoBehaviour
{
    [SerializeField] BasePowerupData powerup;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IPowerupUser user))
        {
            if (user.OnReceivePowerup(powerup.CreateInstance()))
            {
                gameObject.SetActive(false);
            }
        }
    }

}
