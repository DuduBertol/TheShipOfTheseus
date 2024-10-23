using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerCutscene02 : MonoBehaviour
{
    [Header(">> Configs")]
    [SerializeField] private Transform playerParentTransform;
    [SerializeField] private Transform playerCamTransform;
    [SerializeField] private Vector3 initialPlayerPos;
    [SerializeField] private Vector3 initialCamRot;
    [SerializeField] private Transform playerCanvas;
    [SerializeField] private float elapsedTime;
    [SerializeField] private float elapsedTimerMax;


    private bool isFinished;
    private bool isStarted;

    private void Start() 
    {

        StartConfig();

        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        RaycastHit hit;
        Ray ray = playerCamTransform.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            if (hit.transform.gameObject.TryGetComponent(out S_DrawerStartGame drawerStartGame))
            {
                StartCutscene();
            }
        }
    }

    private void StartCutscene() 
    {
        isStarted = true;
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            StartCutscene();
        }

        if(!isStarted)
        {
            //MOUSE
            Cursor.lockState = CursorLockMode.None;    
            Cursor.visible = true;
        }

        if(!isFinished && isStarted)
        {
            CameraLerp();
        }
    }

    private void StartConfig()
    {
        //CAMERA
        S_PlayerCam.Instance.isFreezed = true;
        S_PlayerMovement.Instance.isFreezed = true;

        //PLAYER
        playerParentTransform.position = initialPlayerPos;
        playerCamTransform.rotation = Quaternion.Euler(initialCamRot);

        //MOUSE (isso não ta funcionando, logo coloquei no Update() )
        Cursor.lockState = CursorLockMode.None;    
        Cursor.visible = true;

        //CANVAS
        playerCanvas.gameObject.SetActive(false);
    }
    private void EnableGameplay()
    {
        //CAMERA
        S_PlayerCam.Instance.isFreezed = false;
        S_PlayerMovement.Instance.isFreezed = false;

        //MOUSE
        Cursor.lockState = CursorLockMode.Locked;    
        Cursor.visible = false;

        //CANVAS
        playerCanvas.gameObject.SetActive(true);
    }

    private void CameraLerp()
    {
        elapsedTime += Time.deltaTime;

        float percentageComplete = elapsedTime / elapsedTimerMax;

        float x = Mathf.Lerp(initialCamRot.x, 0, percentageComplete);
        float y = Mathf.Lerp(initialCamRot.y, 0, percentageComplete);
        float z = Mathf.Lerp(initialCamRot.z, 0, percentageComplete);

        playerCamTransform.rotation = Quaternion.Euler(x, y, z);

        if(elapsedTime >= elapsedTimerMax)
        {
            EnableGameplay();
        }
    }
}
