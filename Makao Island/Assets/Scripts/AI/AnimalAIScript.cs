using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AnimalAIScript : MonoBehaviour
{
    public float mGrazingRadius = 4f;
    public float mMinWalkDelay = 0f;
    public float mMaxWalkDelay = 1f;

    private Transform mTransform;
    private NavMeshAgent mAgent;
    private float mMoveCountdown;

    void Start()
    {
        mTransform = GetComponent<Transform>();
        mAgent = GetComponent<NavMeshAgent>();
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

    //Sets a destination along the 'direction' vector
    public void ForcedMove(Vector3 direction, float strength)
    {
        mAgent.SetDestination(mTransform.position + (direction.normalized * (strength * 0.5f)));
    }
}
