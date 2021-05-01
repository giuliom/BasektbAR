using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float m_force = 10f;
    public GameObject m_ballPrefab;

    Ball m_currentBall;
    float m_minimumY = -1000f;
    List<GameObject> m_instancedBalls = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        m_currentBall = GameObject.Instantiate(m_ballPrefab).GetComponent<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        bool touchPressed = false;

        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);

            touchPressed = t.phase == TouchPhase.Began;
        }

        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.L) || touchPressed)
        {
            LaunchBall();
        }

        // TODO recycle balls
        for (int i = m_instancedBalls.Count - 1; i >=0; --i)
        {
            GameObject ball = m_instancedBalls[i];

            if (ball.transform.position.y < m_minimumY)
            {
                m_instancedBalls.RemoveAt(i);
                Destroy(ball);
            }
        }

    }

    // TODO quick hack to have something working
    void LaunchBall()
    {
        var rigidBody = m_currentBall.GetRigidbody();
        var collider = m_currentBall.GetSphereCollider();

        collider.enabled = true;
        rigidBody.useGravity = true;

        Vector3 force_vec = (transform.forward + transform.up * 0.5f) * m_force;
        rigidBody.AddForce(force_vec, ForceMode.Impulse);

        m_instancedBalls.Add(m_currentBall.gameObject);
        m_currentBall = GameObject.Instantiate(m_ballPrefab).GetComponent<Ball>(); ;
    }
}
