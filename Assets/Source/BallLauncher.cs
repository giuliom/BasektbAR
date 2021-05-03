using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    [SerializeField]
    protected GameObject m_ballPrefab;

    [SerializeField]
    protected Transform m_launchPosition;

    [SerializeField]
    protected float m_maxLaunchForce = 100f;

    [SerializeField]
    protected float m_minimumPositionY = -1000f;

    [SerializeField]
    protected float m_maxDistance = 1000f;
    protected float m_maxSquaredDistance;

    [SerializeField]
    protected int m_ballPoolSize = 10;

    protected Ball m_currentBall;
    protected List<Ball> m_availableBallsPool = null;
    protected List<Ball> m_launchedBallsPool = null;

    public float MaxLaunchForce() { return m_maxLaunchForce; }

    void Start()
    {
        m_maxSquaredDistance = m_maxDistance * m_maxDistance;

        m_currentBall = GameObject.Instantiate(m_ballPrefab, m_launchPosition).GetComponent<Ball>();
        m_availableBallsPool = new List<Ball>(m_ballPoolSize);
        m_launchedBallsPool = new List<Ball>(m_ballPoolSize);

        for (int i = 0; i < m_ballPoolSize; ++i)
        {
            Ball ball = GameObject.Instantiate(m_ballPrefab, m_launchPosition).GetComponent<Ball>();
            ball.gameObject.SetActive(false);
            m_availableBallsPool.Add(ball);
        }
    }

    void Update()
    {
        // Only removing them based on position for design purposes
        for (int i = m_launchedBallsPool.Count - 1; i >= 0; --i)
        {
            Ball ball = m_launchedBallsPool[i];

            if (ball.transform.position.y < m_minimumPositionY || (ball.transform.position - transform.position).sqrMagnitude > m_maxSquaredDistance)
            {
                ResetBall(ball);

                m_launchedBallsPool.RemoveAt(i);
                m_availableBallsPool.Add(ball);
            }
        }
    }

    public Ball LaunchBall(in int launcherId, in float force, in Vector3 direction)
    {
        Ball launchedBall = m_currentBall;
        launchedBall.transform.parent = null;

        var rigidBody = m_currentBall.GetRigidbody();
        var collider = m_currentBall.GetSphereCollider();

        collider.enabled = true;
        rigidBody.useGravity = true;

        rigidBody.AddForce(direction * force, ForceMode.Impulse);

        launchedBall.SetLauncherId(launcherId);
        ReplaceCurrentBall();
        m_launchedBallsPool.Add(m_currentBall);

        return launchedBall;
    }

    void ReplaceCurrentBall()
    {
        Ball ball = null;

        if (m_availableBallsPool.Count > 0)
        {
            ball = m_availableBallsPool[0];
            m_availableBallsPool.RemoveAt(0);
        }
        else
        {
            ball = m_launchedBallsPool[0];
            m_launchedBallsPool.RemoveAt(0);
            ResetBall(ball);
        }

        ball.gameObject.SetActive(true);
        m_currentBall = ball.GetComponent<Ball>();
    }

    void ResetBall(Ball ball)
    {
        ball.ResetLauncherId();
        ball.ClearScoreListeners();

        ball.transform.parent = m_launchPosition;
        ball.transform.position = m_launchPosition.position;
        ball.transform.rotation = m_launchPosition.rotation;

        var rb = ball.GetRigidbody();
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        var collider = ball.GetSphereCollider();
        collider.enabled = false;

        ball.gameObject.SetActive(false);
    }

}
