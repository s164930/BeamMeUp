using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerSimple : MonoBehaviour
{

    public float moveTime = 0.1f;
    public CapsuleCollider groundCheck;
    private Rigidbody rb;
    private float inverseTime;

    private float startTime;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inverseTime = 1f / moveTime;
        startTime = Time.time;
    }
    void Update()
    {
        int vertical = 0;
        int horizontal = 0;


        horizontal = (int)(Input.GetAxisRaw("Horizontal"));
        vertical = (int)(Input.GetAxisRaw("Vertical"));

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

        while (sqrRemainingDistance > 0.1)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, end, inverseTime * Time.deltaTime);
            rb.MovePosition(newPosition);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }

        Vector3 cp = transform.position;
        transform.position = new Vector3(Mathf.Round(cp.x), cp.y, Mathf.Round(cp.z));
    }
    protected void move(int xDir, int yDir, int zDir)
    {
        Vector3 start = transform.position;

        Vector3 end = start + new Vector3(xDir, yDir, zDir);

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
				Destroy(gameObject);
            }
            else
            {
                stayTime += Time.deltaTime;
            }
        }

    }

}
