using UnityEngine;

public class SoundsButtons : MonoBehaviour
{
    public AudioSource selectAudio;
    public AudioClip hoverAudio;

    public void HoverSound() => selectAudio.PlayOneShot(hoverAudio);

}
