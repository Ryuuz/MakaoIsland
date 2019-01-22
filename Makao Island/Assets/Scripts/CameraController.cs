using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mCameraHeight = 0.8f;
    public float mTurnSpeed = 1.2f;

    private GameObject mPlayer;
    private Vector2 mRotation = Vector2.zero;
    private Vector3 mCameraPosition;

    void Start()
    {
        mPlayer = GameManager.ManagerInstance().mPlayer;
    }
	
	void Update()
    {
        //Place camera at player's origin, then move up by 'mCameraHeight'
        if(mPlayer)
        {
            mCameraPosition = mPlayer.transform.position;
            mCameraPosition.y += mCameraHeight;
            transform.position = mCameraPosition;
        }
    }

    public void RotateCamera(float xAxis, float yAxis)
    {
        mRotation.x = (mRotation.x + (xAxis * mTurnSpeed)) % 360;
        mRotation.y = (mRotation.y - (yAxis * mTurnSpeed)) % 360;

        //Limit how far you can look up and down
        mRotation.y = Mathf.Max(mRotation.y, -60);
        mRotation.y = Mathf.Min(mRotation.y, 60);

        transform.eulerAngles = new Vector3(mRotation.y, mRotation.x, 0f);

        if(mPlayer)
        {
            mPlayer.GetComponent<PlayerController>().RotateCharacter(Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up));
        }
    }
}
