using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollisionListener
{
    void ProcessCollisionEnter(Collision collision);
    void ProcessCollisionStay(Collision collision);
    void ProcessCollisionExit(Collision collision);

    void ProcessOnTriggerEnter(Collider other);
    void ProcessOnTriggerStay(Collider other);
    void ProcessOnTriggerExit(Collider other);
}

public class CollisionBroadcaster : MonoBehaviour
{
    public List<ICollisionListener> listeners = new List<ICollisionListener>();

    public void OnCollisionEnter(Collision collision)
    {
        foreach (var l in listeners)
        {
            l.ProcessCollisionEnter(collision);
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        foreach (var l in listeners)
        {
            l.ProcessCollisionStay(collision);
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        foreach (var l in listeners)
        {
            l.ProcessCollisionExit(collision);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        foreach (var l in listeners)
        {
            l.ProcessOnTriggerEnter(other);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        foreach (var l in listeners)
        {
            l.ProcessOnTriggerStay(other);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        foreach (var l in listeners)
        {
            l.ProcessOnTriggerExit(other);
        }
    }
}

