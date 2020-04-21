using UnityEngine;

public class player_collision : MonoBehaviour {

	void OnCollisionEnter(Collision collisionInfo)
	{
		if(collisionInfo.collider.tag == "Platform_CP")
		{
			FindObjectOfType<GameManager>().setCheckpoint(collisionInfo.transform.position);
		}

		if(collisionInfo.collider.tag == "Platform_END")
		{
			FindObjectOfType<GameManager>().levelComplete();
		}
	}
}
