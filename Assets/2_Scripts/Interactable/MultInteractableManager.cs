﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MultInteractableManager : MonoBehaviour {

    [SerializeField] private Interactable[] managedInteractables;

    public Interactable[] GetManagedInteractables()
    {
        return managedInteractables;
    }

    private void OnEnable()
    {
        foreach (var interactable in managedInteractables)
        {
            interactable.OnManipulateAddListener(OnInteractableStartManipulation);
            interactable.OnReleaseAddListener(OnInteractableEndManipulation);
        }
    }

    private void OnDisable()
    {
        foreach (var interactable in managedInteractables)
        {
            interactable.OnManipulateRemoveListener(OnInteractableStartManipulation);
            interactable.OnReleaseRemoveListener(OnInteractableEndManipulation);
        }
    }

    private void OnInteractableStartManipulation(VRInteraction caller)
    {
        foreach (var interactable in managedInteractables)
        {
            if (!interactable.isManipulated)
            {
                interactable.enabled = false;
            }
        }
    }

    private void OnInteractableEndManipulation(VRInteraction caller)
    {
        if (!Array.Exists(managedInteractables, i => i.isManipulated))
        {
            Array.ForEach(managedInteractables, i => i.enabled = true);
        }
    }

    public Interactable GetAppropriateInteractable(VRInteraction interaction)
    {
        LayerMask interactLayer = interaction.interactMask;

        return Array.Find(managedInteractables, i => LayerMaskUtils.LayerInMask(i.gameObject.layer, interactLayer));
    }
}


