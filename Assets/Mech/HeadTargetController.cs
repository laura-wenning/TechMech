using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadTargetController : MonoBehaviour
{
  public GameObject playerHead;

  private Transform playerHead_transform;
  private Transform this_transform;

  // Start is called before the first frame update
  void Start()
  {
    playerHead_transform = playerHead.GetComponent(typeof(Transform)) as Transform;
    this_transform = this.GetComponent(typeof(Transform)) as Transform;
  }

  // Update is called once per frame
  void Update()
  {
    
  }

  void FixedUpdate() {
    this_transform.rotation = playerHead_transform.rotation;
  }
}
