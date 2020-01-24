using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public bool canTripleShot = false;
	public bool speedBoostActive = false;

	[SerializeField]
	private GameObject laserPrefab;
	[SerializeField]
	private GameObject tripleShotPrefab;
	[SerializeField]
	private float fireRate = 0.25f;
	private float nextFire = 0.0f;

	[SerializeField]
	public int lives = 3;
	


	private void Start ()
	{
		transform.position = new Vector3(0f, -4.2f, 0f);
	}
	

	private void Update ()
	{
		Movement();
		Shoot();

		if (lives == 0)
		{
			Destroy(this.gameObject);
		}
	}

	private void Movement()
	{
		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");
		float topBoundary = 0f;
		float bottomBoundary = -4.2f;
		float rightBoundary = 9.44f;
		float leftBoundary = -9.44f;
		float speed = 15f;

		if (speedBoostActive == true)
		{
			speed = speed * 2f;
		}
		else
		{
			speed = speed;
		}
		// Allow control using the WASD or arrow keys
		transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);
		transform.Translate(Vector3.up * speed * verticalInput * Time.deltaTime);

		// Set the ships position to never go past the boundaries
		if (transform.position.y > topBoundary)
		{
			transform.position = new Vector3(transform.position.x, topBoundary, 0f);
		}
		else if (transform.position.y < bottomBoundary)
		{
			transform.position = new Vector3(transform.position.x, bottomBoundary, 0f);
		}

		if (transform.position.x > rightBoundary)
		{
			transform.position = new Vector3(leftBoundary, transform.position.y, 0);
		}
		else if (transform.position.x < leftBoundary)
		{
			transform.position = new Vector3(rightBoundary, transform.position.y, 0);
		}
	}

	private void Shoot()
	{
		// If the space key is pressed or the left mouse button is pressed
		if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
		{
			// If the time from the last shot fired is greater than nextFire check if canTripleShot
			if (Time.time > nextFire)
			{
				if (canTripleShot == true)
				{
					Instantiate(tripleShotPrefab, transform.position, Quaternion.identity);
				}
				else
				{
					Instantiate(laserPrefab, transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
				}
				nextFire = Time.time + fireRate;
			}
		}
	}

	public void TripleShotPowerUpOn()
	{
		canTripleShot = true;

		StartCoroutine(TripleShotPowerDownRoutine());
	}

	public void SpeedBoostPowerUpOn()
	{
		speedBoostActive = true;

		StartCoroutine(SpeedBoostPowerDownRoutine());

	}

	public IEnumerator TripleShotPowerDownRoutine()
	{
		yield return new WaitForSeconds(5.0f);

		canTripleShot = false;
	}

	public IEnumerator SpeedBoostPowerDownRoutine()
	{
		yield return new WaitForSeconds(5.0f);

		speedBoostActive = false;
	}
}
