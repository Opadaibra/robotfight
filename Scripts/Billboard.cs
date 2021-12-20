using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
	//public Transform cam;
    GameObject game;
    private void Start()
    {
        
            game = GameObject.Find("Opada");
    }
    void LateUpdate()
    {
		transform.LookAt(transform.position + game.transform.forward);
    }
}
