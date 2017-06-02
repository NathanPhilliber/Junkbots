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
	private GameObject northBall, southBall, eastBall, westBall;

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
			if (hit) {
				hit.collider.GetComponent<Damageable> ().DoDamage (damage);
			}
		}
		if (southCur) {
			RaycastHit2D hit = Physics2D.Raycast (transform.position, new Vector2 (0, -1), Mathf.Abs (transform.position.y - northBall.transform.position.y), mask);
			if (hit) {
				hit.collider.GetComponent<Damageable> ().DoDamage (damage);
			}
		}
		if (eastCur) {
			RaycastHit2D hit = Physics2D.Raycast (transform.position, new Vector2 (1, 0), Mathf.Abs (transform.position.x - northBall.transform.position.x), mask);
			if (hit) {
				hit.collider.GetComponent<Damageable> ().DoDamage (damage);
			}
		}
		if (westCur) {
			RaycastHit2D hit = Physics2D.Raycast (transform.position, new Vector2 (-1, 0), Mathf.Abs (transform.position.x - northBall.transform.position.x), mask);
			if (hit) {
				hit.collider.GetComponent<Damageable> ().DoDamage (damage);
			}
		}
	}

	public void TriggerAction(){
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

	private GameObject LaunchLaser(int xDir, int yDir){
		GameObject ball = (GameObject)Instantiate (laserBallPrefab, transform.position, Quaternion.identity);
		ball.GetComponent<LaserBallBehavior> ().xVel = xDir * laserSpeed;
		ball.GetComponent<LaserBallBehavior> ().yVel = yDir * laserSpeed;
		return ball;
	}

}
