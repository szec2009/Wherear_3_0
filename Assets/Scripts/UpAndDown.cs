using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAndDown : MonoBehaviour {



	/// <summary>
	/// The time taken to move from the start to finish positions
	/// </summary>
	public float timeTakenDuringLerp = 1f;

	/// <summary>
	/// How far the object should move when 'space' is pressed
	/// </summary>
	public Vector3 distanceToMove;
	public Vector3 RotationToMove;

	//Whether we are currently interpolating or not
	public bool _isLerping;
	public bool _isLerpingBack;

	public bool _enableLerpingBack=true;
	public bool _enableRotation = false;
	public bool Stop;

	//The start and finish positions for the interpolation
	public Vector3 _startPosition;
	public Vector3 _endPosition;
	public Quaternion _startRotation;
	public Quaternion _endRotation;

	//The Time.time value when we started the interpolation
	private float _timeStartedLerping;

	public UpAndDown nextObject;

	public List<UpAndDown> AllObject;


	void Start(){
		_startPosition = this.gameObject.transform.localPosition;
		_endPosition = new Vector3(_startPosition.x+distanceToMove.x,_startPosition.y+distanceToMove.y,_startPosition.z+distanceToMove.z);
		_startRotation = this.gameObject.transform.localRotation;
		_endRotation = Quaternion.Euler(new Vector3(RotationToMove.x,RotationToMove.y,RotationToMove.z));
	}



	public void StopAllmoving(){
		Stop = true;
		AllObject.ForEach (x=>x.Stop=true);

	}

	/// <summary>
	/// Called to begin the linear interpolation
	/// </summary>
	public void StartLerping()
	{
		
		if (Stop == true) {
			//We set the start position to the current position, and the finish to 10 spaces in the 'forward' direction
			//_startPosition = this.gameObject.transform.localPosition;
			//_endPosition = new Vector3(_startPosition.x+distanceToMove.x,_startPosition.y+distanceToMove.y,_startPosition.z+distanceToMove.z);
			Stop=false;
			_isLerping = true;
			_timeStartedLerping = Time.time;
		} else {
			Stop = true;
			AllObject.ForEach (x=>x.Stop=true);
		}
	}


	/// <summary>
	/// Called to begin the linear interpolation
	/// </summary>
	public void BackLerping()
	{


		//We set the start position to the current position, and the finish to 10 spaces in the 'forward' direction
		//_startPosition = this.gameObject.transform.localPosition;
		//_endPosition = new Vector3(_startPosition.x+distanceToMove.x,_startPosition.y+distanceToMove.y,_startPosition.z+distanceToMove.z);

		_isLerpingBack = true;
		_timeStartedLerping = Time.time;
	}


	//We do the actual interpolation in FixedUpdate(), since we're dealing with a rigidbody




	void Update() {
//		//get the objects current position and put it in a variable so we can access it later with less code
//		Vector3 pos = transform.localPosition;
//		//calculate what the new Y position will be
//		float newY = Mathf.Sin(Time.time * speed* height);
//		//set the object's Y to the new calculated Y
//		transform.localPosition = new Vector3(pos.x, newY, pos.z) ;
	}


	//We do the actual interpolation in FixedUpdate(), since we're dealing with a rigidbody
	void FixedUpdate()
	{
		

		if(_isLerping)
		{
			//We want percentage = 0.0 when Time.time = _timeStartedLerping
			//and percentage = 1.0 when Time.time = _timeStartedLerping + timeTakenDuringLerp
			//In other words, we want to know what percentage of "timeTakenDuringLerp" the value
			//"Time.time - _timeStartedLerping" is.
			float timeSinceStarted = Time.time - _timeStartedLerping;
			float percentageComplete = timeSinceStarted / timeTakenDuringLerp;
			//Debug.Log (percentageComplete);
			//Perform the actual lerping.  Notice that the first two parameters will always be the same
			//throughout a single lerp-processs (ie. they won't change until we hit the space-bar again
			//to start another lerp)
			transform.localPosition = Vector3.Lerp (_startPosition, _endPosition, percentageComplete);

			if(_enableRotation)
			transform.localRotation = Quaternion.Lerp(_startRotation, _endRotation, percentageComplete);

			//When we've completed the lerp, we set _isLerping to false
			if(percentageComplete >= 1.0f)
			{
				_isLerping = false;

				if(_enableLerpingBack)
					BackLerping ();

			}
		}


		if(_isLerpingBack)
		{
			//We want percentage = 0.0 when Time.time = _timeStartedLerping
			//and percentage = 1.0 when Time.time = _timeStartedLerping + timeTakenDuringLerp
			//In other words, we want to know what percentage of "timeTakenDuringLerp" the value
			//"Time.time - _timeStartedLerping" is.
			float timeSinceStarted = Time.time - _timeStartedLerping;
			float percentageComplete = timeSinceStarted / timeTakenDuringLerp;
			//Debug.Log (percentageComplete);
			//Perform the actual lerping.  Notice that the first two parameters will always be the same
			//throughout a single lerp-processs (ie. they won't change until we hit the space-bar again
			//to start another lerp)
			transform.localPosition = Vector3.Lerp (_endPosition,_startPosition, percentageComplete);

			if(_enableRotation)
			transform.localRotation = Quaternion.Lerp(_endRotation, _startRotation, percentageComplete);

			//When we've completed the lerp, we set _isLerping to false
			if(percentageComplete >= 1.0f)
			{
				_isLerpingBack = false;

				if (Stop == true) {

					_isLerping = false;
					_isLerpingBack = false;
					transform.localPosition = _startPosition;

				} else {
					
					if (nextObject != null) {
						nextObject.Stop = true;
						nextObject.StartLerping ();

					}
				}
			}
		}



	}
}
