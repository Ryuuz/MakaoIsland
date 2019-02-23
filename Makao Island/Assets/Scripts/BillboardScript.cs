using UnityEngine;

public class BillboardScript : MonoBehaviour
{
    private Transform mCamera;

    private void Start()
    {
        mCamera = GameManager.ManagerInstance().mMainCamera.GetComponent<Camera>().transform;
    }

    void LateUpdate()
    {
        //The plane will always face the player
        transform.LookAt(transform.position + mCamera.rotation * Vector3.forward, Vector3.up);
    }
}
