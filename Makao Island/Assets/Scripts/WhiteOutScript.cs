using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WhiteOutScript : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup mCanvasGroup;

    private Image mBackground;
    private Renderer mRenderer;

    void Start()
    {
        mBackground = mCanvasGroup.GetComponentInChildren<Image>();
        mRenderer = GetComponentInChildren<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(WhiteOut());
        }
    }

    private IEnumerator WhiteOut()
    {
        mCanvasGroup.gameObject.SetActive(true);
        mCanvasGroup.alpha = 0f;
        mBackground.color = Color.white;

        Color startColor = mRenderer.material.GetColor("_EmissionColor");
        Color endColor = Color.white * 20f;
        Color emissiveColor;

        float lerpTime = 0f;

        InputHandler.InputInstance().StartCutscene(10f);

        while(lerpTime <= 1f)
        {
            lerpTime += Time.deltaTime * 0.5f;
            mCanvasGroup.alpha = Mathf.Lerp(0f, 1f, lerpTime);
            emissiveColor = Color.Lerp(startColor, endColor, lerpTime);

            mRenderer.material.SetColor("_EmissionColor", emissiveColor);
            DynamicGI.SetEmissive(mRenderer, emissiveColor);

            yield return null;
        }

        SceneManager.LoadScene(2);
    }
}
