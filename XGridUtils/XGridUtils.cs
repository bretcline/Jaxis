#define SAVE_GRID_LAYOUT

using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Card;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;

using PF.Utils.Database;

using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace PF.Utils.XGridUtils
{
	/// <summary>
	/// Summary description for XGrdManipulator.
	/// </summary>
	public class XGrdManipulator
	{

		#region Member variables

		protected Hashtable			m_ViewLayouts = new Hashtable( );
		protected Hashtable			m_LayoutRows = new Hashtable( );

		protected GridControl		m_Grid = null;
		protected AppearanceObject	m_MainGridView = null;
		protected XGrdViewLayout	m_ViewLayout = null;
		protected XGrdViewLayout	m_DefaultViewLayout = null;
		protected object			m_FocusedRowBeforeSort = null;
		protected Hashtable			m_ColumnPanelHeight = new Hashtable();

		protected GridDataView		m_CurrentDataView;

		protected bool				m_RestoreLayout = true;
		protected bool				m_ColumnAutoWidth = false;

		#endregion //Member variables

		#region Public Properties



		virtual public XGrdViewLayout ViewLayout
		{
			get
			{
				if( null == m_ViewLayout )
				{
					m_ViewLayout = m_DefaultViewLayout;
				}
				return m_ViewLayout;
			}
		}
		
		virtual public bool ReadOnly
		{
			get
			{
				return ViewLayout.ReadOnly;
			}
			set
			{
				ViewLayout.ReadOnly = value;
			}
		}

		virtual public bool ShowGroupPanel
		{
			get
			{
				return ViewLayout.ShowGroupPanel;
			}
			set
			{
				ViewLayout.ShowGroupPanel = value;
			}
		}
		
		virtual public bool ShowFooter
		{
			get
			{
				return ViewLayout.ShowFooter;
			}
			set
			{
				ViewLayout.ShowFooter = value;
			}
		}

		virtual public bool RemoveHiddenColumns
		{
			get
			{
				return m_ViewLayout.RemoveHiddenColumns;
			}
			set
			{
				m_ViewLayout.RemoveHiddenColumns = value;
			}
		}

		virtual public Color OddRowColor
		{
			get
			{
				return ViewLayout.OddRowColor;
			}
			set
			{
				ViewLayout.OddRowColor = value;
			}
		}

		virtual public Color EvenRowColor
		{
			get
			{
				return ViewLayout.EvenRowColor;
			}
			set
			{
				ViewLayout.EvenRowColor = value;
			}
		}


		virtual public DataRow BaseViewSelectedDataRow
		{
			get
			{
				DataRow rc = null;
				try 
				{
					if( m_Grid.MainView is GridView)
					{
						int[] SelectedRows = ((GridView) m_Grid.MainView).GetSelectedRows();
						if( null != SelectedRows &&
							0 < SelectedRows.Length) 
						{
							rc = ((GridView) m_Grid.MainView).GetDataRow( SelectedRows[0] );
						}
					}
				}
				catch
				{
					throw;
				}
				return rc;
			}
		}

		virtual public DataRow[] BaseViewSelectedDataRows
		{
			get
			{
				DataRow[] rc = null;
				try 
				{
					int[] SelectedRows = ((GridView) m_Grid.MainView).GetSelectedRows();
					if( null != SelectedRows &&
						0 < SelectedRows.Length ) 
					{
						rc = new DataRow[SelectedRows.Length];
						for( int i = 0; i < SelectedRows.Length; ++i )
						{
							rc[i] = ((ColumnView) m_Grid.MainView).GetDataRow( SelectedRows[i] );
						}
					}
					else
					{
						GridView tmpGridView = m_Grid.MainView as GridView;
						if( null != tmpGridView )
						{
							DataRowView tmpRowView = tmpGridView.GetRow( tmpGridView.FocusedRowHandle ) as DataRowView;
							if( null != tmpRowView )
							{
								rc = new DataRow[1] { tmpRowView.Row };
							}
						}
					}
				}
				catch
				{
					throw;
				}
				return rc;
			}
		}

		virtual public int BaseViewCount
		{
			get
			{
				int rc = -1;
				try
				{
					rc = ((ColumnView) m_Grid.MainView).RowCount;
				}
				catch
				{
					throw;
				}
				return rc;
			}
		}

		virtual public GridDataView CurrentDataView
		{
			get
			{
				try
				{
					return m_CurrentDataView; 
				}
				catch
				{
					throw;
				}
			}
		}
		

//		virtual public int ColumnPanelHeight
//		{
//			get
//			{
//				try
//				{
//					return m_ColumnPanelHeight; 
//				}
//				catch
//				{
//					throw;
//				}
//			}
//			set
//			{
//				try
//				{
//					m_ColumnPanelHeight = value;
//				}
//				catch
//				{
//					throw;
//				}
//			}
//		}


		public bool ColumnAutoWidth
		{
			get
			{
				try
				{
					return m_ColumnAutoWidth; 
				}
				catch
				{
					throw;
				}
			}
			set
			{
				try
				{
					m_ColumnAutoWidth = value;
				}
				catch
				{
					throw;
				}
			}
		}


		public bool RestoreLayout
		{
			get
			{
				try
				{
					return m_RestoreLayout; 
				}
				catch
				{
					throw;
				}
			}
			set
			{
				try
				{
					m_RestoreLayout = value;
				}
				catch
				{
					throw;
				}
			}
		}


		#endregion // Public Properties

		protected XGrdManipulator()
		{
			Init( null );
		}

		public XGrdManipulator(GridControl _SourceGrid)
		{
			Init( _SourceGrid );
		}

		virtual protected void Init( GridControl _SourceGrid )
		{
			string HeaderHeight = System.Configuration.ConfigurationSettings.AppSettings["HeaderHeight"];
			if( null != HeaderHeight && 0 < HeaderHeight.Length )
			{
				string[] NVPairs = HeaderHeight.Split( ',' );
				foreach( string NVPair in NVPairs )
				{
					string[] Pairs = NVPair.Split( '=' );
					if( 2 == Pairs.Length )
					{
						m_ColumnPanelHeight[Pairs[0]] = Convert.ToInt32( Pairs[1] );
					}
				}
			}
			m_Grid = _SourceGrid;
			m_DefaultViewLayout = new XGrdViewLayout( );
			m_ViewLayout = m_DefaultViewLayout;
		}

		#region Public Methods
		virtual public ColumnView GetView( string _RelationName )
		{
			ColumnView rc = null;
			try
			{
				GridLevelNode LevelNode = m_Grid.LevelTree.Find( _RelationName );
				if( null != LevelNode )
				{
					rc = LevelNode.LevelTemplate as ColumnView;
				}
			}
			catch
			{
				throw;
			}
			return rc;
		}

		virtual public void BestFitColumns( )
		{
			((GridView) m_Grid.MainView).BestFitColumns();
		}


		public int RowHandleAtPoint( GridView _View, Point _Location )
		{
			int rc = -1;
			try
			{
				GridHitInfo hi = _View.CalcHitInfo( _Location );
//				int[] Indexes = _View.GetSelectedRows();
//				if( null != Indexes && 1 == Indexes.Length )
				{
//					hi = _View.CalcHitInfo( new Point( _Location.X, _Location.Y ) );

					if( true == hi.InRow )
					{
						if(hi.HitTest != GridHitTest.ColumnButton) 
						{
							if( 0 <= hi.RowHandle ) 
							{
								rc = hi.RowHandle;
							}
							else if( 0 <= _View.FocusedRowHandle )
							{
								rc = _View.FocusedRowHandle;
							}
						}
					}
				}
			}
			catch
			{
				throw;
			}
			return rc;
		}

		#region Load Base View
		virtual public void LoadBaseView( GridDataView _Data, bool _AutogenerateColumns )
		{
			try
			{
				if(null != _Data)
				{
					if( true == _AutogenerateColumns )
					{
						m_Grid.MainView = CreateGridView( _Data.Table );
					}
					BaseView View = m_Grid.MainView;
					m_Grid.DataSource = _Data;
					m_CurrentDataView = _Data;
					((GridView) View).OptionsBehavior.AllowIncrementalSearch = true;
				}
			}
			catch
			{
				throw;
			}
		}

		virtual public void LoadBaseView( GridDataView _Data )
		{
			try
			{
				LoadBaseView( _Data, false );
			}
			catch
			{
				throw;
			}
		}
		
		#endregion // Load Base View
		#region Load Layout


		virtual public void LoadLayout( GridDataView _Data )
		{
			try
			{
				LoadLayout( _Data, true );
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// Loads the layout.
		/// </summary>
		/// <param name="_Data">The _ data.</param>
		/// <param name="_ReadOnly">if set to <c>true</c> [_ read only].</param>
		virtual public void LoadLayout( GridDataView _Data, bool _ReadOnly )
		{
			try
			{
				if( null != _Data && null != _Data.Table )
				{
					string tmp = _Data.BaseRowFilter;
					
					LoadLayout( _Data.Table, _ReadOnly );

					_Data.BaseRowFilter = tmp;
					m_Grid.DataSource = _Data;
				}
			}
			catch
			{
				throw;
			}
		}

		
		virtual public void LoadLayout( DataTable _Data, bool _ReadOnly )
		{
			try
			{
				ReadOnly = _ReadOnly;
				LoadLayout( _Data );
			}
			catch
			{
				throw;
			}
		}

		virtual public void LoadLayout( DataTable _Data )
		{
			try
			{
				m_Grid.MainView = Load( _Data, m_Grid.LevelTree );
				m_Grid.DataSource = _Data;
			}
			catch
			{
				throw;
			}
		}
		

		virtual protected BaseView Load( DataTable _Data, GridLevelNode _Parent )
		{
			GridView rc = null;
			try
			{
				if( null != _Data )
				{
					GridLevelNode ChildNode = _Parent;
					rc = CreateGridView( _Data );

					PreLoad( rc, _Data, _Parent );


					rc.StartSorting += new EventHandler(Views_StartSorting);
					rc.EndSorting += new EventHandler(Views_EndSorting);
					rc.MasterRowExpanded += new CustomMasterRowEventHandler(Views_MasterRowExpanded);

					if( true == _Data.ExtendedProperties.ContainsKey( "VisibleRelationship" ) )
					{
						rc.MasterRowGetRelationName += new MasterRowGetRelationNameEventHandler(Views_MasterRowGetRelationName);
					}
					rc.ColumnPanelRowHeight = Convert.ToInt32( this.m_ColumnPanelHeight[_Data.TableName] ); 
					rc.OptionsBehavior.AllowIncrementalSearch = true;

					if( 0 < _Data.ChildRelations.Count )
					{
						for(int i = 0; i < _Data.ChildRelations.Count; ++i )
						{
							GridLevelNode Holder = new GridLevelNode( );
							Holder.RelationName = _Data.ChildRelations[i].RelationName;

							DataTable Table = _Data.ChildRelations[i].ChildTable;
							
							Holder.LevelTemplate = Load( Table, Holder ) as GridView;

							RemoveView( _Data.ChildRelations[i].RelationName );
							_Parent.Nodes.Add( Holder );
						}
					}
					else
					{
						_Parent.LevelTemplate = rc;
					}
					PostLoad( rc, _Data, _Parent );

					rc.OptionsView.ColumnAutoWidth = ColumnAutoWidth;
				}
			}
			catch
			{
				throw;
			}
			return rc;
		}

		virtual protected void PreLoad( GridView _View, DataTable _Data, GridLevelNode _Parent )
		{

		}

		virtual protected void PostLoad( GridView _View, DataTable _Data, GridLevelNode _Parent )
		{

		}

		#endregion //Load Layout

		virtual protected void Views_MasterRowGetRelationName(object sender, MasterRowGetRelationNameEventArgs e)
		{
			try
			{
				GridView View = sender as GridView;
				if( null != View )
				{
					DataTable Table = ( true == View.DataSource is DataTable ) ? (DataTable)View.DataSource :
						( true == View.DataSource is GridDataView ) ? ((GridDataView)View.DataSource).Table : null;
					if( null != Table )
					{
						if( true == Table.ExtendedProperties.ContainsKey( "VisibleRelationship" ) &&
							null != e &&
							false == View.OptionsDetail.ShowDetailTabs )
						{
							string Name = Table.ExtendedProperties["VisibleRelationship"].ToString();
							e.RelationName = Name;
						}
					}
				}
			}
			catch
			{
				throw;
			}
		}

		virtual protected void RemoveView( string _Name )
		{
			try
			{
				GridLevelNode Node = m_Grid.LevelTree.Nodes[_Name];
				if( null != Node )
				{
					BaseView oldView = Node.LevelTemplate;
					oldView.Dispose( );

					m_Grid.LevelTree.Nodes.Remove( Node );
				}
			}
			catch
			{
				throw;
			}
		}


		#region Layout Preservation
		virtual public void SaveVisibleLayout()
		{
			try
			{
				m_LayoutRows.Clear();
				SaveVisibleLayout( (GridView) m_Grid.MainView, (ArrayList) m_LayoutRows[((GridView) m_Grid.MainView).Name] );			
			}
			catch
			{
				throw;
			}
		}


		virtual public void SaveVisibleLayout(GridView _View, ArrayList _RowLayout )
		{
			try
			{
				if( null == _RowLayout )
				{
					_RowLayout = new ArrayList();
					m_LayoutRows.Add( _View.Name, _RowLayout );
				}

				for( int i = 0; i < _View.DataRowCount; i++ )
				{
					bool Focused = false;
					if( _View.FocusedRowHandle == i )
					{
						Focused = true;
					}
					if( _View.GetMasterRowExpanded( i ) )
					{
						BaseView DetailView =  _View.GetDetailView( i, _View.DefaultRelationIndex );
						if( null != DetailView )
						{
							_RowLayout.Add( new GridRow( true, DetailView.IsZoomedView, Focused, _View.GetDataRow( i ) ) );
							SaveVisibleLayout( (GridView) DetailView, (ArrayList) m_LayoutRows[DetailView.Name] );
						}
					}
					else if( true == Focused )
					{
						_RowLayout.Add( new GridRow( false, false, true, _View.GetDataRow( i ) ) );
					}
				}
			}
			catch
			{
				throw;
			}
		}

		virtual public void RestoreVisibleLayout(GridView _View, ArrayList _RowLayout )
		{
			try
			{
				//_View.BeginUpdate();
				foreach( GridRow Row in _RowLayout )
				{
					int RowHandle = FindRowHandleByRowObject( ref _View, Row.GridDataRowItems );
					if( DevExpress.XtraGrid.GridControl.InvalidRowHandle != RowHandle )
					{
						if( true == Row.IsFocusedRow )
						{
							_View.FocusedRowHandle = RowHandle;
						}
						if( true == Row.IsExpanded )
						{
							_View.SetMasterRowExpanded( RowHandle, true );
							BaseView View = _View.GetDetailView( RowHandle, _View.DefaultRelationIndex );
							if( null != View )
							{
								if( true == Row.IsZoomedRow )
								{
									View.ZoomView();
								}
								GridView DetailView = View as GridView;
								if( null != DetailView )
								{
									RestoreVisibleLayout( DetailView, (ArrayList) m_LayoutRows[View.Name] );
								}
							}
						}
					}
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				//_View.EndUpdate();
			}
		}

		virtual public void RestoreVisibleLayout()
		{
			try
			{
				ArrayList tmp = m_LayoutRows[((ColumnView) m_Grid.MainView).Name] as ArrayList;
				if( null != tmp )
				{
					RestoreVisibleLayout( (GridView) m_Grid.MainView, tmp );
				}
			}
			catch
			{
				throw;
			}
		}
		#endregion //Layout Preservation

		virtual public DataRow GetSelectedDataRow()
		{
			DataRow rc = null;
			try
			{
				GridView View = (GridView) m_Grid.FocusedView;
				rc = GetSelectedDataRow( View );
			}
			catch
			{
				throw;
			}
			return rc;
		}

		virtual public int GetFocusedRowHandle()
		{
			int rc = -1;
			try
			{
				GridView View = (GridView) m_Grid.FocusedView;
				rc = View.FocusedRowHandle;
			}
			catch
			{
				throw;
			}
			return rc;
		}

		virtual public void SetFocusedRowHandle( int _RowHandle )
		{
			try
			{
				GridView View = (GridView) m_Grid.FocusedView;
				View.FocusedRowHandle = _RowHandle;
			}
			catch
			{
				throw;
			}
		}

		virtual public DataRow GetSpecificDataRow( int _RowHandle )
		{
			DataRow rc = null;
			try
			{
				GridView View = (GridView) m_Grid.FocusedView;
				rc = View.GetDataRow( _RowHandle );
			}
			catch
			{
				throw;
			}
			return rc;
		}

		virtual public void RemoveFilters( ColumnView _View )
		{
			try
			{
				if( null != _View )
				{
					foreach( GridColumn Column in _View.Columns )
					{
						Column.ClearFilter( );
					}
				}
			}
			catch
			{
				throw;
			}
		}

		
		virtual public void SetGridFilter( GridView _View, string _Column, string _Filter, bool _Visible )
		{
			try
			{
				if( null != _View && null != _View.Columns[_Column] )
				{
					_View.Columns[_Column].ClearFilter();
					string ColumnName = SqlTool.GetReadableColumnName( _Column );
					if( 0 < _Filter.Length )
					{
						ColumnFilterInfo Filter = new ColumnFilterInfo( _Filter, ColumnName );
						/// it appears that XtraGrid 6.1.4.0 introduced a change that throws an
						/// error if _Filter is blank, so we added the && below.
						if( null != Filter )
						{
							_View.Columns[_Column].FilterInfo = Filter;
						}
					}
					_View.OptionsView.ShowFilterPanel = _Visible;
					_View.BeginSort();
					_View.EndSort( );
				}
			}
			catch
			{
				throw;
			}
		}


		virtual public int AddRow( )
		{
			int rc = -1;
			try
			{
				rc = AddRow( m_Grid.MainView as GridView );
			}
			catch
			{
				throw;
			}
			return rc;
		}
		
		virtual public int AddRow( GridView _View )
		{
			int rc = -1;
			try
			{
				if( null != _View )
				{
					_View.OptionsCustomization.AllowSort = false;
					_View.OptionsBehavior.Editable = true;
					XGrdManipulator.SetEditable( _View, true );

					_View.AddNewRow();
					
					int[] SelectedRows = _View.GetSelectedRows();
					if( null != SelectedRows&&
						0 < SelectedRows.Length )
					{
						for( int p = 0; p < SelectedRows.Length; ++p )
						{
							// when adding a new collateral, it should unselect all the previous selected row an selects only the lastrow
							_View.UnselectRow(SelectedRows[p]);	
						}
					}
					_View.FocusedRowHandle = DevExpress.XtraGrid.GridControl.InvalidRowHandle;
					rc = _View.RowCount - 1;
					_View.SelectRow( rc );
					_View.FocusedRowHandle = rc;
//					for( int i = 0; i < _View.Columns.Count; i++ )
//					{
//						if( 1 == _View.Columns[i].VisibleIndex )
//						{
//							_View.FocusedColumn = _View.Columns[i];
//							break;
//						}
//					}
				}
			}
			catch
			{
				throw;
			}
			return rc;
		}

		virtual public bool DropdownEditorColumn( string _ColumnName, DataTable _Table, string _ValueMember, string _DisplayMember )
		{
			bool rc = false;
			try
			{
				GridColumn Column = ((GridView)m_Grid.MainView).Columns[_ColumnName];
				if( null != Column )
				{
					RepositoryItemComboBox ComboBoxes = m_Grid.RepositoryItems[_ColumnName] as RepositoryItemComboBox;
					if( null == ComboBoxes )
					{
						ComboBoxes = new RepositoryItemComboBox( );
						ComboBoxes.AutoHeight = false;

						ArrayList Items = new ArrayList(  );
						for( int i = 0; i < _Table.Rows.Count; ++i )
						{
							DataRow Row = _Table.Rows[i];
							Utils.Misc.NameValuePair Pair = new Utils.Misc.NameValuePair( Row[_ValueMember], Row[_DisplayMember] );
							Items.Add( new DevExpress.XtraEditors.Controls.ComboBoxItem( Pair ) );
						}
					
						ComboBoxes.Items.AddRange( Items.ToArray( ) );

						ComboBoxes.Name = _ColumnName;
						m_Grid.RepositoryItems.Add( ComboBoxes );
					}
					Column.Caption = Utils.Database.SqlTool.GetReadableColumnName( _ColumnName );
					Column.ColumnEdit = ComboBoxes;
					Column.VisibleIndex = 0;
					rc = true;
				}
			}
			catch
			{
				throw;
			}
			return rc;
		}


		#endregion // Public Methods

		#region Private/Protected Methods

		virtual protected void GetViewSettings( string _TableName )
		{
			try
			{
				m_ViewLayout = m_ViewLayouts[_TableName] as XGrdViewLayout;
				if( null == m_ViewLayout )
				{
					m_ViewLayout = m_DefaultViewLayout;
				}
			}
			catch
			{
				throw;
			}
		}


		virtual protected GridView CreateGridView( DataTable _SourceTable )
		{
			GridView Views = null;
			try
			{
				Hashtable FooterColumns = new Hashtable( );

				foreach( DataColumn Col in _SourceTable.Columns )
				{
					Col.Caption = SqlTool.GetReadableColumnName(Col.Caption);
					if( Col.Caption.StartsWith( "_" ) )
					{
						Col.Caption = Col.Caption.Substring( 1 );
					}
				}

				Views = new GridView( m_Grid );

				Views.Click += new EventHandler(Views_Click);

				PreViewProcessing( Views, _SourceTable );


				// If there are multiple views for a given level, do we show then?  If ShowDetailTabs exists, we do.
				if( _SourceTable.ExtendedProperties.ContainsKey( "ShowDetailTabs" ) )
				{
					Views.OptionsDetail.SmartDetailExpandButtonMode = DetailExpandButtonMode.CheckAllDetails;
					Views.OptionsView.ShowDetailButtons = true;
					Views.DetailTabHeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom;
				}
				else
				{
					Views.OptionsDetail.SmartDetailExpandButtonMode = DetailExpandButtonMode.CheckDefaultDetail;
					Views.OptionsDetail.ShowDetailTabs = false;

					Views.MasterRowExpanding -=new MasterRowCanExpandEventHandler(Views_MasterRowExpanding);
					Views.MasterRowExpanding +=new MasterRowCanExpandEventHandler(Views_MasterRowExpanding);
				}
				Views.OptionsDetail.AllowExpandEmptyDetails = false;

				// If ViewCaption exists, then set the ViewCaption = to the value provided.
				if( _SourceTable.ExtendedProperties.ContainsKey( "ViewCaption" ) )
				{
					Views.ViewCaption = _SourceTable.ExtendedProperties["ViewCaption"].ToString( );
				}
				// If ViewCaption exists, then set the ViewCaption = to the value provided.
				if( _SourceTable.ExtendedProperties.ContainsKey( "ShowFooter" ) )
				{
					ShowFooter = true;
					string ColumnList = _SourceTable.ExtendedProperties["ShowFooter"].ToString( );
					string[] Columns = ColumnList.Split( '|' );
					foreach( string Column in Columns )
					{
						string[] FooterColumn = Column.Split( ':' );
						if( 2 == FooterColumn.Length )
						{
							FooterColumns.Add( FooterColumn[0], FooterColumn[1] );
						}
						else
						{
							throw new Exception( "XGrdUtils->CreateGridView():  Invalid Number of parameters for the Footer Column" );
						}
					}
				}
				
				Views.OptionsView.ShowGroupPanel = ShowGroupPanel;
				Views.PopulateColumns( _SourceTable );
				Views.Name = _SourceTable.TableName;

				//Will this keep the focused row??  we'll see....
				Views.OptionsBehavior.KeepFocusedRowOnUpdate = true;

#if SAVE_GRID_LAYOUT
				if( false == RestoreViewLayout( Views ) )
#endif
				{
					GetViewSettings( _SourceTable.TableName );
				
					Views.OptionsView.ShowFooter = ShowFooter;

					Views.Appearance.EvenRow.BackColor = EvenRowColor;
					Views.Appearance.EvenRow.BackColor2 = EvenRowColor;
					Views.Appearance.EvenRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;

					Views.Appearance.OddRow.BackColor = OddRowColor;
					Views.Appearance.OddRow.BackColor2 = OddRowColor;
					Views.Appearance.OddRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;

					foreach( DataColumn Col in _SourceTable.Columns )
					{
						string ColCaption = Col.Caption.ToUpper();
						GridColumn GridCol = Views.Columns[Col.ColumnName];

						if( ColCaption.StartsWith( "HIDE_" ) || ColCaption.StartsWith( "_HIDE_" ) )
						{
							GridCol.VisibleIndex = -1;
							if( true != GridCol.OptionsColumn.ReadOnly )
							{
								GridCol.OptionsColumn.ReadOnly = true;
								GridCol.OptionsColumn.AllowEdit = false;
							}

							CustomHideColumnProcessing( Views, Col.ColumnName );
						}
						else
						{
							string Caption = SqlTool.GetReadableColumnName(Col.Caption);
							if( Caption.StartsWith( "_" ) )
							{
								Caption = Caption.Substring( 1 );
							}
							GridCol.Caption = Caption; 

							GridCol.AppearanceHeader.Assign( HeaderStyle );
							if( true == ReadOnly )
							{
								//Turn ReadOnly OFF and NonEditable ON for the column
								GridCol.OptionsColumn.ReadOnly = true;
								GridCol.OptionsColumn.AllowEdit = false;
							}
							if( typeof(decimal) == Col.DataType )
							{
								//TODO:  This probably should look at the datatype,
								//pretty safe bet that if we're sending a decimal value down it is an amount,
								//could be a percentage though.
								GridCol.DisplayFormat.FormatType = FormatType.Numeric;
								GridCol.DisplayFormat.FormatString = "c2";
							}
							if( typeof(double) == Col.DataType )
							{
								GridCol.DisplayFormat.FormatType = FormatType.Numeric;
								GridCol.DisplayFormat.FormatString = "p";
							}
							if( true == FooterColumns.Contains( Col.ColumnName ) )
							{
								SetFooterColumn( GridCol, FooterColumns[Col.ColumnName].ToString( ) );
							}
							CustomColumnProcessing( Views, Col );
						}
					}

				}
				PostViewProcessing( Views, _SourceTable );

				if( true == m_Grid.IsPrintingAvailable )
				{
					Views.OptionsPrint.PrintDetails = true;
					Views.OptionsPrint.PrintPreview = true;
					Views.OptionsPrint.PrintDetails = true;
				}
			}
			catch
			{
				throw;
			}
			return Views;
		}

		virtual protected void PreViewProcessing( GridView _View, DataTable _SourceTable )
		{
			try
			{
				
			}
			catch
			{
				throw;
			}
		}

		virtual protected void CustomHideColumnProcessing( ColumnView _View, string _ColumnName )
		{
			try
			{
			}
			catch
			{
				throw;
			}
		}

		virtual protected void CustomColumnProcessing( GridView _View, DataColumn _Column )
		{
			try
			{
			}
			catch
			{
				throw;
			}
		}

		virtual protected void PostViewProcessing( GridView _View, DataTable _SourceTable )
		{
			try
			{
			}
			catch
			{
				throw;
			}
		}
		

		virtual protected void Views_MasterRowExpanded(object sender, CustomMasterRowEventArgs e)
		{
			BaseView View = ((GridView) sender).GetDetailView(e.RowHandle, e.RelationIndex);
			((GridView) View).BestFitColumns();
		}


		virtual public int FindRowHandleByRowObject( GridView _View, object _Row ) 
		{
			int rc = DevExpress.XtraGrid.GridControl.InvalidRowHandle;
			try
			{
				if( null != _Row )
				{
					for( int i = 0; i < _View.DataRowCount; i++ )
					{
						if( _Row.Equals( _View.GetRow(i) ) )
						{
							return i;
						}
					}
				}
			}
			catch
			{
				throw;
			}
			return rc;
		}

		virtual public int FindRowHandleByRowObject( ref GridView _View, object[] _Row )
		{
			int rc = DevExpress.XtraGrid.GridControl.InvalidRowHandle;
			try
			{
				GridColumn[] Columns = new GridColumn[_View.Columns.Count];
				for( int i = 0; i < _View.Columns.Count; i++ )
				{
					Columns[i] = _View.Columns[i];
				}
				rc = XGrdManipulator.LocateRowByMultipleValues( (ColumnView) _View, Columns, _Row, 0 );
			}
			catch
			{
				throw;
			}
			return rc;

		}

		virtual protected AppearanceObject HeaderStyle
		{
			get
			{
				AppearanceObject rc = null;
				try
				{
					if( null == m_MainGridView )
					{
						m_MainGridView = new AppearanceObject();

						Font HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 
							8.0F, 
							System.Drawing.FontStyle.Regular, 
							System.Drawing.GraphicsUnit.Point, 
							((System.Byte)(0)));

						m_MainGridView.Font = HeaderFont;
						m_MainGridView.TextOptions.WordWrap = WordWrap.Wrap;
						m_MainGridView.TextOptions.VAlignment = VertAlignment.Center;
						m_MainGridView.TextOptions.HAlignment = HorzAlignment.Default;

						m_MainGridView.BackColor = SystemColors.Window;
						m_MainGridView.ForeColor = SystemColors.WindowText;
						m_MainGridView.BackColor2 = Color.Empty;
						m_MainGridView.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
					}
					rc = m_MainGridView;
				}
				catch
				{
					throw;
				}
				return rc;
			}
		}
		

		#endregion // Private Methods

		#region Static Members

		static public DataRow GetSelectedDataRow( GridView _View )
		{
			DataRow rc = null;
			try 
			{
				if( null != _View )
				{
					int[] SelectedRows = _View.GetSelectedRows();
					if( null != SelectedRows &&
						0 < SelectedRows.Length ) 
					{
						rc = _View.GetDataRow( SelectedRows[0] );
					}
					else
					{
						DataRowView tmpRowView = _View.GetRow( _View.FocusedRowHandle ) as DataRowView;
						if( null != tmpRowView )
						{
							rc = tmpRowView.Row;
						}
					}
				}
			}
			catch
			{
				throw;
			}
			return rc;
		}

		
		static public DataRow[] GetSelectedDataRows( GridView _View )
		{
			DataRow[] rc = null;
			try 
			{
				int[] SelectedRows = _View.GetSelectedRows();
				if( null != SelectedRows &&
					0 < SelectedRows.Length ) 
				{
					rc = new DataRow[SelectedRows.Length];
					for( int i = 0; i < SelectedRows.Length; ++i )
					{
						rc[i] = _View.GetDataRow(SelectedRows[i]);
					}
				}
			}
			catch
			{
				throw;
			}
			return rc;
		}

				
		static public DataRow[] GetAllDataRows( GridView _View )
		{
			DataRow[] rc = null;
			try 
			{
				rc = new DataRow[_View.RowCount];
				for( int i = 0; i < _View.RowCount; ++i )
				{
					DataRowView tmp = _View.GetRow( i ) as DataRowView;
					if( null != tmp )
					{
						rc[i] = tmp.Row;
					}
				}
			}
			catch
			{
				throw;
			}
			return rc;
		}
		
		static public DataRow SearchForRow( GridControl _Control, string _SearchText, string _ColumnName, bool _Zoom )
		{
			DataRow rc = null;
			try
			{
				object[] Keys = new object[_Control.LevelTree.Nodes.Count];
				
				for( int i = 0; i < _Control.LevelTree.Nodes.Count && null == rc; ++i )
				{
					GridView View = _Control.LevelTree.Nodes[i].LevelTemplate as GridView;
					if( null != View )
					{
						rc = SearchForRow( ref View, 0, _SearchText, _ColumnName, _Zoom );
					}
				}
				
			}
			catch
			{
				throw;
			}
			return rc;
		}

		static public DataRow SearchForRow( ref GridView _View, int _RowHandle,  string _SearchText, string _ColumnName, bool _Zoom )
		{
			DataRow rc = null;
			try
			{
				//				ColumnFilterInfo Filter = null;
				if( null != _View && 0 < _View.RowCount )
				{
					GridColumn Column = _View.Columns[_ColumnName];
					if( Column != null ) 
					{
						int rhFound = _View.LocateByDisplayText( _RowHandle, Column, _SearchText );

						// focusing the cell
						if( GridControl.InvalidRowHandle != rhFound ) 
						{
							_View.FocusedRowHandle = rhFound;
							_View.FocusedColumn = Column;
							_View.SetMasterRowExpanded( rhFound, true );

							GridView DetailView = (GridView)_View.GetDetailView( rhFound, _View.DefaultRelationIndex );
							if( null != DetailView )
							{
								_View = DetailView;
								if( true == _Zoom )
								{
									_View.ZoomView();
								}
								_View.GridControl.FocusedView = _View;
								if( 0 < _View.RowCount )
								{
									_View.FocusedRowHandle = 0;
									_View.SelectRow( 0 );

									rc = _View.GetDataRow( 0 );
								}
							}
							else
							{
								rc = _View.GetDataRow( rhFound );
							}
						}
						//						Column.FilterInfo = Filter;

					}
					if( null == rc )
					{
						// TODO: Search through the child views for the item.
					}
				}
			}
			catch
			{
				throw;
			}
			return rc;
		}


		
		static public int LocateRowByMultipleValues( ColumnView _View, GridColumn[] _Columns, object[] _Values, int _StartRowHandle ) 
		{
			int rc = DevExpress.XtraGrid.GridControl.InvalidRowHandle;
			try
			{
				// checking whether the arrays have the same length
				if (_Columns.Length == _Values.Length) 
				{
					// obtaining the number of data rows within the view
					int dataRowCount;
					if (_View is CardView)
					{
						dataRowCount = (_View as CardView).RowCount;
					}
					else
					{
						dataRowCount = (_View as GridView).DataRowCount;
					}
					// traversing the data rows to find a match
					bool match;
					object currValue;
					for (int currentRowHandle = _StartRowHandle; currentRowHandle < dataRowCount; currentRowHandle++) 
					{
						match = true;
						for( int i = 0; i < _Columns.Length; i++ ) 
						{
							currValue = _View.GetRowCellValue(currentRowHandle, _Columns[i]);
							if( false == (_Values[i] is System.Array) )	//Arrays are not comparable
							{									//this excludes UpdatedCounter or any other 'Byte[]' column
								if( null == currValue || false == currValue.Equals( _Values[i] ) )
								{
									match = false;
								}
							}
						}
						if( true == match )
						{
							rc = currentRowHandle;
							break;
						}
					}
				}
			}
			catch
			{
				throw;
			}
			return rc;
		}


		public static void FormatLookUpEdit( DevExpress.XtraEditors.LookUpEdit _Lookup, object _DataSource, string _Display, string _Value )
		{
			try
			{
				object Current = _Lookup.EditValue;
				_Lookup.Properties.DisplayMember = _Display;
				_Lookup.Properties.ValueMember = _Value;
				_Lookup.Properties.DataSource = _DataSource;
				_Lookup.Properties.Columns.Clear();

				LookUpColumnInfo DisplayColumn = new LookUpColumnInfo(_Display, SqlTool.GetReadableColumnName( _Display ), 100);
				_Lookup.Properties.Columns.Add( DisplayColumn );	
				_Lookup.EditValue = Current;
			}
			catch
			{
				throw;
			}
		}

		/// Overloaded to accept an array of display fields defaulting DisplayMember to
		/// the first in the list.
		public static void FormatLookUpEdit( DevExpress.XtraEditors.LookUpEdit _Lookup, 
			object _DataSource, 
			string[] _Display, 
			string _Value )
		{
			try
			{
				FormatLookUpEdit( _Lookup, _DataSource, _Display, 0, _Value );				
			}
			catch
			{
				throw;
			}
		}

		/// Overloaded to accept an array of display fields. Requires int (_DisplayChoice) to
		/// specify which is to become the DisplayMember; default is 0.
		public static void FormatLookUpEdit( DevExpress.XtraEditors.LookUpEdit _Lookup, 
			object _DataSource, 
			string[] _Display, 
			int _DisplayChoice,
			string _Value )
		{
			try
			{
				if( 0 < _DisplayChoice || _Display.Length <= _DisplayChoice ) 
					_DisplayChoice = 0;
				LookUpColumnInfo DisplayColumn;
				object Current = _Lookup.EditValue;
				_Lookup.Properties.DisplayMember = _Display[_DisplayChoice];
				_Lookup.Properties.ValueMember = _Value;
				_Lookup.Properties.DataSource = _DataSource;
				_Lookup.Properties.Columns.Clear();

				for( int i = 0; i < _Display.Length; i++ )
				{
					DisplayColumn = new LookUpColumnInfo( _Display[i], 
						SqlTool.GetReadableColumnName( _Display[i] ), 100);
					_Lookup.Properties.Columns.Add( DisplayColumn );	
				}
				_Lookup.EditValue = Current;
			}
			catch
			{
				throw;
			}
		}

		public static void SetEditable( ColumnView _Grid, bool _Editable )
		{
			try
			{
				GridDataView tmpView = _Grid.DataSource as GridDataView;
				if( null != tmpView )
				{
					foreach( DataColumn Col in tmpView.Table.Columns )
					{
						_Grid.OptionsBehavior.Editable = _Editable;
						_Grid.Columns[Col.ColumnName].OptionsColumn.ReadOnly = !_Editable;
						_Grid.Columns[Col.ColumnName].OptionsColumn.AllowEdit = _Editable;
					}
				}
			}
			catch
			{
				throw;
			}
		}

		public static void ExpandAll( GridView _View, bool _Expand ) 
		{
			//Prevent excessive visual updates
			_View.BeginUpdate();
			try 
			{
				for( int i = 0; i < _View.RowCount; ++i )
				{
					_View.SetMasterRowExpanded( i, _Expand );
				}
			}
			finally 
			{
				//Enable visual updates
				_View.EndUpdate();
			}
		}		


		#endregion // Static Members

		#region Footer Display

		virtual public void SetFooterColumn( DevExpress.XtraGrid.Columns.GridColumn _Column, string _Type )
		{
			try
			{
				_Column.SummaryItem.DisplayFormat = _Type + ": {0}";
				_Column.SummaryItem.SummaryType = (DevExpress.Data.SummaryItemType) Enum.Parse( typeof( DevExpress.Data.SummaryItemType ), _Type, true );
			}
			catch
			{
				throw;
			}
		}

		#endregion Footer Display

		virtual protected void Views_StartSorting(object sender, EventArgs e)
		{
			try
			{
				ColumnView View = sender as DevExpress.XtraGrid.Views.Base.ColumnView;
				if( null != View )
				{
					m_FocusedRowBeforeSort = View.GetRow( View.FocusedRowHandle );
				}
				else
				{
					View = sender as DevExpress.XtraGrid.Views.Grid.GridView;
					if( null != View )
					{
						m_FocusedRowBeforeSort = View.GetRow( View.FocusedRowHandle );
					}
				}
			}
			catch
			{
				throw;
			}
		}

		virtual protected void Views_EndSorting(object sender, EventArgs e)
		{
			try
			{
				GridView View = sender as GridView;
				int RowHandle = FindRowHandleByRowObject( View, m_FocusedRowBeforeSort );
				if( GridControl.InvalidRowHandle != RowHandle )
				{
					View.FocusedRowHandle = RowHandle;
					m_FocusedRowBeforeSort = null;
				}
			}
			catch
			{
				throw;
			}
		}

#if SAVE_GRID_LAYOUT

		#region GridLayout Save/Restore

		public void SaveGridLayout( GridControl _Grid, bool _SaveFilters )
		{
			try
			{
				DevExpress.XtraGrid.Views.Base.BaseView View = _Grid.MainView;
				SaveViewLayout( _Grid.MainView, _SaveFilters );
				for( int i = 0; i < _Grid.LevelTree.Nodes.Count; ++i )
				{
					SaveViewLayout( _Grid.LevelTree.Nodes[i].LevelTemplate, _SaveFilters );
				}
			}
			catch
			{
				throw;
			}
		}

		public void SaveViewLayout( BaseView _View, bool _SaveFilters )
		{
			try
			{
#if DEBUG
				string BasePath = Application.StartupPath + @"\Data\GridLayout\";
#else
				string BasePath = Application.UserAppDataPath + @"\Data\GridLayout\";
#endif
				if( false == Directory.Exists( BasePath ) )
				{
					Directory.CreateDirectory( BasePath );
				}
				string Name = GetViewPath( _View );
				if( 0 < _View.Name.Length )
				{
					PF.Utils.Encryption.PFHash Encrypt = new PF.Utils.Encryption.PFHash( );
					Name = Encrypt.Encrypt( Name );
					string Path = BasePath + FixFileName( Name ) + ".xml";

					if( false == System.IO.Directory.Exists( BasePath ) )
					{
						System.IO.Directory.CreateDirectory( BasePath );
					}

					if( false == _SaveFilters )
					{
						RemoveFilters( _View as ColumnView );
					}

					_View.SaveLayoutToXml( Path, DevExpress.Utils.OptionsLayoutBase.FullLayout );
				}
				
			}
			catch
			{
				throw;
			}
		}

		protected string FixFileName( string _Name )
		{
			string rc = string.Empty;
			try
			{
				rc = _Name.Replace( '|', '!' );
				rc = rc.Replace( '"', '\'' );
				rc = rc.Replace( '?', '&' );
				rc = rc.Replace( '*', '@' );
				rc = rc.Replace( '<', ',' );
				rc = rc.Replace( '>', '.' );
				rc = rc.Replace( ':', ';' );
				rc = rc.Replace( '/', '%' );
				rc = rc.Replace( '\\', '^' );
			}
			catch
			{
				throw;
			}
			return rc;
		}

		public bool RestoreGridLayout( GridControl _Grid )
		{
			bool rc = false;
			try
			{
				DevExpress.XtraGrid.Views.Base.BaseView View = _Grid.MainView;
				RestoreViewLayout( _Grid.MainView );
				for( int i = 0; i < _Grid.LevelTree.Nodes.Count; ++i )
				{
					rc |= RestoreViewLayout( _Grid.LevelTree.Nodes[i].LevelTemplate );
				}
			}
			catch
			{
				throw;
			}
			return rc;
		}

		public bool RestoreViewLayout( BaseView _View )
		{
			bool rc = false;
			try
			{
				bool BadFile = false;
				if( true == m_RestoreLayout )
				{
#if DEBUG 
					string BasePath = Application.StartupPath + @"\Data\GridLayout\";
#else
					string BasePath = Application.UserAppDataPath + @"\Data\GridLayout\";
#endif
					if( false == Directory.Exists( BasePath ) )
					{
						Directory.CreateDirectory( BasePath );
					}
					
					string Name = GetViewPath( _View );
					if( 0 < Name.Length )
					{
						PF.Utils.Encryption.PFHash Encrypt = new PF.Utils.Encryption.PFHash( );
						Name = Encrypt.Encrypt( Name );
						string Path = BasePath + FixFileName( Name ) + ".xml";
						if( true == System.IO.File.Exists( Path ) )
						{
							DataSet tmp = new DataSet( );
							try
							{
								tmp.ReadXml( Path, XmlReadMode.ReadSchema );
							}
							catch
							{
								//File.Delete( Path );
								File.Move( Path, Path + "." + DateTime.Now.ToFileTime( ).ToString( ) );
								BadFile = true;
							}
							finally
							{
								tmp.Dispose( );
							}

							if( false == BadFile )
							{
								if( false == Utils.IO.XML.XMLFileWorker.HasEqualTagCount( Path ) )
								{
									System.IO.File.Delete( Path );
								}
								else
								{
									/// We originally tried to catch a bad file here, but DevExpress has locked the file
									/// and we cannot Move or Delete it.
									_View.RestoreLayoutFromXml( Path, DevExpress.Utils.OptionsLayoutBase.FullLayout );
							
									ColumnView View = _View as ColumnView;

									for( int i = 0; i < View.Columns.Count; ++i )
									{
										string ColumnName = View.Columns[i].FieldName;

										if( ColumnName.StartsWith( "HIDE_" ) || ColumnName.StartsWith( "_HIDE_" ) )
										{
											View.Columns[i].VisibleIndex = -1;
											CustomHideColumnProcessing( View, ColumnName );
										}
										else if( true == ReadOnly )
										{
											//Turn ReadOnly OFF and NonEditable ON for the column
											View.Columns[i].OptionsColumn.ReadOnly = true;
											View.Columns[i].OptionsColumn.AllowEdit = false;
										}

									}
									rc = true;
								}
							}
						}
					}
				}
			}
			catch
			{
				throw;
			}
			return rc;
		}
		#endregion // GridLayout Save/Restore
#endif

		private void Views_Click(object sender, EventArgs e)
		{
			try
			{
				BaseView View = sender as BaseView;
				Int64 ModKeys = (Int64)( Keys.Control | Keys.Shift | Keys.Alt );
				if( (Int64)Control.ModifierKeys == ModKeys )
				{
					string Path = GetViewPath( View );
					MessageBox.Show( Path );
				}
			}
			catch
			{
				throw;
			}
		}
		
		protected string GetViewPath( BaseView _View )
		{
			string rc = string.Empty;
			try
			{
				rc = _View.Name;
				for( Control Parent = _View.GridControl.Parent; null != Parent; Parent = Parent.Parent )
				{
					rc = Parent.Name + "\\" + rc;
				}

			}
			catch
			{
				throw;
			}
			return rc;
		}

		private void Views_MasterRowExpanding(object sender, MasterRowCanExpandEventArgs e)
		{
			try
			{
				if( 0 != e.RelationIndex )
				{
					e.Allow = false;
				}
			}
			catch
			{
				throw;
			}
		}
	}
}
