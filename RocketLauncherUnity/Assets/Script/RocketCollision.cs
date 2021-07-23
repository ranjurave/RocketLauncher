using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RocketCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision) {
        switch (collision.gameObject.tag) {
            case "Friendly":
                Debug.Log("ok to hit");
                break;
            case "Respawn":
                StartCoroutine(ReloadLevel());
                Debug.Log(" killed");
                break;
            case "Finish":
                LoadNextLevel();
                break;
        }
    }
    void LoadNextLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings) {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    IEnumerator ReloadLevel() {
        Movement mvmt = GetComponent<Movement>();
        mvmt.enabled = false;
        mvmt.rocketAS.enabled = false;
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        mvmt.enabled = true;
        mvmt.rocketAS.enabled = true ;
    }
}
