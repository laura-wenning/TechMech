using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTargetController : MonoBehaviour
{
  public GameObject playerHead;
  public GameObject playerHand;
  public GameObject mechHead;
  public GameObject shoulder;
  public bool right;

  public float maxReach;

  private Transform playerHead_transform;
  private Transform playerHand_transform;
  private Transform mechHead_transform;
  private Transform this_transform;
  private Transform shoulder_transform;

  public GameObject mechEmpty; // The encompassing empty for the mech
  private float multiplier = 4;

  // Start is called before the first frame update
  void Start()
  {
    // Fetches all the transform components for each item we use for faster reference
    playerHead_transform = this.playerHead.GetComponent(typeof(Transform)) as Transform;
    playerHand_transform = this.playerHand.GetComponent(typeof(Transform)) as Transform;
    mechHead_transform = this.mechHead.GetComponent(typeof(Transform)) as Transform;
    this_transform = this.GetComponent(typeof(Transform)) as Transform;
    shoulder_transform = this.shoulder.GetComponent(typeof(Transform)) as Transform;
  }

  // Update is called once per frame
  void Update()
  {
    
  }

  void FixedUpdate() {
    this.MoveToPosition();
  }

  private void MoveToPosition() {
    // The displacement of the player hand relative to the player head. Think of this
    // as a unit vector, with the unit being an arm length
    Vector3 playerDisplacement = playerHand_transform.position - playerHead_transform.position;
    Vector3 mechHandDisplacement = playerDisplacement * multiplier;

    // Fixes an odd issue where down is up and vice versa. 
    // TODO - check that the rotation is in the right direction
    // mechHandDisplacement = Vector3.Scale(mechHandDisplacement, new Vector3(1, -1, 1));

    // Determines the rotation fo the mech head relative to the player head. 
    Quaternion mechRotation = mechHead_transform.rotation * Quaternion.Inverse(playerHead_transform.rotation);
    // Calculates the position of the hand
    // TODO - use the mech torso rather than head
    Vector3 targetPosition = (mechHead_transform.position + (mechRotation * mechHandDisplacement));
    Vector3 shoulderToHand = targetPosition - shoulder_transform.position;
    if (shoulderToHand.magnitude > this.maxReach) {
      targetPosition = (Vector3.Normalize(shoulderToHand) * this.maxReach) + shoulder_transform.position;
    }
    this_transform.position = targetPosition;
    // this_transform.rotation = mechHead_transform.rotation * playerRotation;
  }
}
