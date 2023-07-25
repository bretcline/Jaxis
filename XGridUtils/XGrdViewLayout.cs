using System;
using System.Drawing;

namespace PF.Utils.XGridUtils
{
	public class XGrdViewLayout
	{
		private bool m_ReadOnly = true;
		private bool m_ShowGroupPanel = true;
		private bool m_ShowFooter = false;
		private bool m_RemoveHiddenColumns = false;
		private Color m_OddRowColor = Color.White;
		private Color m_EvenRowColor = Color.White;

		#region Public Properties

		
		public bool ReadOnly
		{
			get
			{
				return m_ReadOnly;
			}
			set
			{
				m_ReadOnly = value;
			}
		}

		public bool ShowGroupPanel
		{
			get
			{
				return m_ShowGroupPanel;
			}
			set
			{
				m_ShowGroupPanel = value;
			}
		}
		
		public bool ShowFooter
		{
			get
			{
				return m_ShowFooter;
			}
			set
			{
				m_ShowFooter = value;
			}
		}
		
		public bool RemoveHiddenColumns
		{
			get
			{
				return m_RemoveHiddenColumns;
			}
			set
			{
				m_RemoveHiddenColumns = value;
			}
		}

		public Color OddRowColor
		{
			get
			{
				return m_OddRowColor;
			}
			set
			{
				m_OddRowColor = value;
			}
		}

		public Color EvenRowColor
		{
			get
			{
				return m_EvenRowColor;
			}
			set
			{
				m_EvenRowColor = value;
			}
		}
		#endregion // Public Properties
	}
}
