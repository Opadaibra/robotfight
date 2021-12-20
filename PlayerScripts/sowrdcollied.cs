using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sowrdcollied : MonoBehaviour
{
    GameObject x1;
    Material x;
    float time;
    int Attacked;
    TowDimntionalAnimationController two;
    private void Start()
    {
            
    }
   
    private void OnCollisionEnter(Collision collision)
    {
        time = Time.time;
        
        if(collision.collider.tag=="cube" )
        {
            Debug.Log(collision.collider.tag);
            if(time > 5)
                Destroy(collision.gameObject);
        }       
    }
}
