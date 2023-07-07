using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OpenableDoor : MonoBehaviour, IInteractable
{
    private string _prompt = "Press E to Open";
    [SerializeField] private InventorySystem inventory;
    [SerializeField] private int itemId;
    [SerializeField] private InteractionPromptUI interactionPromptUI;
    [SerializeField] private ShowGuidance LargerGuidance;
    [SerializeField] private string secondaryPrompt;
    [SerializeField] private int requiredAmount;
    public string InteractionPrompt => _prompt;


    private bool IsOpen = false;
    [SerializeField] private float Speed = 1.0f;
    [SerializeField] private float RotationAmount = 90f;
    [SerializeField] private float ForwardDirection = 0;
    [SerializeField] GameObject player;
    private Vector3 StartRotation;
    private Vector3 Forward;

    private Coroutine AnimationCoroutine;


    private void Awake()
    {
        StartRotation = transform.rotation.eulerAngles;
        Forward = transform.forward;
    }

    public void Open(Vector3 UserPosition)
    {
        if (!IsOpen)
        {
            if(AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            float dot = Vector3.Dot(Forward, (UserPosition - transform.position).normalized);
            Debug.Log($"Dot: {dot.ToString("N3")}");
            AnimationCoroutine = StartCoroutine(DoRotationOpen(dot));
        }
    }

    private IEnumerator DoRotationOpen(float ForwardAmount)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation;

        Debug.Log("sstart" +startRotation);
        //Debug.Log(RotationAmount);

        if (ForwardAmount <= ForwardDirection)
        {
            endRotation = Quaternion.Euler(new Vector3(0, StartRotation.y - RotationAmount, 0));
        }
        else
        {
            endRotation = Quaternion.Euler(new Vector3(0, StartRotation.y + RotationAmount, 0));
            
        }

        IsOpen = true;

        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
    }

    public void Close()
    {
        if (IsOpen)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }
            AnimationCoroutine = StartCoroutine(DoRotationClose());
        }
    }

    private IEnumerator DoRotationClose()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(StartRotation);

        IsOpen = false;

        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
    }

    public bool Interact(Player interactor)
    {
        if (itemId != -1) {
            
            if (inventory.Container.Items.Any(x => x.ID == itemId && x.StackSize >= requiredAmount))
            {
                OpenOrClose();
            }
            else if (itemId == 1)
            {
                var items = inventory.Container.Items.Find(x => x.ID == itemId);
                var nroOfItems = 0;
                if (items != null)
                {
                    Debug.Log(items.StackSize);
                    nroOfItems = items.StackSize;
                }

                secondaryPrompt = $"Find {requiredAmount - nroOfItems} diary pages to open.";
                if (interactionPromptUI.IsDisplayed) interactionPromptUI.Close();
                interactionPromptUI.SetUp(secondaryPrompt);
            }
            else
            {
                interactionPromptUI.SetUp(secondaryPrompt);
            }
        }
        else
        {
            OpenOrClose();
        }
        return true;
    }

    private void OpenOrClose()
    {
        if (IsOpen)
        {
            _prompt = "Press E to Close";
            Close();
        }
        else
        {
            _prompt = "Press E to Open";
            Open(player.transform.position);
        }
    }

}
