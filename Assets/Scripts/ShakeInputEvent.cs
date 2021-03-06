using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://stackoverflow.com/questions/31389598/how-can-i-detect-a-shake-motion-on-a-mobile-device-using-unity3d-c-sharp

public class ShakeInputEvent : MonoBehaviour
{
	public delegate void onShakeInput();
	/// when the phone is shaken
	public static event onShakeInput OnShakeInput;
	public static void InvokeShakeInputEvent() { if (OnShakeInput != null) { OnShakeInput(); } }

	float accelerometerUpdateInterval = 1.0f / 60.0f;
	// The greater the value of LowPassKernelWidthInSeconds, the slower the
	// filtered value will converge towards current input sample (and vice versa).
	float lowPassKernelWidthInSeconds = 1.0f;
	// This next parameter is initialized to 2.0 per Apple's recommendation,
	// or at least according to Brady! ;)
	float shakeDetectionThreshold = 2.0f;

	float lowPassFilterFactor;
	Vector3 lowPassValue;

	//ignore shakes that happen within 2 seconds
	float ShakeTimeLimit = 2;
	float NextShakeTime = 0;

	void Start()
	{
		lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
		shakeDetectionThreshold *= shakeDetectionThreshold;
		lowPassValue = Input.acceleration;
	}

	void Update()
	{
		if (Time.time < NextShakeTime){return;}

		Vector3 acceleration = Input.acceleration;
		lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
		Vector3 deltaAcceleration = acceleration - lowPassValue;

		if (deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold)
		{
			// Perform your "shaking actions" here. If necessary, add suitable
			// guards in the if check above to avoid redundant handling during
			// the same shake (e.g. a minimum refractory period).
			Debug.Log("Shake event detected at time "+Time.time);
			NextShakeTime = Time.time + ShakeTimeLimit;
			InvokeShakeInputEvent();
		}
	}
}
