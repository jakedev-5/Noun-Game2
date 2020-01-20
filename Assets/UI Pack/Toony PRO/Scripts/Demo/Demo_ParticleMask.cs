// UI Pack : Toony PRO
// Version: 1.0.5
// Author: Gold Experience Team (http://ge-team.com/pages/unity-3d/)
// Support: geteamdev@gmail.com
// Please direct any bugs/comments/suggestions to support e-mail (geteamdev@gmail.com)

#region Namespaces

	using UnityEngine;
	using System.Collections;

	using UnityEngine.UI;

#endregion

// ######################################################################
// This class seachs up in Hierarchy for parent Mask object then it mask the ParticleSystem with found Mask.
// ######################################################################

public class Demo_ParticleMask : MonoBehaviour {
	
	// ######################################################################
	// Variables
	// ######################################################################
	#region Variables

		private CanvasScaler	m_CanvasScaler = null;		// Canvas Scaler
		private Mask			m_ParentMask = null;		// Mask component that found in parent object.

		private float			m_Left;						// Left position of Mask area
		private float			m_Right;					// Right position of Mask area
		private float			m_Top;						// Top position of Mask area
		private float			m_Bottom;					// Bottom position of Mask area

		private ParticleSystem	m_ParticleSystem;			// Self ParticleSystem component
	
	#endregion //Variables

	// ######################################################################
	// MonoBehaviour functions
	// ######################################################################

	#region MonoBehaviour functions

		// Use this for initialization
		void Start () {

			// Get ParticleSystem component of this object.
			m_ParticleSystem = gameObject.GetComponent<ParticleSystem>();
			if(m_ParticleSystem!=null)
			{ 
				// Search up through its parents in hierarchy for Mask component
				m_ParentMask = GetParentMask(this.transform.parent);
				if(m_ParentMask!=null)
				{
					// Search up through its parents in hierarchy for CanvasScaler component
					m_CanvasScaler = GetParentCanvasScaler(this.transform.parent);
					if(m_CanvasScaler!=null)
					{
						// Calculate for Mask area
						if(m_CanvasScaler.uiScaleMode == CanvasScaler.ScaleMode.ConstantPixelSize)
						{ 
							float width = (m_ParentMask.rectTransform.rect.width/m_CanvasScaler.referencePixelsPerUnit)*(m_CanvasScaler.scaleFactor);
							float height = (m_ParentMask.rectTransform.rect.height/m_CanvasScaler.referencePixelsPerUnit)*(m_CanvasScaler.scaleFactor);
							m_Left		= m_ParentMask.transform.position.x-(width/2);
							m_Top		= m_ParentMask.transform.position.y-(height/2);
							m_Right		= m_ParentMask.transform.position.x+(width/2);
							m_Bottom	= m_ParentMask.transform.position.y+(height/2);
						}

						// Mask ParticleSystem
						CheckMask();
					}
				}
			}

		}
	
		// Update is called once per frame
		void Update () {

			// Check if there are ParticleSystem and CanvasScaler and Mask
			if(m_ParticleSystem!=null && m_CanvasScaler!=null && m_ParentMask!=null)
			{
				if(m_ParticleSystem.enableEmission)
				{ 
					// Mask ParticleSystem
					CheckMask();
				}
			}
		}

	#endregion //MonoBehaviour functions

	// Mask ParticleSystem
	void CheckMask()
	{
		// This ParticleSystem is in mask
		if(this.transform.position.x > m_Left && this.transform.position.x < m_Right && this.transform.position.y > m_Top && this.transform.position.y < m_Bottom)
		{
			if(m_ParticleSystem.isPlaying==false)				
			{ 
				m_ParticleSystem.Play();
			}
		}
		// This ParticleSystem is out mask
		else
		{ 
			if(m_ParticleSystem.isPlaying==true)			
			{ 
				m_ParticleSystem.Stop();
			}
		}
	}

	// Search up through its parents in hierarchy for Mask component
	Mask GetParentMask(Transform trans)
	{ 
		// Return when root of the hierarchy is reached
		if(trans==null)
			return null;

		Mask pMask = trans.gameObject.GetComponent<Mask>();
		if(pMask!=null)
		{
			return pMask;
		}
		
		return GetParentMask(trans.parent);
	}

	// Search up through its parents in hierarchy for CanvasScaler component
	CanvasScaler GetParentCanvasScaler(Transform trans)
	{ 
		// Return when root of the hierarchy is reached
		if(trans==null)
			return null;

		CanvasScaler pCanvasScaler = trans.gameObject.GetComponent<CanvasScaler>();
		if(pCanvasScaler!=null)
		{
			return pCanvasScaler;
		}
		
		return GetParentCanvasScaler(trans.parent);
	}
}
