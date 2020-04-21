using UnityEngine;

public class follow_player : MonoBehaviour {

	public Transform player;
	public float smooth = 3f;
	public float smoothSpeed = 0.650f;
	public float rotationOffsetX = 11.2f;
	public Vector3 Offset;

	void FixedUpdate ()
	{
		Vector3 position = player.position + Offset;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, position, smoothSpeed);
		transform.position = smoothedPosition;

		float rotationAdapt = rotationOffsetX + transform.rotation.x - player.GetComponent<Rigidbody>().velocity.y/2;
		Quaternion target = Quaternion.Euler(rotationAdapt, 0, 0);
		transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * smooth);

	}
}
