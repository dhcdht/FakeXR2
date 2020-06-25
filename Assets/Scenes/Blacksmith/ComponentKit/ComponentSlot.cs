using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ComponentSlot : MonoBehaviour
{
	public string _type = null;
	public GameObject _visual = null;
	
	public Component _originComponent;
	public Component _currentComponent;

	private ComponentManager _componentManager;
	private List<ComponentSlot> _willConnectComponentSlots = new List<ComponentSlot>();
	private ComponentSlot _connectedSlot = null;

	public void setEnableSlotVisual(bool enableVisual)
	{
		_visual.SetActive(enableVisual);
	}

	public bool getIsReadyConnect()
	{
		return !getIsConnected() && (_willConnectComponentSlots.Count > 0);
	}
	
	public bool getIsConnected()
	{
		return _connectedSlot != null;
	}

	public bool canConnectToSlot(ComponentSlot slot)
	{
		if (slot == null)
		{
			return false;
		}
		if (_connectedSlot != null)
		{
			return false;
		}
		if (!_willConnectComponentSlots.Contains(slot))
		{
			return false;
		}
		if (slot._currentComponent == this._currentComponent)
		{
			return false;
		}
		if (!slot._type.Equals(this._type))
		{
			return false;
		}

		return true;
	}

	public bool connectToSlot(ComponentSlot slot)
	{
		if (!this.canConnectToSlot(slot))
		{
			return false;
		}

		_willConnectComponentSlots.Remove(slot);
		_connectedSlot = slot;
		this.setEnableSlotVisual(false);

		return true;
	}

	public bool disconnectToSlot(ComponentSlot slot)
	{
		if (_connectedSlot != slot)
		{
			return false;
		}

		_connectedSlot = null;
		return true;
	}

	private void Awake()
	{
		_componentManager = GameObject.Find("[ComponentManager]").GetComponent<ComponentManager>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("ComponentSlot"))
		{
			ComponentSlot componentSlot = other.GetComponent<ComponentSlot>();
			_willConnectComponentSlots.Add(componentSlot);
			if (!this.canConnectToSlot(componentSlot))
			{
				_willConnectComponentSlots.Remove(componentSlot);
				return;
			}
			
			this.setEnableSlotVisual(true);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("ComponentSlot"))
		{
			ComponentSlot componentSlot = other.GetComponent<ComponentSlot>();
			_willConnectComponentSlots.Remove(componentSlot);
			if (_willConnectComponentSlots.Count == 0)
			{
				this.setEnableSlotVisual(false);
			}
		}
	}
}
