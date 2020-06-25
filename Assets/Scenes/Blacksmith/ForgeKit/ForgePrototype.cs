using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Random = UnityEngine.Random;

public class ForgePrototype : MonoBehaviour
{
    public int _forgePoint = 150;
    public GameObject _forgeIngot;
    public GameObject _hitEffect;
    public GameObject _resultObject;
    public List<AudioClip> _hitAudioClips;

    private GameObject _originParentObject;
    private Rigidbody _forgeAnvil = null;
    private VisualEffect _hitVisualEffect;
    private AudioSource _hitAudioSource;

    private ForgeManager _forgeManager;
    private XRBaseInteractable _interactable;

    private void Awake()
    {
        _originParentObject = this.transform.parent.gameObject;
        _hitVisualEffect = _hitEffect.GetComponent<VisualEffect>();
        _hitAudioSource = _hitEffect.GetComponent<AudioSource>();

        _forgeManager = GameObject.Find("[ForgeManager]").GetComponent<ForgeManager>();
        _interactable = this.GetComponent<XRBaseInteractable>();
        
        _resultObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.rigidbody == null)
        {
            return;
        }
        
        if (other.rigidbody.CompareTag("ForgeAnvil"))
        {
            _forgeAnvil = other.rigidbody;
        }
        else if (other.rigidbody.CompareTag("Hammer"))
        {
            if (_forgeAnvil == null)
            {
                return;
            }
            if (!_forgeIngot.activeSelf)
            {
                return;
            }

            doProcessHitFroge();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.rigidbody == null)
        {
            return;
        }
        
        if (other.rigidbody.CompareTag("ForgeAnvil"))
        {
            _forgeAnvil = null;
        }
    }

    private void doProcessHitFroge()
    {
        _forgeManager._leftController.GetComponent<XRController>().SendHapticImpulse(0.5f, 0.5f);
        _forgeManager._rightController.GetComponent<XRController>().SendHapticImpulse(0.5f, 0.5f);

        _hitVisualEffect.Play();
        _hitAudioSource.PlayOneShot(_hitAudioClips[Random.Range(0, _hitAudioClips.Count)]);

        _forgePoint -= 50;

        if (_forgePoint <= 0)
        {
            _forgeIngot.SetActive(false);
            _resultObject.SetActive(true);

            _resultObject.transform.parent = null;
            _resultObject.transform.forward = _forgeIngot.transform.forward;
            _resultObject.transform.position = _forgeIngot.transform.position;
            Destroy(_originParentObject, 1.0f);
        }
    }
}
