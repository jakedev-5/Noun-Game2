// UI Pack : Toony PRO
// Version: 1.0.5
// Author: Gold Experience Team (http://ge-team.com/pages/unity-3d/)
// Support: geteamdev@gmail.com
// Please direct any bugs/comments/suggestions to support e-mail (geteamdev@gmail.com)

#region Namespaces

	using UnityEngine;
	using System.Collections;

#endregion

// This class describes information of each cloud
[System.Serializable]
public class UIPackToonyCloud
{
	public float m_MoveSpeed;						// Move speed
	public RectTransform m_RectTransform;			// RectTransform of cloud
	public Vector2 m_OriginalLocalPos;				// Origin anchoredPosition
}

// ######################################################################
// UIPackToonyCloudManager class manages all cloud sprite, 
// it moves could from lef to right or from right to left.
// It also brings off-screen cloud sprite back to screen.
// ######################################################################

public class UIPackToonyCloudManager : MonoBehaviour
{
	
	// ######################################################################
	// Variables
	// ######################################################################

	#region Variables

		//[HideInInspector]								// Remark this line if you want to see each cloud's details
		public UIPackToonyCloud[] m_CloudList = null;	// Array of cloud

		public float m_MinSpeed = 0.05f;				// Min speed
		public float m_MaxSpeed = 0.3f;					// Max speed

	#endregion //Variables
	
	// ######################################################################
	// MonoBehaviour functions
	// ######################################################################

	#region Monobehavior
	
		// Use this for initialization
		void Start()
		{		
			InitMe();
		}
		
		// Update is called once per frame
		void Update ()
		{
			if(m_CloudList==null)
				return;
			
			// Check if the screen resolution was changed
			if(m_CameraRightEdge!=m_ParentCanvasRectTransform.rect.width/2 || m_CameraTopEdge!=m_ParentCanvasRectTransform.rect.height/2)
			{
				InitMe();
			}

			// Update all children cloud
			int index = 0;
			foreach(UIPackToonyCloud child in m_CloudList)
			{
				if(child.m_RectTransform.gameObject.activeSelf==true)
				{
					// Move cloud
					m_CloudList[index].m_RectTransform.anchoredPosition = new Vector2(m_CloudList[index].m_RectTransform.anchoredPosition.x + (m_CloudList[index].m_MoveSpeed * Time.deltaTime),
																					 m_CloudList[index].m_RectTransform.anchoredPosition.y);

					// Moving from left to right
					if(m_CloudList[index].m_MoveSpeed>0)
					{
						// Check if cloud move off the right-edge
						if(m_CloudList[index].m_RectTransform.anchoredPosition.x>m_CameraRightEdge+m_CloudList[index].m_RectTransform.rect.width/2)
						{
								// Random new speed
								m_CloudList[index].m_MoveSpeed = Random.Range(m_MinSpeed,m_MaxSpeed);

								// Re-position to other side of screen
								m_CloudList[index].m_RectTransform.anchoredPosition = new Vector2(m_CameraLeftEdge-m_CloudList[index].m_RectTransform.rect.width,
																								 Random.Range(m_CameraBottomEdge*0.25f, m_CameraTopEdge));
						}
					}
					// Moveing from right to left
					else
					{
						// Check if cloud move off the left-edge
						if(m_CloudList[index].m_RectTransform.anchoredPosition.x<m_CameraLeftEdge-m_CloudList[index].m_RectTransform.rect.width/2)
						{

								// Random new speed
								m_CloudList[index].m_MoveSpeed = -Random.Range(m_MinSpeed,m_MaxSpeed);
						
								// Re-position to other side of screen
								m_CloudList[index].m_RectTransform.anchoredPosition = new Vector3(m_CameraRightEdge+m_CloudList[index].m_RectTransform.rect.width,
																								 Random.Range(m_CameraBottomEdge*0.25f, m_CameraTopEdge));
						}
					}


				}

				index++;
			}
		}
	
	#endregion //Monobehavior

	// ######################################################################
	// Utility functions
	// ######################################################################

	#region Utility functions

		// This class need Canvas to work properly.
		Canvas	m_Parent_Canvas = null;

		// Edge position of camera perspective
		float m_CameraLeftEdge;
		float m_CameraRightEdge;
		float m_CameraTopEdge;
		float m_CameraBottomEdge;

		// If you have error at this line on Unity 5.x, please make sure that you are using Unity 5.x with a valid license.
		RectTransform	m_ParentCanvasRectTransform		= null;

		// Search for parent Canvas and calculate view size of camera 
		void FindParentCanvasAndCameraArea()
		{
			// Search for the parent Canvas
			if(m_Parent_Canvas==null)
				m_Parent_Canvas = GEAnimSystem.Instance.GetParent_Canvas(transform);

			// Calculate view size of camera 
			if(m_Parent_Canvas!=null)
			{
				m_ParentCanvasRectTransform = m_Parent_Canvas.GetComponent<RectTransform>();
			
				m_CameraRightEdge = (m_ParentCanvasRectTransform.rect.width/2);
				m_CameraLeftEdge = -m_CameraRightEdge;
				m_CameraTopEdge = (m_ParentCanvasRectTransform.rect.height/2);
				m_CameraBottomEdge = -m_CameraTopEdge;
			}
		}

		// Initial sprite and put them to the array
		void InitMe()
		{
			// Search for parent Canvas and calculate view size of camera 
			FindParentCanvasAndCameraArea();

			// Init array of UIPackToonyCloud
			m_CloudList = new UIPackToonyCloud[transform.childCount];

			// Put all sprites to m_CloudList
			int index = 0;
			foreach(Transform child in transform)
			{
				// Create new UIPackToonyCloud class
				m_CloudList[index] = new UIPackToonyCloud();

				// Random speed
				m_CloudList[index].m_MoveSpeed = Random.Range(m_MinSpeed,m_MaxSpeed);

				// Random first direction
				int RandomDirection = Random.Range(0,2);
				// Left/right direction
				if(RandomDirection==0)
				{
					m_CloudList[index].m_MoveSpeed *= -1;
				}
			
				// Keep this RectTransform
				m_CloudList[index].m_RectTransform = child.GetComponent<RectTransform>();

				// Keep original anchoredPosition to use later
				m_CloudList[index].m_OriginalLocalPos = m_CloudList[index].m_RectTransform.anchoredPosition;

				// Random cloud position
				m_CloudList[index].m_RectTransform.anchoredPosition = new Vector2(Random.Range(m_CameraLeftEdge, m_CameraRightEdge),
																								Random.Range(m_CameraBottomEdge*0.25f, m_CameraTopEdge));

				// Increase index
				index++;
			}
		}

	#endregion //Utility functions
}