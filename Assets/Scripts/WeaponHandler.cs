using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private Weapon weapon;

    private void Update()
    {
        if (CharacterMovement.isShooting)
        {
            weapon.Shoot();
        }
        if (CharacterMovement.hasReloaded)
        {
            weapon.Reload();
        }
    }
}
