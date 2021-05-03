using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    protected Player m_player = null;

    [SerializeField]
    protected Basket m_basket = null;

    [SerializeField]
    protected Text m_scoreText = null;

    [SerializeField]
    protected Text m_distanceText = null;

    [SerializeField]
    protected Slider m_forceBar = null;

    void Start()
    {
        
    }

    void Update()
    {
        UpdateScore();
        UpdateDistance();
        UpdateForceBar();
    }

    void UpdateScore()
    {
        m_scoreText.text = m_player.Score() + " Points";
    }

    protected void UpdateDistance()
    {
        float distance = Vector3.Distance(m_basket.transform.position, m_player.transform.position);
        int centimeters = Mathf.RoundToInt(distance * 100);

        if (centimeters >= 100)
        {
            m_distanceText.text = centimeters / 100 + "." + centimeters % 100  + "m";
        }
        else
        {
            m_distanceText.text = centimeters + "cm";
        }
    }

    protected void UpdateForceBar()
    {
        float charge = m_player.CurrentChargeUnary();

        if (charge > 0f)
        {
            m_forceBar.gameObject.SetActive(true);
            m_forceBar.value = charge;
        }
        else
        {
            m_forceBar.gameObject.SetActive(false);
        }
    }
}
