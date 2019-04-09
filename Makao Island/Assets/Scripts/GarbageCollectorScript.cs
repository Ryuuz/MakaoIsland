using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollectorScript : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "PhysicsObject")
        {
            Rigidbody body = other.attachedRigidbody;

            if (body)
            {
                body.AddForce(body.velocity * (-1f) * 6f);
            }
        }
    }

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