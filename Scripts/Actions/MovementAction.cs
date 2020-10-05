using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewMovementAction", menuName = "Action/MovementAction", order = 1)]
public class MovementAction : Action
{


    public Vector2 Direction;
    [Range(0,10)]
    public float Speed;
    private Rigidbody2D _body;
    private Animator _anim;
    private Looper _looper;
    public bool FlyingMode = false;
    public bool PatrolMovement = false;

    [Range(1,10)]
    public float PatrolDistance;

    public override void OnActionEnter(Looper looper)
    {
         base.OnActionEnter(looper);
        _body = looper.GetComponent<Rigidbody2D>();
        _anim = looper.GetComponent<Animator>();
        _looper = looper;

        if (FlyingMode) { 
            _body.isKinematic = true;
        } else
        {
            _body.isKinematic = false;
        }

        if (PatrolMovement)
        {
            if(_body.velocity.x != 0)
            {
                Direction = _body.velocity;
            }
        }

        _anim.SetBool("walking", true);
    }

    public override void OnActionExit()
    {
        base.OnActionExit();
        _anim.SetBool("walking", false);
    }

    protected override IEnumerator ActionRoutine(Action triggerAction)
    {
        float lastYVelocity = 0;
        float lastXPosition = _looper.transform.position.x;
        
        while (triggerAction.IsActionActive())
        {
            if (PatrolMovement)
            {
                float distanceFromlastPosition = Mathf.Abs(_looper.transform.position.x - lastXPosition);
                if (distanceFromlastPosition > PatrolDistance)
                {
                    Direction.x *= -1;
                    lastXPosition = _looper.transform.position.x;
                }
            }
            
            
            if(Direction.x != 0)
            {
                _body.velocity = new Vector2(Direction.x * Speed, _body.velocity.y);

            } else if (Direction.y != 0)
            {
                _body.velocity = new Vector2(_body.velocity.x, Direction.y * Speed);
            } else if (Direction.x == 0 &&Direction.y == 0)
            {
                if(_body.isKinematic) _body.velocity = new Vector2(0, 0);
            }

            

            ChangeSpriteDirection();

            if (_body.velocity.y > lastYVelocity)
            {
                _anim.SetBool("jumpup", true);
                _anim.SetBool("jumpdown", false);
            }
            else if (_body.velocity.y < lastYVelocity)
            {
                _anim.SetBool("jumpdown", true);
                _anim.SetBool("jumpup", false);
            }
            else if (_body.velocity.y == lastYVelocity)
            {
                _anim.SetBool("jumpdown", false);
                _anim.SetBool("jumpup", false);
                _anim.SetBool("walking", true);
            }

            yield return null;

        }

        OnActionExit();
        
    }


    private void ChangeSpriteDirection()
    {
        Vector3 localScale = _looper.transform.localScale;


        if(_body.velocity.x != 0) { 
            localScale.x = (_body.velocity.x / Mathf.Abs(_body.velocity.x)) * Mathf.Abs(localScale.x);
            _looper.transform.localScale = localScale;
        }



    }
}
