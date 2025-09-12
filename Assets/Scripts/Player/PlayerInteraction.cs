using TMPro;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] float interactionDistance = 15;

    public GameObject interactionUI;

    public GameObject startButtonObject;

    public GameObject ammoTrigger;

    [Header("References")]
    public Timer timer;
    [SerializeField] Ammo ammo;
    [SerializeField] Weapon[] weapons;

    [Header("Text")]
    public TMP_Text nameGun;
    public TMP_Text startButtonText;
    public TMP_Text ammoText;

    public bool OnUIInteraction = true;

    private void Update()
    {
        InteractionRay();
    }

    // доделать наведение на предметы

    public void InteractionRay() 
    {
        Ray ray = mainCamera.ViewportPointToRay(Vector3.one / 2f);

        RaycastHit hit;

        bool hitSomething = false;

        if (Physics.Raycast(ray, out hit, interactionDistance)) 
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                hitSomething = true;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();

                    if (hit.collider.gameObject == startButtonObject && startButtonObject.tag == "StartButton")
                        timer.СoutDown();

                    if(hit.collider.gameObject == ammoTrigger) 
                        ammo.AddAmmo();
                }

                ShowName(hit);
                OnUIInteraction = hitSomething;
            }

            CheckActiveUI(hitSomething);
        }
    }

    public void CheckActiveUI(bool hitSomething) 
    {
        OnUIInteraction = hitSomething;
        interactionUI.SetActive(hitSomething);
        nameGun.gameObject.SetActive(hitSomething);
        startButtonText.gameObject.SetActive(hitSomething);
        ammoText.gameObject.SetActive(hitSomething);
    }

    public void OffText() 
    {
        startButtonText.text = "";
        ammoText.text = "";
    }

    public void ShowName(RaycastHit hit) 
    {
        switch (hit.collider.name)
        {
            case "revolver":
                nameGun.text = "revolver";
                OffText();
                break;

            case "shotgun":
                nameGun.text = "shotgun";
                OffText();
                break;

            case "carabine":
                nameGun.text = "carabine";
                OffText();
                break;

            case "mauser":
                nameGun.text = "mauser";
                OffText();
                break;

            case "volcanic":
                nameGun.text = "volcanic";
                OffText();
                break;

            case "StartButton":
                startButtonText.text = "start";
                ammoText.text = "";
                nameGun.text = "";
                break;

            case "Ammo":
                ammoText.text = "ammo";
                startButtonText.text = "";
                nameGun.text = "";
                break;
        }
    }
}
