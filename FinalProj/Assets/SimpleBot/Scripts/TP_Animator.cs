using UnityEngine;
using System.Collections;

//holds and decides movement states

//if you have multiple meshes, have to assign mesh parts root in variable: meshRoot 

public class TP_Animator : MonoBehaviour 
{
    public enum Direction
    { 
        Stationary, Forward, Backward, Left, Right,
        LeftForward, RightForward, LeftBackward, RightBackward
    }

    public enum CharacterState
    { 
        Idle, Running, WalkBackward, StrafingLeft, StrafingRight, Jumping, 
        Falling, Landing, Climbing, Sliding, Using, Dead, ActionLocked
    }



    public Direction direction { get; set; }
    public CharacterState state { get; set; }
    public bool isDead { get; set; }

    public static TP_Animator instance;

    private CharacterState lastState;//keep track of our last state '

    private Transform climbPoint;//for our climbing volume
    public Vector3 climbOffset = Vector3.zero;
    public Vector3 postClimbOffset = Vector3.zero; //makes sure charactercontroller snaps to character end of animation
    public float climbJumpStartTime = .16f;//the time of animation that player's toes are just about to leave ground
    public float climbAnchorTime = .33f;//figure out how much time in seconds into the animation we grab the ledge.
    private Transform root;//for matching our characterController to the root
    private Vector3 initialPosition = Vector3.zero;//respawn point
    private Quaternion initialRotation = Quaternion.identity;

    public Transform meshRoot;//the folder holding all our meshes (used for turning off each mesh renderer)

    private GameObject ragdoll;

    private float climbTargetOffset = 0f;//keeps camera on clibming
    private float climbInitialTargetHeight = 0f;

	void Awake () 
    {
        instance = this;
        root = transform.FindChild("root_jt") as Transform;
        initialPosition = transform.position;
        initialRotation = transform.rotation;
	}

    void Start()
    {
    }
	
	void Update () 
    {
        DetermineCurrentState();
        ProcessCurrentState();

  	}

    public void DetermineCurrentMoveDirection()
    {
        bool forward = false;
        bool backward = false;
        bool left = false;
        bool right = false;

        if (TP_Motor.instance.moveVector.z > 0)
        { forward = true; }
        if (TP_Motor.instance.moveVector.z < 0)
        { backward = true; }
        if (TP_Motor.instance.moveVector.x > 0)
        { right = true; }
        if (TP_Motor.instance.moveVector.x < 0)
        { left = true; }

        if (forward)
        {
            if (left)
            {
                direction = Direction.LeftForward;
            }
            else if (right)
            {
                direction = Direction.RightForward;            
            }
            else
            {
                direction = Direction.Forward;
            }
        
        }
        else if (backward)
        {
            if (left)
            {
                direction = Direction.LeftBackward;
            }
            else if (right)
            {
                direction = Direction.RightBackward;
            }
            else
            {
                direction = Direction.Backward;
            }
        }
        else if (left)
        {
            direction = Direction.Left;
        }
        else if (right)
        {
            direction = Direction.Right;
        }
        else
        {
            direction = Direction.Stationary;
        }

    }

    void DetermineCurrentState()
    { 
        if(state == CharacterState.Dead) { return; }

        if (!TP_Controller.characterController.isGrounded)//we're in the air
        {
            if (state != CharacterState.Falling &&
               state != CharacterState.Jumping &&
               state != CharacterState.Landing)
            {
                //we should be falling
                Fall();
            }            
        }

        //(we check falling again here because above we might have come into falling state)
        if (state != CharacterState.Falling && state != CharacterState.Jumping &&
           state != CharacterState.Landing && state != CharacterState.Using &&
           state != CharacterState.Climbing && state != CharacterState.Sliding)
        { 
            //we must be in a movement state
            switch(direction)
            {
                case Direction.Stationary:
                    state = CharacterState.Idle;
                    break;
                case Direction.Forward:
                    state = CharacterState.Running;
                    break;
                case Direction.Backward:
                    state = CharacterState.WalkBackward;
                    break;
                case Direction.Left:
                    state = CharacterState.StrafingLeft;
                    break;
                case Direction.Right:
                    state = CharacterState.StrafingRight;
                    break;
                case Direction.LeftForward:
                    state = CharacterState.Running;
                    break;
                case Direction.RightForward:
                    state = CharacterState.Running;
                    break;
                case Direction.LeftBackward:
                    state = CharacterState.WalkBackward;
                    break;
                case Direction.RightBackward:
                    state = CharacterState.WalkBackward;
                    break;
            }
        }
    }

    void ProcessCurrentState()
    {
        switch (state)
        {
            case CharacterState.Idle:
                Idle();
                break;
            case CharacterState.Running:
                Running();
                break;
            case CharacterState.WalkBackward:
                WalkBackward();
                break;
            case CharacterState.StrafingLeft:
                StrafeLeft();
                break;
            case CharacterState.StrafingRight:
                StrafeRight();
                break;
            case CharacterState.Jumping:
                Jumping();
                break;
            case CharacterState.Falling:
                Falling();
                break;
            case CharacterState.Landing:
                Landing();
                break;
            case CharacterState.Climbing:
                Climbing();
                break;
            case CharacterState.Sliding:
                Sliding();
                break;
            case CharacterState.Using:
                Using();
                break;
            case CharacterState.Dead:
                Dead();
                break;
            case CharacterState.ActionLocked:
                break;
        }
    }

    #region Character State Methods

    void Idle()
    { 
        GetComponent<Animation>().CrossFade("Idle");
    }

    void Running()
    {
        GetComponent<Animation>().CrossFade("RunForward");
    }

    void WalkBackward()
    {
        GetComponent<Animation>().CrossFade("WalkBack");
    }

    void StrafeLeft()
    {
        GetComponent<Animation>().CrossFade("StrafeLeft");
    }

    void StrafeRight()
    {
        GetComponent<Animation>().CrossFade("StrafeRight");
    }

    void Using()//checks if using anim is done and stops it. (not necesary in mecanim)
    {
        if (!GetComponent<Animation>().isPlaying)//this is supposed to be for when we are already in a using state and are ending
        {
            state = CharacterState.Idle;
            GetComponent<Animation>().CrossFade("Idle");
        }        
    }

    //TODO: wtf?
    void Jumping()//handles jumping state
    {
        if ((!GetComponent<Animation>().isPlaying && TP_Controller.characterController.isGrounded) ||
            TP_Controller.characterController.isGrounded)
        {//we are grounded, go straight into landing animation
            GetComponent<Animation>().CrossFade("JumpLand");  
            state = CharacterState.Landing;
        }
        else if(!GetComponent<Animation>().IsPlaying("Jump"))//we are still in air and jump anim has stopped
        {
            state = CharacterState.Falling;
            GetComponent<Animation>().CrossFade("Falling");
            TP_Motor.instance.IsFalling = true;
        }
        else//we must still be jumping
        {
            state = CharacterState.Jumping;       
        }
    }

    void Falling()
    {
        if (TP_Controller.characterController.isGrounded)
        {
            GetComponent<Animation>().CrossFade("JumpLand");
            state = CharacterState.Landing;
        }
    }

    void Landing()
    {
        if (lastState == CharacterState.Running)
        {
            if (!GetComponent<Animation>().IsPlaying("JumpLand"))//make sure we're not alreayd land animating
            {
                state = CharacterState.Running;
                GetComponent<Animation>().CrossFade("RunForward");
            }
        }
        else
        {
            if (!GetComponent<Animation>().IsPlaying("JumpLand"))
            {
                state = CharacterState.Idle;
                GetComponent<Animation>().CrossFade("Idle");
            }
        }

        TP_Motor.instance.IsFalling = false;
         
    }

    void Sliding()
    {
        if (!TP_Motor.instance.isSliding)//if we're no longer sliding
        {
            state = CharacterState.Idle;
            GetComponent<Animation>().CrossFade("Idle");
        }
    }

    void Climbing()
    {
        if (GetComponent<Animation>().isPlaying)
        {
            float time = GetComponent<Animation>()["Climb"].time;
            if (time > climbJumpStartTime && time < climbAnchorTime)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 
                                                        Mathf.Lerp(transform.rotation.eulerAngles.y,
                                                        climbPoint.rotation.eulerAngles.y, (time - climbJumpStartTime) / (climbAnchorTime - climbJumpStartTime) ),
                                                        transform.rotation.eulerAngles.z );

                Vector3 climbOffsetLocal = transform.TransformDirection(climbOffset);
                transform.position = Vector3.Lerp(transform.position,
                new Vector3(climbPoint.position.x,transform.position.y, climbPoint.position.z) + climbOffsetLocal,
                (time - climbJumpStartTime) / (climbAnchorTime - climbJumpStartTime));
            }

            //keep the camera following climb
            TP_Camera.instance.targetLookAt.localPosition = new Vector3(TP_Camera.instance.targetLookAt.localPosition.x,
                root.localPosition.y + climbTargetOffset,
                TP_Camera.instance.targetLookAt.localPosition.z);
        }
        else
        {//end of climbing animation
            state = CharacterState.Idle;
            GetComponent<Animation>().Play("Idle");
            Vector3 postClimbOffsetLocal = transform.TransformDirection(postClimbOffset);
            transform.position = new Vector3(root.position.x,
                climbPoint.position.y + climbPoint.localScale.y / 2,
                root.position.z) + postClimbOffsetLocal;

            //set back targetLookAt after climb
            TP_Camera.instance.targetLookAt.localPosition = new Vector3(TP_Camera.instance.targetLookAt.localPosition.x,
                climbInitialTargetHeight,
                TP_Camera.instance.targetLookAt.localPosition.z);
        }
    
    }

    void Dead()
    {
        state = CharacterState.Dead;
    }

    

    #endregion

    #region Start Action Methods

    //Use() does some stuff to get us ready to go into the Using state
    public void Use()//called in TP_Controller's Use() function
    {
        state = CharacterState.Using;
        GetComponent<Animation>().CrossFade("Using");
    }

    public void Jump()//called from TP_Controller.cs
    {
        if (!TP_Controller.characterController.isGrounded || isDead || state == CharacterState.Jumping)
        { return;  }

        lastState = state;
        state = CharacterState.Jumping;
        GetComponent<Animation>().CrossFade("Jump");

    }

    public void Fall()
    {
        //keeps from stuttering on uneven ground (going into fall state)
        if (TP_Motor.instance.verticalVelocity > -5 || isDead)
        { return; }

        lastState = state;
        state = CharacterState.Falling;
        TP_Motor.instance.IsFalling = true;
        GetComponent<Animation>().CrossFade("Falling");

    }

    public void Slide()
    {
        state = CharacterState.Sliding;
        GetComponent<Animation>().CrossFade("Falling");
    }

    public void Climb()
    {
        if (!TP_Controller.characterController.isGrounded || isDead || climbPoint == null)
        { return; }

        //if we're more than 60 degree's away from facing the cliff
        if (Mathf.Abs(climbPoint.transform.rotation.eulerAngles.y - transform.rotation.eulerAngles.y) > 60)
        {
            TP_Controller.instance.Jump();
            return;
        }

        state = CharacterState.Climbing;
        GetComponent<Animation>().CrossFade("Climb");

        //because our root is what moves while animating, we are getting the offset between the roto and
        //the targetLookAt
        climbTargetOffset = TP_Camera.instance.targetLookAt.localPosition.y - root.localPosition.y;
        climbInitialTargetHeight = TP_Camera.instance.targetLookAt.localPosition.y;
    }

    public void Die()//initialize everything we need to die
    {
        isDead = true; 
        SetupRagdoll();
        Dead();

    }

    public void Reset()//come back to life
    {
        isDead = false;
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        state = CharacterState.Idle;
        GetComponent<Animation>().Play("Idle");
        ClearRagdoll();
    }

    #endregion

    public void SetClimbPoint(Transform climbPoint)
    {
        this.climbPoint = climbPoint;
        TP_Controller.instance.climbEnabled = true;
    }

    public void ClearClimbPoint(Transform climbPoint)
    {
        if (this.climbPoint == climbPoint)
        {
            this.climbPoint = null;
            TP_Controller.instance.climbEnabled = false;
        }
    }

    void SetupRagdoll()
    { 
        //instantiate ragdoll
        //match character position and rotation
        if (ragdoll == null)
        {
            ragdoll = GameObject.Instantiate(Resources.Load("Ragdoll_Simple"),
                                            transform.position,
                                            transform.rotation) as GameObject;

        }
               

        //match ragdoll's skeleton to character's. (so its not tpose)
        Transform characterRoot = transform.FindChild("root_jt");
        Transform ragdollRoot = ragdoll.transform.FindChild("root_jt");
        MatchChildrenTransform(characterRoot, ragdollRoot);

        //hide our character
        foreach (Transform child in meshRoot)
        {
            child.GetComponent<Renderer>().enabled = false;
        }

        //tell camera to look at ragdoll
        TP_Camera.instance.targetLookAt = ragdoll.transform.FindChild("root_jt/chest_jt");
    }

    void ClearRagdoll()
    { 
        //destroy ragdoll
        if (ragdoll != null)
        {
            GameObject.Destroy(ragdoll);
            ragdoll = null;
        }
        
        //show character again
        foreach (Transform child in meshRoot)
        {
            child.GetComponent<Renderer>().enabled = true;
        }

        //tell cam to look at character's lookAtTarget
        TP_Camera.instance.targetLookAt = transform.FindChild("targetLookAt");
    
    }

    void MatchChildrenTransform(Transform source, Transform target)
    { 
        //march through skeleton heirarchy, matching joint rotations.
        //continues to call itself until theres no more children in that object
        if (source.childCount > 0)
        {
            foreach(Transform sourceTransform in source.transform)
            {
                Transform targetTransform = target.FindChild(sourceTransform.name);//counterpart of source child

                if (targetTransform != null)
                {
                    MatchChildrenTransform(sourceTransform, targetTransform);
                    targetTransform.localPosition = sourceTransform.localPosition;
                    targetTransform.localRotation = sourceTransform.localRotation;
                      
                }
            }


        
        }
    }
}
