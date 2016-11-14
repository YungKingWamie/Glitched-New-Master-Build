using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerFlashlight : MonoBehaviour {
	//public GameObject target;
	public float viewRadius;
	GameObject flashlight;
	public LayerMask feeder;
	[Range(0,360)]
	public float viewAngle;
	public bool stunFeeder;
	public List<Transform> visibleTargets = new List<Transform>();
	public Light toggle;
	public KeyCode toggleFlashlightKey;

	// Use this for initialization
	void Start () {
        flashlight = GameObject.FindGameObjectWithTag("Flashlight");
        toggle = flashlight.GetComponent<Light> ();
		stunFeeder = false;
		viewRadius = flashlight.GetComponent<Light> ().range;
	}
	
	// Update is called once per frame
	void Update () {
		if (toggle.enabled == true && Input.GetKeyDown (toggleFlashlightKey)) 
			toggle.enabled = false;
		else if(toggle.enabled == false && Input.GetKeyDown(toggleFlashlightKey))	
			toggle.enabled = true;

		if (toggle.enabled == true) {
			visibleTargets.Clear ();
			Collider[] targetInViewRadius = Physics.OverlapSphere (transform.position, viewRadius, feeder);
			for (int i = 0; i < targetInViewRadius.Length; i++) {
				Transform enemyTarget = targetInViewRadius [i].transform;
				Vector3 dirToTarget = (enemyTarget.position - transform.position).normalized;
				if (Vector3.Angle (transform.forward, dirToTarget) < viewAngle / 2) {
					float distToTarget = Vector3.Distance (transform.position, enemyTarget.position);
					if (Physics.Raycast (transform.position, dirToTarget, distToTarget, feeder)) {
						visibleTargets.Add (enemyTarget);
						stunFeeder = true;
					} else {
						stunFeeder = false;
					}
				}
			}
		} else {
			stunFeeder = false;
		}
	}
	public Vector3 dirAngle(float angleInDegrees, bool angleIsGlobal){
		if (!angleIsGlobal) {
			angleInDegrees += transform.eulerAngles.y;
		}
		return new Vector3 (Mathf.Sin (angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos (angleInDegrees * Mathf.Deg2Rad));
	}
}
