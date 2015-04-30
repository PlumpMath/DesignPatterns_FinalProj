using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour
{
    public Transform player;//target on our player to which camera looks at    
    public float height = 7;

    float X_Smooth = 0.1f;//used for both x position and z
    float Y_Smooth = 0.1f;//positional smoothing
    Transform myTransform;
    private float velX = 0f;//required references for positional SmoothDamp()
    private float velY = 0f;
    private float velZ = 0f;
    Vector3 desiredPosition = Vector3.zero;
    float rotationX = 0;
    float rotationY = 35;
    private float mouseX = 35f;//for right click rotation
    private float mouseY = 0f;
    public float Y_MinLimit = -40f;
    public float Y_MaxLimit = 80f;

    private Vector3 position = Vector3.zero;
    public float distance = 7f;//our current distance
    public float distanceMin = 3f;
    public float distanceMax = 15f;
    public float distanceSmooth = 0.02f;//smoothing our camera as it moves toward and away from player
    public float X_MouseSensitivity = 5f;
    public float Y_MouseSensitivity = 5f;
    public float mouseWheelSensitivity = 10f;
    private float desiredDistance = 0f;
    private float startDistance = 0f;//save for cam reset
    private float velDistance = 0f;

    bool cam1 = false;

    public void Start()
    {
        myTransform = transform;
        desiredPosition = player.position + (player.forward * -distance + (Vector3.up * height));
        startDistance = distance;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))//toggle cam
        {
            cam1 = !cam1;
            Reset();
        }
        if (cam1)
        {
            if (Input.GetKey("right"))
            {
                rotationX -= 100 * Time.deltaTime;
                if (rotationX < -25) rotationX = -25;
            }

            if (Input.GetKey("left"))
            {
                rotationX += 100 * Time.deltaTime;
                if (rotationX > 25) rotationX = 25;
            }

            if (Input.GetKey("up"))
            {
                rotationY += 50 * Time.deltaTime;
                if (rotationY > 45) rotationY = 45;
            }

            if (Input.GetKey("down"))
            {
                rotationY -= 50 * Time.deltaTime;
                if (rotationY < 25) rotationY = 25;
            }


            //myTransform.position = desiredPosition;
            desiredPosition = CalculatePosition(rotationY, rotationX, distance);
            transform.position = new Vector3(
                 Mathf.SmoothDamp(myTransform.position.x, desiredPosition.x, ref velX, X_Smooth),
                 Mathf.SmoothDamp(myTransform.position.y, desiredPosition.y, ref velY, Y_Smooth),
                 Mathf.SmoothDamp(myTransform.position.z, desiredPosition.z, ref velZ, X_Smooth));

            transform.LookAt(player);
        }
        else//cam2
        {
            if (Input.GetMouseButton(1))//right mouse button
            {
                //mouseX += Input.GetAxis("Mouse X") * X_MouseSensitivity;
                //mouseY -= Input.GetAxis("Mouse Y") * Y_MouseSensitivity;

                ////clamp our mouse y rotation
                //mouseY = Helper.ClampAngle(mouseY, Y_MinLimit, Y_MaxLimit);
                //desiredDistance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * mouseWheelSensitivity, distanceMin, distanceMax);

                //distance = Mathf.SmoothDamp(distance, desiredDistance, ref velDistance, distanceSmooth);
                ////calculate desired position
                //desiredPosition = CalculatePosition(mouseY, mouseX, distance);

                //UpdatePosition();
            }
            else
            {
                mouseY = 35;
                mouseX = 0;

                desiredDistance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * mouseWheelSensitivity, distanceMin, distanceMax);
                distance = Mathf.SmoothDamp(distance, desiredDistance, ref velDistance, distanceSmooth);
                
                    desiredPosition = player.position + (player.forward * -distance + (Vector3.up * height));

                transform.position = new Vector3(
                     Mathf.SmoothDamp(myTransform.position.x, desiredPosition.x, ref velX, X_Smooth),
                    Mathf.SmoothDamp(myTransform.position.y, desiredPosition.y, ref velY, Y_Smooth),
                     Mathf.SmoothDamp(myTransform.position.z, desiredPosition.z, ref velZ, X_Smooth));

                transform.LookAt(player);
                
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
        transform.LookAt(player);
    }

    Vector3 CalculatePosition(float rotationX, float rotationY, float distance)
    {
        Vector3 direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
        return player.position + rotation * direction;
    }
    Vector3 CalculatePosition2(float rotationX, float rotationY)
    {
        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
        return   rotation * new Vector3(1, 1, 1);
    }
    float ClampAngle(float angle, float min, float max)
    {
        //keeps everything within -360 to 360 and then clamps between  min and max
        //eg: 900 would turn into 180
        do
        {
            if (angle < -360)
            { angle += 360; }
            if (angle > 360)
            { angle -= 360; }

        } while (angle < -360 || angle > 360);
        //when we finish the loop our end angle is definately between -360 to 360, now we can
        //clamp it to passed/specified amount. 
        return Mathf.Clamp(angle, min, max);
    }

    public void Reset()
    {
        mouseX = 0;
        mouseY = 10;
        distance = startDistance;
        desiredDistance = distance;
    }
}