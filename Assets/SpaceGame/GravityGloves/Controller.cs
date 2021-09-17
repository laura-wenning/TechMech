using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Controller : MonoBehaviour
{
  public SteamVR_Input_Sources handType;
  public SteamVR_Behaviour_Pose controllerPose;

  public GameObject gripPrefab;
  public GameObject holePrefab;
  public GameObject funnelPrefab;

  public SteamVR_Action_Boolean grabAction;
  public SteamVR_Action_Boolean primeAction;  
  public SteamVR_Action_Boolean fireAction;

  private GameObject grip;
  private GameObject hole;
  private GameObject funnel;

  // Start is called before the first frame update
  void Start()
  {
    grip = Instantiate(gripPrefab);
    hole = Instantiate(holePrefab);
    funnel = Instantiate(funnelPrefab);

    grip.GetComponent<ControllerGrabObject>().controllerPose = controllerPose;

    grip.transform.parent = transform;
    grip.transform.localPosition = new Vector3(0, 0, 0);
    
    hole.transform.parent = transform;
    hole.transform.localPosition = new Vector3(0, 0, 0.1f);

    funnel.transform.parent = transform;
    funnel.transform.Rotate(0, 0, -90);
    funnel.transform.localPosition = new Vector3(0, 0, 0.25f);


    hole.SetActive(false);
    funnel.SetActive(true);

    // funnel.transform.parent = transform;
  }

  void Update() {
    RunGripCheck();
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    // grip.transform.position = transform.position;
    // hole.transform.localPosition = 
    
    RunPrimeCheck();
    RunFireCheck();
  }

  private void RunGripCheck() {
    var component = grip.GetComponent<ControllerGrabObject>();
    if (grabAction.GetLastStateDown(handType)) {
      component.AttemptGrab();
    }

    if (grabAction.GetLastStateUp(handType)) {
      component.AttemptRelease();
    }
  }

  private void RunPrimeCheck() {
    if (primeAction.GetState(handType)) {
      hole.SetActive(true);
      funnel.SetActive(true);
      funnel.GetComponent<GravityFunnel>().Prime();
    } else {
      hole.GetComponent<GravityHole>().Deactivate();
      funnel.GetComponent<GravityFunnel>().Deactivate();
      hole.SetActive(false);
      funnel.SetActive(false);
    }
  }

  private void RunFireCheck() {
    if (primeAction.GetState(handType) && fireAction.GetState(handType)) {
      hole.GetComponent<GravityHole>().Fire();
      funnel.GetComponent<GravityFunnel>().Fire();
    } else {
      hole.GetComponent<GravityHole>().Deactivate();
    }
  }
}
