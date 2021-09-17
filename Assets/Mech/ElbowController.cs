using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElbowController : MonoBehaviour
{
  public GameObject shoulder;
  public GameObject hand;

  private Transform this_transform;
  private Transform shoulder_transform;
  private Transform hand_transform;
  private float reach = 2;

  // Start is called before the first frame update
  void Start()
  {
    this.this_transform = this.GetComponent(typeof(Transform)) as Transform;
    this.shoulder_transform = shoulder.GetComponent(typeof(Transform)) as Transform;
    this.hand_transform = hand.GetComponent(typeof(Transform)) as Transform;
  }

  // Update is called once per frame
  void Update()
  {
      
  }

  void FixedUpdate() {
    this.MoveToPosition();
  }

  private void MoveToPosition() {
    Vector3 midpoint = this.hand_transform.position - this.shoulder_transform.position;
    midpoint = midpoint / 2;

    float angle = Mathf.Acos(midpoint.magnitude / ((reach + 0.01f) / 2));
    float opposite = Mathf.Sin(angle);

    Vector3 target = midpoint + this.shoulder_transform.position;
    target.y = target.y - opposite;

    this.this_transform.position = target;
  }
}
