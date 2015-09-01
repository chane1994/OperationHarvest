using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{
	public Transform Target;
	
	void LateUpdate()
	{
		gameObject.transform.RotateAround(Target.position, Vector3.up, 5 * Time.deltaTime);
	}
}
