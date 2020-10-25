using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour
{

    public bool isGUI = true;

    public Rigidbody jumpObject = null;
    readonly float jumpForce = 390.0f; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnGUI ()
	{
        if (isGUI)
        {
            //GUI.Box(new Rect(Screen.width - 110, 10, 100, 90), "Jump Motion");
            if (GUI.Button(new Rect(Screen.width - 100, Screen.height - 40, 80, 20), "Jump"))
                if( jumpObject && jumpObject.velocity.y == 0f ) jumpObject.AddForce( transform.up * this.jumpForce );
        }
	}
}
