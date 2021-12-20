using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Playerhurt : MonoBehaviour
{
    public HealthBar healthBar;
    int MaxHealthy = 100;
    int currentHealthy;
    Animator animator;
    GameObject camera;
    // Start is called before the first frame update
    void Start()
    {

        animator = GetComponent<Animator>();
        currentHealthy = MaxHealthy;
        healthBar.SetMaxHealth(currentHealthy);
        camera = GameObject.Find("PlayerCamera");

    }
    public void increasehelthy()
    {
        if (currentHealthy > 80)
        {
            currentHealthy = 100;
        }
        if (currentHealthy <= 80)
        {
            currentHealthy += 20;
        }
        healthBar.SetHealth(currentHealthy);
    }

    public void TakeDamge(int damage)
    {
        animator.SetTrigger("hurt");
        currentHealthy -= damage;
        healthBar.SetHealth(currentHealthy);
        if (currentHealthy <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        //Enemy = GameObject.Find("EnemyPrefabs");
        //Play die Animation
        animator.SetBool("IsDead", true);
        FindObjectOfType<AudioManger>().Stop("Theme");
        FindObjectOfType<AudioManger>().play("Death");
        Invoke("Gameover",2.5f);
        GetComponent<Collider>().enabled = false;
        GetComponent<TowDimntionalAnimationController>().enabled = false;
        GetComponent<PlayerRifleCombat>().enabled = false;
        camera.GetComponent<CameraFollow>().enabled = false;
        this.enabled = false;
        //stop enemy
    }
    public bool IsActualyDead()
    {
        return animator.GetBool("IsDead");
    }
    void Gameover()
    {
           FindObjectOfType<PlayerRifleManger>().GameOver();
    }
}
