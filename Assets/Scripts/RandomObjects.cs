using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjects : MonoBehaviour {
	public Object[] objeto;
	private float minxPos = -8.5f;
	private float minzPos = -7.5f;
	private float maxxPos = 8.5f;
	private float maxzPos = 7.5f;
	private float xPos;
	private float zPos;
	private int position;

	void Start () {
		spawnObjects ();
	}

	void Update () {
	}
		void spawnObjects(){
		position = Random.Range (0, objeto.Length);
		xPos = Random.Range (minxPos, maxxPos);
		zPos = Random.Range (minzPos, maxzPos);
		Vector3 newPos = new Vector3 (xPos, 0.5f, zPos);
		Instantiate (objeto[position], newPos, Quaternion.identity);
			}

}
