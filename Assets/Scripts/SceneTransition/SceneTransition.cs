using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public TMP_Text loadingParcent;
    public Image loadingProgressBar;

    private static SceneTransition instance;
    private static bool shouldPlayOpeningAnimation = false;

    public Animator  animator;
    private AsyncOperation loadingSceneOperation;

    public static void SwitchToScene(string sceneName) 
    {
        instance.animator.SetTrigger("sceneClosing");

        instance.loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);
        instance.loadingSceneOperation.allowSceneActivation = false;

        instance.loadingProgressBar.fillAmount = 0;
    }

    private void Start()
    {
        instance = this;

        animator = GetComponent<Animator>();

        if (shouldPlayOpeningAnimation)
        {
            animator.SetTrigger("sceneOpening");
            instance.loadingProgressBar.fillAmount = 1;

            shouldPlayOpeningAnimation = false;
        }
    }

    private void Update()
    {
        if (loadingSceneOperation != null) 
        {
            loadingParcent.text = Mathf.RoundToInt(loadingSceneOperation.progress * 100) + "%";
            //loadingProgressBar.fillAmount = loadingSceneOperation.progress;

            loadingProgressBar.fillAmount = Mathf.Lerp(loadingProgressBar.fillAmount, loadingSceneOperation.progress, Time.deltaTime * 5);
        }
    }

    public void OnAnimationOver() 
    {
        shouldPlayOpeningAnimation = true;
        instance.loadingSceneOperation.allowSceneActivation = true;
    }
}
