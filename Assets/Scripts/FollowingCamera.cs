using UnityEngine;

public class FollowingCamera : MonoBehaviour {

	public Transform target;
	private float smoothSpeed = 10f;
	
	private Vector3 offset;

	private void Start() {
		
		offset = target.transform.position - transform.position;
		
	}
	void FixedUpdate ()
	{	
		//Move
		var newPosition = target.transform.position - offset;
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * smoothSpeed);

		//Look
		var direction = target.position - transform.position;
		//var newRotation = Quaternion.LookRotation(direction, Vector3.up);
        //transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, smoothSpeed);
		//transform.LookAt(target);
		transform.Rotate(0f, direction.y, 0f);
		
	}

}