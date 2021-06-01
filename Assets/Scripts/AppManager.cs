using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoSingleton<AppManager>
{
    [Tooltip("Please enter the model name from imported models in Resources/Models directory")]
    [SerializeField] string modelName;
    private GameObject _model;
    private Camera mainCam;

    // Start is called before the first frame update
    private void Start()
    {

        _model = Resources.Load("Models/"+modelName) as GameObject;

        _model = Instantiate(_model, Vector3.zero, Quaternion.identity);

        mainCam = Camera.main;

        StartCoroutine( setupCamera());

    }

    public GameObject getModel()
    {
        return _model;
    }

    /// <summary>
    /// Ensure Camera show the whole model inside it's view
    /// </summary>
    /// <returns></returns>
    private IEnumerator setupCamera()
    {
       
        Transform[] transforms = _model.GetComponentsInChildren<Transform>();

        bool allVisable = false;
        while(!allVisable)
        {
            foreach (Transform t in transforms)
            {
                // check that all transforms in viewport bounds
                Vector3 screenPoint = mainCam.WorldToViewportPoint(t.gameObject.transform.position);
                bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
                if (!onScreen)
                {
                    yield return null;

                    Vector3 direction = mainCam.transform.position - _model.transform.position;

                    mainCam.transform.Translate(direction * Time.deltaTime);

         
                    var cameraDirection = _model.transform.position - mainCam.transform.position;

                    cameraDirection.x = 0;

                    var rotation = Quaternion.LookRotation(cameraDirection);

                    mainCam.transform.rotation = Quaternion.Slerp(mainCam.transform.rotation, rotation, Time.deltaTime);

                    allVisable = false;
                    break;
                }
                else
                {
                    allVisable = true;
                }
            }
            
        }
    }
    
}
