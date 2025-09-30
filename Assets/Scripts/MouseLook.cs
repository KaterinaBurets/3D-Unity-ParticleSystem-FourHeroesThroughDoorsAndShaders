using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 80f;
    public Transform playerBody;
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
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);
        
        float mw = Input.GetAxis("Mouse ScrollWheel");
        if (mw > 0.1)
        {
            transform.position += transform.forward * Time.deltaTime * 100;/*Приближение*/
        }
        if (mw < -0.1)
        {
            transform.position -= transform.forward * Time.deltaTime * 100;/*Отдаление*/
        }

    }
}