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

    [SerializeField]
    protected Button m_resetButton = null;

    //TODO original transform for Basket, move elsewhere
    protected Vector3 m_originalPositionOffset;
    protected Quaternion m_originalRotationOffset;

    void Start()
    {
        //TODO move elsewhere
        m_originalRotationOffset = Quaternion.Inverse(m_basket.transform.rotation) * m_player.transform.rotation;
        m_originalPositionOffset = m_basket.transform.position - m_player.transform.position;
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
        float distance = m_player.RealDistanceFromBasket(m_basket);
        int centimeters = Mathf.RoundToInt(distance * 100);

        if (centimeters >= 100)
        {
            int remainder = centimeters % 100;
            string sremainder = "." + (remainder < 10 ? "0" : "") + remainder;

            m_distanceText.text = centimeters / 100 + sremainder + "m";
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

    public void ResetScene()
    {
        //TODO move elsewhere
        m_basket.transform.rotation = m_player.transform.rotation * m_originalRotationOffset;
        Vector3 offset = m_player.transform.rotation * m_originalPositionOffset;
        m_basket.transform.position = m_player.transform.position + offset;
    }
}
