﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchController : MonoBehaviour
{
    public ParticleSystem FlamePS;
    public ParticleSystem SmokePS;
    public Light m_fireLight;

    /// <summary>
    /// Duration the flame will last
    /// </summary>
    public float DurationRemaining = 255f;
    /// <summary>
    /// Amount to decay the duration by every tick
    /// </summary>
    public float DecayRate = 1f;


    private UnityEngine.Coroutine m_fadeRoutine = null;
    /// <summary>
    /// Start duration of the DurationRemaining variable
    /// </summary>
    private float m_startDuration;

    void Start()
    {
        // Store the start value so we know what to diivide against later
        m_startDuration = DurationRemaining;

        // Start flame routine to decay
        if (m_fadeRoutine == null)
        {
            m_fadeRoutine = StartCoroutine(FadeParticleSystem());
        }
    }

    private IEnumerator FadeParticleSystem()
    {
        while (true)
        {
            DurationRemaining -= (DecayRate * Time.deltaTime);

            var flameRenderer = FlamePS.GetComponent<ParticleSystemRenderer>();
            var smokeRenderer = SmokePS.GetComponent<ParticleSystemRenderer>();

            // Update flame alpha
            Color color = flameRenderer.material.GetColor("_TintColor");
            color.a = DurationRemaining / m_startDuration;
            if (flameRenderer)
                flameRenderer.material.SetColor("_TintColor", color);

            // Update smoke alpha
            var smokeColor = smokeRenderer.material.GetColor("_TintColor");
            smokeColor.a = DurationRemaining / m_startDuration;
            smokeRenderer.material.SetColor("TintColor", smokeColor);

            // Update light for fire
            m_fireLight.intensity = (DurationRemaining / m_startDuration) * 2f;
            //Debug.Log("Light: " + m_fireLight.intensity);

            if (DurationRemaining <= 0f)
            {
                Debug.LogError("Game Over - No Flame left");
                break;
            }

            yield return null;
        }
    }

    /// <summary>
    /// Adds an amount to the duration remaining of the torch
    /// </summary>
    /// <param name="amount">Amount to add to the torch duration</param>
    public void AddToFlame(float amount)
    {
        DurationRemaining += amount;
        Debug.Log("New DurationAmount: " + DurationRemaining);
    }

}
