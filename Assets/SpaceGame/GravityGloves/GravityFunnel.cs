using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GravityFunnel : MonoBehaviour
{
  private GameObject targetObject;

  // Start is called before the first frame update
  void Start()
  {

  }

  public void Deactivate() {
    targetObject = null;
  }

  public void Prime() {
    Vector3 targetPos = transform.position; //controllerPose.transform.position;
    Vector3 targetVector = transform.TransformDirection(Vector3.up);

    // If connected to target object
    if (targetObject != null) {
      Debug.DrawRay(
        transform.position,
        (transform.position - targetObject.transform.position),
        Color.yellow
      );
      if (Vector3.Angle(targetVector, transform.position - targetObject.transform.position) > 5.0f) {
        targetObject = null;
      }
      return;
    }

    RaycastHit hit;
    Debug.DrawRay(targetPos, targetVector, Color.yellow);
    if (Physics.Raycast(targetPos, targetVector, out hit, 100)) {
      if (!hit.rigidbody) { return; }
      if (hit.rigidbody.gameObject.GetComponent<Tetherable>()) {
        targetObject = hit.rigidbody.gameObject;
      }      
    }
  }

  public void Fire() {
    if (targetObject) {
      targetObject.GetComponent<Rigidbody>().AddForce(
        (transform.position - targetObject.transform.position) * 5,
        ForceMode.Force
      );
    }
  }
}
