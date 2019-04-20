using UnityEngine;

[RequireComponent(typeof(CharacterController))]
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
    private bool mSprinting = false;

    void Start()
    {
        mCharacterController = GetComponent<CharacterController>();
        mCharacterTransform = GetComponent<Transform>();
        mCurrentMovementSpeed = mWalkSpeed;
        mCurrentFallSpeed = 0f;
    }

    void Update()
    {
        MoveCharacter();
    }

    private void FixedUpdate()
    {
        Gravity();
    }

    //Moves the character with different speed depending on whether sprinting or not
    void MoveCharacter()
    {
        if(mCharacterController.isGrounded)
        {
            mCurrentMovementSpeed = mSprinting ? mRunSpeed : mWalkSpeed;
        }
        
        Vector3 moving = mMovementDirection * mCurrentMovementSpeed;

        //Clamp the speed
        if(moving.magnitude > mCurrentMovementSpeed)
        {
            moving = moving.normalized * mCurrentMovementSpeed;
        }

        mCharacterController.Move((moving + new Vector3(0f, mCurrentFallSpeed, 0f)) * Time.deltaTime);
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
    public void SetMovementDirection(Vector2 direction)
    {
        if(mCharacterController.isGrounded)
        {
            mMovementDirection = mCharacterTransform.TransformDirection(new Vector3(direction.x, 0f, direction.y));
        }
        else
        {
            //Add the new directions to the current movement direction for slight changes
            mMovementDirection += mCharacterTransform.TransformDirection(new Vector3((direction.x * 0.05f), 0f,  (direction.y * 0.1f)));
        }
    }

    //Set the movement speed based on wether the character is running or not
    public void TriggerSprint(bool sprinting)
    {
        mSprinting = sprinting;
    }

    //Use the special action if it is available
    public void TriggerSpecialAction(bool active)
    {
        if(mSpecialAction != null)
        {
            mSpecialAction.UseSpecialAction(active);
        }
    }

    //Player can push cetain objects when colliding with them
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "PhysicsObject")
        {
            Rigidbody body = hit.collider.attachedRigidbody;

            if(body && !body.isKinematic && hit.moveDirection.y > -0.3f)
            {
                body.AddForce(new Vector3(hit.moveDirection.x, 0f, hit.moveDirection.z) * (mCurrentMovementSpeed * 0.1f), ForceMode.Impulse);
            }
        }
        else if(hit.gameObject.tag == "Animal")
        {
            AnimalAIScript animal = hit.gameObject.GetComponent<AnimalAIScript>();
            if(animal)
            {
                animal.ForcedMove(mMovementDirection, mCurrentMovementSpeed);
            }
        }
    }
}
