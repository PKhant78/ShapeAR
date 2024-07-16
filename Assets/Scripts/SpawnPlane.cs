using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnPlane : MonoBehaviour
{

    public GameObject planeObject;
    private Camera mainCamera;
    TouchControls controls;
    bool isPressed;
    private GameObject currentPlane;
    private bool rotatedTo60 = false;

    private void Awake()
    {
        controls = new TouchControls();

        controls.control.touch.performed += _ => isPressed = true;
        controls.control.touch.canceled += _ => isPressed = false;
    }

    void Start()
    {
        mainCamera = Camera.main;


        spawnPlane();
    }

    void spawnPlane()
    {
        Vector3 spawnPosition = mainCamera.transform.position + mainCamera.transform.forward * 0.5f;

        Quaternion spawnRotation = Quaternion.LookRotation(mainCamera.transform.forward) * Quaternion.Euler(-30f, 0f, 0f);

        if (currentPlane != null)
        {
            Destroy(currentPlane); 
        }

        currentPlane = Instantiate(planeObject, spawnPosition, spawnRotation);
    }
    
     private void Update()
     {

        if (Pointer.current == null || isPressed == false) {
            Debug.Log("null pointer");
            return;
        }
            

        // Store the current touch position.
        var touchPosition = Pointer.current.position.ReadValue();

        Debug.Log("touchPos" + touchPosition);
     
         Ray ray = Camera.main.ScreenPointToRay(touchPosition);
         RaycastHit hit;


         if (Physics.Raycast(ray, out hit, float.MaxValue))
         {
            Debug.Log("Ray is casted from touch");
                 // Toggle between 30 degrees and 60 degrees
                 if (rotatedTo60)
                 {
                     RotatePlane(-30f); // Rotate back to 30 degrees
                 }
                 else
                 {
                     RotatePlane(-60f); // Rotate to 60 degrees
                 }

                 rotatedTo60 = !rotatedTo60; // Toggle rotation state
             
         }
         else
         {
             Debug.Log("No hit detected");
         }
     }

     void RotatePlane(float targetRotation)
     {
         if (currentPlane != null)
         {
             
            currentPlane.transform.rotation = Quaternion.Euler(targetRotation, 0f, 0f); 
         }
     }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }
}
