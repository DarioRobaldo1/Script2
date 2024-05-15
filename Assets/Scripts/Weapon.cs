using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform spawnPos;
    [SerializeField] private BulletBehaviour bulletPF;
    [SerializeField] private uint mag;
    [SerializeField] private uint rpm;
    [SerializeField] private uint reloadTime;

    private uint currentMag;
    private float cd;
    private float timer;
    private bool isReloading;
    private float bulletForce = 100f;
    private int bulletDamage = 2;
    private Queue<BulletBehaviour> bulletQ = new();
    

    private static float Delta(uint value) => 60f / value;
    private void ResetMag() => currentMag = mag;


    private void Start()
    {
        cd = Delta(rpm);
        timer = cd;
        currentMag = mag;
        PreparePool(bulletQ, bulletPF, spawnPos, mag);
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    private IEnumerator WaitToReload(float time)
    {
        isReloading = true;
        ResetMag();
        yield return new WaitForSeconds(time);
        isReloading = false;
    }

    public void Shooting()
    {
        
        if (timer > cd && currentMag > 0)
        {
            Debug.Log("SH");
        timer = 0;
        currentMag--;
        BulletBehaviour currentGO = bulletQ.Dequeue();
        currentGO.Shoot(spawnPos, bulletForce, bulletDamage);
        bulletQ.Enqueue(currentGO);
            
        }
        
    }

    public void Reload()
    {
        StartCoroutine(WaitToReload(reloadTime));
    }

    private void PreparePool(Queue<BulletBehaviour> q, BulletBehaviour prefab, Transform pos, uint amount)
    {
        q.Clear();
        for (int i = 0; i < amount; i++)
        {
            /*BulletBehaviour bb = Instantiate(prefab, pos.position, pos.rotation).GetComponent<BulletBehaviour>();
            q.Enqueue(bb);*/
            
            
            BulletBehaviour go = Instantiate(prefab, pos.position, pos.rotation);
            q.Enqueue(go);
        }

        
    }
    private bool CanShoot() => CharacterMovement.isShooting 
        && timer >= cd 
        &&currentMag > 0 
        && !isReloading;
}
