using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public float mSpeed = 3.5f;
    public Transform mDawnLocation;
    public Transform mDayLocation;
    public Transform mDuskLocation;
    public Transform mNightLocation;
    public Sprite mIcon;

    [HideInInspector]
    public bool mTalking = false;

    private NavMeshAgent mAgent;

    // Start is called before the first frame update
    void Start()
    {
        mAgent = GetComponent<NavMeshAgent>();
        mAgent.speed = mSpeed;

        GameManager.ManagerInstance().eSpeedChanged.AddListener(ChangingSpeed);
        GameManager.ManagerInstance().eTimeChanged.AddListener(Transition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Transition(DayCyclus time)
    {
        Transform pos = null;

        switch (time)
        {
            case DayCyclus.dawn:
                pos = mDawnLocation;
                break;

            case DayCyclus.day:
                pos = mDayLocation;
                break;

            case DayCyclus.dusk:
                pos = mDuskLocation;
                break;

            case DayCyclus.night:
                pos = mNightLocation;
                break;
        }

        if(pos)
        {
            StartCoroutine("MoveWhenReady", pos.position);
        }
        
    }

    public void ChangingSpeed(float speed)
    {
        mAgent.speed = mSpeed * speed;
    }

    private IEnumerator MoveWhenReady(Vector3 position)
    {
        yield return new WaitUntil(() => mTalking == false);

        mAgent.SetDestination(position);
    }
}
