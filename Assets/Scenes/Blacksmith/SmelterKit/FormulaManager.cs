using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

/*
 * 配方
 */
public interface FormulaInterface
{
    List<Tuple<BaseMaterialInterface, int>> getInput();
    List<Tuple<BaseMaterialInterface, int>> getOutput();
}

/*
 * 基础配方
 */
public class BaseFormula : FormulaInterface
{
	protected List<Tuple<BaseMaterialInterface, int>> _input;
	protected List<Tuple<BaseMaterialInterface, int>> _output;

	public BaseFormula(List<Tuple<BaseMaterialInterface, int>> input, List<Tuple<BaseMaterialInterface, int>> output)
	{
		_input = input;
		_output = output;
	}

	public List<Tuple<BaseMaterialInterface, int>> getInput()
	{
		return _input;
	}

	public List<Tuple<BaseMaterialInterface, int>> getOutput()
	{
		return _output;
	}
}

public class FormulaManager : MonoBehaviour
{
	private List<FormulaInterface> _formulas;

	public FormulaManager()
	{
		registFormula(new BaseFormula(
			new List<Tuple<BaseMaterialInterface, int>>()
			{
				
			}, 
			new List<Tuple<BaseMaterialInterface, int>>()
			{
				
			}
		));
	}

	public bool registFormula(FormulaInterface formula)
	{
		_formulas.Add(formula);

		return true;
	}

	public List<FormulaInterface> getTargetFormula(Type type)
	{
		List<FormulaInterface> ret = new List<FormulaInterface>();
		
		foreach (FormulaInterface formula in _formulas)
		{
			if (formula.getOutput()[0].Item1 == type)
			{
				ret.Add(formula);
			}
		}

		return ret;
	}
	
	public List<FormulaInterface> getTargetFormula(IngotInterface ingotInterface)
	{
		return getTargetFormula(ingotInterface.GetType());
	}
}
