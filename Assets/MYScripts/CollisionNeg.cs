using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionNeg : MonoBehaviour
{
    public GameObject ignoreCollision;
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(ignoreCollision.GetComponent<Collider>(), GetComponent<Collider>());
    }

    // Update is called once per frame
    void Update()
    {
        //Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GetComponent<Collider>());
    }
}
