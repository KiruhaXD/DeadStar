using UnityEngine;

public class ButtonInteract : MonoBehaviour, IInteractable
{
    public Animator anim;
    bool isCanPress;

    private void Start()
    {
        if (isCanPress)
            anim.SetBool("isPress", true);
    }
    public string GetDescription()
    {
        return "E";
    }

    public void Interact()
    {
        isCanPress = !isCanPress;

        if (isCanPress)
            anim.SetBool("isPress", true);
        else
            anim.SetBool("isPress", false);
    }
}
