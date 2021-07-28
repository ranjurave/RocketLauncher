using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RocketCollision : MonoBehaviour
{
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;
    bool isTransitioning = false;

    AudioSource rocketHitSound;
    Movement mvmt;

    void Start() {
        mvmt = GetComponent<Movement>();
        rocketHitSound = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision) {
        if (!isTransitioning) {
            switch (collision.gameObject.tag) {
                case "Friendly":
                    //Debug.Log("ok to hit");
                    break;
                case "Respawn":
                    StartCoroutine(ReloadLevel());
                    //Debug.Log(" killed");
                    break;
                case "Finish":
                    StartCoroutine(LoadNextLevel());
                    break;
            }
        }
    }
    IEnumerator LoadNextLevel() {
        rocketHitSound.Stop();
        isTransitioning = true;
        rocketHitSound.PlayOneShot(successSound);
        mvmt.enabled = false;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings) {
            nextSceneIndex = 0;
        }
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(nextSceneIndex);
        mvmt.enabled = true;
    }

    IEnumerator ReloadLevel() {
        rocketHitSound.Stop();
        isTransitioning = true;
        rocketHitSound.PlayOneShot(crashSound);
        mvmt.enabled = false;
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        mvmt.enabled = true;
    }
}
