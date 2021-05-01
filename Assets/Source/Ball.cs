using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class Ball : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody m_rigidbody;
    SphereCollider m_sphereCollider;

    public Rigidbody GetRigidbody() { return m_rigidbody; }
    public SphereCollider GetSphereCollider() { return m_sphereCollider; }

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_sphereCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
