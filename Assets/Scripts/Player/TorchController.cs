using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchController : MonoBehaviour
{
    public ParticleSystem FlamePS;
    public ParticleSystem SmokePS;

    public float DurationRemaining = 255f;
    public float DecayRate = 1f;
    private UnityEngine.Coroutine m_fadeRoutine = null;

    private float m_startDuration;

    void Start()
    {
        m_startDuration = DurationRemaining;

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

            var renderer = FlamePS.GetComponent<ParticleSystemRenderer>();

            Color color = renderer.material.GetColor("_TintColor");
            color.a = DurationRemaining / m_startDuration;

            Debug.Log("Duration Remaining: " + color.a);

            renderer.material.SetColor("_TintColor", color);

            if (DurationRemaining <= 0f)
            {
                Debug.Log("Game Over - No Flame");
                break;
            }

            yield return null;
        }
    }
}
