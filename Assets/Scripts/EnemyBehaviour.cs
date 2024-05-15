using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public int Hp {  get; private set; }
    public bool isDead => Hp <= 0;
    public void TakeDamage(int damage)
    {
        Hp -= damage;
        if (isDead)
        {
            Destroy(gameObject);
        }
    }
}
