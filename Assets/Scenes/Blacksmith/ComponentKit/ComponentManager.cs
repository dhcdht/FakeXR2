using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ComponentManager : MonoBehaviour
{
	public GameObject _leftController;
	public GameObject _rightController;

	private XRBaseInteractor _leftInteractor;
	private XRBaseInteractor _rightInteractor;
	private Component _leftComponent = null;
	private Component _rightComponent = null;

	private void Awake()
	{
		_leftInteractor = _leftController.GetComponent<XRBaseInteractor>();
		_rightInteractor = _rightController.GetComponent<XRBaseInteractor>();
	}

	public void onXRSelectEnter(XRBaseInteractor interactor, Component component)
	{
		if (interactor == _leftInteractor)
		{
			_leftComponent = component;
		}
		else if (interactor == _rightInteractor)
		{
			_rightComponent = component;
		}
	}

	public void onXRSelectExit(XRBaseInteractor interactor, Component component)
	{
		if (interactor == _leftInteractor)
		{
			if (_rightComponent == null)
			{
				_leftComponent = null;
			}
			else
			{
				unionComponent();
			}
		}
		else if (interactor == _rightInteractor)
		{
			if (_leftComponent == null)
			{
				_rightComponent = null;
			}
			// else
			// {
			// 	unionComponent();
			// }
		}
	}

	private void unionComponent()
	{
		if (_rightComponent.addSubComponent(_leftComponent))
		{
			_leftComponent = null;
		}
	}
}
