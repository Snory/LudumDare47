using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewJumpAction", menuName = "Action/JumpAction", order = 1)]
public class JumpAction : Action
{

    public float JumpForce;
    private Rigidbody2D _body;
    private Animator _anim;
    private Looper _looper;
    private bool _jump;


    public override void OnActionEnter(Looper looper)
    {

        base.OnActionEnter(looper);
        _body = looper.GetComponent<Rigidbody2D>();
        _anim = looper.GetComponent<Animator>();
        _looper = looper;
        _body.isKinematic = false;

    }

    public override void OnActionExit()
    {
        base.OnActionExit();
        _anim.SetBool("jumpdown", false);
        _anim.SetBool("jumpup", false);

    }

    protected override IEnumerator ActionRoutine(Action triggerAction)
    {
        float lastYVelocity = 0;

        while (triggerAction.IsActionActive())
        {
            if (_looper.IsGrounded)
            {
                _body.velocity = new Vector2(_body.velocity.x, JumpForce);
            }

            if(_body.velocity.y > lastYVelocity)
            {
                _anim.SetBool("jumpup", true);
                _anim.SetBool("jumpdown", false);
            } else if (_body.velocity.y < lastYVelocity)
            {
                _anim.SetBool("jumpdown", true);
                _anim.SetBool("jumpup", false);

            }
            else if (_body.velocity.y == lastYVelocity) {
                _anim.SetBool("jumpdown", false);
                _anim.SetBool("jumpup", false);
                _anim.SetBool("walking", true);
            }

            yield return null;
        }

        OnActionExit();


    }
}
