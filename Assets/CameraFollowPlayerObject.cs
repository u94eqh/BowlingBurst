using UnityEngine;
using System.Collections;

public class CameraFollowPlayerObject : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;
	public bool gutterFlag = false;
	// Use this for initialization
	void Start () {
		offset = transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (gutterFlag == false) {
			transform.position = player.transform.position + offset;
		} else {
			transform.position = transform.position;
		}
	}
}
