using UnityEngine;
using System.Collections;

public class TestIKScript : MonoBehaviour {

	protected Animator animator;
	
	public bool ikActive = false;
	public Transform rightHandObj;
	public Transform lookObj;
    public GameObject gun;
	
	void Start () 
	{
		animator = GetComponent<Animator>();
        rightHandObj = GameObject.FindGameObjectWithTag("LookAtObj").transform;
        lookObj = GameObject.FindGameObjectWithTag("LookAtObj").transform;
        gun = GameObject.FindGameObjectWithTag("Gun");
	}
	void Update()
	{

	}
	//a callback for calculating IK
	void OnAnimatorIK()
	{
		if(animator) {
			
			//if the IK is active, set the position and rotation directly to the goal. 
			if(ikActive) {
                if (gun != null)
                {
                    gun.GetComponent<LookAtTarget>().lookForTarget = true;
                }
				// Set the look target position, if one has been assigned
				if(lookObj != null) {
					animator.SetLookAtWeight(1);
					animator.SetLookAtPosition(lookObj.position);
				}    
				
				// Set the right hand target position and rotation, if one has been assigned
				if(rightHandObj != null) {
          
					animator.SetIKPositionWeight(AvatarIKGoal.RightHand,1);
					animator.SetIKRotationWeight(AvatarIKGoal.RightHand,1);  
					animator.SetIKPosition(AvatarIKGoal.RightHand,rightHandObj.position);
					animator.SetIKRotation(AvatarIKGoal.RightHand,Quaternion.LookRotation(rightHandObj.position));

				}        
				
			}
			
			//if the IK is not active, set the position and rotation of the hand and head back to the original position
			else {          
				animator.SetIKPositionWeight(AvatarIKGoal.RightHand,0);
				animator.SetIKRotationWeight(AvatarIKGoal.RightHand,0); 
				animator.SetLookAtWeight(0);
                if (gun != null)
                {
                    gun.GetComponent<LookAtTarget>().lookForTarget = false;
                }
			}
		}
	}    
}
