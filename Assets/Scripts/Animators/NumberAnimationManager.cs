using System;
using TMPro;
using UnityEngine;

public class NumberAnimationManager {
    public float lerpDuration = 0.8f;
    private float timeElapsed;
    private int animationStartpoint;
    private int animationEndpoint;

    private TMP_Text text;
    private bool animating;

    private bool setup = false;

    public NumberAnimationManager(TMP_Text textArg) {
        text = textArg;
        animating = false;
    }

    public void WhenUpdate() {
        if (animating) {
            if (!setup) {
                Debug.LogError("AnimationManager not setup before animation");
                return;
            }

            if (timeElapsed < lerpDuration) {
                text.text = Math.Floor(Mathf.Lerp(animationStartpoint, animationEndpoint, timeElapsed / lerpDuration)).ToString();
                timeElapsed += Time.deltaTime;
            }
            else {
                animating = false;
                setup = false;

                text.text = animationEndpoint.ToString();
            }
        }
    }

    public void Setup(int endNumber, int startNumber) {
        timeElapsed = 0;
        animationStartpoint = startNumber;
        animationEndpoint = endNumber;

        setup = true;
    }
    public void Setup(int endNumber) {
        // TODO brittle int parsing, improve
        Setup(endNumber, Int32.Parse(text.text));
    }

    public void Begin() {
        animating = true;
    }

    public bool IsAnimating() {
        return animating;
    }
}
