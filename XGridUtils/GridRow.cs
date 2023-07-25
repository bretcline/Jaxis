using System;
using System.Data;

namespace PF.Utils.XGridUtils
{
	public class GridRow
	{
		private bool m_IsExpanded = false;
		private bool m_IsZoomedRow = false;
		private bool m_IsFocusedRow = false;
		//			private DataRow m_GridDataRow = null;
		private object[] m_GridDataRowItems = null;

		protected GridRow()
		{

		}

		public GridRow(bool _IsExpanded, bool _IsZoomedRow, bool _IsFocusedRow, DataRow _GridDataRow)
		{
			m_IsExpanded = _IsExpanded;
			m_IsZoomedRow = _IsZoomedRow;
			m_IsFocusedRow = _IsFocusedRow;
			m_GridDataRowItems = _GridDataRow.ItemArray;
		}

		public bool IsExpanded
		{
			get
			{
				return m_IsExpanded;
			}
		}

		public bool IsZoomedRow
		{
			get
			{
				return m_IsZoomedRow;
			}
		}

		public bool IsFocusedRow
		{
			get
			{
				return m_IsFocusedRow;
			}
		}

		public object[] GridDataRowItems
		{
			get
			{
				return m_GridDataRowItems;
			}
		}

	}
}
