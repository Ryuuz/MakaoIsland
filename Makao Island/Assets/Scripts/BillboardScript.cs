using UnityEngine;

public class BillboardScript : MonoBehaviour
{
    private Transform mCamera;
    private Transform mTransform;

    private void Start()
    {
        mTransform = GetComponent<Transform>();
        GameObject tempCam = GameManager.ManagerInstance().mMainCamera;

        if(tempCam)
        {
            mCamera = tempCam.transform;
        }
    }

    void LateUpdate()
    {
        //The plane will always face the player
        if(mCamera)
        {
            mTransform.LookAt(mTransform.position + (mCamera.rotation * Vector3.forward), Vector3.up);
        }
    }
}
