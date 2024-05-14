using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform spawnPos;
    [SerializeField] private GameObject bulletPF;
    [SerializeField] private uint mag;
    [SerializeField] private uint rpm;

    private uint currentMag;
    private float cd;
    private float timer;
    private static float Delta(uint value) => 60f / value;

    private void Start()
    {
        cd = Delta(rpm);
        timer = cd;
        currentMag = mag;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (CharacterMovement.isShooting && timer >= cd && currentMag > 0)
        {
            Shoot();
            timer = 0;
            currentMag--;
        }
    }

    private void Shoot()
    {
        GameObject go = Instantiate(bulletPF, spawnPos.position, spawnPos.rotation);
        go.GetComponent<Rigidbody>().AddForce(spawnPos.forward * 100, ForceMode.Impulse);
    }
}
