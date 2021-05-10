using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    [SerializeField]
    protected float m_movementSpeed = 1f;

    [SerializeField]
    protected float m_rotationSpeed = 15f;

    void Update()
    {
#if UNITY_EDITOR
        float speed = m_movementSpeed * Time.deltaTime;
        float rotation = m_rotationSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(transform.forward * speed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-transform.forward * speed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-transform.right * speed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(transform.right * speed);
        }
        
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(transform.up, -rotation);
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(transform.up, rotation);
        }
#endif
    }
}
