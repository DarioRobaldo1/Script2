using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform spawnPos;
    [SerializeField] private GameObject bulletPF;
    [SerializeField] private uint mag;
    [SerializeField] private uint rpm;
    [SerializeField] private uint reloadTime;

    private uint currentMag;
    private float cd;
    private float timer;
    private bool isReloading;
    private Queue<GameObject> bulletQ = new();

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

    public void Shoot()
    {
        
        if (timer > cd && currentMag > 0)
        {
        timer = 0;
        currentMag--;
        GameObject currentGO = bulletQ.Dequeue();
        Rigidbody currentRB = currentGO.GetComponent<Rigidbody>();
        currentGO.SetActive(true);
        currentGO.transform.SetPositionAndRotation(spawnPos.position, spawnPos.rotation);
        currentRB.velocity = Vector3.zero;
        currentRB.AddForce(spawnPos.forward * 100f, ForceMode.Impulse);
        bulletQ.Enqueue(currentGO);
            
        }
        
    }

    public void Reload()
    {
        StartCoroutine(WaitToReload(reloadTime));
    }

    private Queue<GameObject> PreparePool(Queue<GameObject> q, GameObject prefab, Transform pos, uint amount)
    {
        q.Clear();
        for (int i = 0; i < amount; i++)
        {
            /*BulletBehaviour bb = Instantiate(prefab, pos.position, pos.rotation).GetComponent<BulletBehaviour>();
            q.Enqueue(bb);*/
            
            
            GameObject go = Instantiate(prefab, pos.position, pos.rotation);
            go.SetActive(false);
            q.Enqueue(go);
        }

        return null;
    }
    private bool CanShoot() => CharacterMovement.isShooting 
        && timer >= cd 
        &&currentMag > 0 
        && !isReloading;
}
