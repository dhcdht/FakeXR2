﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XROffsetGrabInteractable : XRGrabInteractable
{
	private Vector3 interactorPosition = Vector3.zero;
	private Quaternion interactorRotation = Quaternion.identity;

	protected override void OnSelectEnter(XRBaseInteractor interactor)
	{
		base.OnSelectEnter(interactor);

		if (!(interactor is XRDirectInteractor))
		{
			return;
		}
		interactorPosition = interactor.attachTransform.localPosition;
		interactorRotation = interactor.attachTransform.localRotation;
		bool hasAttach = attachTransform != null;
		interactor.attachTransform.position = hasAttach ? attachTransform.position : transform.position;
		interactor.attachTransform.rotation = hasAttach ? attachTransform.rotation : transform.rotation;
	}

	protected override void OnSelectExit(XRBaseInteractor interactor)
	{
		base.OnSelectExit(interactor);

		if (!(interactor is XRDirectInteractor))
		{
			return;
		}
		interactor.attachTransform.localPosition = interactorPosition;
		interactor.attachTransform.localRotation = interactorRotation;
		interactorPosition = Vector3.zero;
		interactorRotation = Quaternion.identity;
	}
}
