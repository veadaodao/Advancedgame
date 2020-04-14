using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageDoor : MonoBehaviour
{
    public Animator DoorOpen;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Unit")
        {
            DoorOpen.SetBool("Open", true);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Unit")
        {
            DoorOpen.SetBool("Open", false);
        }
    }
}
