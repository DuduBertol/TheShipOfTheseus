using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Headbob : MonoBehaviour
{
    public float bobSpeed;
    public float bobMultiply;
    public float bobAmount;
    // public float rotationAmount = 20f;

    private float defaultYPos = 0f; 
    // private Vector3 defaultRotation; 
    [SerializeField] private float timer = 0f;
    
    private float footstepTime;
    [SerializeField] private float footstepTimeMax;

    private void Start()
    {
        defaultYPos = transform.localPosition.y;
        // defaultRotation = transform.localEulerAngles;
    }

    private void Update()
    {
        if(!S_EventManager.Instance.isGameOver && !S_EventManager.Instance.isPaused && S_EventManager.Instance.isGameStarded)
        {
            float horizontalInput = Input.GetAxis("Horizontal"); 
            float verticalInput = Input.GetAxis("Vertical");
            
            if(Mathf. Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f)
            {
                timer += Time.deltaTime * bobSpeed;
                float newY = defaultYPos + Mathf. Sin(timer) * bobAmount;
            
                // float rotationZ = Mathf.Sin(timer) * rotationAmount;
                
                transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z); 
                // transform. localEulerAngles = new Vector3(defaultRotation.x, defaultRotation.y, defaultRotation.z + rotationZ);

                PlayFootstepsSound();
            }
            else
            {
                timer = 0f;
                transform. localPosition = new Vector3(transform. localPosition.x, defaultYPos, transform. localPosition.z); 
                // transform. localEulerAngles = defaultRotation;
            }

            
        }
        
    }

    private void PlayFootstepsSound()
    {
        footstepTime += Time.deltaTime;

        if(footstepTime >= footstepTimeMax)
        {
            SoundManager.Instance.PlayFootstepsSound(transform.position, 0.025f);
            footstepTime = 0f;
        }
    }
}
