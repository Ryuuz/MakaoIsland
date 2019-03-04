using UnityEngine;

public class BillboardScript : MonoBehaviour
{
    private Transform mCamera;

    private void Start()
    {
        Camera tempCam = GameManager.ManagerInstance().mMainCamera.GetComponent<Camera>();

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
            transform.LookAt(transform.position + mCamera.rotation * Vector3.forward, Vector3.up);
        }
    }
}
