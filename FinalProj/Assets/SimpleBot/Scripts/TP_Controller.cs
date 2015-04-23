using UnityEngine;
using System.Collections;

//attach to Player along with TP_Motor.cs
//Processes Input from player (does not take mouse input, thats done in TP_Camera)
//stores input in a 3D Vector, and then sends TP_Motor the info in order to move player
//constantly checks to make sure we have a camera
public class TP_Controller : MonoBehaviour 
{
    public static CharacterController characterController;//static so we can call it by using the class name
    public static TP_Controller instance;//reference to current instance of this script
    //set in TP_Animator::SetClimbPoint() which is called from ClimbingVolume::OnTriggerEnter()
    public bool climbEnabled { get; set; }//determines funcitonality of jump button

	// Use this for initialization
	void Awake () 
    {
        //characterController = GetComponent("CharacterController") as CharacterController;
        characterController = GetComponent<CharacterController>();
        instance = this;
        TP_Camera.UseExistingOrCreateNewCamera();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Camera.main == null)//if no camera in scene, dont do anything    
        { return; }

        TP_Motor.instance.ResetMotor();

        if (!TP_Animator.instance.isDead &&
            TP_Animator.instance.state != TP_Animator.CharacterState.Using &&
            TP_Animator.instance.state != TP_Animator.CharacterState.Landing &&//would add ( || TP_Animator.instance.animation.IsPlaying("RunLand") ) i.a.
            TP_Animator.instance.state != TP_Animator.CharacterState.Landing)
        {
            GetLocomotionInput();
            HandleActionInput();
        }
        else if(TP_Animator.instance.isDead)//if we're dead and you hit a key
        {
            if (Input.anyKeyDown)
            { TP_Animator.instance.Reset(); }
        }

        

        TP_Motor.instance.UpdateMotor();

	}

    void GetLocomotionInput()
    {       

        float deadZone = 0.1f;//this can be set in Input manager >_>  

        if (Input.GetAxis("Vertical") > deadZone || Input.GetAxis("Vertical") < -deadZone)
        {
            TP_Motor.instance.moveVector += new Vector3(0, 0, Input.GetAxis("Vertical"));
        }
        if (Input.GetAxis("Horizontal") > deadZone || Input.GetAxis("Horizontal") < -deadZone)
        {
            TP_Motor.instance.moveVector += new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        }

        TP_Animator.instance.DetermineCurrentMoveDirection();

    }

    void HandleActionInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (climbEnabled)
            {
                Climb();
            }
            else
            {
                Jump();
            }
        }

        if(Input.GetKeyDown(KeyCode.E) )
        {
            Use();
        }

        if(Input.GetKeyDown(KeyCode.F1) )
        {
            Die();
        }
    }

    public void Jump()
    {
        TP_Motor.instance.Jump();//the motor moves the player into the air
        TP_Animator.instance.Jump();//the animator plays jump animation

    }

    public void Use()
    {
        //the place to add sound effects or particles maybe?
        TP_Animator.instance.Use();
    }

    public void Climb()
    {
        TP_Animator.instance.Climb();
    }

    public void Die()
    {
        TP_Animator.instance.Die();
    }
}
