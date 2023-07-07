using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{/*
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionPointRadius = 0.25f;
    [SerializeField] private LayerMask interactableMask;
    [SerializeField] private InteractionPromptUI interactionPromptUI;
    [SerializeField] InputActionReference interactionInput;

    private readonly Collider[] colliders = new Collider[3];
    [SerializeField] private int numFound;

    private IInteractable interactable;


    private void Update()
    {
        numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionPointRadius, colliders, interactableMask);

        if (numFound > 0)
        {
            interactable = colliders[0].GetComponent<IInteractable>();

            if (interactable != null)
            {
                if (!interactionPromptUI.IsDisplayed) interactionPromptUI.SetUp(interactable.InteractionPrompt);
                interactionInput.action.performed += DoInteraction;
            } 
        }

        else
        {
            if (interactable != null) interactable = null;
            if (interactionPromptUI.IsDisplayed) interactionPromptUI.Close();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionPointRadius);
    }

    private void DoInteraction(InputAction.CallbackContext obj)
    {
        interactable.Interact(this);
    }*/
}
