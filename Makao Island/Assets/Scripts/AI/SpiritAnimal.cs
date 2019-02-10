using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritAnimal : AIController
{
    public SpiritAnimalType mAnimalType;

    protected override void Start()
    {
        base.Start();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            FadeOut();
        }
    }

    private IEnumerator FadeOut()
    {
        yield return null;
    }
}
