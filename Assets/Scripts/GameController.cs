using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent (typeof(AudioSource))]
public class GameController : MonoBehaviour
{
	private Vector3 startPos = new Vector3 (0.0f, 0.0f, 0.0f);
	private Vector3 endPos = new Vector3 (0.0f, 0.0f, 0.0f);
	public float velocity = 1f;
	private Vector3 direction;
	private bool victory = false;
	private bool isThrown;
	private Rigidbody rb;
	private float y;
	private float z;
	private float Timeleft;
	private float round;
	private static int count = 1;
	public Text timeText;
	public Text levelText;
	public static int seconds;

	public Object[] objeto;
	private float minxPos = -8.5f;
	private float minzPos = -7.5f;
	private float maxxPos = 8.5f;
	private float maxzPos = 7.5f;
	private float xPos;
	private float zPos;
	private int position;
	private int numObs;

	private AudioSource audioSrc;
	public AudioClip bounce;
	void Start ()
	{
		numObs = count*2;
		int contador = 0;
		if (numObs > 80) {
			numObs = 80;
		}
		while (contador < numObs) {
			spawnObjects ();
			contador++;
		}
		audioSrc = GetComponent<AudioSource>();
		Timeleft = 15 + seconds;
		isThrown = false;
		rb = GetComponent<Rigidbody> ();
		y = rb.transform.position.y;
		SetTimeText ();
		SetLevelText ();
	}

	void Update ()
	{
		Timeleft -= Time.deltaTime;
		SetTimeText ();
		if (Timeleft < 0) {
			count = 1;
			victory = false;
			seconds = 0;
			RestartGame ();
		}
		if (isThrown) {
			 
			if (rb.velocity.magnitude < 3.0f) {
				rb.velocity = Vector3.zero;
				rb.angularVelocity = Vector3.zero;
				isThrown = false;
			}
		} else {
			if (Input.GetMouseButtonDown (0)) {
				Debug.Log (startPos);
				startPos = Input.mousePosition;
				startPos.z = startPos.y;
				startPos.y = 0f;
				Debug.Log (startPos);
			}
			if (Input.GetMouseButtonUp (0)) {
				endPos = Input.mousePosition;
				endPos.z = endPos.y;
				endPos.y = 0f;
				Debug.Log (endPos);
				direction = endPos - startPos;
				float distance = direction.sqrMagnitude;

				Vector3 throwDir = new Vector3 (direction.x, 0.0f, direction.z);
				rb.AddForce (throwDir * velocity * 1.25f);
				isThrown = true;
			}
		}
	}
	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.CompareTag ("VictoryHole")) {
		} else {
			
			audioSrc.PlayOneShot (bounce);
		}
	}

	void spawnObjects(){
		position = Random.Range (0, objeto.Length);
		xPos = Random.Range (minxPos, maxxPos);
		zPos = Random.Range (minzPos, maxzPos);
		Vector3 newPos = new Vector3 (xPos, 0.5f, zPos);
		Instantiate (objeto[position], newPos, Quaternion.identity);
	}


	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("VictoryHole")) {
			other.gameObject.SetActive (false);
			victory = true;
			win ();
		}
	}

	void win ()
	{
		if (victory) {
			seconds = (int) Timeleft;
			RestartGame ();
			count = count + 1;
		}
	}

	void SetTimeText ()
	{
		round = Mathf.Round (Timeleft);
		timeText.text = "TIME: " + round.ToString ();
	}

	void SetLevelText ()
	{
		levelText.text = "LEVEL: " + count.ToString ();
	}

	public void RestartGame ()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
}

