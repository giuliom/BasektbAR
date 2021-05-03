using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BallLauncher))]
public class Player : MonoBehaviour
{
    [SerializeField]
    protected int m_score = 0;

    BallLauncher m_ballLauncher = null;

    [SerializeField]
    protected float m_launchChargingMaxTimeS = 1.5f;

    protected float m_chargingCounterS = -1f;

    public int Score() { return m_score; }

    void Start()
    {
        m_ballLauncher = GetComponent<BallLauncher>();

        Input.simulateMouseWithTouches = true;
    }

    void Update()
    {
        TouchPhase touch = TouchPhase.Canceled;

        if (Input.touchCount > 0 
            && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) == false)
        {
            touch = Input.GetTouch(0).phase;
        }

        if (m_chargingCounterS >= 0f)
        {
            m_chargingCounterS += Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.L) || touch == TouchPhase.Began)
        {
            m_chargingCounterS = 0f;
        }

        if (Input.GetMouseButtonUp(1) || Input.GetKeyUp(KeyCode.L) || touch == TouchPhase.Ended)
        {
            float launchForce = CurrentChargeUnary() * m_ballLauncher.MaxLaunchForce();
            Vector3 launchDir = transform.forward + transform.up * 0.75f;

            Ball ball = m_ballLauncher.LaunchBall(GetInstanceID(), launchForce, launchDir);
            ball.m_scoreEvent += ScoredEvent; 

            m_chargingCounterS = -1f;
        }
    }

    public float CurrentChargeUnary()
    {
        Assert.AreNotEqual(0f, m_launchChargingMaxTimeS);

        float charge = 0f;

        if (m_chargingCounterS >= 0f)
        {
            charge = Mathf.Clamp(m_chargingCounterS, 0, m_launchChargingMaxTimeS) / m_launchChargingMaxTimeS;
        }

        return charge;
    }

    void ScoredEvent(int value)
    {
        m_score += value;
    }
}
