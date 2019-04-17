using UnityEngine;

public class OceanVolumeScript : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        //Physics objects are slowed down and start sinking
        if (other.tag == "PhysicsObject")
        {
            Rigidbody body = other.attachedRigidbody;

            if (body)
            {
                body.AddForce(body.velocity * (-1f) * 6f);
            }
        }
    }

    //Destroy the object when it reaches the bottom of the trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PhysicsObject")
        {
            if (other.transform.position.y < -15f)
            {
                Destroy(other.gameObject);
            }
        }
    }
}
