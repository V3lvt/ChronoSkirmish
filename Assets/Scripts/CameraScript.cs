using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [Header("References")]
    public Transform playerBody;
    [Header("Weapon Follow")]
    public Transform heldWeapon;

    [Header("Settings")]
    public float mouseSensitivity = 100f;
    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);

        if (heldWeapon != null)
        {
            Vector3 e = heldWeapon.localEulerAngles;
            e.x = xRotation;
            heldWeapon.localEulerAngles = e;
        }
    }
}