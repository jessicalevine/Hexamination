using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimationQueue : MonoBehaviour {
    Queue<AnimationManager> animationManagers;

    public static AnimationQueue Instance { get; private set; }

    private bool animating;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    private void Start() {
        animationManagers = new Queue<AnimationManager>();
        AnimationManager.AnimationComplete += OnComplete;
    }

    public void Queue(AnimationManager animationManager) {
        animationManagers.Enqueue(animationManager);

        if (animating == false) {
            Animate();
        }
    }

    private void OnComplete() {
        if (animationManagers.Count > 0) {
            Animate();
        } else {
            animating = false;
        }
    }

    private void Animate() {
        if (animationManagers.Count > 0) {
            animating = true;
            animationManagers.Dequeue().Begin();
        }
    }
}
