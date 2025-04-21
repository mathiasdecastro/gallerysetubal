using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class MenuManager : MonoBehaviour
{
    public Image fadeImage;        
    public float fadeDuration = 1f;
    public string sceneToLoad = "SampleScene";
    public AudioClip clickSound;
    private AudioSource audioSource;
    private XRRayInteractor rayInteractor;

    [System.Obsolete]
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rayInteractor = FindObjectOfType<XRRayInteractor>();
    }

    void Update()
    {
        if (rayInteractor != null) {}

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                    StartGame();
            }
        }
    }

    public void StartGame()
    {
        if (clickSound != null && audioSource != null)
            audioSource.PlayOneShot(clickSound);

        StartCoroutine(FadeAndLoad());
    }

    IEnumerator FadeAndLoad()
    {
        float t = 0f;
        Color c = fadeImage.color;
        
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(0f, 1f, t / fadeDuration);
            fadeImage.color = c;
            
            yield return null;
        }

        SceneManager.LoadScene(sceneToLoad);
    }

    IEnumerator FadeIn()
    {
        float t = 0f;
        Color c = fadeImage.color;
        
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(1f, 0f, t / fadeDuration);
            fadeImage.color = c;
           
            yield return null;
        }
    }
}