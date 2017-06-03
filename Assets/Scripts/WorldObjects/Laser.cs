using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour, IInteractable {

	public bool north, south, east, west;

	public bool toggleNorthOnTrigger, toggleSouthOnTrigger, toggleEastOnTrigger, toggleWestOnTrigger;

	public GameObject laserBallPrefab;

	public float laserSpeed;

	public SpriteRenderer northSprite, southSprite, eastSprite, westSprite;

	public LayerMask mask;
	public int damage;

	private bool northCur, southCur, eastCur, westCur;
	private bool northStart, southStart, eastStart, westStart;
	private GameObject northBall, southBall, eastBall, westBall;

	private bool lastToggle;

	void Start(){
		northSprite.enabled = false;
		southSprite.enabled = false;
		westSprite.enabled = false;
		eastSprite.enabled = false;

		if (toggleNorthOnTrigger) {
			northSprite.enabled = true;
		}
		if (toggleSouthOnTrigger) {
			southSprite.enabled = true;
		}
		if (toggleWestOnTrigger) {
			westSprite.enabled = true;
		}
		if (toggleEastOnTrigger) {
			eastSprite.enabled = true;
		}

		northStart = north;
		southStart = south;
		eastStart = east;
		westStart = west;
	}

	void Update(){



		if (northCur != north) {
			northCur = north;

			if (northCur) {
				northBall = LaunchLaser (0, 1);
			} else {
				northBall.GetComponent<LaserBallBehavior> ().Deactivate ();
				northBall = null;
			}
		}

		if (southCur != south) {
			southCur = south;

			if (southCur) {
				southBall = LaunchLaser (0, -1);
			} else {
				southBall.GetComponent<LaserBallBehavior> ().Deactivate ();
				southBall = null;
			}
		}

		if (eastCur != east) {
			eastCur = east;

			if (eastCur) {
				eastBall = LaunchLaser (1, 0);
			} else {
				eastBall.GetComponent<LaserBallBehavior> ().Deactivate ();
				eastBall = null;
			}
		}

		if (westCur != west) {
			westCur = west;

			if (westCur) {
				westBall = LaunchLaser (-1, 0);
			} else {
				westBall.GetComponent<LaserBallBehavior> ().Deactivate ();
				westBall = null;
			}
		}

		if (northCur) {
			RaycastHit2D hit = Physics2D.Raycast (transform.position, new Vector2 (0, 1), Mathf.Abs (transform.position.y - northBall.transform.position.y), mask);
			if (hit && hit.collider.gameObject != gameObject && hit.collider.GetComponent<Damageable>()) {
				hit.collider.GetComponent<Damageable> ().DoDamage (damage);
			}
		}
		if (southCur) {
			RaycastHit2D hit = Physics2D.Raycast (transform.position, new Vector2 (0, -1), Mathf.Abs (transform.position.y - southBall.transform.position.y), mask);
			if (hit && hit.collider.gameObject != gameObject && hit.collider.GetComponent<Damageable>()) {
				hit.collider.GetComponent<Damageable> ().DoDamage (damage);
			}
		}
		if (eastCur) {
			RaycastHit2D hit = Physics2D.Raycast (transform.position, new Vector2 (1, 0), Mathf.Abs (transform.position.x - eastBall.transform.position.x), mask);
			if (hit && hit.collider.gameObject != gameObject && hit.collider.GetComponent<Damageable>()) {
				hit.collider.GetComponent<Damageable> ().DoDamage (damage);
			}
		}
		if (westCur) {
			RaycastHit2D hit = Physics2D.Raycast (transform.position, new Vector2 (-1, 0), Mathf.Abs (transform.position.x - westBall.transform.position.x), mask);
			if (hit && hit.collider.gameObject != gameObject && hit.collider.GetComponent<Damageable>()) {
				hit.collider.GetComponent<Damageable> ().DoDamage (damage);
			}
		}

		if(lastToggle == false){
			north = northStart;
			south = southStart;
			west = westStart;
			east = eastStart;
		}

	}

	public void TriggerAction(bool toggle){
		lastToggle = toggle;
		if(toggle == false){

			if (toggleNorthOnTrigger) {
				north = !northStart;
			}
			if (toggleSouthOnTrigger) {
				south = !southStart;
			}
			if (toggleWestOnTrigger) {
				west = !westStart;
			}
			if (toggleEastOnTrigger) {
				east = !eastStart;
			}
		}
		else{
			
			if (toggleNorthOnTrigger) {
				north = !north;
			}
			if (toggleSouthOnTrigger) {
				south = !south;
			}
			if (toggleWestOnTrigger) {
				west = !west;
			}
			if (toggleEastOnTrigger) {
				east = !east;
			}
		}
	}

	private GameObject LaunchLaser(int xDir, int yDir){
		GameObject ball = (GameObject)Instantiate (laserBallPrefab, transform.position, Quaternion.identity);
		ball.GetComponent<LaserBallBehavior> ().xVel = xDir * laserSpeed;
		ball.GetComponent<LaserBallBehavior> ().yVel = yDir * laserSpeed;
		ball.GetComponent<LaserBallBehavior> ().laserBox = gameObject;
		return ball;
	}

}
