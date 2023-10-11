using System;
using TMPro;
using UnityEngine;

public class NumberAnimationManager
{
    public float lerpDuration = 0.8f;
    public float pulseFrequency = 1.0f; // Adjust the frequency of the pulse
    public float pulseAmplitude = 0.1f; // Adjust the amplitude of the pulse

    private float timeElapsed;
    private int animationStartpoint;
    private int animationEndpoint;

    private TMP_Text text;
    private bool animating;
    private bool setup = false;
    private bool pulsing = false;

    private float originalScaleX;
    private float originalScaleY;

    public NumberAnimationManager(TMP_Text textArg)
    {
        text = textArg;
        animating = false;

        // Store the original scale
        originalScaleX = text.transform.localScale.x;
        originalScaleY = text.transform.localScale.y;
    }

    public void WhenUpdate()
    {
        if (animating)
        {
            if (!setup)
            {
                Debug.LogError("AnimationManager not set up before animation");
                return;
            }

            if (timeElapsed < lerpDuration)
            {
                float t = timeElapsed / lerpDuration;

                // Lerp the number
                text.text = Math.Floor(Mathf.Lerp(animationStartpoint, animationEndpoint, t)).ToString();

                if (pulsing)
                {
                    // Calculate the pulsing scale using a sine wave
                    float pulseScale = 1.0f + Mathf.Sin(t * pulseFrequency * 2 * Mathf.PI) * pulseAmplitude;

                    // Apply the pulsing effect to the text's scale
                    text.transform.localScale = new Vector3(originalScaleX * pulseScale, originalScaleY * pulseScale, 1.0f);
                }

                timeElapsed += Time.deltaTime;
            }
            else
            {
                animating = false;
                setup = false;
                pulsing = false;

                text.text = animationEndpoint.ToString();
                text.transform.localScale = new Vector3(originalScaleX, originalScaleY, 1.0f); // Reset the scale to normal
            }
        }
    }

    public void Setup(int endNumber, int startNumber)
    {
        timeElapsed = 0;
        animationStartpoint = startNumber;
        animationEndpoint = endNumber;

        // Check if the number is changing and enable pulsing
        pulsing = animationStartpoint != animationEndpoint;

        setup = true;
    }

    public void Setup(int endNumber)
    {
        Setup(endNumber, Int32.Parse(text.text));
    }

    public void Begin()
    {
        animating = true;
    }

    public bool IsAnimating()
    {
        return animating;
    }
}
