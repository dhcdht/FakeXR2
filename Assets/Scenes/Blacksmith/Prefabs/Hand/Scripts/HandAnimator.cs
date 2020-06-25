using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class HandAnimator : MonoBehaviour
{
    public float _fingerSpeed = 5.0f;
    public XRController _xrController;

    private List<Finger> _gripFingers = new List<Finger>()
    {
        new Finger(FingerType.Middle),
        new Finger(FingerType.Ring),
        new Finger(FingerType.Pinky),
    };
    private List<Finger> _trigerFingers = new List<Finger>()
    {
        new Finger(FingerType.Thumb),
        new Finger(FingerType.Index),
    };

    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckTriger();
        CheckGrip();

        AnimateFinger(_trigerFingers);
        AnimateFinger(_gripFingers);
    }
    
    private void CheckTriger()
    {
        if (_xrController.inputDevice.TryGetFeatureValue(CommonUsages.trigger, out float trigerValue))
        {
            SetFingersTargetValue(_trigerFingers, trigerValue);
        }
    }

    private void CheckGrip()
    {
        if (_xrController.inputDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            SetFingersTargetValue(_gripFingers, gripValue);
        }
    }

    private void SetFingersTargetValue(List<Finger> fingers, float target)
    {
        foreach (Finger finger in fingers)
        {
            finger._target = target;
        }
    }

    private void AnimateFinger(List<Finger> fingers)
    {
        foreach (Finger finger in fingers)
        {
            float time = _fingerSpeed * Time.unscaledTime;
            finger._current = Mathf.MoveTowards(finger._current, finger._target, time);
            
            _animator.SetFloat(finger._type.ToString(), finger._current);
        }
    }
}