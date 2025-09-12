using TMPro;
using UnityEngine;

public class ItemInteractable : MonoBehaviour, IInteractable
{
    bool isCanPress;

    public string GetDescription() 
    {
        return "E";
    }

    public void Interact() 
    {
        isCanPress = !isCanPress;
    }
}
