// File: PlayerController.cs
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float mouseSensitivity = 100f;
    public Transform playerCamera;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    private bool wallMode = false; // Toggle for wall mode
    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleMovement();

        // Toggle between wall-building and shooting modes with 'V' key
        if (Input.GetKeyDown(KeyCode.V))
        {
            wallMode = !wallMode;
        }

        HandleAction();
    }

    private void HandleMovement()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        float moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        transform.position += move;
    }

    private void HandleAction()
    {
        if (wallMode)
        {
            // Wall-building mode: left-click to place a wall
            if (Input.GetMouseButtonDown(0))
            {
                IceWallManager.Instance.PlaceWall(playerCamera.position + playerCamera.forward * 2f, Quaternion.identity);
            }
        }
        else
        {
            // Shooting mode: left-click to fire a bullet
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            }
        }
    }
}
