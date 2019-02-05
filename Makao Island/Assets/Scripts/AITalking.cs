using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AITalking : MonoBehaviour
{
    public Sprite mIcon;

    private AIController mController;

    [SerializeField]
    private RectTransform mSpeechBubble;

    private void Awake()
    {
        mController = GetComponent<AIController>();
        ToggleSpeechBubble(false);
    }

    public void ToggleSpeechBubble(bool show)
    {
        if(show)
        {
            mSpeechBubble.gameObject.SetActive(true);
        }
        else
        {
            mSpeechBubble.gameObject.SetActive(false);
        }
    }

    public IEnumerator LookAtObject(Vector3 obj)
    {
        Quaternion endRotation = Quaternion.LookRotation(obj - transform.position, Vector3.up);

        while (Quaternion.Angle(transform.rotation, endRotation) > 2f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, endRotation, Time.deltaTime * GameManager.ManagerInstance().mGameSpeed);
            yield return null;
        }
    }

    public void SetTalking(bool talking)
    {
        mController.mTalking = talking;
    }
}
