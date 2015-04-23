using UnityEngine;
using System.Collections;

//attach to Player along with TP_Controller.cs
//moves character around. takes MoveVEctor from TP_Controller and processes it
//converts player movement from local into world space
//assigns character rotation according to the camera
public class TP_Motor : MonoBehaviour 
{
    public static TP_Motor instance;//reference to current instance of this script
    public float forwardSpeed = 10f;
    public float backwardSpeed = 2f;
    public float strafingSpeed = 8f;
    public float slideSpeed = 10f;
    public float jumpSpeed = 6f;
    public Vector3 moveVector;

    //gravity
    public float gravity = 21f;//arbitrary value
    public float terminalVelocity = 20f;//cut off velocity for applying gravity (you wont fall faster than this)
    public float verticalVelocity;//do they mean jump velocity?

    //sliding:
    //slide threshold sets when we will slide, but character controller's Slope Limit sets what slope we can move up.
    //so if we are on a high slop but our threshold isn't set, we wont slide and we wont move up. 
    public float slidethreshold = 0.6f;//angle of the normal at which we will begin sliding(.6 is 60%)
    public float maxControllableSlideMagnitude = 0.4f;//if we are sliding too fast we dont have controller input anymore
    private Vector3 slideDirection;
    public bool isSliding { get; set; }

    //dying
    public float fatalFallHeight = 7f;
    private float startFallHeight;

    private bool isFalling = false;
    public bool IsFalling
    {
        get { return isFalling; }
        set
        {
            isFalling = value;

            if (isFalling)
            {
                startFallHeight = transform.position.y;
            }
            else
            {
                if (startFallHeight - transform.position.y > fatalFallHeight)
                {
                    TP_Controller.instance.Die();
                }
            }
        }
    }


	void Awake () 
    {
        instance = this;
	
	}
	
	public void UpdateMotor ()//called by TP_Controller and only updates if TP_Controller tells it to
    {
        SnapAlignCharacterWithCamera();
        ProcessMotion();
	}

    public void ResetMotor()
    {
        verticalVelocity = moveVector.y;//causes us to continuously fall
        moveVector = Vector3.zero;//keeps motion from being additive each frame
    }

    void ProcessMotion()
    {
        //Transform MoveVector to world space
        if (!TP_Animator.instance.isDead)
        {
            moveVector = transform.TransformDirection(moveVector);
        }
        else
        { 
            moveVector = new Vector3(0, moveVector.y,0);//in case we die in air, we keep our vertical velocity
        }

        //normalize MoveVector if magnitude > 1
        if (moveVector.magnitude > 1)
        {
            moveVector = Vector3.Normalize(moveVector);
        }

        //apply slide if applicable
        ApplySlide();

        //multiply MoveVector by MoveSpeed
        moveVector *= forwardSpeed;

        //reapply verticalVelocity moveVectory.y
        moveVector = new Vector3(moveVector.x, verticalVelocity, moveVector.z);

        //apply gravity
        ApplyGravity();

        //Actually move our character
        TP_Controller.characterController.Move(moveVector * Time.deltaTime);
    }

    void ApplyGravity()
    {
        if (moveVector.y > -terminalVelocity)
        { 
            moveVector = new Vector3(moveVector.x, moveVector.y - gravity * Time.deltaTime, moveVector.z);
        }
        if (TP_Controller.characterController.isGrounded && moveVector.y < -1)
        {
            moveVector = new Vector3(moveVector.x, -1, moveVector.z);
        }
        
    }

    void ApplySlide()
    {
        if (!TP_Controller.characterController.isGrounded)
        { return; }

        slideDirection = Vector3.zero;//reset slide direction
        RaycastHit hitInfo;

        //passing our position, but above ourselfs because we are shooting the ray downward into the floor
        //vector3.down is the direction we are shooting. (vector3.up = (0,1,0) vector3.down = (0,-1,0) )
        if(Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hitInfo))//save hit information in our variable
        {
            //check if normal is within our slideThreshold
            if (hitInfo.normal.y < slidethreshold)
            {
                slideDirection = new Vector3(hitInfo.normal.x, -hitInfo.normal.y, hitInfo.normal.z);//-y because we slide down
                if (!isSliding)
                {
                    TP_Animator.instance.Slide();//only start sliding if we're not alreay sliding
                }
                isSliding = true;
            }
            else
            {
                isSliding = false;
            }
        }

        if (slideDirection.magnitude < maxControllableSlideMagnitude)
        {           
            moveVector += slideDirection;//we just add slide vector to our own movement input
        }
        else//we're sliding too steep, cant control
        {
            moveVector = slideDirection;//no input to affect moveVector
        }




    }

    public void Jump()
    {
        if (TP_Controller.characterController.isGrounded)
        {
            verticalVelocity = jumpSpeed;  
        }
    }

    void SnapAlignCharacterWithCamera()
    {
        if (moveVector.x != 0 || moveVector.z != 0)//we are moving in either of the axis's
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, transform.eulerAngles.z);

        }
    }

    float MoveSpeed()
    {
        float moveSpeed = 0f;

        switch (TP_Animator.instance.direction)
        { 
            case TP_Animator.Direction.Stationary:
                moveSpeed = 0f;
                break;
            case TP_Animator.Direction.Forward:
                moveSpeed = forwardSpeed;
                break;
            case TP_Animator.Direction.Backward:
                moveSpeed = backwardSpeed;
                break;
            case TP_Animator.Direction.Left:
                moveSpeed = strafingSpeed;
                break;
            case TP_Animator.Direction.Right:
                moveSpeed = strafingSpeed;
                break;
            case TP_Animator.Direction.LeftForward:
                moveSpeed = forwardSpeed;
                break;
            case TP_Animator.Direction.RightForward:
                moveSpeed = forwardSpeed;
                break;
            case TP_Animator.Direction.LeftBackward:
                moveSpeed = backwardSpeed;
                break;
            case TP_Animator.Direction.RightBackward:
                moveSpeed = backwardSpeed;
                break;                         
        }

        if (isSliding)//if we're sliding
        {
            moveSpeed = slideSpeed;
        }

        return moveSpeed;
    }

}

