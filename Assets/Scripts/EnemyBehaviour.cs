using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour , IDamageable
{
    public int Hp { get; private set; } = 5;
    public bool IsDead => Hp <= 0;
    public void TakeDamage(int damage)
    {
        Hp -= damage;
        if (IsDead)
        {
            Destroy(gameObject);
        }
    }
}
