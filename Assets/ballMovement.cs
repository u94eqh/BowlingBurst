using UnityEngine;
using System.Collections;

public class ballMovement : MonoBehaviour {
	bool gutterFlag, jumpFlag;
	float timer = 5.0f;
	Vector3 originalPosition;
	public ArrayList pins = new ArrayList();
	int fallen = 0;

	void Start(){
		gutterFlag = false; 
		jumpFlag = false;
		originalPosition = new Vector3 (0, 1, 2);
		GameObject pin = GameObject.Find("pin");
		GameObject pin1 = GameObject.Find("pin 1");
		GameObject pin2 = GameObject.Find("pin 2");
		GameObject pin3 = GameObject.Find("pin 3");
		GameObject pin4 = GameObject.Find("pin 4");
		GameObject pin5 = GameObject.Find("pin 5");
		GameObject pin6 = GameObject.Find("pin 6");
		GameObject pin7 = GameObject.Find("pin 7");
		GameObject pin8 = GameObject.Find("pin 8");
		GameObject pin9 = GameObject.Find("pin 9");
		
		pins.Add(pin.transform);
		pins.Add(pin1.transform);
		pins.Add(pin2.transform);
		pins.Add(pin3.transform);
		pins.Add(pin4.transform);
		pins.Add(pin5.transform);
		pins.Add(pin6.transform);
		pins.Add(pin7.transform);
		pins.Add(pin8.transform);
		pins.Add(pin9.transform);
	}

	public void setGutterFlagTrue(){
		gutterFlag = true;
	}

	void FixedUpdate(){

		if (gutterFlag == false) {
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");

			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			Vector3 jump = new Vector3 (0.0f, 500.0f, 0.0f);


			if (GetComponent<Rigidbody> ().velocity.magnitude < 50 && (gutterFlag == false)) {
				gutterFlag = false;
				GetComponent<Rigidbody> ().AddForce (movement * 15);
				//GetComponent<Rigidbody> ().maxAngularVelocity = 1000000000.0f;
			}

			if (Input.GetKeyDown (KeyCode.Space) && gutterFlag == false && jumpFlag == false) {
				jumpFlag = true;
				GetComponent<Rigidbody> ().AddForce (jump);
			}

			foreach(Transform pin in pins){
				if(pin.up.y < 0.6f){
					fallen++;
				}
			}

			if(fallen == 10){
				Application.LoadLevel(Application.loadedLevel);
			}

		} 
		else {
			timer -= Time.deltaTime;
			if(timer <= 0){
				GetComponent<Rigidbody> ().Sleep();
				GetComponent<Rigidbody>().MovePosition(originalPosition);
				gutterFlag = false;
				timer = 5.0f;
				GetComponent<Rigidbody> ().WakeUp();

			}
		}
	}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.name == "Plane") {
			jumpFlag = false;
		}

	}

	void OnTriggerEnter(Collider collider){
		if (collider.name == "LeftInvisibleWall" || collider.name == "RightInvisibleWall") {
			gutterFlag = true;
		}
	}

	void OnTriggerStay(Collider collider){
		if (collider.name == "PinsTimerTrigger") {
			timer -= Time.deltaTime;
			if(timer <= 0){
				GetComponent<Rigidbody> ().Sleep();
				GetComponent<Rigidbody>().MovePosition(originalPosition);
				timer = 5.0f;
				GetComponent<Rigidbody> ().WakeUp();
				
			}
		}
	}
}
