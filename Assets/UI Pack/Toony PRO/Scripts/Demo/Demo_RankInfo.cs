// UI Pack : Toony PRO
// Version: 1.0.5
// Author: Gold Experience Team (http://ge-team.com/pages/unity-3d/)
// Support: geteamdev@gmail.com
// Please direct any bugs/comments/suggestions to support e-mail (geteamdev@gmail.com)

using UnityEngine;

	using System.Collections;

	#region Namespaces
	using UnityEngine.UI;

#endregion

// ######################################################################
// This class describes information of each rank
// ######################################################################

public class Demo_RankInfo : MonoBehaviour
{
	
	#region Variables

		public Text		m_TextRank			= null;
		public Text		m_TextPlayerName	= null;
		public Text		m_TextBestScore		= null;
		public Image	m_Portrait			= null;	

	#endregion //Variables

	// ######################################################################
	// MonoBehaviour functions
	// ######################################################################

	#region MonoBehaviour functions

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	#endregion //MonoBehaviour Functions
	
	// Bind children objects to this object's variables.
	void BindGameObjects(Transform trans)
	{
		switch(trans.name)
		{ 
			case "TextRank":
				m_TextRank = trans.gameObject.GetComponent<Text>();
				break;
			case "TextPlayerName":
				m_TextPlayerName = trans.gameObject.GetComponent<Text>();
				break;
			case "TextBestScore":
				m_TextBestScore = trans.gameObject.GetComponent<Text>();
				break;
			case "Portrait":
				m_Portrait = trans.gameObject.GetComponent<Image>();
				break;
		}

		// Bind objects for children
		foreach(Transform child in trans)
		{
 			BindGameObjects(child.transform);
		}
	}
	
	// Set information to Rank.
	// Note this function have to be called anytime after BindGameObjects is called.
	public void SetInfo(string Number, string Name, string Score, Sprite ProtraitSprite)
	{
		// Bind children objects to this object's variables.
		BindGameObjects(this.transform);

		m_TextRank.text = Number;
		m_TextPlayerName.text = Name;
		m_TextBestScore.text = Score;
		m_Portrait.sprite = ProtraitSprite;
	}
}
