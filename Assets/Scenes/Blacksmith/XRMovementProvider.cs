using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class XRMovementProvider : LocomotionProvider
{
    public XRController _moveController;
    public float _moveSpeed = 1.0f;
    public float _gravityMultiplier = 1.0f;

    private CharacterController _characterController;
    private GameObject _head;
    protected override void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _head = GetComponent<XRRig>().cameraGameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        PositionCharacterController();
    }

    // Update is called once per frame
    void Update()
    {
        PositionCharacterController();
        
        MoveCharacter();
        
        ApplyGravity();
    }

    private void PositionCharacterController()
    {
        float headHeight = Mathf.Clamp(_head.transform.localPosition.y, 1, 2);
        _characterController.height = headHeight;

        Vector3 newCharCenter = new Vector3();
        newCharCenter.y = headHeight / 2;
        newCharCenter.y += _characterController.skinWidth;

        newCharCenter.x = _characterController.center.x;
        newCharCenter.z = _characterController.center.z;

        _characterController.center = newCharCenter;
    }

    private void MoveCharacter()
    {
        if (_moveController.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 axis))
        {
            Vector3 direction = new Vector3(axis.x, 0, axis.y);
            Vector3 rotation = new Vector3(0, _head.transform.eulerAngles.y, 0);

            Vector3 towards = Quaternion.Euler(rotation) * direction;

            Vector3 move = direction * _moveSpeed * Time.deltaTime;
            _characterController.Move(move);
        }
    }

    private void ApplyGravity()
    {
        if (!_characterController.isGrounded)
        {
            Vector3 gravity = Vector3.zero;
            gravity += Time.deltaTime * _gravityMultiplier * Physics.gravity * Time.deltaTime;
            _characterController.Move(gravity);
        }
    }
}
