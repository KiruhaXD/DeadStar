using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CommonMenu : MonoBehaviour
{
    public List<Animator> animators;
    public GameObject menuSettings;
    public Image authorImage;
    public Button closeImageAuthor;

    [SerializeField] MenuSettings _menuSettings;

    private void Start()
    {
        menuSettings.SetActive(false);
        authorImage.gameObject.SetActive(false);
        closeImageAuthor.gameObject.SetActive(false);
    }

    private void Update()
    {
        foreach (Animator anim in animators)
        {
            anim.Update(Time.unscaledDeltaTime);
        }
    }

    public void PlayGame() => SceneTransition.SwitchToScene("SampleScene");

    public void Tutorial() => SceneTransition.SwitchToScene("Tutorial");

    public void SettingButton() 
    {
        menuSettings.SetActive(true);
    } 

    public void EscapeFromSettings() => menuSettings.SetActive(false);

    public void AuthorButton() 
    {
        closeImageAuthor.gameObject.SetActive(true);
        authorImage.gameObject.SetActive(true);
    }

    public void CloseAuthorImage() 
    {
        closeImageAuthor.gameObject.SetActive(false);
        authorImage.gameObject.SetActive(false);
    } 

    public void ExitGame() => Application.Quit();

}
