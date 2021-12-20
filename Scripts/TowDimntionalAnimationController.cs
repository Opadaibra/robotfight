    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class TowDimntionalAnimationController : MonoBehaviour
{
    CharacterController Player;
    Animator animator;
    float VelocityX=0.0f;
    float VelocityZ=0.0f;
    float accelaration = 2.0f;
    float decelaration = 1.0f;
    float maximumnwalkvelocity=0.5f;
    float maximumrunvelocity = 2.0f;
    Playerhurt playerhurt;
    PlayerRifleCombat playerRifle;
    int VelocityXHash;
    int VelocityZHash;


    float gravity = -12f;
    float jumpheight = 2;
    float velocityY;

   
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Player = GetComponent<CharacterController>();
       //player = GetComponent<Rigidbody>();
        VelocityZHash = Animator.StringToHash("Velocity Z");
        VelocityXHash = Animator.StringToHash("Velocity X");
        playerhurt = FindObjectOfType<Playerhurt>();
        playerRifle = FindObjectOfType<PlayerRifleCombat>();
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.tag=="Health")
        {
            playerhurt.increasehelthy();
            Destroy(other.gameObject);
        }
       if(other.tag=="Reload")
        {
            playerRifle.IncreasAmmo();
            Destroy(other.gameObject);
        }
    }
    //handless accelration and deceleration
    void ChangeVelocity(bool forwardpressed, bool rightPressed, bool lefttPressed,bool runpressed, float currrentmaxvelocity)
    {
        ////////moveforward///////
        if (forwardpressed && VelocityZ < currrentmaxvelocity)
        {
            VelocityZ += Time.deltaTime * accelaration;
        }
        ////move right
        if (rightPressed && VelocityX < currrentmaxvelocity)
        {
            VelocityX += Time.deltaTime * accelaration;
        }
        ///move left
        if (lefttPressed && VelocityX > -currrentmaxvelocity)
        {
            VelocityX -= Time.deltaTime * accelaration;
        }
        // implement decelration for Forward
        if (!forwardpressed && VelocityZ > 0.0f)
        {
            VelocityZ -= Time.deltaTime * decelaration;
        }
        //implemment decelration for Right
        if (!rightPressed && VelocityX > 0.0f)
        {
            VelocityX -= Time.deltaTime * decelaration;
        }
        //implemment decelration for left
        if (!lefttPressed && VelocityX < 0.0f)
        {
            VelocityX += Time.deltaTime * decelaration;
        }
    }
    void lockorupdateVelocity(bool forwardpressed, bool rightPressed, bool lefttPressed, bool runpressed, float currrentmaxvelocity)
    {
        if (!forwardpressed && VelocityZ < 0.0f)
        {
            VelocityZ = 0.0f;
        }


        if (!rightPressed && !lefttPressed && VelocityX != 0.0f && (VelocityX > -0.05 && VelocityX < 0.05f))
        {
            VelocityX = 0.0f;
        }
        ///////////////////foeward Running
        if (runpressed && forwardpressed && VelocityZ > currrentmaxvelocity)
        {
            VelocityZ = currrentmaxvelocity;
        }
        else if (forwardpressed && VelocityZ > currrentmaxvelocity)
        {
            VelocityZ -= Time.deltaTime * decelaration;
            if (VelocityZ > currrentmaxvelocity && VelocityZ < (currrentmaxvelocity + 0.05f))
            {
                VelocityZ = currrentmaxvelocity;
            }
        }
        else if (forwardpressed && VelocityZ < currrentmaxvelocity && VelocityZ > (currrentmaxvelocity - 0.05f))
        {
            VelocityZ = currrentmaxvelocity;
        }
        /////////rightPreessed with running
        if (runpressed && rightPressed && VelocityX > currrentmaxvelocity)
        {
            VelocityX = currrentmaxvelocity;
        }
        else if (rightPressed && VelocityX > currrentmaxvelocity)
        {
            VelocityX -= Time.deltaTime * decelaration;
            if (VelocityX > currrentmaxvelocity && VelocityX < (currrentmaxvelocity + 0.05f))
            {
                VelocityX = currrentmaxvelocity;
            }
        }
        else if (rightPressed && VelocityX < currrentmaxvelocity && VelocityX > (currrentmaxvelocity - 0.05f))
        {
            VelocityX = currrentmaxvelocity;
        }
        /////////////////////lefttPreessed with running
        if (runpressed && lefttPressed && VelocityX < -currrentmaxvelocity)
        {
            VelocityX = -currrentmaxvelocity;
        }
        else if (lefttPressed && VelocityX < -currrentmaxvelocity)
        {
            VelocityX += Time.deltaTime * decelaration;
            if (VelocityX < -currrentmaxvelocity && VelocityX > (-currrentmaxvelocity - 0.05f))
            {
                VelocityX = -currrentmaxvelocity;
            }
        }
        else if (lefttPressed && VelocityX > -currrentmaxvelocity && VelocityX < (-currrentmaxvelocity + 0.05f))
        {
            VelocityX = -currrentmaxvelocity;
        }
    }
    void Gravity()
    {
        velocityY += Time.deltaTime * gravity;
        Player.Move(Vector3.up * velocityY*Time.deltaTime);
        if(Player.isGrounded)
        {
            velocityY = 0;
        }
    }
    void jump()
    {
        animator.SetTrigger("jump");
        float jumpvelocity = Mathf.Sqrt(-2 * gravity * jumpheight);
        velocityY = jumpvelocity;
    }
    // Update is called once per frame
  
    void Update()
    {
        bool forwardpressed = Input.GetKey(KeyCode.W);
        bool lefttPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool runpressed = Input.GetKey(KeyCode.LeftShift);
        bool jumppressed = Input.GetKeyDown(KeyCode.Space);
        float currrentmaxvelocity = runpressed ? maximumrunvelocity : maximumnwalkvelocity;
        ChangeVelocity(forwardpressed, rightPressed, lefttPressed, runpressed, currrentmaxvelocity);
        lockorupdateVelocity(forwardpressed, rightPressed, lefttPressed, runpressed, currrentmaxvelocity);
        animator.SetFloat(VelocityZHash, VelocityZ);
        animator.SetFloat(VelocityXHash, VelocityX);
     
        Gravity();
        if (Player.isGrounded)
        {
            if (jumppressed)
            {
               
                jump();
                Debug.Log("Pressd");  
            }
        }
      }
 
}
