using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_controls : MonoBehaviour {

	public Rigidbody Rb;
	public float forwardForce = 2000f;
	public float hSpeed = 500f;
	public float jumpAmount = 200f;
	public float speedLimit = 100f;

	void FixedUpdate ()
	{
		if(Rb.velocity.y > 8.5f){
			Rb.constraints = RigidbodyConstraints.FreezePositionY;
			Rb.constraints = RigidbodyConstraints.None;
		}
			//Player inputs
			if(Input.GetKey("r")){
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
			if(Input.GetKey(KeyCode.Escape)){
				SceneManager.LoadScene("main_menu");
			}
			//L/R
			if(Rb.velocity.x < speedLimit){
				if(Input.GetKey("d"))
				{
					Rb.AddForce(hSpeed * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
				}
				if(Input.GetKey("q"))
				{
					Rb.AddForce(- hSpeed * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
				}
			}

			//F/B
			if(Rb.velocity.z < speedLimit){
				if(Input.GetKey("z"))
				{
					Rb.AddForce(0, 0, forwardForce);
				}
				if(Input.GetKey("s"))
				{
					Rb.AddForce(0, 0, -forwardForce);
				}
			}

			//JUMP
			if(Input.GetKey(KeyCode.Space)){
				if(Rb.velocity.y > -0.01 && Rb.velocity.y < 0.01){
					Rb.AddForce(0, jumpAmount, 0);
				}
			}



/*
			if(Rb.position.y < -0f)
			{
				FindObjectOfType<GameManager>().endGame();
			}*/
	}
}
