using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public InputHandler mInputHandler;
    public GameObject mPlayer;
    public GameObject mMainCamera;

    private static GameManager gameManager;

    public static GameManager ManagerInstance()
    {
        if(gameManager == null)
        {
            GameObject manager = GameObject.Find("Manager");

            if (manager)
            {
                gameManager = manager.AddComponent<GameManager>();
            }
            else
            {
                manager = new GameObject("Manager");
                gameManager = manager.AddComponent<GameManager>();
            }
        }

        return gameManager;
    }

    private void Awake()
    {
        if(gameManager == null)
        {
            gameManager = this;
        }
        else if(gameManager != this)
        {
            Destroy(this);
        }

        if (!mPlayer)
        {
            mPlayer = GameObject.Find("Player");
        }

        if (!mMainCamera)
        {
            mMainCamera = GameObject.Find("Main Camera");
        }
    }

    void Start()
    {
        mInputHandler = InputHandler.InputInstance();
    }
	
	void Update()
    {
		
	}
}
