using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Component : MonoBehaviour
{
	private ComponentManager _componentManager = null;
	private List<ComponentSlot> _selfSlots = new List<ComponentSlot>();
	private Component _parentComponent = null;
	private List<Component> _subComponents = new List<Component>();

	private Rigidbody _rigidbody;
	private XRBaseInteractable _interactable;

	private void Awake()
	{
		_componentManager = GameObject.Find("[ComponentManager]").GetComponent<ComponentManager>();

		_rigidbody = this.GetComponent<Rigidbody>();
		_interactable = this.GetComponent<XRBaseInteractable>();
		if (_interactable != null)
		{
			_interactable.onSelectEnter.AddListener(onXRSelectEnter);
			_interactable.onSelectExit.AddListener(onXRSelectExit);
			
			ComponentSlot[] componentSlots = this.GetComponentsInChildren<ComponentSlot>();
			_selfSlots.AddRange(componentSlots);
			foreach (ComponentSlot componentSlot in componentSlots)
			{
				componentSlot._originComponent = this;
				componentSlot._currentComponent = this;
			}
		}
	}

	public void onXRSelectEnter(XRBaseInteractor interactor)
	{
		_componentManager.onXRSelectEnter(interactor, this);
	}

	public void onXRSelectExit(XRBaseInteractor interactor)
	{
		_componentManager.onXRSelectExit(interactor, this);
	}

	public List<ComponentSlot> getAllSlots()
	{
		List<ComponentSlot> results = new List<ComponentSlot>();
		results.AddRange(_selfSlots);
		
		foreach (Component subComponent in _subComponents)
		{
			results.AddRange(subComponent.getAllSlots());
		}

		return results;
	}
	
	public List<Component> getSubComponents()
	{
		return _subComponents;
	}

	public List<ComponentSlot> getReadyConnectSlots()
	{
		List<ComponentSlot> results = new List<ComponentSlot>();

		List<ComponentSlot> componentSlots = this.getAllSlots();
		foreach (ComponentSlot componentSlot in componentSlots)
		{
			if (componentSlot.getIsReadyConnect())
			{
				results.Add(componentSlot);
			}
		}

		return results;
	}

	public bool addSubComponent(Component subComponent)
	{
		ComponentSlot fromSlot = subComponent.getReadyConnectSlots().FirstOrDefault();
		ComponentSlot toSlot = this.getReadyConnectSlots().FirstOrDefault();
		if (fromSlot == null || toSlot == null)
		{
			return false;
		}
		if (!fromSlot.connectToSlot(toSlot))
		{
			return false;
		}
		if (!toSlot.connectToSlot(fromSlot))
		{
			fromSlot.disconnectToSlot(toSlot);
			return false;
		}

		subComponent.setEnableGrab(false);
		subComponent.transform.parent = this.transform;

		subComponent.transform.forward = toSlot.transform.forward;
		subComponent.transform.rotation = toSlot.transform.rotation;
		subComponent.transform.position += (toSlot.transform.position - fromSlot.transform.position);

		this._subComponents.Add(subComponent);
		subComponent._parentComponent = this;
		// this._subJoints.Add(fixedJoint);

		foreach (ComponentSlot componentSlot in this.getAllSlots())
		{
			componentSlot._currentComponent = this;
		}

		return true;
	}

	public void setEnableSlotVisual(bool enableVisual)
	{
		foreach (ComponentSlot slot in this.getAllSlots())
		{
			if (!slot.getIsConnected())
			{
				slot.setEnableSlotVisual(enableVisual);
			}
		}
	}

	public void setEnableGrab(bool enableGrab)
	{
		if (enableGrab)
		{
			throw new NotImplementedException();
		}
		else
		{
			Destroy(_interactable);
			Destroy(_rigidbody);
		}
	}
}
