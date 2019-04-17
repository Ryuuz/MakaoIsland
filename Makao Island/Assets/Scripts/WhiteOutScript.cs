using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WhiteOutScript : MonoBehaviour
{
    public float mFadeDelay = 0.5f;

    [SerializeField]
    private CanvasGroup mCanvasGroup;

    private Image mBackground;
    private Renderer mRenderer;

    void Start()
    {
        if(mCanvasGroup)
        {
            mBackground = mCanvasGroup.GetComponentInChildren<Image>();
        }
        
        mRenderer = GetComponentInChildren<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && mCanvasGroup && mRenderer)
        {
            StartCoroutine(WhiteOut());
        }
    }

    private IEnumerator WhiteOut()
    {
        //Start transparent
        mCanvasGroup.gameObject.SetActive(true);
        mCanvasGroup.alpha = 0f;

        if(mBackground)
        {
            mBackground.color = Color.white;
        }

        Color startColor = mRenderer.material.GetColor("_EmissionColor");
        Color endColor = Color.white * 20f;
        Color emissiveColor;

        float lerpTime = 0f;

        //Block user input
        InputHandler.InputInstance().StartCutscene(10f);

        while(lerpTime <= 1f)
        {
            lerpTime += (Time.deltaTime * mFadeDelay);
            mCanvasGroup.alpha = Mathf.Lerp(0f, 1f, lerpTime);
            emissiveColor = Color.Lerp(startColor, endColor, lerpTime);

            //Update material and global illumination
            mRenderer.material.SetColor("_EmissionColor", emissiveColor);
            DynamicGI.SetEmissive(mRenderer, emissiveColor);

            yield return null;
        }

        //Load the scene with the credits
        SceneManager.LoadScene(2);
    }
}
