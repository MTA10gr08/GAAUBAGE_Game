using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NavigationBehaviour : MonoBehaviour
{
    public Animator fader;
    private void Awake() {
    }

    public void NavigateTo(string scene) {
        if (SceneManager.GetActiveScene().name == scene) {
            return;
        }
        StartCoroutine(FadingTo(scene));
        //SceneManager.LoadSceneAsync(scene);
    }
    public void ReturnHome() {
        if (PlayerPrefs.GetString("Tag") == "Narr") {
            StartCoroutine(FadingTo("Narrative_Home"));
            return;
        }
        StartCoroutine(FadingTo("BLAP_Home"));
    }

    IEnumerator FadingTo(string sceneToLoad) {
        fader.SetBool("Fade", true);
        yield return new WaitUntil(() => fader.gameObject.GetComponent<Image>().color.a == 1);
        SceneManager.LoadSceneAsync(sceneToLoad);
    }
}
