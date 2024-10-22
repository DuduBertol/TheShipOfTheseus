using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class S_PlayerCutscene : MonoBehaviour
{
    //Player inicia olhando para o criado mudo

    //Quando ele clicar na primeira gaveta ["Play"]

    //Gaveta abre

    //Smoth na tela com o vignette

    // Isfreezed da camera = false

    //Camera reseta por padrão (0,0,0)

    //Jogo começa com a gaveta aberta


    [Header(">> Player Configs")]
    [SerializeField] private Transform playerParentTransform;
    [SerializeField] private Transform playerCamTransform;
    [SerializeField] private Vector3 initialPlayerPos;
    [SerializeField] private Vector3 initialCamRot;
    [SerializeField] private Transform playerCanvas;


    [Header(">> Cutscene Config")]
    [Range(1f, 10f)][SerializeField] private float closeImageTimerMax;
    [Range(1f, 10f)][SerializeField] private float openImageTimerMax;
    private float elapsedCloseImageTime;
    private float elapsedOpenImageTime;

    [Header(">> Volume Config")]
    [SerializeField]private float closedVigneteValue;
    [SerializeField] private float openedVigneteValue;
    [SerializeField] private float closedColorValue;
    [SerializeField] private float openedColorValue;
    

    private Volume globalVolume;
    private Vignette vignette;
    private ColorAdjustments colorAdjustments;

    private bool isFinished;
    private bool isStarted;
    private int state = 1;
    

    private void Start() 
    {
        globalVolume = FindObjectOfType<Volume>();

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

        globalVolume.profile.TryGet<Vignette>(out vignette);
        globalVolume.profile.TryGet<ColorAdjustments>(out colorAdjustments);

        vignette.intensity.value = openedVigneteValue;
        colorAdjustments.postExposure.value = openedColorValue;
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
            if(state == 1) ImageClose();

            else if(state == 2) ImageOpen();
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
    

    private void ImageClose()
    {
        elapsedCloseImageTime += Time.deltaTime;

        float percentageComplete = elapsedCloseImageTime / closeImageTimerMax;

        vignette.intensity.value = Mathf.Lerp(openedVigneteValue, closedVigneteValue, percentageComplete);
        colorAdjustments.postExposure.value = Mathf.Lerp(openedColorValue, closedColorValue, percentageComplete);

        if(elapsedCloseImageTime >= closeImageTimerMax)
        {
            state = 2;

            EnableGameplay();
        }
    }
    private void ImageOpen()
    {
        elapsedOpenImageTime += Time.deltaTime;

        float percentageComplete = elapsedOpenImageTime / openImageTimerMax;

        vignette.intensity.value = Mathf.Lerp(closedVigneteValue, openedVigneteValue, percentageComplete);
        colorAdjustments.postExposure.value = Mathf.Lerp(closedColorValue, openedColorValue, percentageComplete);

        if(elapsedOpenImageTime >= openImageTimerMax)
        {
            vignette.intensity.value = openedVigneteValue;
            colorAdjustments.postExposure.value = openedColorValue;
            
            isFinished = true;
        }
    }
}
