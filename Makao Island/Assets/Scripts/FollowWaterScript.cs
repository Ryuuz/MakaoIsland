using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWaterScript : MonoBehaviour
{
    [SerializeField]
    private GameObject mOcean;

    private Transform mTransform;
    private float mAmount;
    private float mSpeed;
    private float mHeight;
    private float mOceanPositionY;
    private Vector3 mPosition;

    void Start()
    {
        Material oceanMaterial = mOcean.GetComponent<Renderer>().material;
        mTransform = GetComponent<Transform>();

        mOceanPositionY = mOcean.GetComponent<Transform>().position.y;
        mSpeed = oceanMaterial.GetFloat("_Speed");
        mHeight = oceanMaterial.GetFloat("_Height");
        mAmount = oceanMaterial.GetFloat("_Amount");

        mTransform.position.Set(mTransform.position.x, mOceanPositionY, mTransform.position.z);
    }

    void Update()
    {
        mPosition = mTransform.position;

        mPosition.y = mOceanPositionY + Mathf.Sin((Time.time * 2f) * mSpeed + (mPosition.x * mPosition.z * mAmount)) * mHeight;
        mTransform.position = mPosition;
    }
}
