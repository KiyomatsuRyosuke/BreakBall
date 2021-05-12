using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoTuoch : MonoBehaviour
{
    public Rigidbody rb;

    private Vector3 moveVec = Vector3.zero;
    private Vector3 nowPos = Vector3.zero;
    private float moveLow = 0.3f;
    private float startPos = 25.0f;
    private float returnPos = -25.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();

        float rand = Random.value;
        if(rand <= moveLow)
        {
            rand = moveLow;
        }
        rand *= 50;
        moveVec.x = rand;
    }

    // Update is called once per frame
    void Update()
    {
        this.rb.velocity = moveVec;

        if(transform.position.x < returnPos)
        {
            nowPos = transform.position;
            nowPos.x = returnPos;
            transform.position = nowPos;

            moveVec.x *= -1;
        }
        if(transform.position.x > startPos)
        {
            nowPos = transform.position;
            nowPos.x = startPos;
            transform.position = nowPos;

            moveVec *= -1;
        }
    }
}
