using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using Bhaptics.SDK2;

public class HapticGrab : MonoBehaviour
{
    // Called by the "Select Entered" event of the XR Grab Interactable
    public void OnGrab(SelectEnterEventArgs args)
    {
        bool isLeft = IsLeftHand(args.interactorObject);
        BhapticsLibrary.Play(isLeft ? "grab_left" : "grab_right");
    }

    private bool IsLeftHand(IXRSelectInteractor interactor)
    {
        // XRI 3.x: interactors know their handedness
        if (interactor is IXRInteractor baseInteractor)
        {
            if (baseInteractor.handedness == InteractorHandedness.Left) return true;
            if (baseInteractor.handedness == InteractorHandedness.Right) return false;
        }

        // Fallback: guess from the object name (e.g. "Left Controller")
        return interactor.transform.name.ToLower().Contains("left");
    }
}