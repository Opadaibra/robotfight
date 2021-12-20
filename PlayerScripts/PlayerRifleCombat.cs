using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerRifleCombat : MonoBehaviour
{
    
    Animator animator;
    public int Damage = 10;
    public float Range = 100f;
    public float FireRate = 15f;
    public int Ammo = 30;
    //Crosshair object
    GameObject Crooshair;

     ///
    GameObject camera1;
    public GameObject Flashlight ;
    public GameObject Impact ;
    
    bool Reloading=false;
    private float nexttimetofire = 0;
    public int Ammolimit = 90;
    private void Start()
    {
        animator = GetComponent<Animator>();
        camera1 = GameObject.Find("PlayerCamera");
        Crooshair = GameObject.Find("Cursor");
        Flashlight.SetActive(false);
        Debug.Log(Flashlight.name);
        Ammo = Ammolimit;
    }
    void Update()
    {
        if (Reloading)
        {
            if(Input.GetMouseButton(0))
                FindObjectOfType<AudioManger>().play("triggerclick");
            return;
        }
        if(Ammo <= 0)
        {
            StartCoroutine(Reload());
            Flashlight.SetActive(false);
            animator.SetBool("Attack", false);
            return;
        }
        if (Input.GetMouseButton(0) && Time.time >= nexttimetofire && Ammo > 0 )
            {
                nexttimetofire = Time.time + 1 / FireRate;
                Shoot();
                Flashlight.SetActive(true);
                Crooshair.GetComponent<scalCrosshair>().TriggingCross();
            }

        else if (!Input.GetMouseButton(0))
        {
            animator.SetBool("Attack", false);
            Flashlight.SetActive(false);
            Crooshair.GetComponent<scalCrosshair>().UnTriggingCross();
        }
    }
    void Shoot()
    {
        Ammo--;  
        RaycastHit hit;
        if(Physics.Raycast(camera1.transform.position,camera1.transform.forward,out hit,Range))
        {
            
            enemyhurt Enemy= hit.transform.GetComponent<enemyhurt>();
            if(Enemy!=null)
            {
                Enemy.TakeDamge(10);
                    
            }
            if(hit.rigidbody!=null)
            {
                hit.rigidbody.AddForce(-hit.normal * 30);
            }
           GameObject go= Instantiate(Impact, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(go, 1f);
       }
        FindObjectOfType<AudioManger>().play("shoot");
        
        animator.SetBool("Attack", true);
    }
    public void IncreasAmmo()
    {
        Ammo += 200;
    }
   
    
    
    IEnumerator Reload()
    {
        Reloading = true;
        FindObjectOfType<AudioManger>().play("Reload");
        animator.SetTrigger("Reload");
        yield return new WaitForSeconds(3.35f);
        Ammo = Ammolimit;
        Reloading = false;
    }


}
