using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Light))]

public class SoftFlicker : MonoBehaviour {

	public int speed=6;
	float random;
	public Light light;
	public float minIntensity = 0.1f;
	public float maxIntensity = 1.5f;

	// Use this for initialization
	void Start()
	{
		random = Random.Range(0.0f, 65535.0f);
	}
	
	// Update is called once per frame
	void Update()
	{
		float noise = Mathf.PerlinNoise(random, Time.time*speed);
		light.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
	}
}
