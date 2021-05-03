using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    [SerializeField]
    private Text m_fpsText = null;

    [SerializeField]
    private float m_updateTimeS = 1f;

    private int m_frameCounter = 0;
    private float m_elapsedTime = 0f;

    void Start()
    {
#if !DEVELOPMENT_BUILD && !UNITY_EDITOR
        m_fpsText.gameObject.SetActive(false);
#endif
    }

    void Update()
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        if (Time.unscaledDeltaTime > 0)
        {
            ++m_frameCounter;
            m_elapsedTime += Time.unscaledDeltaTime;

            if (m_elapsedTime >= m_updateTimeS)
            {
                int fps = Mathf.RoundToInt(((float)m_frameCounter) / m_elapsedTime);
                m_fpsText.text = fps + " fps";

                m_frameCounter = 0;
                m_elapsedTime = 0f;
            }
        }
#endif
    }
}
