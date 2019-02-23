using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float mWalkSpeed = 5f;
    public float mRunSpeed = 10f;
    public float mJumpForce = 5f;
    public float mMaxFallSpeed = 20f;
    public SpecialActionObject mSpecialAction { get; set; }

    private Vector3 mMovementDirection = Vector3.zero;
    private float mCurrentFallSpeed;
    private float mCurrentMovementSpeed;
    private CharacterController mCharacterController;
    private Transform mCharacterTransform;

    private void Awake()
    {
        mCharacterController = GetComponent<CharacterController>();
        mCharacterTransform = GetComponent<Transform>();
    }

    void Start()
    {
        GameManager.ManagerInstance();
        GetComponent<MeshRenderer>().enabled = false;
        mCurrentMovementSpeed = mWalkSpeed;
        mCurrentFallSpeed = 0f;
    }

    void Update()
    {
        Gravity();
        MoveCharacter();
    }

    //Moves the character with different speed depending on whether it's on the ground or in the air
    void MoveCharacter()
    {
        if (mCharacterController.isGrounded)
        {
            mCharacterController.Move(((mMovementDirection * mCurrentMovementSpeed) + new Vector3(0f, mCurrentFallSpeed, 0f)) * Time.deltaTime);
        }
        else
        {
            mCharacterController.Move(((mMovementDirection * (mCurrentMovementSpeed * 0.6f)) + new Vector3(0f, mCurrentFallSpeed, 0f)) * Time.deltaTime);
        }
    }

    //Applies gravity to the character
    void Gravity()
    {
        if (!mCharacterController.isGrounded)
        {
            mCurrentFallSpeed += Physics.gravity.y * Time.deltaTime;
            mCurrentFallSpeed = Mathf.Max(mCurrentFallSpeed, -mMaxFallSpeed);
        }
    }

    //Rotates the character to the given rotation
    public void RotateCharacter(Quaternion charRotation)
    {
        mCharacterTransform.rotation = charRotation;
    }

    //Makes the character jump if grounded
    public void Jump()
    {
        if (mCharacterController.isGrounded)
        {
            mCurrentFallSpeed = mJumpForce;
        }
    }

    //Sets the direction the player should move in
    public void SetMovementDirection(float horizontal, float vertical)
    {
        mMovementDirection = mCharacterTransform.TransformDirection(new Vector3(horizontal, 0f, vertical));
    }

    //Set the movement speed based on wether the character is running or not
    public void TriggerSprint(bool sprinting)
    {
        if (sprinting)
        {
            mCurrentMovementSpeed = mRunSpeed;
        }
        else
        {
            mCurrentMovementSpeed = mWalkSpeed;
        }
    }

    //Use the special action if it is available
    public void TriggerSpecialAction(bool active)
    {
        if(mSpecialAction != null)
        {
            mSpecialAction.UseSpecialAction(active);
        }
    }
}
