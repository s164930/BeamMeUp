using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerSimple : MonoBehaviour
{

    public float moveTime = 0.1f;
    public CapsuleCollider groundCheck;
    public Animator animator;
    private Rigidbody rb;
	public LevelManager levelManager;
    private float inverseTime;

    private float startTime;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inverseTime = 1f / moveTime;
        startTime = Time.time;
		levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }
    void Update()
    {
        int vertical = 0;
        int horizontal = 0;

        horizontal = (int)(Input.GetAxisRaw("Horizontal"));
        vertical = (int)(Input.GetAxisRaw("Vertical"));
		float cameraRightx = Camera.main.transform.right.x;
		float cameraForwardx = Camera.main.transform.forward.x;

		/**
		 * Block of ugly code, that changes the controls based on the camera angle
		 */
		if(cameraRightx >= 0.5 && (cameraForwardx < 0.5 && cameraForwardx > -0.5)){
			// Forward movement, same as camera
			
		} else if(cameraForwardx < -0.3 && (cameraRightx < 0.5 && cameraRightx > -0.5)){
			// Camera is to the right of the player, forward is D
			int placeholder = horizontal;
			horizontal = vertical * -1;
			vertical = placeholder;
		} else if((cameraForwardx < 0.5 && cameraForwardx > -0.5) && cameraRightx < -0.5){
			// Camera is facing the player everything is opposite
			horizontal *= -1;
			vertical *= -1;
		} else if(cameraForwardx > 0.3 && (cameraRightx < 0.5 && cameraRightx > -0.5)){
			// Camera is to the left of the player, forward is A
			int placeholder = horizontal;
			horizontal = vertical;
			vertical = placeholder * -1;
		}

        if (horizontal != 0)
        {
            vertical = 0;
        }
		

        if ((horizontal != 0 || vertical != 0) && Time.time - startTime > 0.5)
        {
            move(horizontal, 0, vertical);
            startTime = Time.time;
        }
    }
    protected IEnumerator SmoothMovement(Vector3 end)
    {
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        while (sqrRemainingDistance > 0.00001)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, end, inverseTime * Time.deltaTime);
            rb.MovePosition(newPosition);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }

        Vector3 cp = transform.position;
        transform.position = new Vector3(Mathf.Round(cp.x), cp.y, Mathf.Round(cp.z));
        animator.SetBool("isWalking", false);
    }
    protected void move(int xDir, int yDir, int zDir)
    {
        animator.SetBool("isWalking", true);
        Vector3 start = transform.position;

        Vector3 end = start + new Vector3(xDir, yDir, zDir);
        Vector3 lookAtThis = start - new Vector3(xDir, yDir, zDir);

        transform.LookAt(lookAtThis);
		

        StartCoroutine(SmoothMovement(end));
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null)
        {
            other.GetComponent<Rigidbody>().isKinematic = false;
            other.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    private float stayTime = 0.0f;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "EndBlock")
        {
            if (stayTime > 0.5f)
            {
				levelManager.ReachedEnd();
				Destroy(gameObject);
            }
            else
            {
                stayTime += Time.deltaTime;
            }
        }

    }

}
