using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mCameraHeight = 0.8f;
    public float mTurnSpeed = 1.2f;

    private Transform mTransform;
    private Transform mPlayer;
    private PlayerController mPlayerScript;
    private Vector2 mRotation = Vector2.zero;
    private Vector3 mCameraPosition;

    void Start()
    {
        mTransform = GetComponent<Transform>();
        mPlayer = GameManager.ManagerInstance().mPlayer.transform;
        mPlayerScript = mPlayer.GetComponent<PlayerController>();

        //Set the camera's start rotation to that of the player object (only around y axis)
        mRotation.x = mPlayer.rotation.eulerAngles.y;
    }
	
	void LateUpdate()
    {
        //Place camera at player's origin, then move up by 'mCameraHeight'
        if(mPlayer)
        {
            mCameraPosition = mPlayer.position;
            mCameraPosition.y += mCameraHeight;
            mTransform.position = mCameraPosition;
        }

        //Rotate camera and character
        mTransform.eulerAngles = new Vector3(mRotation.y, mRotation.x, 0f);
        if (mPlayerScript)
        {
            mPlayerScript.RotateCharacter(Quaternion.AngleAxis(mTransform.rotation.eulerAngles.y, Vector3.up));
        }
    }

    public void RotateCamera(Vector2 rotationValues)
    {
        mRotation.x = (mRotation.x + (rotationValues.x * mTurnSpeed)) % 360;
        mRotation.y = (mRotation.y - (rotationValues.y * mTurnSpeed)) % 360;

        //Limit how far you can look up and down
        mRotation.y = Mathf.Max(mRotation.y, -60);
        mRotation.y = Mathf.Min(mRotation.y, 60);
    }
}
