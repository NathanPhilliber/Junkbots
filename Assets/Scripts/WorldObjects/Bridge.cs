using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour, IInteractable {

	public float length;		//	How long the bridge is
	public GameObject bridge;	//	The actual bridge sub-piece
	public float speed;			//	How fast the bridge moves

	public bool goRight;		//	Which direction the bridge moves in

	public float soundXDiff;

	private int pressedTime = 0;
	private SoundManager sounds;
	private float lastX;
	private Transform cameraPos;
    private RaycastController bridgeRaycaster;

    public LayerMask passengerMask;
    List<PassengerMovement> passengerMovements;
    Dictionary<Transform, Controller2D> passengerControllers = new Dictionary<Transform, Controller2D>();

    void Start () {
		Reload ();
		sounds = Camera.main.GetComponent<SoundManager> ();
		lastX = transform.position.x;
		cameraPos = Camera.main.transform;
	}

	/*
	 * Resizes the sprite size and the collider size according to the bridge's length
	 */
	public void Reload(){
		bridge.GetComponent<BoxCollider2D>().size = new Vector2(length, bridge.GetComponent<BoxCollider2D>().size.y);
		bridge.GetComponent<SpriteRenderer>().size = new Vector2(length, bridge.GetComponent<SpriteRenderer>().size.y);

        bridgeRaycaster = bridge.GetComponentInChildren<RaycastController>();
        bridgeRaycaster.Start();
	}
	

	void FixedUpdate () {
		if (pressedTime > 0) {
			pressedTime--;
		} else {
            bridgeRaycaster.UpdateRaycastOrigins();
            Vector3 velocity = CalculatePlatformMovement();
            CalculatePassengerMovement(velocity);
            MovePassengers(true);
            bridge.transform.Translate(velocity);
            MovePassengers(false);

            UpdateSound ();
		}
	}

    Vector3 CalculatePlatformMovement()
    {
        if (!goRight && bridge.transform.localPosition.x < length / 2)
        {               //	If we need to go right, then move the bridge right
            return new Vector3(speed * Time.deltaTime, 0, 0);
        }
        else if (goRight && bridge.transform.localPosition.x > -length / 2)
        {       //	If we need to go left, then move the bridge left
            return new Vector3(-speed * Time.deltaTime, 0, 0);
        }
        else
        {
            return Vector3.zero;
        }
    }

    void MovePassengers(bool beforeMovePlatform)
    {
        foreach (PassengerMovement passenger in passengerMovements)
        {
            if (!passengerControllers.ContainsKey(passenger.transform))
            {
                passengerControllers.Add(passenger.transform, passenger.transform.GetComponent<Controller2D>());
            }

            if (passenger.moveBeforePlatform == beforeMovePlatform)
            {
                passengerControllers[passenger.transform].Move(passenger.velocity, passenger.standingOnPlatform);
            }
        }
    }

    void CalculatePassengerMovement(Vector3 velocity)
    {
        HashSet<Transform> movedPassengers = new HashSet<Transform>();
        passengerMovements = new List<PassengerMovement>();

        float directionX = Mathf.Sign(velocity.x);
        float directionY = Mathf.Sign(velocity.y);

        // Up and down movement
        if (velocity.y != 0)
        {
            float rayLength = Mathf.Abs(velocity.y) + bridgeRaycaster.skinWidth;

            for (int i = 0; i < bridgeRaycaster.verticalRayCount; i++)
            {
                Vector2 rayOrigin = (directionY == -1) ?
                    bridgeRaycaster.raycastOrigins.bottomLeft : bridgeRaycaster.raycastOrigins.topLeft;

                rayOrigin += Vector2.right * (bridgeRaycaster.verticalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, passengerMask);
                

                if (hit)
                {
                    if (!movedPassengers.Contains(hit.transform))
                    {
                        movedPassengers.Add(hit.transform);

                        float pushX = (directionY == 1) ? velocity.x : 0;
                        float pushY = velocity.y - (hit.distance - bridgeRaycaster.skinWidth) * directionY;

                        passengerMovements.Add(new PassengerMovement(hit.transform, new Vector3(pushX, pushY), directionY == 1, true));
                    }
                }
            }
        }

        // Left and right movement
        if (velocity.x != 0)
        {
            float rayLength = Mathf.Abs(velocity.x) + bridgeRaycaster.skinWidth;

            for (int i = 0; i < bridgeRaycaster.horizontalRayCount; i++)
            {
                Vector2 rayOrigin = (directionX == -1) ?
                    bridgeRaycaster.raycastOrigins.bottomLeft : bridgeRaycaster.raycastOrigins.bottomRight;

                rayOrigin += Vector2.up * (bridgeRaycaster.horizontalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, passengerMask);

                if (hit)
                {
                    if (!movedPassengers.Contains(hit.transform))
                    {
                        movedPassengers.Add(hit.transform);

                        float pushX = velocity.x - (hit.distance - bridgeRaycaster.skinWidth) * directionX;
                        float pushY = -bridgeRaycaster.skinWidth;

                        passengerMovements.Add(new PassengerMovement(hit.transform, new Vector3(pushX, pushY), false, true));
                    }
                }
            }
        }

        // Move player with platform if directly on top
        if (directionY == -1 || velocity.y == 0 && velocity.x != 0)
        {
            float rayLength = 2 * bridgeRaycaster.skinWidth;

            for (int i = 0; i < bridgeRaycaster.verticalRayCount; i++)
            {
                Vector2 rayOrigin = bridgeRaycaster.raycastOrigins.topLeft;

                rayOrigin += Vector2.right * (bridgeRaycaster.verticalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up, rayLength, passengerMask);
                
                Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

                if (hit)
                {
                    if (!movedPassengers.Contains(hit.transform))
                    {
                        movedPassengers.Add(hit.transform);

                        float pushX = velocity.x;
                        float pushY = velocity.y;

                        passengerMovements.Add(new PassengerMovement(hit.transform, new Vector3(pushX, pushY), true, false));
                    }
                }
            }
        }
    }

    /*
	 * This bridge's action is to flip the move state
	 */
    public void TriggerAction(bool toggle){
		//goRight = !goRight;
		pressedTime++;
		Move();

		if (toggle) {
			goRight = !goRight;
		}
	}

	void Move(){
        Vector3 velocity = Vector3.zero;

        if (goRight && bridge.transform.localPosition.x < length/2) {				//	If we need to go right, then move the bridge right
			velocity = new Vector3(speed*Time.deltaTime, 0, 0);
		}
		else if (!goRight && bridge.transform.localPosition.x > -length/2) {		//	If we need to go left, then move the bridge left
			velocity = new Vector3(-speed*Time.deltaTime, 0, 0);
		}

        
        CalculatePassengerMovement(velocity);
        MovePassengers(true);
        bridge.transform.Translate(velocity);
        MovePassengers(false);

        UpdateSound ();
	}

	void UpdateSound(){
		
		if (Mathf.Abs(bridge.transform.position.x - lastX) > soundXDiff){
			if (Vector3.Distance (bridge.transform.position, cameraPos.position) < 100) {
				sounds.PlaySound (11);
			}
			lastX = bridge.transform.position.x;
		}
	}

    struct PassengerMovement
    {
        public Transform transform;
        public Vector3 velocity;
        public bool standingOnPlatform;
        public bool moveBeforePlatform;

        public PassengerMovement(Transform _transform, Vector3 _velocity, bool _standingOnPlatform, bool _moveBeforePlatform)
        {
            transform = _transform;
            velocity = _velocity;
            standingOnPlatform = _standingOnPlatform;
            moveBeforePlatform = _moveBeforePlatform;
        }
    }
}
