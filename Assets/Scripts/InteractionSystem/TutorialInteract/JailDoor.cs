using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JailDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private InventorySystem inventory;
    [SerializeField] private int itemId;
    [SerializeField] private InteractionPromptUI interactionPromptUI;
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
            if (AnimationCoroutine != null)
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

        if (ForwardAmount >= ForwardDirection)
        {
            endRotation = Quaternion.Euler(new Vector3(0, startRotation.y - RotationAmount, 0));
        }
        else
        {
            endRotation = Quaternion.Euler(new Vector3(0, startRotation.y + RotationAmount, 0));
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
        if (inventory.Container.Items.Any(x => x.ID == itemId))
        {
            if (IsOpen)
            {
                Close();
            }
            else
            {
                Open(player.transform.position);
            }
        }
        else
        {
            if (interactionPromptUI.IsDisplayed) interactionPromptUI.Close();
            if (itemId == 2) interactionPromptUI.SetUp("You need to find a key");
            if (itemId == 10) interactionPromptUI.SetUp("You should pick up that torch on the groud");
            if (itemId == 3) interactionPromptUI.SetUp("You should pick up that diary");
        }
        return true;
    }

}