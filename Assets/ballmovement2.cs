using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ballmovement2 : MonoBehaviour {
	bool gutterFlag, jumpFlag, loseControlFlag;
	float timer = 5.0f;
	Vector3 originalPosition;
	public List<GameObject> pins = new List<GameObject>();
	public List<GameObject> removePins = new List<GameObject> ();
	Text gameText;
	int fallen = 0;
	Vector3 pinJump = new Vector3 (0, 35.0f, 0);
	
	GameObject pin;
	GameObject pin1;
	GameObject pin2;
	GameObject pin3;
	GameObject pin4;
	GameObject pin5;
	GameObject pin6;
	GameObject pin7;
	GameObject pin8;
	GameObject pin9;
	
	void Start(){
		gutterFlag = false; 
		jumpFlag = false;
		originalPosition = new Vector3 (0, 1, 2);
		
		gameText = GameObject.Find ("Text").GetComponent<Text> ();
		gameText.text = "";
		
		pin = GameObject.Find("pin");
		pin1 = GameObject.Find("pin 1");
		pin2 = GameObject.Find("pin 2");
		pin3 = GameObject.Find("pin 3");
		pin4 = GameObject.Find("pin 4");
		pin5 = GameObject.Find("pin 5");
		pin6 = GameObject.Find("pin 6");
		pin7 = GameObject.Find("pin 7");
		pin8 = GameObject.Find("pin 8");
		pin9 = GameObject.Find("pin 9");
		
		pins.Add(pin);
		pins.Add(pin1);
		pins.Add(pin2);
		pins.Add(pin3);
		pins.Add(pin4);
		pins.Add(pin5);
		pins.Add(pin6);
		pins.Add(pin7);
		pins.Add(pin8);
		pins.Add(pin9);
	}
	
	void FixedUpdate(){
		
		if (fallen == 10) {
			gameText.text = "Good job!";
			timer -= Time.deltaTime;
			if (timer <= 0) {
				Application.LoadLevel (0);
			}
		} 
		else {
			if(loseControlFlag == false){
				if (gutterFlag == false) {
					float moveHorizontal = Input.GetAxis ("Horizontal");
					float moveVertical = Input.GetAxis ("Vertical");
					
					Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
					Vector3 jump = new Vector3 (0.0f, 500.0f, 0.0f);
					
					
					if (GetComponent<Rigidbody> ().velocity.magnitude < 200 && (gutterFlag == false)) {
						gutterFlag = false;
						GetComponent<Rigidbody> ().AddForce (movement * 15);
						//GetComponent<Rigidbody> ().maxAngularVelocity = 1000000000.0f;
					}
					
					if (Input.GetKeyDown (KeyCode.Space) && gutterFlag == false && jumpFlag == false) {
						jumpFlag = true;
						GetComponent<Rigidbody> ().AddForce (jump);
					}
					
					for(int i = pins.Count-1; i >= 0; i--){
						if (pins[i].transform.up.y < 0.99f) {
							removePins.Add(pins[i]);
							if (fallen < 10) {
								fallen++;
							}
							pins.RemoveAt(i);
						}
					}

					foreach(GameObject pin in removePins){
						Destroy(pin);
					}
					
				} else {
					gameText.text = "Gutterball! " + ((int)timer).ToString();
					timer -= Time.deltaTime;
					if (timer <= 0) {
						GetComponent<Rigidbody> ().Sleep ();
						GetComponent<Rigidbody> ().MovePosition (originalPosition);
						gutterFlag = false;
						timer = 5.0f;
						GetComponent<Rigidbody> ().WakeUp ();
						gameText.text = "";
					}
				}
			}
			else{
				
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
			loseControlFlag = false;
		}
		
		if (collider.name == "LoseControlTrigger") {
			loseControlFlag = true;
		}
		
		if (collider.name == "PinJumpTrigger") {
			if(pin != null){
				pin.GetComponent<Rigidbody>().AddForce(pinJump);
			}
			if(pin1 != null){
				pin1.GetComponent<Rigidbody>().AddForce(pinJump);
			}
			if(pin2 != null){
				pin2.GetComponent<Rigidbody>().AddForce(pinJump);
			}
			if(pin3 != null){
				pin3.GetComponent<Rigidbody>().AddForce(pinJump);
			}
			if(pin4 != null){
				pin4.GetComponent<Rigidbody>().AddForce(pinJump);
			}
			if(pin5 != null){
				pin5.GetComponent<Rigidbody>().AddForce(pinJump);
			}
			if(pin6 != null){
				pin6.GetComponent<Rigidbody>().AddForce(pinJump);
			}
			if(pin7 != null){
				pin7.GetComponent<Rigidbody>().AddForce(pinJump);
			}
			if(pin8 != null){
				pin8.GetComponent<Rigidbody>().AddForce(pinJump);
			}
			if(pin9 != null){
				pin9.GetComponent<Rigidbody>().AddForce(pinJump);
			}
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
				gameText.text = "";
			}
		}
	}
	
	void OnTriggerExit(Collider collider){
		if (collider.name == "PinsTimerTrigger") {
			loseControlFlag = false;
		}
	}
}
