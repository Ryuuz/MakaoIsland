﻿using UnityEngine;

public class CloudScript : MonoBehaviour
{
    public int mNumberOfQuads = 20;
    public float mCloudHeight = 40f;
    public float mCloudScale = 20f;
    public Gradient mCloudColor;

    [SerializeField]
    private GameObject mCloudObject;

    private int mWidth = 256;
    private int mHeight = 256;

    private float mOriginX = 0f;
    private float mOriginY = 0f;
    
    
    private Texture2D mNoiseTexture;
    private Material mMaterial;
    private Color[] mPixels;

    void Start()
    {
        //Get everything ready for the texture creation
        float yValue = transform.position.y;
        mNoiseTexture = new Texture2D(mWidth, mHeight);
        mNoiseTexture.wrapMode = TextureWrapMode.Repeat;
        mPixels = new Color[mNoiseTexture.width * mNoiseTexture.height];
        CalculateNoise();

        DrawClouds();

        mMaterial = mCloudObject.GetComponent<Renderer>().sharedMaterial;

        //Set the shader values
        if (mMaterial)
        {
            mMaterial.SetTexture("_MainTex", mNoiseTexture);
            mMaterial.SetTexture("_SecondTex", mNoiseTexture);
            mMaterial.SetFloat("_MidYValue", yValue);
            mMaterial.SetFloat("_Height", mCloudHeight);
        }
    }

    //Create a texture using Perlin noise
    private void CalculateNoise()
    {
        float xCoord;
        float yCoord;
        float sample;

        //Go through each pixel of the texture
        for (int i = 0; i < mNoiseTexture.height; i++)
        {
            for (int j = 0; j < mNoiseTexture.width; j++)
            {
                xCoord = mOriginX + (float)j / mNoiseTexture.width * mCloudScale;
                yCoord = mOriginY + (float)i / mNoiseTexture.height * mCloudScale;
                sample = Mathf.PerlinNoise(xCoord, yCoord);
                mPixels[i * mNoiseTexture.width + j] = new Color(sample, sample, sample);
            }
        }

        mNoiseTexture.SetPixels(mPixels);
        mNoiseTexture.Apply();
    }

    //Instantiate the quads with the cloud texture
    private void DrawClouds()
    {
        //Distance between the quads
        float offset = mCloudHeight / (float)mNumberOfQuads / 2f;

        Vector3 startPosition = transform.position + (Vector3.up * (offset * (float)mNumberOfQuads / 2f));

        for (int i = 0; i < mNumberOfQuads; i++)
        {
            Instantiate(mCloudObject, startPosition - (Vector3.up * offset * i), transform.rotation, transform);
        }
    }

    //Change the color of the clouds based on 'progress'
    public void UpdateCloudColor(float progress)
    {
        if(mMaterial)
        {
            mMaterial.SetColor("_Color", mCloudColor.Evaluate(progress));
        }
    }
}
