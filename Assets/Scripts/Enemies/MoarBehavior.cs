using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoarBehavior : MonoBehaviour {

	public Bridge protectionBridge;
	public Laser roomCloseLaser, floorLaser; 
	public GameObject pump1, pump2, pump3, wall;
	public LayerMask mask;

	public GameObject enemyBomber, enemyDrone;

	private int state = 0;
	private int counter = 0;
	private bool playerInside = false;

	private int deadPumps = 0;
	private bool pump1State = true, pump2State = true, pump3State = true;

	public void FixedUpdate(){

		if (playerInside) {
			StateUpdate ();
		}

		if (pump1State && pump1 == null) {
			pump1State = false;
			deadPumps++;
		}
		if (pump2State && pump2 == null) {
			pump2State = false;
			deadPumps++;
		}
		if (pump3State && pump3 == null) {
			pump3State = false;
			deadPumps++;
		}


	}

	private void KillSelf(){
		Destroy (wall);
		Destroy (gameObject);
	}

	private void StateUpdate(){
		counter++;

		if (deadPumps == 3) {
			if (floorLaser.west) {
				floorLaser.TriggerAction (true);
			}
			protectionBridge.goRight = false;
			KillSelf();
			return;
		}

		if (state == 0) {
			protectionBridge.goRight = false;
			counter = 0;
			state++;
		} else if (state == 1) {
			if (counter >= 150) {

				if (deadPumps == 0) {
					Instantiate (enemyDrone, transform.position - new Vector3 (35, 0, 0), Quaternion.identity);
				}
				else if (deadPumps == 1) {
					Instantiate (enemyBomber, transform.position - new Vector3 (35, 0, 0), Quaternion.identity);
				}
				else if (deadPumps == 2) {
					Instantiate (enemyBomber, transform.position - new Vector3 (35, 0, 0), Quaternion.identity);
					Instantiate (enemyDrone, transform.position - new Vector3 (27, 5, 0), Quaternion.identity);
				}


				counter = 0;
				state++;
			}
		} else if (state == 2) {
			if (counter >= 250) {

				protectionBridge.goRight = true;

				counter = 0;
				state++;
			}
		} else if (state == 3) {
			if (counter == 100) {
				floorLaser.TriggerAction (true);
			}
			if (counter == 300) {
				floorLaser.TriggerAction (true);
				protectionBridge.goRight = false;
				counter = 0;
				state = 0;
			}
		}

	}

	void OnTriggerEnter2D(Collider2D other){

		if (!playerInside) {
			if (mask == (mask | (1 << other.gameObject.layer))) {
				playerInside = true;
				roomCloseLaser.TriggerAction (true);
			}
		}
	}

}
