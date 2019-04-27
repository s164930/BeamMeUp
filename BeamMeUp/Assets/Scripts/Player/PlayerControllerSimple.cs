using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerSimple : MonoBehaviour
{

    public float moveTime = 0.1f;
    public CapsuleCollider groundCheck;
    public Animator animator;
    public GameObject cube;
    private Rigidbody rb;
	public LevelManager levelManager;
    private float inverseTime;
    private Stack<Vector3> prevPos;
    private Stack<Vector3> prevCube;

    private float startTime;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inverseTime = 1f / moveTime;
        startTime = Time.time;
		levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        prevPos = new Stack<Vector3>();
        prevCube = new Stack<Vector3>();
    }
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            RedoAction();
        }
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

        while (sqrRemainingDistance > 0.001)
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
        prevPos.Push(start);

        Vector3 end = start + new Vector3(xDir, yDir, zDir);
        Vector3 lookAtThis = start - new Vector3(xDir, yDir, zDir);

        transform.LookAt(lookAtThis);
		

        StartCoroutine(SmoothMovement(end));
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "EndBlock")
        {
            other.gameObject.GetComponent<EndBlock>().Explode();
        }
    }
    private bool enableGravity = true;
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null)
        {
            prevCube.Push(other.transform.position);
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

    public void RedoAction()
    {
        groundCheck.enabled = false;
        if (prevCube.Count != 0)
        {
            Instantiate(cube, prevCube.Pop(), cube.transform.rotation);
        }
        transform.position = prevPos.Pop();
        groundCheck.enabled = true;
    }

}
