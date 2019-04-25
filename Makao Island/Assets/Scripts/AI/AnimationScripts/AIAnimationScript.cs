using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
public class AIAnimationScript : MonoBehaviour
{
    public float mMinDelay = 0f;
    public float mMaxDelay = 1f;
    public AudioClip mWalkingSound;
    public string[] mExtraIdleAnimations;

    //Always used
    protected Animator mAnimator;
    protected GameManager mGameManager;

    protected NavMeshAgent mAgent;
    protected Transform mTransform;
    protected AudioSource mAudio;
    protected Vector3 mPreviousPosition = new Vector3();
    protected string mDefaultIdleAnimation;
    protected float mCurrentTime = 0f;

    protected virtual void Start()
    {
        mAnimator = GetComponent<Animator>();
        mAudio = GetComponentInParent<AudioSource>();
        mGameManager = GameManager.ManagerInstance();
        mGameManager.eSpeedChanged.AddListener(SetPlaySpeed);
        SetPlaySpeed(mGameManager.mGameSpeed);

        AnimatorClipInfo[] clipInfo = mAnimator.GetCurrentAnimatorClipInfo(0);
        mDefaultIdleAnimation = clipInfo[0].clip.name;
        mAnimator.Play(mDefaultIdleAnimation, 0, Random.value);

        mCurrentTime = Random.Range(mMinDelay, mMaxDelay);
    }

    protected virtual void Update()
    {
        mCurrentTime -= Time.deltaTime;

        WalkingAnimation();

        if (mCurrentTime <= 0f)
        {
            RandomIdleAnimation();
            mCurrentTime = Random.Range(mMinDelay, mMaxDelay);
        }
    }

    //Sets the variable that decides the speed of the animation
    public void SetPlaySpeed(float speed)
    {
        float newSpeed = speed > 1f ? 3f : 1f;
        mAnimator.SetFloat("GameSpeed", newSpeed);
    }

    protected void WalkingAnimation()
    {
        Vector3 positionOffset = new Vector3();

        if (mTransform)
        {
            positionOffset = mTransform.position - mPreviousPosition;
            mPreviousPosition = mTransform.position;
        }

        //If position has changed and the change is significant enough
        if (positionOffset != Vector3.zero && positionOffset.sqrMagnitude > (0.01f * 0.01f))
        {
            if (!mAnimator.GetBool("Walking"))
            {
                mAnimator.SetBool("Walking", true);

                if(mAudio && mAudio.isPlaying)
                {
                    mAudio.Stop();
                }
            }

            //Speed of the walk animation
            if(mAgent)
            {
                mAnimator.SetFloat("WalkSpeed", Mathf.Max(mAgent.velocity.magnitude / mAgent.speed, 0.6f));
            }
            else
            {
                mAnimator.SetFloat("WalkSpeed", Mathf.Max(positionOffset.magnitude, 0.6f));
            }

            if(mAudio && mWalkingSound && !mAudio.isPlaying)
            {
                mAudio.PlayOneShot(mWalkingSound);
            }
        }
        else
        {
            if (mAnimator.GetBool("Walking"))
            {
                mAnimator.SetBool("Walking", false);

                if (mAudio && mWalkingSound && mAudio.isPlaying)
                {
                    mAudio.Stop();
                }
            }

            //If agent has stopped moving before reaching the destination but is within an acceptable distance
            if (mAgent && (mAgent.remainingDistance > 0f) && (mAgent.remainingDistance < 2f))
            {
                mAgent.isStopped = true;
                mAgent.ResetPath();
            }
        }
    }

    //Randomly plays one of the idle animations in the array
    protected virtual void RandomIdleAnimation()
    {
        if(mAnimator.GetCurrentAnimatorStateInfo(0).IsName(mDefaultIdleAnimation) && mExtraIdleAnimations.Length > 0)
        {
            mAnimator.CrossFade(mExtraIdleAnimations[Random.Range(0, mExtraIdleAnimations.Length)], 0.2f);
        }
    }
}
