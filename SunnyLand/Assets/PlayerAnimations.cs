using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private CharacterController2D controller2D;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        controller2D = GetComponentInParent<CharacterController2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("speed", Mathf.Abs(controller2D.rb.velocity.x));
        anim.SetBool("jumping", controller2D.isJumping);
        anim.SetBool("falling", controller2D.isFalling);
    }
}
