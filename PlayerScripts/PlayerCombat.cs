using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    Animator animator;
    GameObject particle;
    public Transform AttackPoint;
    public LayerMask EnemyLayers;

    public float AttackRange = 0.5f;
    public int AttackDamage = 20;

    public float hittinghold = 2f;
    float nexthit = 0f;
    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        particle = GameObject.Find("light");
        particle.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (AttackPoint == null)
            return;
        if(Time.time >= nexthit)
        {
            if(Input.GetMouseButton(0))
            {
                Attack();
                particle.SetActive(true);
                Invoke("turnoff", 1);
                nexthit = Time.time + 1f / hittinghold;
            }
       }
    }
    private void turnoff()
    {
        
        particle.SetActive(false);
    }
    void Attack()
    {
        animator.SetTrigger("Attack");
        Collider[] hitEnemies=Physics.OverlapSphere(AttackPoint.position, AttackRange,EnemyLayers);
        foreach(Collider enemy in hitEnemies)
        {
            Debug.Log("Hit"+enemy.name);
            enemy.GetComponentInChildren<enemyhurt>().TakeDamge(AttackDamage);
            
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
            return;
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
}
