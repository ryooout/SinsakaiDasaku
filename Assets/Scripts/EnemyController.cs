using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour
{
    public Transform target;
    NavMeshAgent agent;
    Animator animator;
    public Collider weaponCollider;
    int enemyHp = 2;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = target.transform.position;
        animator.SetFloat("Distance", agent.remainingDistance);
    }
    private void OnTriggerEnter(Collider other)
    {
        Damage damager = other.GetComponent<Damage>();
        if (damager != null)
        {
            enemyHp--;
            Debug.Log("敵はダメージを受ける");
        }
        if(enemyHp==0)
        {
            Destroy(gameObject);
        }
    }
    public void HideColliderWeapon()
    {
        weaponCollider.enabled = false;
    }
    public void ShowColliderWeapon()
    {
        weaponCollider.enabled = true;
    }
}
