using System;
using UnityEngine;

public class AnimationManager {
    public static event Action AnimationComplete;

    public float lerpDuration = 0.2f;
    private float timeElapsed;
    private Vector2 animationStartpoint;
    private Vector2 animationEndpoint;

    private GameObject objToMove;
    private bool animating;

    private GameObject objToActivate = null;

    private bool setup = false;

    public AnimationManager(GameObject objArg) {
        objToMove = objArg;
        animating = false;
    }

    public void WhenUpdate() {
        if (animating) {
            if (!setup) {
                Debug.LogError("AnimationManager not setup before animation");
                return;
            }

            if (timeElapsed < lerpDuration) {
                objToMove.transform.position = Vector3.Lerp(animationStartpoint, animationEndpoint, timeElapsed / lerpDuration);
                timeElapsed += Time.deltaTime;
            }
            else {
                CompleteAnimation();
                objToMove.transform.position = animationEndpoint;
            }
        }
    }

    public void Setup(Vector2 endLocation, Vector2 startLocation, GameObject obj = null) {
        objToActivate = obj;
        timeElapsed = 0;
        animationStartpoint = startLocation;
        animationEndpoint = endLocation;

        setup = true;
    }
    public void Setup(Vector2 endLocation, GameObject obj = null) {
        Setup(endLocation, objToMove.transform.position, obj);
    }

    internal void CompleteAnimation() {
        animating = false;
        setup = false;
        if (AnimationComplete != null) {
            AnimationComplete.Invoke();
        }
    }

    public void Begin() {
        animating = true;
        if (objToActivate != null) {
            objToActivate.SetActive(true);
            objToActivate = null;
        }
    }

    public bool IsAnimating() {
        return animating;
    }
}
