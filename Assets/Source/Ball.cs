using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ScoreDelegate(int value);

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class Ball : MonoBehaviour
{
    protected int m_launcherId = -1;

    Rigidbody m_rigidbody               = null;
    SphereCollider m_sphereCollider     = null;

    public event ScoreDelegate m_scoreEvent;

    public int GetLauncherId()                  { return m_launcherId; }
    public void SetLauncherId(in int id)        { m_launcherId = id; }
    public void ResetLauncherId()               { m_launcherId = -1; }
    public Rigidbody GetRigidbody()             { return m_rigidbody; }
    public SphereCollider GetSphereCollider()   { return m_sphereCollider; }

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_sphereCollider = GetComponent<SphereCollider>();
    }

    public void Score(in int value)
    {
        m_scoreEvent(value);
    }

    public void ClearScoreListeners()
    {
        foreach (var d in m_scoreEvent.GetInvocationList())
        {
            m_scoreEvent -= (ScoreDelegate)d;
        }
    }

}
