using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] AmmoSlot[] ammoSlots;
    
    [System.Serializable]
    private class AmmoSlot
    {
        public AmmoType ammoType;
        public int ammoAmmount;
    }

    public int GetCurrentAmmo(AmmoType ammoType)
    {
        return GetAmmoSlot(ammoType).ammoAmmount;
    }

    public void ReduceCurrentAmmo(AmmoType ammoType)
    {
        GetAmmoSlot(ammoType).ammoAmmount--;
    }

    public void IncreaseCurrentAmmo(AmmoType ammoType, int ammoAmmount)
    {
        GetAmmoSlot(ammoType).ammoAmmount += ammoAmmount;
    }

    private AmmoSlot GetAmmoSlot(AmmoType ammoType) 
    { 
        foreach (AmmoSlot slot in ammoSlots)
        { 
            if (slot.ammoType == ammoType)
            {
                return slot;
            }
        }
        return null;
    }
}