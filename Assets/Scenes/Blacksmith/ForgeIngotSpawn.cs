using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ForgeIngotSpawn : MonoBehaviour
{
	public List<GameObject> _forgeIngotPrefabs;

	private GameObject _currentIngot;
	private ForgePrototype _currentProtoType;

	private void Start()
	{
		_currentIngot = RandomInitAIngot();
		_currentProtoType = _currentIngot.GetComponentInChildren<ForgePrototype>();
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.GetComponentInParent<ForgePrototype>() == _currentProtoType)
		{
			_currentIngot = RandomInitAIngot();
			_currentProtoType = _currentIngot.GetComponentInChildren<ForgePrototype>();
		}
	}

	private GameObject RandomInitAIngot()
	{
		int randomIndex = Random.Range(0, _forgeIngotPrefabs.Count);
		GameObject ret = Instantiate(_forgeIngotPrefabs[randomIndex], this.transform.position, this.transform.rotation);

		return ret;
	}
}
