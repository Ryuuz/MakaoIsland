using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AnimalAIScript : MonoBehaviour
{
    public float mSpeed = 2f;
    public float mAcceleration = 8f;
    public float mGrazingRadius = 4f;
    public float mMinWalkDelay = 0f;
    public float mMaxWalkDelay = 1f;

    private Transform mTransform;
    private NavMeshAgent mAgent;
    private float mMoveCountdown;
    private GameManager mGameManager;

    void Start()
    {
        mTransform = GetComponent<Transform>();
        mAgent = GetComponent<NavMeshAgent>();
        mGameManager = GameManager.ManagerInstance();
        mGameManager.eSpeedChanged.AddListener(ChangeSpeed);
        mMoveCountdown = Random.Range(mMinWalkDelay, mMaxWalkDelay);
    }

    void Update()
    {
        mMoveCountdown -= Time.deltaTime;

        //Walks to a new position within 'mGrazingDistance' at a random interval
        if(mMoveCountdown <= 0f)
        {
            mAgent.SetDestination(mTransform.position + Random.insideUnitSphere * mGrazingRadius);
            mMoveCountdown = Random.Range(mMinWalkDelay, mMaxWalkDelay);
        }
    }

    //Change the agent's speed when the time speeds up
    public void ChangeSpeed(float speed)
    {
        if(speed > 1f)
        {
            mAgent.speed = 10f;
            mAgent.acceleration = 60f;
        }
        else
        {
            mAgent.speed = mSpeed;
            mAgent.acceleration = mAcceleration;
        }
    }

    //Sets a destination along the 'direction' vector
    public void ForcedMove(Vector3 direction, float strength)
    {
        mAgent.SetDestination(mTransform.position + (direction.normalized * (strength * 0.5f)));
    }
}
