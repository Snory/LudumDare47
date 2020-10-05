using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{



    private float _cameraSpeed = 0.3f;
    [SerializeField]
    private float _offsetZ, _offsetX;

    public float MinimumX;
    public float MaximumX;

    [SerializeField]
    private Transform _target;
    private Rigidbody2D _body;

    private Vector3 _aheadposition, _backwardposition;
    private Vector3 _currentVelocity;




    // Start is called before the first frame update
    void Start()
    {

        _offsetZ = (transform.position - _target.position).z;

        _body = _target.GetComponentInChildren<Rigidbody2D>();
        MinimumX = 0;


    }

    private void FixedUpdate()
    {
      
        _aheadposition = _target.position + Vector3.right * _offsetX;
        _backwardposition = _target.position + Vector3.left * _offsetX;


        if(_body.velocity.x != 0) { 
            if (_aheadposition.x >= transform.position.x && _body.velocity.x > 0 && transform.position.x < MaximumX)
            {

                Vector3 newCameraPosition = Vector3.SmoothDamp(transform.position, _aheadposition, ref _currentVelocity, _cameraSpeed);

                transform.position = new Vector3(newCameraPosition.x, this.transform.position.y, _offsetZ);

            }

            if (_backwardposition.x <= transform.position.x && _body.velocity.x < 0 && transform.position.x > MinimumX)
            {

                Vector3 newCameraPosition = Vector3.SmoothDamp(transform.position, _backwardposition, ref _currentVelocity, _cameraSpeed);

                transform.position = new Vector3(newCameraPosition.x, this.transform.position.y, _offsetZ);

            }
        }


    }
}
