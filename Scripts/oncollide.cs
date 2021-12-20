using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oncollide : MonoBehaviour
{
   
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag=="Player")
        {
            Debug.Log("we hit hit y bit" + collision.collider.tag);
            FindObjectOfType<Playerhurt>().increasehelthy();
            gameObject.SetActive(false);
        }
    }
}
