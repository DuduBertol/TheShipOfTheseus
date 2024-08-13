using UnityEngine;

/// <summary>
/// A simple FPP (First Person Perspective) camera rotation script.
/// Like those found in most FPS (First Person Shooter) games.
/// </summary>
public class CameraMovement : MonoBehaviour {

    public float Sensitivity {
        get { return sensitivity; }
        set { sensitivity = value; }
    }
    [Range(0.1f, 9f)][SerializeField] public float sensitivity = 2f;
    [Range(0f, 90f)][SerializeField] float yRotationLimit = 88f;

    private float startSensivity;
    [Tooltip("Limits vertical camera rotation. Prevents the flipping that happens when rotation goes above 90.")]

    Vector2 rotation = Vector2.zero;
    const string xAxis = "Mouse X"; //Strings in direct code generate garbage, storing and re-using them creates no garbage
    const string yAxis = "Mouse Y";

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

        if(Input.GetKeyDown(KeyCode.Q))
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

        // rotation.x += Input.GetAxis(xAxis) * sensitivity * 0;
        rotation.y += Input.GetAxis(yAxis) * sensitivity;
        rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);
        // var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
        var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

        transform.localRotation = yQuat; //Quaternions seem to rotate more consistently than EulerAngles. Sensitivity seemed to change slightly at certain degrees using Euler. transform.localEulerAngles = new Vector3(-rotation.y, rotation.x, 0);
    }
}