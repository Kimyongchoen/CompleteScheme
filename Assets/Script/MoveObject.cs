using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public float speed;
    private Vector3 vector;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    private IEnumerator MoveCoroutine()
    {
        vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

        animator.SetFloat("DirX", vector.x);
        animator.SetFloat("DirY", vector.y);
        animator.SetBool("Walking", true);
        
        transform.Translate(vector.x * speed, vector.y * speed , 0);
        
        animator.SetBool("Walking", false);

        yield return new WaitForSeconds(0.01f);
    }
    private void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            animator.SetBool("Walking", true);
            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            transform.Translate(vector.x * speed, vector.y * speed, 0);

        }
        else
        {
            animator.SetBool("Walking", false);
            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);
        }

        //StartCoroutine("MoveCoroutine");
    }
}
