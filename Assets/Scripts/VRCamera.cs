using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRCamera : MonoBehaviour
{

    [SerializeField]
    Color rayColor = Color.magenta;

    [SerializeField, Range(0.1f, 100f)]
    float rayDistance = 5f;

    [SerializeField]
    LayerMask rayLayerDetection;

    RaycastHit hit;

    [SerializeField]
    Transform reticleTrs;

    [SerializeField]
    Vector3 initialScale;

    bool objectTouched;


    void Update()
    {
        if(Physics.Raycast(transform.position, transform.forward, out hit, rayDistance, rayLayerDetection))
        {

            if(!objectTouched) objectTouched = true;
            if(!reticleTrs) return;
            reticleTrs.position = hit.point;
            reticleTrs.localScale = initialScale * hit.distance;
            //reticleTrs.LookAt(hit.normal);

        } else {

            if(objectTouched){
                
                objectTouched = false;
                reticleTrs.localScale = initialScale;
                reticleTrs.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
            
            }
        }

    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.color = rayColor;
        Gizmos.DrawRay(transform.position, transform.forward * rayDistance);
    }

}
