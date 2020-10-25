using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	private CharacterController characterController;
	private Vector3 velocity;
	[SerializeField]
	private float walkSpeed = 0;

    private Animator animator;
    private AnimatorStateInfo currentState;		// 現在のステート状態を保存する参照
	private AnimatorStateInfo previousState;	// ひとつ前のステート状態を保存する参照

	[SerializeField]
	private float jumpPower = 0;
	private float currentJumpPower = 0;


	bool canMove {
		get{
			var state = animator.GetCurrentAnimatorStateInfo(0);

			return state.fullPathHash == Animator.StringToHash("Base Layer.Standing@loop")
			    || state.fullPathHash == Animator.StringToHash("Base Layer.Walking@loop")
			    || state.fullPathHash == Animator.StringToHash("Base Layer.Running@loop");
		}
	}


	// Use this for initialization
	void Start () {
		characterController = GetComponent <CharacterController> ();
        animator = GetComponent <Animator> ();
        currentState = animator.GetCurrentAnimatorStateInfo (0);
	}
	
	// Update is called once per frame
    void Update () {
		if(characterController.isGrounded ) {
			currentJumpPower = 0f;
			if( canMove ){
				velocity = new Vector3 (Input.GetAxis ("Horizontal"), 0f, Input.GetAxis ("Vertical"));
 
				if(velocity.magnitude > 0.01f){
					animator.SetFloat("Speed", velocity.magnitude);
					transform.LookAt(transform.position + velocity);
				} else {
					animator.SetFloat("Speed", 0f);
				}

				velocity *= walkSpeed;

				if (Input.GetKeyDown(KeyCode.Space))//  もし、スペースキーがおされたら、
            	{
                	currentJumpPower = jumpPower;//  y座標をジャンプ力の分だけ動かす
					animator.SetTrigger( "JumpTrigger" );
            	}
			}
		}
		else if( animator.GetCurrentAnimatorStateInfo(0).fullPathHash == Animator.StringToHash("Base Layer.OnGround") ){
			velocity.x = 0f;
			velocity.z = 0f;
		}

		//Debug.Log( animator.GetCurrentAnimatorStateInfo(0).fullPathHash != Animator.StringToHash("Base Layer.OnGround") );
		// Debug.Log( animator.GetCurrentAnimatorStateInfo(0).fullPathHash );

		currentJumpPower += Physics.gravity.y * Time.deltaTime;
		velocity.y = currentJumpPower;
		characterController.Move(velocity * Time.deltaTime);

		animator.SetBool("OnGround", characterController.isGrounded );
	}
}