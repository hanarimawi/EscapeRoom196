using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserController : MonoBehaviour {

	[SerializeField]
	GameObject sword;

	[SerializeField]
	GameObject key;

	[SerializeField]
	GameObject letter;

	[SerializeField]
	Text letterText;

	[SerializeField]
	Text gameText;

	private bool haveLetter = false;
	private bool getfree = false;

	private float X_MIN = -3f;
	private float X_MAX = 3f;

	private float Y_MIN = 0f;
	private float Y_MAX = 10f;

	private float Z_MIN = -3.5f;
	private float Z_MAX = 3f;

	private float speed = 1.2f;
	private float rotation_speed = 13f;

	// Use this for initialization
	void Start () {

		gameText.text = "Free yourself!";
	}

	// Update is called once per frame
	void Update () {
		var movement = Vector3.zero;
		float rotation = 0f;

		if (Input.GetKey("w"))
			movement.x--;
		if (Input.GetKey("s"))
			movement.x++;
		if (Input.GetKey("a"))
			movement.z--;
		if (Input.GetKey("d"))
			movement.z++;

		if (Input.GetKey ("q"))
			rotation = 1f; //rotate up and down, need left and right
		if (Input.GetKey ("e"))
			rotation = -1f;

		transform.localPosition = new Vector3(
			transform.localPosition.x + (movement.x * speed * Time.deltaTime), 
			transform.localPosition.y, 
			transform.localPosition.z + (movement.z * speed * Time.deltaTime)
		);

		transform.Rotate (new Vector3 (rotation * rotation_speed * Time.deltaTime, 0));

		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, X_MIN, X_MAX),
			Mathf.Clamp(transform.position.y, Y_MIN, Y_MAX),
			Mathf.Clamp(transform.position.z, Z_MIN, Z_MAX));

		Vector3 userPos = new Vector3 (transform.localPosition.x, 0, transform.localPosition.z);
		Vector3 swordPos = new Vector3 (sword.transform.localPosition.x, 0, sword.transform.localPosition.z);
		if (Vector3.Distance (userPos, swordPos) < 1) {
			gameText.text = "You are free!";
			getfree = true;
		}
		Vector3 keyPos = new Vector3 (key.transform.localPosition.x, 0, key.transform.localPosition.z);
		if (Vector3.Distance (userPos, keyPos) < 1 && getfree == true) {
			gameText.text = "You get a key and a letter!";
			haveLetter = true;
		}

		Vector3 letterPos = new Vector3 (letter.transform.localPosition.x, 0, letter.transform.localPosition.z);
		if (Vector3.Distance (userPos, letterPos) < 1 && getfree == true) {
			letterText.text = "<i>Notification</i>: <color=#FFFFFFEA>You just get a letter from your loyal CabinBoy, Lloyd." +
				"\n\nDear Captain:" +
				"\n\tI am sorry that I cannot save you out when the mutiny happened.I left this letter along with the key to offer you some help." +
				"This is the key to the lock of the jail cell. In addition, here is the steps about how to build a cannon." +
				"\n\t\t1. You need a cannon ball." +
				"\n\t\t2. You need gunpowder." +
				"\n\nBest regards." +
				"\nLloyd</color>";
		}


		if (Vector3.Distance (userPos, letterPos) > 1 && haveLetter == true) {
			letterText.text = "<i>Notification</i>:<color=#FFFFFFEA> You now have: a key, a letter.</color>" +
				"\n<i>What you need</i>:<color=#FFFFFFEA> gunpowder, cannonball.</color>";
		}


	}


}

 