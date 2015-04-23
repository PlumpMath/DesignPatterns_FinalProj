using UnityEngine;
using System.Collections;

//Attach this script to the main camera and make sure an object is parented to the Player called 'targetLookAt'
//move camera around and positions properly
//takes input on it's own. even thogh TP_Controller has a set of input management, 
//our camera can exist on its own if it wants, for something like a flythrough/cutscene 
//uses mouse movement and scroll to set the camera position/rotation
//takes control of a main camera or creates it's own

//occlusion culling: make sure all small items are on Ignore Raycast layer. eg: spider webs, hanging ropes. things
//that if htey intersect with one of our cip plane points it won't move the camera. 

public class TP_Camera : MonoBehaviour 
{
    public static TP_Camera instance;
    public Transform targetLookAt;//target on our player to which camera looks at
    public float distance = 5f;//our current distance
    public float distanceMin = 3f;
    public float distanceMax = 10f;
    public float distanceSmooth = 0.05f;//smoothing our camera as it moves toward and away from player
    public float X_MouseSensitivity = 5f;
    public float Y_MouseSensitivity = 5f;
    public float mouseWheelSensitivity = 5f;
    public float X_Smooth = 0.05f;
    public float Y_Smooth =  0.1f;
    public float Y_MinLimit = -40f;
    public float Y_MaxLimit = 80f;


    private float mouseX = 0f;
    private float mouseY = 0f;
    private float velX = 0f;
    private float velY = 0f;
    private float velZ = 0f;
    private float velDistance = 0f;
    private float startDistance = 0f;
    private Vector3 position = Vector3.zero;
    private float desiredDistance = 0f;
    private Vector3 desiredPosition = Vector3.zero;
    public float deathCameraHeight = 60f;
    public float deathCameraSpin = 10f;
    public float deathCameraDistance = 5f;

    //occlusion
    public float occlusionDistanceStep = 0.5f;//how much we jump forward on each occlusion check
    public int maxOcclusionChecks = 10;//how many times we check before just jumping to the position
    
    //reset position after occlusion
    public float distanceResumeSmooth = 1f;//smoothing used when moving back to old position.
    //we have a distanceSmooth value which works for regular camera smoothing and a preOccludedSmooth which is only for
    //moving camera back to position. all our distance smoothing will be set by distanceSmoothAlt and it will set its values
    //based on those other 2 smoothings depending on the situation
    private float distanceSmoothAlt = 0f;//alternates between distanceSmooth and distanceResumeSmooth O_o?
    private float preOccludedDistance = 0;//our distance before occluding
	// Use this for initialization
	void Awake () 
    {
        instance = this;
       
	}

    void Start()
    {
        distance = Mathf.Clamp(distance, distanceMin, distanceMax);
        startDistance = distanceMax;
        Reset();
    }

    //want to make sure to update our camera after everything in the game is calculated
	void LateUpdate () 
    {
        if (targetLookAt == null)
        {
            return;
        }

        if (!TP_Animator.instance.isDead)
        {
            HandlePlayerInput();
        }
        else
        {
            desiredDistance = deathCameraDistance;
            mouseX += Time.deltaTime * deathCameraSpin;
            mouseY = deathCameraHeight;
        }
             
        int count = 0;
        do
        {
            CalculateDesiredPosition();
            count++;
        } while (CheckIfOccluded(count));

        CalculateDesiredPosition();  

        UpdatePosition();
	    
	}

    void HandlePlayerInput()
    {
        float deadZone = 0.01f;//can set in Input >_>

        if (Input.GetMouseButton(1))//right mouse button
        {
            mouseX += Input.GetAxis("Mouse X") * X_MouseSensitivity;
            mouseY -= Input.GetAxis("Mouse Y") * Y_MouseSensitivity;
                
        }

        //clamp our mouse y rotation
        mouseY = Helper.ClampAngle(mouseY, Y_MinLimit, Y_MaxLimit);

        if (Input.GetAxis("Mouse ScrollWheel") < -deadZone || Input.GetAxis("Mouse ScrollWheel") > deadZone)
        {
            desiredDistance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * mouseWheelSensitivity, distanceMin, distanceMax);
            //if we adjusted camera distance after occluding, we'll reset our preOccluded so we dont move back to it later
            preOccludedDistance = desiredDistance;
            distanceSmoothAlt = distanceSmooth;//we know that we are going to smooth with normal smooth,not preoccluded

        }

    }

    void CalculateDesiredPosition()
    { 
        //evaluate distance
        ResetDesiredDistance();
        distance = Mathf.SmoothDamp(distance, desiredDistance, ref velDistance, distanceSmoothAlt);
        //calculate desired position
        desiredPosition = CalculatePosition(mouseY, mouseX, distance);
    }

    Vector3 CalculatePosition(float rotationX, float rotationY, float distance)
    {
        Vector3 direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
        return targetLookAt.position + rotation * direction;
    }

    //while this is true we know that we are occluded and continue checking until max limit of checks
    bool CheckIfOccluded(int count)
    {
        bool isOccluded = false;

        float nearestDistance = CheckCameraPoints(targetLookAt.position, desiredPosition);
        if(nearestDistance != -1)
        {
            if (count < maxOcclusionChecks)
            {
                isOccluded = true;
                distance -= occlusionDistanceStep;
                if (distance < 0.25f)//if a camera gets too close to target, it doesnt come closer
                {
                    distance = 0.25f;
                }
            }
            else//we've checked too many times, just give up and move to point
            {
                distance = nearestDistance - Camera.main.nearClipPlane;
            }

            desiredDistance = distance;
            distanceSmoothAlt = distanceResumeSmooth;//we know we are occluded
        }

        return isOccluded;
    }

    float CheckCameraPoints(Vector3 from, Vector3 to)
    {
        float nearestDistance = -1f;//an impossible value for nearest raycast, so we can tell if we are not occluded

        RaycastHit hitInfo;

        //calculate points of clip plane
        //accessing Helper's sctructure, to creat our own structure then assigning values based on the funciton
        Helper.ClipPlanePoints clipPlanePoints = Helper.ClipPlaneAtNear(to);//passing the camera point?

        //Draw lines in editor for visualization
        //multiplyin gby negative makes the point go back behind the camera by the distance between cam to clip plane
        Debug.DrawLine(from, to + transform.forward* -GetComponent<Camera>().nearClipPlane, Color.red);
        Debug.DrawLine(from, clipPlanePoints.UpperLeft);
        Debug.DrawLine(from, clipPlanePoints.LowerLeft);
        Debug.DrawLine(from, clipPlanePoints.UpperRight);
        Debug.DrawLine(from, clipPlanePoints.LowerRight);
        //drawing the outline of the clip plane
        Debug.DrawLine(clipPlanePoints.UpperLeft, clipPlanePoints.UpperRight);
        Debug.DrawLine(clipPlanePoints.UpperRight, clipPlanePoints.LowerRight);
        Debug.DrawLine(clipPlanePoints.LowerRight, clipPlanePoints.LowerLeft);
        Debug.DrawLine(clipPlanePoints.LowerLeft, clipPlanePoints.UpperLeft);

        //figuring out nearest
        if (Physics.Linecast(from, clipPlanePoints.UpperLeft, out hitInfo) && hitInfo.collider.tag != "Player")
        {
            nearestDistance = hitInfo.distance;
        }
        if (Physics.Linecast(from, clipPlanePoints.LowerLeft, out hitInfo) && hitInfo.collider.tag != "Player")
        {
            if(hitInfo.distance < nearestDistance || nearestDistance == -1)
            nearestDistance = hitInfo.distance;
        }
        if (Physics.Linecast(from, clipPlanePoints.UpperRight, out hitInfo) && hitInfo.collider.tag != "Player")
        {
            if (hitInfo.distance < nearestDistance || nearestDistance == -1)
                nearestDistance = hitInfo.distance;
        }
        if (Physics.Linecast(from, clipPlanePoints.LowerRight, out hitInfo) && hitInfo.collider.tag != "Player")
        {
            if (hitInfo.distance < nearestDistance || nearestDistance == -1)
                nearestDistance = hitInfo.distance;
        }
        if (Physics.Linecast(from, to + transform.forward * -GetComponent<Camera>().nearClipPlane, out hitInfo) && hitInfo.collider.tag != "Player")
        {
            if (hitInfo.distance < nearestDistance || nearestDistance == -1)
                nearestDistance = hitInfo.distance;
        }

        //we've figured out our nearest distance

        return nearestDistance;
    }

    void ResetDesiredDistance()
    {
        //is desiredDistance closer?
        if (desiredDistance < preOccludedDistance)//if yes, we know our camera was repositioned/occluded
        {
            //based on our mouse rotations, determines the preoccluded position in space
            Vector3 pos = CalculatePosition(mouseY, mouseX, preOccludedDistance);
            float nearestDistance = CheckCameraPoints(targetLookAt.position, pos);//returns -1 if it found no collisions
            if (nearestDistance == -1 || nearestDistance > preOccludedDistance)//no colisions detected or we're ina n open area and cam can push back a bit
            {
                desiredDistance = preOccludedDistance;
            }
        }
    }

    void UpdatePosition()
    {
        float posX = Mathf.SmoothDamp(position.x, desiredPosition.x, ref velX, X_Smooth);
        float posY = Mathf.SmoothDamp(position.y, desiredPosition.y, ref velY, Y_Smooth);
        float posZ = Mathf.SmoothDamp(position.z, desiredPosition.z, ref velZ, X_Smooth);
        position = new Vector3(posX, posY, posZ);

        transform.position = position;
        transform.LookAt(targetLookAt);
    }

    public void Reset()
    {
        mouseX = 0;
        mouseY = 10;
        distance = startDistance;
        desiredDistance = distance;
        preOccludedDistance = distance;
    }

    public static void UseExistingOrCreateNewCamera()
    {
        GameObject tempCamera;
        GameObject targetLookAt;
        TP_Camera myCamera;
        if (Camera.main != null)//if a main cam exists
        {
            tempCamera = Camera.main.gameObject;
        }
        else
        {
            tempCamera = new GameObject("MainCamera");
            tempCamera.AddComponent<Camera>();
            tempCamera.tag = "MainCamera";
        }

        if (tempCamera.GetComponent<TP_Camera>() == null)
        {
            tempCamera.AddComponent<TP_Camera>();//adds a script, would probably want to take this whole funciton out... >_>
        }

        myCamera = tempCamera.GetComponent<TP_Camera>() as TP_Camera;

        targetLookAt = GameObject.Find("targetLookAt") as GameObject;//looks for a targetLookAt on the Player
        if (targetLookAt == null)
        {
            targetLookAt = new GameObject("targetLookAt");
            targetLookAt.transform.position = Vector3.zero;
        }

        myCamera.targetLookAt = targetLookAt.transform;
    }
}
 

