using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mCameraHeight = 0.8f;
    public float mTurnSpeed = 1.2f;

    [SerializeField]
    private GameObject mPlayer;

    private Vector2 mRotation = Vector2.zero;
    private Vector3 mCameraPosition;

    // Use this for initialization
    void Start ()
    {
        mPlayer = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Place camera at player's origin, then move up by 'mCameraHeight'
        mCameraPosition = mPlayer.transform.position;
        mCameraPosition.y += mCameraHeight;
        transform.position = mCameraPosition;

        mRotation.x = (mRotation.x + (Input.GetAxisRaw("Mouse X") * mTurnSpeed)) % 360;
        mRotation.y = (mRotation.y - (Input.GetAxisRaw("Mouse Y") * mTurnSpeed)) % 360;

        //Limit how far you can look up and down
        mRotation.y = Mathf.Max(mRotation.y, -60);
        mRotation.y = Mathf.Min(mRotation.y, 60);

        transform.eulerAngles = new Vector3(mRotation.y, mRotation.x, 0f);

        mPlayer.GetComponent<CharacterMovement>().RotateCharacter(Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up));
    }
}
