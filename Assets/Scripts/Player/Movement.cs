using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public Animator animator;

    private Vector3 direction;

    //get input from player
    //apply movement to sprite

    private void Update()
    {
        Vector3 Direction = new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"),0.0f);

         Direction.x = Input.GetAxisRaw("Horizontal");
         Direction.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", Direction.x);
        animator.SetFloat("Vertical", Direction.y);
        animator.SetFloat("Magnitude", Direction.magnitude);

        direction = new Vector3(Direction.x, Direction.y, 0);


        ///transform.position += Direction * speed * Time.deltaTime;
        //transform.position = transform.position + Direction * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        //move the player
        this.transform.position += direction.normalized * speed * Time.deltaTime;
    }
}
