using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{

    [Header("New Scene Variables")]
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorage;

    [Header("Transition Variables")]
    public SecondaryState secondaryState;
    public bool indoors;
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float waitTime;

    private void Awake()
    {
        
        if(fadeInPanel != null)
        {

            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1);
        }

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.CompareTag("Player") && !other.isTrigger)
        {

            if(indoors)
            {

                secondaryState.indoors = true;

            }
            else
            {

                secondaryState.indoors = false;

            }

            playerStorage.initialValue = playerPosition;

            StartCoroutine(FadeCo());

            //SceneManager.LoadScene(sceneToLoad);

        }

    }

    public IEnumerator FadeCo()
    {

        if (fadeOutPanel != null)
        {

            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);

        }

        yield return new WaitForSeconds(waitTime);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        while(!asyncOperation.isDone)
        {

            yield return null;

        }

    }

}
