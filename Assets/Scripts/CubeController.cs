using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.MagicLeap;


public class CubeController : MonoBehaviour
{
    private MagicLeapInputs mlInputs;
    private MagicLeapInputs.ControllerActions controllerActions;
    public GameObject NewCube;
    public GameObject CubeOrigin;

    void Start()
    {
        mlInputs = new MagicLeapInputs();
        mlInputs.Enable();
        controllerActions = new MagicLeapInputs.ControllerActions(mlInputs);

        controllerActions.Bumper.performed += HandleOnTrigger;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void HandleOnTrigger(InputAction.CallbackContext obj)
    {
        Debug.Log("Button pressed");
        GameObject CubeClone = Instantiate(NewCube, CubeOrigin.transform.position, CubeOrigin.transform.rotation);

    }
}
