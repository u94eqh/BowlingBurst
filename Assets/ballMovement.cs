using UnityEngine;
using System.Collections;

public class ballMovement : MonoBehaviour {
	bool gutterFlag = false;
	float gutterPos1 = 5.5f;
	float gutterPos2 = -5.5f;

	void FixedUpdate(){
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		Vector3 jump = new Vector3 (0.0f, 500.0f, 0.0f);

		if (GetComponent<Rigidbody> ().velocity.magnitude < 50 && Physics.Raycast (transform.position, -Vector3.up, 1) && (gutterFlag == false)) {
			gutterFlag = false;
			GetComponent<Rigidbody> ().AddForce (movement * 20);
		}

		if (Input.GetKeyDown (KeyCode.Space) && Physics.Raycast (transform.position, -Vector3.up, 1) && gutterFlag == false) {
			gutterFlag = false;
			GetComponent<Rigidbody>().AddForce (jump);
		}

		//if (Physics.Raycast (transform.position, -Vector3.up, 1) && gutterFlag == false) {
		//	gutterFlag = true;
		//}

		//if (gutterFlag == true) {
		//	GetComponent<CameraFollowPlayerObject>().gutterFlag = true;
		//}
	}
}
