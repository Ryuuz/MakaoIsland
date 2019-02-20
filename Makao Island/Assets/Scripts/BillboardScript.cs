using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardScript : MonoBehaviour
{
    private Camera mCamera;

    private void Start()
    {
        mCamera = GameManager.ManagerInstance().mMainCamera.GetComponent<Camera>();
    }

    void LateUpdate()
    {
        //The plane will always face the player
        transform.LookAt(transform.position + mCamera.transform.rotation * Vector3.forward, Vector3.up);
    }
}
