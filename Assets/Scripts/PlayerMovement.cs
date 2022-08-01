using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float rotationSpeed = 720.0f;

    void Update()
    {
        if (!Input.GetKey(KeyCode.Mouse1))
        {
            float ejeX = Input.GetAxis("Horizontal");
            float ejeY = Input.GetAxis("Vertical");

            Vector3 movementDirection = new Vector3(ejeX, 0.0f, ejeY);
            movementDirection.Normalize();

            transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);

            if (movementDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}
