using UnityEngine;
using System.Collections;

public class CameraLimitXY : MonoBehaviour {

    public float mDelta = 10f; // Pixels. The width border at the edge in which the movement work
    public float mSpeed = 30f; // Scale. Speed of the movement
    public float xSensitivity = 20f;
    public float ySensitivity = 20f;
    

    // Use this for initialization
    void Start() {

    }
    void LateUpdate()
    {

        //transform.position.x = Mathf.Clamp(transform.position.x, -10, 10);

        // Check if on the right edge
        if (Input.mousePosition.x >= Screen.width - mDelta)
        {
            // Move the camera
            transform.position += transform.right * Time.deltaTime * mSpeed;
        }


        if (Input.mousePosition.x <= 0 + mDelta)
        {
            // Move the camera
            transform.position -= transform.right * Time.deltaTime * mSpeed;
        }


        if (Input.mousePosition.y >= Screen.height - mDelta)
        {
            // Move the camera
            transform.position += transform.forward * Time.deltaTime * mSpeed;
        }

        if (Input.mousePosition.y <= 0 + mDelta)
        {
            // Move the camera
            transform.position -= transform.forward * Time.deltaTime * mSpeed;
        }


        // Changing an angle, if mouse button is held  
        //if (Input.GetMouseButton(1))
        //{
        //    // Update x, y angle with the mouse delta
        //    xAngleChange = Input.GetAxis("Mouse X") * xSensitivity * Time.deltaTime;
        //    yAngleChange = Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime;

        //    // Rotate around the current up direction by the delta mouse x
        //    transform.rotation = transform.rotation *
        //             Quaternion.Euler(-yAngleChange, xAngleChange, 0);
        //}

        // Update is called once per frame
       
    }

    void Update()
    {

    }
}



