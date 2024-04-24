using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteCarrier : MonoBehaviour
{
    public Transform head;
    public float itemPickupDistance;
    public Transform attachedObject = null;
    float attachedDistance = 0f;

    void Update()
    {
        RaycastHit hit;
        bool cast = Physics.Raycast(head.position, head.forward, out hit, itemPickupDistance); // determines whether??? the player is looking at a pickable obj

        // Check for LMB input
        if (Input.GetMouseButtonDown(0))
        {
            //  Drop the picked object
            if (attachedObject != null) // if an obj is currently picked by the player
            {
                attachedObject.SetParent(null);

                //  Disable is kinematic for the rigidbody, if any
                if (attachedObject.GetComponent<Rigidbody>() != null)
                {
                    attachedObject.GetComponent<Rigidbody>().isKinematic = false;
                    attachedObject.GetComponent<Rigidbody>().freezeRotation = false;
                }


                //  Enable the collider, if any
                if (attachedObject.GetComponent<Collider>() != null)
                    attachedObject.GetComponent<Collider>().enabled = true;

                attachedObject = null; // to remove the reference
            }
            //  Pick up an object
            else
            {
                if (cast)
                {
                    if (hit.transform.CompareTag("Notes"))
                    {
                        attachedObject = hit.transform;
                        attachedObject.SetParent(transform);

                        attachedDistance = Vector3.Distance(attachedObject.position, head.position);

                        //  Enable is kinematic for the rigidbody, if any
                        if (attachedObject.GetComponent<Rigidbody>() != null)
                            attachedObject.GetComponent<Rigidbody>().isKinematic = true;

                        //  Disable the collider, if any
                        //  This is necessary
                        if (attachedObject.GetComponent<Collider>() != null)
                            attachedObject.GetComponent<Collider>().enabled = false;
                    }
                }
            }
        }
    }

    void LateUpdate()
    {
        //  Update the position and rotation of the attached object
        if (attachedObject != null) // if the player is currently picking up an obj
        {
            attachedObject.position = head.position + head.forward * 1.5f;
            attachedObject.Rotate(transform.right * Input.mouseScrollDelta.y * 30f, Space.World); // using the scroll wheel of the mouse, we can rotate the obj
        }
    }
}
