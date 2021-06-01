using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controllers : MonoBehaviour
{
    [SerializeField] bool CameraRotation;


    private Camera mainCam;

    Vector3 previousPosition;

    private void Start()
    {
        mainCam = Camera.main;

    }

    private void Update()
    {
        if (Input.touchCount < 2)
        {
            if (Input.GetMouseButtonDown(0))
            {
                previousPosition = mainCam.ScreenToViewportPoint(Input.mousePosition);
            }

            if (Input.GetMouseButton(0))
            {
                if (CameraRotation)
                {
                    rotateCamera();
                }
                else
                {
                    rotateModel();
                }
            }
            /////////////////////////////////////////
            /// you can use right mouse click to rotate model instead of camera if RotateCamera = true
            if (Input.GetMouseButtonDown(1))
            {
                previousPosition = mainCam.ScreenToViewportPoint(Input.mousePosition);
            }
            if (Input.GetMouseButton(1))
            {

                rotateModel();
            }
            //////////////////
            ///

        }

        if(Input.touchCount >= 2)
        {
            pinch();
        }



        // another zoom by scrolling ( for not mobile devices )
        if (Input.GetAxis("Mouse ScrollWheel") > 0 || Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            zoomCamera(Input.GetAxis("Mouse ScrollWheel"));
        }
    }

    private void rotateCamera()
    {
        Vector3 direction = mainCam.ScreenToViewportPoint(Input.mousePosition) - previousPosition;


        mainCam.transform.RotateAround(AppManager.Instance.getModel().transform.position,new Vector3(1, 0, 0), -direction.y * 180);
        mainCam.transform.RotateAround(AppManager.Instance.getModel().transform.position,new Vector3(0, 1, 0), direction.x * 180);

        previousPosition = mainCam.ScreenToViewportPoint(Input.mousePosition);
    }

    private void rotateModel()
    {
        GameObject model = AppManager.Instance.getModel();

        Vector3 direction = mainCam.ScreenToViewportPoint(Input.mousePosition) - previousPosition;


        model.transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
        model.transform.Rotate(new Vector3(0, 1, 0), -direction.x * 180, Space.World);

        previousPosition = mainCam.ScreenToViewportPoint(Input.mousePosition);
    }

    private void pinch()
    {

        if (Input.touchCount == 2)
        {

            // get current touch positions
            Touch tZero = Input.GetTouch(0);
            Touch tOne = Input.GetTouch(1);

            // get touch position from the previous frame
            Vector2 tZeroPrevious = tZero.position - tZero.deltaPosition;
            Vector2 tOnePrevious = tOne.position - tOne.deltaPosition;

            float oldTouchDistance = Vector2.Distance(tZeroPrevious, tOnePrevious);
            float currentTouchDistance = Vector2.Distance(tZero.position, tOne.position);

            // get offset value
            float deltaDistance = oldTouchDistance - currentTouchDistance;
            zoomCamera(deltaDistance);
        }
    }

    void zoomCamera(float distance)
    {
        Debug.Log("ziaaa distance:  "+distance.ToString());
        
        Transform newPos = mainCam.transform;

        newPos.position  += (AppManager.Instance.getModel().transform.position - mainCam.transform.position).normalized * -distance;

        if (!CameraRotation)
        {
            if (newPos.position.y > 0)
                mainCam.transform.position = newPos.position;
        }
        else
        {
            Vector3 toTarget = (AppManager.Instance.getModel().transform.position - newPos.transform.position).normalized;

            if (Vector3.Dot(toTarget, newPos.forward) > 0)
            {
                mainCam.transform.position = newPos.position;
            }
        }
        

    }
}
