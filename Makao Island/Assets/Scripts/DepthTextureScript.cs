using UnityEngine;

[ExecuteInEditMode]
public class DepthTextureScript : MonoBehaviour
{
    private Camera mCamera;

    void Start()
    {
        mCamera = GetComponent<Camera>();
        mCamera.depthTextureMode = DepthTextureMode.Depth;
    }
}
