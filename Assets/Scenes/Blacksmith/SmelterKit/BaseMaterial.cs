using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

/*
 * 基础材料接口
 */
public interface BaseMaterialInterface
{
	/// <summary>
	/// 种类
	/// </summary>
	/// <returns></returns>
	string getType();
}

/*
 * 矿石接口
 */
public interface OreInterface : BaseMaterialInterface
{
    
}

/*
 * 金属锭接口
 */
public interface IngotInterface : BaseMaterialInterface
{
	
}

public class BaseMaterial : BaseMaterialInterface
{
	protected string _type;

	public BaseMaterial(string type)
	{
		_type = type;
	}

	public string getType()
	{
		return _type;
	}
}
