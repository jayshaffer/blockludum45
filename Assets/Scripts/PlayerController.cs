using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public LayerMask jumpMask;
    public GameObject sprite;
    public GameObject jumpDetect;
    Rigidbody2D _rb;
    Collider2D boxCollider;
    GameController gameController;
    SpriteRenderer spriteRenderer;
    Animator animator;
    public float unfreezeAt;
    bool frozen = true;
    JumpTrigger jumpTrigger;
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        _rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<CapsuleCollider2D>();
        spriteRenderer = sprite.GetComponent<SpriteRenderer>();
        animator = sprite.GetComponent<Animator>();
        jumpTrigger = jumpDetect.GetComponent<JumpTrigger>();
        Freeze();
    }

    void Update()
    {
        if(frozen){
            return;
        }
        if (Input.GetButtonDown("Jump") && jumpTrigger.onGround)
        {
            _rb.AddForce(transform.up * jumpForce);
        }
    }
    
    public void Freeze(){
        animator.SetBool("isOff", true);
        frozen = true;
    }

    public void Unfreeze(){
        animator.SetBool("isOff", false);
        frozen = false;
    }

    void FixedUpdate()
    {
        if(unfreezeAt != 0 && Time.time > unfreezeAt){
            Unfreeze();
        }
        if(_rb.velocity.x < 0){
            spriteRenderer.flipX = true;
        }
        else{
            spriteRenderer.flipX = false;
        }
        if (frozen)
        {
            return;
        }
        float horizontal = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        _rb.velocity = new Vector3(horizontal, _rb.velocity.y, 0);
        
        float maxX = boxCollider.bounds.max.x;
        float minX = boxCollider.bounds.min.x;
        Debug.DrawRay(new Vector3(maxX, transform.position.y, 0), transform.TransformDirection(Vector3.right) * .1f, Color.yellow);
        Debug.DrawRay(new Vector3(minX, transform.position.y, 0), transform.TransformDirection(Vector3.left) * .1f, Color.yellow);
        if (_rb.velocity.x > 0
            && Physics2D.Raycast(new Vector3(maxX, transform.position.y, 0), transform.TransformDirection(Vector3.right), .1f, jumpMask))
        {
            _rb.velocity = new Vector3(0, _rb.velocity.y, 0);
        }
        if (_rb.velocity.x < 0
            && Physics2D.Raycast(new Vector3(minX, transform.position.y, 0), transform.TransformDirection(Vector3.left), .1f, jumpMask))
        {
            _rb.velocity = new Vector3(0, _rb.velocity.y, 0);
        }
    }

    void OnDrawGizmos()
    {

    }
}
