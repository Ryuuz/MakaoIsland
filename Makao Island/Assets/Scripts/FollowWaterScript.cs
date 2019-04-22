using UnityEngine;

public class FollowWaterScript : MonoBehaviour
{
    [SerializeField]
    private GameObject mOcean;

    private Transform mTransform;
    private float mAmount;
    private float mSpeed;
    private float mHeight;
    private float mStartYPosition;
    private Vector3 mPosition = new Vector3();

    void Start()
    {
        Material oceanMaterial = mOcean.GetComponent<Renderer>().material;
        mTransform = GetComponent<Transform>();

        mStartYPosition = mTransform.position.y;

        //Retrieve values from the material
        mSpeed = oceanMaterial.GetFloat("_Speed");
        mHeight = oceanMaterial.GetFloat("_Height");
        mAmount = oceanMaterial.GetFloat("_Amount");
    }

    void Update()
    {
        mPosition = mTransform.position;

        //Uses the same function as the shader to get y
        mPosition.y = mStartYPosition + Mathf.Sin((Time.timeSinceLevelLoad * 2f) * mSpeed + (mPosition.x * mPosition.z * mAmount)) * mHeight;
        mTransform.position = mPosition;
    }
}
