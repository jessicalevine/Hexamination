using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralSoundTrigger : MonoBehaviour {
    [SerializeField] private GeneralEvent generalEvent;
    [SerializeField] private AudioSource sound;

    void Start() {
        if (generalEvent == null)
            Debug.LogError("No generalEvent found");

        if (sound == null)
            Debug.LogError("No sound found");

        // For whatever GeneralEvent you attach to this object in the Unity editor, when that
        // GeneralEvent has Raise() called on it, do what's in the OnAction method
        generalEvent.Action += OnAction;
    }
    private void OnAction() {
        sound.PlayOneShot(sound.clip);
    }
}
