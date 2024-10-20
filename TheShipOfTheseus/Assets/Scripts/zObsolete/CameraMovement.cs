using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public float Sensitivity {
        get { return sensitivity; }
        set { sensitivity = value; }
    }
    [Range(0.1f, 9f)][SerializeField] public float sensitivity = 2f;
    [Range(0f, 90f)][SerializeField] float yRotationLimit = 88f;

    private Vector2 rotation = Vector2.zero;
    private const string xAxis = "Mouse X"; 
    private const string yAxis = "Mouse Y";

    private void Awake() 
    {
            
    }

    private void Start() 
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;    
    }

    void Update()
    {
        HandleMovement();

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!Cursor.visible)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;    
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;    
            }
        }
    }

    private void HandleMovement()
    {
        if(!GameController.Instance.IsGameStarted || GameController.Instance.IsGamePaused) return;

        rotation.y += Input.GetAxis(yAxis) * sensitivity;
        rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);
        var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

        transform.localRotation = yQuat;
    }
}