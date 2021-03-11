using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRCamera : MonoBehaviour
{
  [SerializeField]
  Color rayColor = Color.green;
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

  VRControls vrcontrols;

  Target target;

  void Awake()
  {
    vrcontrols = new VRControls();
  }

  void OnEnable()
  {
    vrcontrols.Enable();
  }

  void OnDisable()
  {
    vrcontrols.Disable();
  }

  void Start()
  {
      reticleTrs.localScale = initialScale;
      vrcontrols.Gameplay.VRClic.performed += _=> ClickOverObject();
  }

  void ClickOverObject() => target?.HandleColor();

  void FixedUpdate()
  {
    if(Physics.Raycast(transform.position, transform.forward, out hit, rayDistance, rayLayerDetection))
    {
      target = hit.collider.GetComponent<Target>();
      //target.HandleColor();
      reticleTrs.position = hit.point;
      reticleTrs.localScale = initialScale * hit.distance;
      reticleTrs.localRotation = Quaternion.LookRotation(hit.normal);
    }
    else
    {
      //target ??= null;
      if(target) target = null;
      reticleTrs.localScale = initialScale;
      reticleTrs.localPosition = new Vector3(0, 0, 1);
      reticleTrs.localRotation = Quaternion.identity;
    }
  }

  void OnDrawGizmosSelected()
  {
    Gizmos.color = rayColor;
    Gizmos.DrawRay(transform.position, transform.forward * rayDistance);
  }
}