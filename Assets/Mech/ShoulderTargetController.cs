using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoulderTargetController : MonoBehaviour
{
  public bool isRight;
  public GameObject debugTorso;

  private Transform this_transform;
  private Transform debugTorso_transform;

  // Start is called before the first frame update
  void Start() {
    this.this_transform = this.GetComponent(typeof(Transform)) as Transform;
    this.debugTorso_transform = debugTorso.GetComponent(typeof(Transform)) as Transform;
  }

  // Update is called once per frame
  void Update()
  {
      
  }

  void FixedUpdate() {
    this.MoveToPosition();
  }

  private void MoveToPosition() {
    Vector3 pos = this.debugTorso_transform.position;
    Quaternion rot = this.debugTorso_transform.rotation;
    Vector3 scale = this.debugTorso_transform.localScale;
    
    if (isRight) {
      pos.z -= scale.z / 2;
    } else {
      pos.z += scale.z / 2;
    }

    this.this_transform.position = pos;
    this.this_transform.rotation = rot;
  }
 }
