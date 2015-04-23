using UnityEngine;

//a static class which holds some methods that dont belong in any other class
public static class Helper
{
    public struct ClipPlanePoints
    { //the 4 points of our camera near clip plane for occlusion culling
        public Vector3 UpperLeft;
        public Vector3 UpperRight;
        public Vector3 LowerLeft;
        public Vector3 LowerRight;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        //keeps everything within -360 to 360 and then clamps between  min and max
        //eg: 900 would turn into 180
        do
        {
            if (angle < -360)
            { angle += 360; }
            if (angle > 360)
            { angle -= 360; }
        
        }while(angle < -360 || angle > 360);
        //when we finish the loop our end angle is definately between -360 to 360, now we can
        //clamp it to passed/specified amount. 
        return Mathf.Clamp(angle, min, max);
    }

    public static ClipPlanePoints ClipPlaneAtNear(Vector3 pos)//passes the position of camera
    {
        ClipPlanePoints clipPlanePoints = new ClipPlanePoints();

        if(Camera.main == null)
        { return clipPlanePoints;}//returns empty points

        Transform transform = Camera.main.transform;
        float halfFOV = (Camera.main.fieldOfView / 2) * Mathf.Deg2Rad;
        float aspect = Camera.main.aspect;
        float distance = Camera.main.nearClipPlane;
        float height = distance * Mathf.Tan(halfFOV);
        float width = height * aspect;

        //create a lowerRight point in the camera's point space
        clipPlanePoints.LowerRight = pos + transform.right * width;//moves the point to the right by width
        clipPlanePoints.LowerRight -= transform.up * height;//move it down
        //slide our point forward to where the clip plane is
        clipPlanePoints.LowerRight += transform.forward * distance;//move it forward in z

        //we could probably do the other 3 based off the first point >_> but we didnt

        clipPlanePoints.LowerLeft = pos - transform.right * width;
        clipPlanePoints.LowerLeft -= transform.up * height;
        clipPlanePoints.LowerLeft += transform.forward * distance;

        clipPlanePoints.UpperRight = pos + transform.right * width;
        clipPlanePoints.UpperRight += transform.up * height;
        clipPlanePoints.UpperRight += transform.forward * distance;

        clipPlanePoints.UpperLeft = pos - transform.right * width;
        clipPlanePoints.UpperLeft += transform.up * height;
        clipPlanePoints.UpperLeft += transform.forward * distance;
        
        return clipPlanePoints;
    }
}

