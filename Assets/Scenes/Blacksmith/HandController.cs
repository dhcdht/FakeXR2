using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandController : MonoBehaviour
{
    public void onXRHoverEnter(XRBaseInteractable interactable)
    {
        Component component = interactable.GetComponent<Component>();
        if (component == null)
        {
            return;
        }

        component.setEnableSlotVisual(true);
    }

    public void onXRHoverExit(XRBaseInteractable interactable)
    {
        Component component = interactable.GetComponent<Component>();
        if (component == null)
        {
            return;
        }

        component.setEnableSlotVisual(false);
    }

    public void onXRSelectEnter(XRBaseInteractable interactable)
    {
        // ComponentSlot[] componentSlots = interactable.GetComponentsInChildren<ComponentSlot>();
        // if (componentSlots.Length == 0)
        // {
        //     return;
        // }
        //
        // foreach (ComponentSlot componentSlot in componentSlots)
        // {
        //     componentSlot.setEnableSlotVisual(true);
        // }
    }

    public void onXRSelectExit(XRBaseInteractable interactable)
    {
        // ComponentSlot[] componentSlots = interactable.GetComponentsInChildren<ComponentSlot>();
        // if (componentSlots.Length == 0)
        // {
        //     return;
        // }
        //
        // foreach (ComponentSlot componentSlot in componentSlots)
        // {
        //     componentSlot.setEnableSlotVisual(false);
        // }
    }
}
