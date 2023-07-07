using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDataPersistence
{
    public InventorySystem inventory;

    [SerializeField] private Transform _parent;
    [SerializeField] private Transform _lookAtTarget;
    [SerializeField] private float lookSpeed = 1f;
    [SerializeField] private ShowGuidance showGuidance;
    [SerializeField] InputActionReference interactionInput;
    private GameObject collectableItem;

    private bool useLookAt;
    private Vector3 _targetPosition;

    [SerializeField]
    private LayerMask pickableLayerMask;

    [SerializeField]
    private Transform playerCameraTransform;

    [SerializeField]
    private float hitRange = 2f;

    [SerializeField]
    private Transform pickUpParent;

    [SerializeField]
    private GameObject inHandItem;
    private Rigidbody heldObjRB;

    private RaycastHit hit;
    private bool firstTime;
    private bool canvasOpen;

    [SerializeField] private float pickupForce = 150.0f;
    private GameObject selectedObject;

    public static SerializableDictionary<string, ItemPickUpSaveData> activeItems;

    //Interaction
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionPointRadius = 0.25f;
    [SerializeField] private LayerMask interactableMask;
    [SerializeField] private InteractionPromptUI interactionPromptUI;

    private readonly Collider[] colliders = new Collider[3];
    [SerializeField] private int numFound;

    private IInteractable interactable;

    [SerializeField] private GameObject meat;
    [SerializeField] private GameObject mug;
    [SerializeField] private GameObject scroll;
    [SerializeField] private GameObject carrot;
    [SerializeField] private GameObject bread;
    [SerializeField] private GameObject torch;

    // Story canvas for closing
    [SerializeField] private GameObject storyCanvas;
    [SerializeField] private GameObject instructionCanvas;
    [SerializeField] private GameObject decryptCanvas;


    private void Awake()
    {
        activeItems = new SerializableDictionary<string, ItemPickUpSaveData>();
    }

    public void SaveData(GameData data)
    {
        Debug.Log("Saving Inventory Items");
        data.inventoryItems = inventory.Container.Items;
        data.activeItems = activeItems;
        data.TorchFirst = firstTime;
        foreach (var item in activeItems)
        {
            Debug.Log(item.ToString());
        }
    }

    public void LoadData(GameData data)
    {
        inventory.Container.Items.Clear();
        Debug.Log("Loading Inventory Items");
        inventory.Container.Items = data.inventoryItems;
        InventorySystem.OnInventoryChanged?.Invoke(false);
        firstTime = data.TorchFirst;
        if (data.activeItems.Count > 0)
        {
            placeItems(data);
        }
    }

    private void OnEnable()
    {
        EventManager.GetInventoryItem += EventManagerOnGetInventoryItem;
    }

    

    private void OnDisable()
    {
        EventManager.GetInventoryItem -= EventManagerOnGetInventoryItem;
    }

    /*private void placeItems(GameData data)
    {
        UniqueID[] itemsWithIds = FindObjectsOfType<UniqueID>();

        foreach (KeyValuePair<string, ItemPickUpSaveData> entry in data.activeItems)
        {
            if (!(itemsWithIds.Any(x => x.ID == entry.Key)))
            {
                Debug.Log("creating" + entry.Key);
                var item = Instantiate(inventory.database.GetItem[entry.Value.id].prefab, entry.Value.position, entry.Value.rotation);
                if (!(item.AddComponent<ItemObject>())) item.AddComponent<ItemObject>();
            }
        }
    }*/

    private void placeItems(GameData data)
    {
        UniqueID[] itemsWithIds = FindObjectsOfType<UniqueID>();

        foreach (KeyValuePair<string, ItemPickUpSaveData> entry in data.activeItems)
        {
            if (!(itemsWithIds.Any(x => x.ID == entry.Key)))
            {
                if (entry.Value.id != 1 && entry.Value.id != 2 && entry.Value.id != 0)
                {
                    Debug.Log("creating" + entry.Key);
                    var item = Instantiate(inventory.database.GetItem[entry.Value.id].prefab, entry.Value.position, entry.Value.rotation);
                    if (!(item.AddComponent<ItemObject>())) item.AddComponent<ItemObject>();
                }
            }
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PointOfInterest")
        {
            Debug.Log("Seeing point of interest");
            interactionPromptUI.SetUp("Press E to Collect Item");
            //showGuidance.SetUpGuidance("Press E to Collect Item");
            _targetPosition = other.transform.position;
            useLookAt = true;
            collectableItem = other.gameObject;
        }

    }

    public void OnTriggerExit(Collider other)
    {
        useLookAt = false;
        collectableItem = null;
    }

    void PickupObject(GameObject pickObj)
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
            heldObjRB = pickObj.GetComponent<Rigidbody>();
            heldObjRB.useGravity = false;
            heldObjRB.constraints = RigidbodyConstraints.FreezeRotation;
            heldObjRB.transform.parent = pickUpParent;
            heldObjRB.drag = 5;
            inHandItem = pickObj;
            if (pickObj.tag == "PointOfInterest") inHandItem.tag = "Pickable";
            if (pickObj.layer == 8) pickObj.layer = 10;
        }
    }

    void MoveObject()
    {
        heldObjRB.velocity += (pickUpParent.transform.position + heldObjRB.position) * Time.deltaTime;
        if (Vector3.Distance(inHandItem.transform.position, pickUpParent.position) > 0.1f)
        {
            Vector3 moveDirection = (pickUpParent.position - inHandItem.transform.position);
            heldObjRB.AddForce(moveDirection * pickupForce);
        }
    }

    void DropObject()
    {
        heldObjRB.useGravity = true;
        if (SceneManager.GetActiveScene().buildIndex != 10)
        {
            if (inHandItem.tag == "Pickable") inHandItem.tag = "PointOfInterest";
        }
        if (inHandItem.layer == 10) inHandItem.layer = 8;
        heldObjRB.drag = 1;
        heldObjRB.constraints = RigidbodyConstraints.None;
        heldObjRB.transform.parent = null;
        inHandItem = null;
    }

    private void Collect(InputAction.CallbackContext obj)
    {
        if (useLookAt && collectableItem != null)
        {
            
            var item = collectableItem.GetComponent<ItemCollectable>();

            if (item)
            {
                if (item.item.name == "Torch" && firstTime)
                {
                    EventManager.OnTakeTorch();
                    firstTime = false;
                }
                inventory.AddItem(new Item(item.item));
                collectableItem.GetComponent<ItemObject>().OnHandlePickupItem();
            }
            return;
        }
        if (hit.collider != null && inHandItem == null)
        {
            Debug.Log(hit.collider.name);
            PickupObject(hit.collider.gameObject);
        }
        else if ((hit.collider != null || hit.collider == null) && inHandItem != null)
        {
            if (inHandItem != null)
            {
                DropObject();
            }
            else
            {
                return;
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            if (inHandItem != null)
            {
                MoveObject();
            }
        }
        /*
         *  else if (hit.collider == null && inHandItem != null)
        {
            if (inHandItem != null)
            {
                DropObject();
            }
            else
            {
                return;
            }
        }
        */
        return;


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionPointRadius);
    }

    private void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().buildIndex != 4)
        {
            if (inHandItem != null)
            {
                MoveObject();
            }
        }
    }

    private void Update()
    {
        /*if (!useLookAt && !collectableItem && hit.collider != null)
        {
            showGuidance.CloseGuidance();
        }*/
        

        if (!useLookAt || !collectableItem)
        {
            if (numFound <= 0 || hit.collider != null)
            {
                interactionPromptUI.Close();
                //showGuidance.CloseGuidance();
            }
        }

            //collect item
        if (!useLookAt || !collectableItem)
        {
            //showGuidance.CloseGuidance();
            _targetPosition = _parent.position + _parent.forward * 2f + new Vector3(0f, 2f, 0f);
        }
        _lookAtTarget.transform.position = Vector3.Lerp(_lookAtTarget.transform.position, _targetPosition, Time.deltaTime * lookSpeed);
        interactionInput.action.performed += Collect;

        //open inventory
        if (Input.GetKeyDown(KeyCode.I) && decryptCanvas.activeSelf == false)
        {
            canvasOpen = false;
            foreach (Transform t in storyCanvas.transform)
            {
                if (t.gameObject.activeSelf && t.gameObject.tag != "timer")
                {
                    canvasOpen = true;
                }
            }
            foreach (Transform t in instructionCanvas.transform)
            {
                if (t.gameObject.activeSelf)
                {
                    canvasOpen = true;
                }
            }
            if (!canvasOpen) EventManager.OnOpenInventory();          
        }
        //close canvases
        if (Input.GetKeyDown(KeyCode.C))
        {
            foreach (Transform t in storyCanvas.transform)
            {
                if (t.gameObject.tag != "timer")
                {
                    t.gameObject.SetActive(false);
                }
            }
            foreach (Transform t in instructionCanvas.transform)
            {
                t.gameObject.SetActive(false);
            }
        }

        numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionPointRadius, colliders, interactableMask);

        if (numFound > 0)
        {
            interactable = colliders[0].GetComponent<IInteractable>();

            if (interactable != null)
            {
                if (!interactionPromptUI.IsDisplayed) interactionPromptUI.SetUp(interactable.InteractionPrompt);
                if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.KeypadEnter) && decryptCanvas.activeSelf == false) interactable.Interact(this);
            }
        }
        if (numFound <= 0)
        {
            if (interactable != null) interactable = null;
           // if (interactionPromptUI.IsDisplayed) interactionPromptUI.Close();
        }


        //Lift item
        
        Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * hitRange, Color.red);
        if (hit.collider != null)
        {
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(false);
           // showGuidance.CloseGuidance();
        }

        if (Physics.Raycast(
            playerCameraTransform.position,
            playerCameraTransform.forward,
            out hit,
            hitRange,
            pickableLayerMask) && inHandItem == null)
        {
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(true);
            //showGuidance.SetUpGuidance("Press E to Pick Up");
            interactionPromptUI.SetUp("Press E to Pick Up");
        }

        /*
        private void OnApplicationQuit()
        {
            inventory.Container.Items.Clear();
        }*/
    }

    private void EventManagerOnGetInventoryItem(string name)
    {
        GameObject inventoryItem;
        string currentScene = SceneManager.GetActiveScene().name;

        if (name == "Meat") selectedObject = meat;
        if (name == "Mug of Beer") selectedObject = mug;
        if (name == "Carrot") selectedObject = carrot;
        if (name == "Bread") selectedObject = bread;
        if (name == "Torch" && currentScene != "DarkRoom" && currentScene != "Sokkelo") selectedObject = torch;
        if (name == "Torch" && currentScene == "DarkRoom")
        {
            EventManager.OnTorchInHand();
        }
        if (name == "Scroll")
        {
            EventManager.OnShowStory();
        }
        if (name == "Diary")
        {
            EventManager.OnShowDiary();
        }

        if (selectedObject != null)
        {
            Time.timeScale = 1;
            if (inHandItem != null)
            {
                DropObject();
            }
            inventoryItem = Instantiate(selectedObject, pickUpParent.position, Quaternion.identity);
            inventoryItem.AddComponent<ItemObject>();
            
            PickupObject(inventoryItem);
            inventoryItem.TryGetComponent<ItemCollectable>(out ItemCollectable item);
            inventory.RemoveItem(item);
            inventoryItem.GetComponent<ItemObject>().OnHandleTakeItemFromInv();
        }
        Time.timeScale = 1;
        return;
    }
}