using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 10.0f; //cuanto mayor sea este valor, más rápido será el reajuste de la cámara a su nueva posición
    public Vector3 offset = new Vector3(0.0f, 2.0f, -7.5f);

    public float sensitivity = 0.5f;
    public bool locked = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            locked = false;
            transform.eulerAngles += sensitivity * new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
        }
        else
        {
            locked = true;
        }
        
    }

    void FixedUpdate()
    {
        if (locked)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothPosition;

            transform.LookAt(target);
        }
    }
}
