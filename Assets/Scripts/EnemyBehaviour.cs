using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour , IDamageable
{
    public int Hp { get; private set; } = 5;
    public bool IsDead => Hp <= 0;

    [SerializeField] public Transform target;
    private NavMeshAgent agent;



    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        agent.SetDestination(target.position);
    }
    public void TakeDamage(int damage)
    {
        Hp -= damage;
        if (IsDead)
        {
            Destroy(gameObject);
        }
    }


}
