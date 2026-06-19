using System;
using UnityEngine;

public static class PlayerEvents
{
    public static Action<Collision> OnPlayerCollisionEnter;
    public static Action<Collider> OnPlayerTriggerEnter;

    public static void RaisePlayerCollisionEnter(Collision other)
    {
        OnPlayerCollisionEnter?.Invoke(other);
    }
    public static void RaisePlayerTriggerEnter(Collider other)
    {
        OnPlayerTriggerEnter?.Invoke(other);
    }
}
