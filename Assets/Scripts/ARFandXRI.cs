using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.MagicLeap;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.Android;
using System;

public class ARFandXRI : MonoBehaviour
{

    private ARPlaneManager planeManager;
    private readonly MLPermissions.Callbacks permissionCallbacks = new MLPermissions.Callbacks();
    
    [SerializeField, Tooltip("Maximum number of planes to return each query")]
    private uint maxResults = 100;

    [SerializeField, Tooltip("Minimum plane area to treat as a valid plane")]
    private float minPlaneArea = 0.25f;


    private void Awake()
    {
        permissionCallbacks.OnPermissionDenied += PersmissionCallbacks_OnPermissionDenied;
        permissionCallbacks.OnPermissionGranted += PersmissionCallbacks_OnPermissionGranted;
        permissionCallbacks.OnPermissionDeniedAndDontAskAgain += PersmissionCallbacks_OnPermissionDenied;
    }

    private void PersmissionCallbacks_OnPermissionGranted(string permission)
    {
        Debug.Log("Permission granted");
        planeManager.enabled = true;
    }

    private void PersmissionCallbacks_OnPermissionDenied(string permission)
    {
        Debug.LogError($"Failed to create Planes Subsystem due to missing or denied {MLPermission.SpatialMapping} permission. Please add to manifest. Disabling script.");
        enabled = false;
    }

    void Start()
    {
        planeManager = FindObjectOfType<ARPlaneManager>();
        if (planeManager == null)
        {
            Debug.LogError("Failed to find ARPlaneManager in scene. Disabling Script");
            enabled = false;
        }
        else
        {
            // disable planeManager until we have successfully requested required permissions
            planeManager.enabled = false;
            Debug.LogError("Found, no permission requested yet");
        }

        MLPermissions.RequestPermission(MLPermission.SpatialMapping, permissionCallbacks);
    }

    // Update is called once per frame
    void Update()
    {
        //if (planeManager.enabled)
        //{
        //    PlanesSubsystem.Extensions.Query = new PlanesSubsystem.Extensions.PlanesQuery
        //    {
        //        Flags = planeManager.currentDetectionMode.ToMLQueryFlags() | PlanesSubsystem.Extensions.MLPlanesQueryFlags.Polygons | PlanesSubsystem.Extensions.MLPlanesQueryFlags.Semantic_Wall,
        //        BoundsCenter = Camera.main.transform.position,
        //        BoundsRotation = Camera.main.transform.rotation,
        //        BoundsExtents = Vector3.one * 20f,
        //        MaxResults = maxResults,
        //        //MinHoleLength = minHoleLength,
        //        MinPlaneArea = minPlaneArea
        //    };
        //}
    }
}
