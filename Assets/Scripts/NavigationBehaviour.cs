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
        StartCoroutine(Fading(scene));
        //SceneManager.LoadSceneAsync(scene);
    }
    public void ReturnHome() {
        if (PlayerPrefs.GetString("Tag") == "Narr") {
            StartCoroutine(Fading("Narrative_Home"));
            //SceneManager.LoadSceneAsync("Narrative_Home");
            return;
        }
        StartCoroutine(Fading("BLAP_Home"));
        //SceneManager.LoadSceneAsync("BLAP_Home");
    }
    //public void ReturnToPrevious() {
    //    //Loade previous Scene if Feature is implemented
    //    Debug.Log("ReturnToPrevious : NYI");
    //}

    IEnumerator Fading(string sceneToLoad) {
        fader.SetBool("Fade", true);
        yield return new WaitUntil(() => fader.gameObject.GetComponent<Image>().color.a == 1);
        SceneManager.LoadSceneAsync(sceneToLoad);
    }
}
