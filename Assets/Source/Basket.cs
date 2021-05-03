using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour, ICollisionListener
{
    [SerializeField]
    protected CollisionBroadcaster m_hoopCollider = null;

    void Start()
    {
        m_hoopCollider.listeners.Add(this);
    }

    void Update()
    {
        
    }

    void ICollisionListener.ProcessOnTriggerEnter(Collider other)
    {
        // Going downwards, valid score
        if (Vector3.Dot(transform.up, other.attachedRigidbody.velocity) < 0f)
        {
            Ball ball = other.gameObject.GetComponent<Ball>();
            if (ball != null)
            {
                ball.Score(1);
            }
        }
        else // Going upwards, invalid attempt
        {
            other.attachedRigidbody.velocity = Vector3.zero;
        }
    }

    void ICollisionListener.ProcessOnTriggerStay(Collider other)
    {

    }

    void ICollisionListener.ProcessOnTriggerExit(Collider other)
    {

    }

    void ICollisionListener.ProcessCollisionEnter(Collision collision)
    {

    }

    void ICollisionListener.ProcessCollisionStay(Collision collision)
    {

    }

    void ICollisionListener.ProcessCollisionExit(Collision collision)
    {

    }
}
