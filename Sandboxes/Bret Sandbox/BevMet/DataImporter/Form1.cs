using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using JaxisExtensions;
using System.Data.Common;
using System.Diagnostics;

namespace DataImporter
{
    public partial class Form1 : Form
    {
        protected List<MaterialType> m_Types = new List<MaterialType>( );
        protected List<MaterialType> m_SubTypes = new List<MaterialType>( );
        protected Dictionary<string, TreeNode> m_Nodes = new Dictionary<string, TreeNode>( );
        protected EntityConfiguration m_Data;

        protected TreeNode m_CurrentNode = null;
        
        public Form1( )
        {
            InitializeComponent( );
        }

        private void btnLoad_Click( object sender, EventArgs e )
        {
            m_Data = Open( txtPath.Text );

            Reload( );
        }

        private void Reload( )
        {
            m_Types.Clear( );
            m_SubTypes.Clear( );
            m_Nodes.Clear( );
            tvMaterial.Nodes.Clear( );

            if( null != m_Data )
            {
                txtSAASOrg.Text = m_Data.MaterialTypeList[0].saasOrg;
                int Count = 0;
                bool MoreLevels = true;
                List<Bitmap> Images = new List<Bitmap>( );

                List<MaterialType> MTList = m_Data.MaterialTypeList.OrderBy( MT => MT.materialType ).ToList( );
                m_Data.MaterialList = m_Data.MaterialList.OrderBy( M => M.shortDescription ).ToArray( );


                IEnumerable<MaterialType> Misplaced = MTList.Where( MT => MT.materialType.Equals( "MISPLACED" ) );
                if( 0 == Misplaced.Count() )
                {
                    MaterialType MisplacedItem = new MaterialType( );
                    MisplacedItem.description = "MISPLACED";
                    MisplacedItem.materialType = "MISPLACED";
                    MisplacedItem.parent = "MISPLACED";
                    MisplacedItem.levelToRoot = "0";
                    MTList.Add( MisplacedItem );


                }
                m_Data.MaterialTypeList = MTList.ToArray( );
                while( true == MoreLevels )
                {
                    IEnumerable<MaterialType> MaterialTypes = m_Data.MaterialTypeList.Where( M => M.levelToRoot.Equals( Count.ToString( ) ) );
                    if( 0 < MaterialTypes.Count( ) )
                    {
                        foreach( MaterialType MT in MaterialTypes )
                        {
                            MT.description = MT.description.Replace( "\'", "" );

                            TreeNode Node = new TreeNode( MT.materialType );
                            Node.Tag = MT;
                            if( !m_Nodes.ContainsKey( MT.materialType ) )
                            {
                                m_Nodes.Add( MT.materialType, Node );
                            }
                            if( 0 == Count )
                            {
                                tvMaterial.Nodes.Add( Node );
                            }
                            else if( 1 == Count )
                            {
                                m_Types.Add( MT );
                            }
                            else if( 2 == Count )
                            {
                                m_SubTypes.Add( MT );
                            }

                            if( m_Nodes.ContainsKey( MT.parent ) && MT.materialType != MT.parent )
                            {
                                m_Nodes[MT.parent].Nodes.Add( Node );
                            }
                            else if ( 0 < Count )
                            {
                                Debug.Print( string.Format( "No Parent {0} - {1}", MT.description, MT.materialType ) );
                                MT.parent = "MISPLACED";
                                m_Nodes[MT.parent].Nodes.Add( Node );
                            }
                        }
                        ++Count;
                    }
                    else
                    {
                        MoreLevels = false;
                    }
                }

                foreach( Material T in m_Data.MaterialList )
                {
                    T.shortDescription = T.shortDescription.Replace( "\'", "" );

                    
                    int index = T.volume.IndexOf( '.' );
                    if( !T.shortDescription.Contains ( T.volume.Substring( 0, ( 0 < index ) ? index : T.volume.Length ) ) )
                    {
                        T.shortDescription = string.Format( "{0} {1} {2}", T.shortDescription, T.volume, T.volumeUom );

                    }

                    if( null != T && null != T.materialType && m_Nodes.ContainsKey( T.materialType ) )
                    {
                        TreeNode Node = new TreeNode( T.shortDescription );
                        Node.Tag = T;
                        m_Nodes[T.materialType].Nodes.Add( Node );
                    }
                    else
                    {
                        Debug.Print( string.Format( "{0} - {1} : {2}", T.materialType, T.materialNumber, T.shortDescription ) );
                        List<MaterialType> Lst = m_Data.MaterialTypeList.Where( M => M.materialType.StartsWith( T.shortDescription ) ).ToList( );
                        if( 0 < Lst.Count )
                        {
                            T.materialType = Lst[0].materialType;
                            TreeNode Node = new TreeNode( T.shortDescription );
                            Node.Tag = T;
                            m_Nodes[T.materialType].Nodes.Add( Node );
                        }
                        //MessageBox.Show( string.Format( "{0} - {1}", T.shortDescription, T.materialNumber ) );
                    }
                }
                if( 0 < Images.Count )
                {
                    //                    pictureEdit1.Image = Images[0];
                }
            }
        }

        public EntityConfiguration Open( string _XmlFile )
        {
            StreamReader stream = null;
            XmlTextReader reader = null;
            try
            {
                string Cleaned;
                using( StreamReader Reader = new StreamReader( _XmlFile ) )
                {
                    Cleaned = Reader.ReadToEnd( );
                    Cleaned = Cleaned.Replace( "ñ", "n" );
                }
                using( StreamWriter Writer = new StreamWriter( _XmlFile, false ) )
                {
                    Writer.Write( Cleaned );
                }

                // serialise to object

                XmlSerializer serializer = new XmlSerializer( typeof( EntityConfiguration ) );

                stream = new StreamReader( _XmlFile ); // read xml data

                reader = new XmlTextReader( stream );  // create reader

                // covert reader to object

                return (EntityConfiguration)serializer.Deserialize( reader );
            }
            catch( Exception err )
            {
                return null;
            }
            finally
            {
                if( stream != null ) stream.Close( );
                if( reader != null ) reader.Close( );
            }
        }
        6
        public void Save( string _XmlFile, EntityConfiguration _Entity )
        {
            StreamWriter stream = null;
            try
            {
                // serialise to object
                stream = new StreamWriter( _XmlFile, false ); // read xml data
                // covert reader to object

                XmlSerializer xs = new XmlSerializer( typeof( EntityConfiguration ) );
                {
                    UTF8Encoding encoding = new UTF8Encoding( );
                    xs.Serialize( stream, _Entity );
                }
            }
            catch( Exception err )
            {
            }
            finally
            {
                if( stream != null ) stream.Close( );
            }
        }

        public bool GetBmpFileFromXmlEx( string _Data, out Bitmap bmp )
        {
            bmp = null;
            try
            {
                Convert.FromBase64String( _Data );
                byte[] buff = Convert.FromBase64String( _Data );
                MemoryStream mem = new MemoryStream( buff );
                bmp = new Bitmap( mem );
            }
            catch( Exception e )
            {
                MessageBox.Show( e.ToString( ), "Error!!" );
            }
            return true;
        }

        private void btnOpenFile_Click( object sender, EventArgs e )
        {
            if( DialogResult.OK == ofdImportFile.ShowDialog( ) )
            {
                txtPath.Text = ofdImportFile.FileName;
            }
        }

        private void tvMaterial_NodeMouseClick( object sender, TreeNodeMouseClickEventArgs e )
        {
            //m_CurrentNode = e.Node;
            //MaterialType PType = null;
            //if( e.Node.Tag is Material )
            //{
            //    Material Mat = e.Node.Tag as Material;

            //    txtUPC.Text = Mat.materialNumber;
            //    txtVolumn.Text = Mat.volume;
            //    txtName.Text = Mat.shortDescription;

            //    IEnumerable<MaterialType> Parent = m_Data.MaterialTypeList.Where( M => M.materialType == Mat.materialType );
            //    if( 0 < Parent.Count( ) )
            //    {
            //        int Count = 0;
            //        while ( 100 > Count )
            //        {
            //            if( 0 < Parent.Count( ) )
            //            {
            //                PType = Parent.First( );
            //                if( PType != null )
            //                {
            //                    IEnumerable<MaterialType> SubType = m_SubTypes.Where( M => M.materialType == PType.materialType );
            //                    if( 0 == SubType.Count( ) )
            //                    {
            //                        Parent = m_Data.MaterialTypeList.Where( M => M.materialType == PType.parent );
            //                    }
            //                    else
            //                    {
            //                        PType = SubType.First( );
            //                        break;
            //                    }
            //                }
            //            }
            //            ++Count;
            //        }
            //        cmbSubType.DataSource = m_SubTypes.Where( T => T.parent == PType.parent ).ToList( );
            //        cmbSubType.DisplayMember = "materialType";
            //        cmbSubType.SelectedItem = PType;

            //        cmbParent.DataSource = m_Data.MaterialTypeList.Where( M => M.materialType == Mat.materialType ).ToList( );
            //        cmbParent.DisplayMember = "materialType";

            //        if( !string.IsNullOrWhiteSpace( Mat.ImageData ) )
            //        {
            //            Bitmap Pict;
            //            GetBmpFileFromXmlEx( Mat.ImageData, out Pict );
            //            pbImage.Image = Pict;
            //        }
            //    }
            //}
            //else if( e.Node.Tag is MaterialType )
            //{
            //    MaterialType Mat = e.Node.Tag as MaterialType;

            //    txtUPC.Text = string.Empty;
            //    txtVolumn.Text = string.Empty;
            //    txtName.Text = Mat.materialType;

            //    IEnumerable<MaterialType> Parent = m_Data.MaterialTypeList.Where( M => M.materialType == Mat.materialType );
            //    if( 0 < Parent.Count( ) )
            //    {
            //        int Count = 0;
            //        while( 100 > Count )
            //        {
            //            PType = Parent.First( );
            //            if( PType != null )
            //            {
            //                IEnumerable<MaterialType> SubType = m_SubTypes.Where( M => M.materialType == PType.materialType );
            //                if( 0 == SubType.Count( ) )
            //                {
            //                    Parent = m_Data.MaterialTypeList.Where( M => M.materialType == PType.parent );
            //                    ++Count;
            //                }
            //                else
            //                {
            //                    PType = SubType.First( );
            //                    break;
            //                }
            //            }
            //        }
            //        cmbSubType.DataSource = null;

            //        cmbParent.DataSource = m_Data.MaterialTypeList.Where( M => M.materialType == Mat.parent ).ToList( );
            //        cmbParent.DisplayMember = "materialType";

            //        if( !string.IsNullOrWhiteSpace( Mat.ImageData ) )
            //        {
            //            Bitmap Pict;
            //            GetBmpFileFromXmlEx( Mat.ImageData, out Pict );
            //            pbImage.Image = Pict;
            //        }
            //        else
            //        {
            //            pbImage.Image = null;
            //        }
            //    }
            //}
        }

        public byte[] imageToByteArray( Image imageIn )
        {
         MemoryStream ms = new MemoryStream();
         imageIn.Save(ms,System.Drawing.Imaging.ImageFormat.Gif);
         return  ms.ToArray();
        }

        private void btnUpdate_Click( object sender, EventArgs e )
        {
            if( m_CurrentNode.Tag is Material )
            {
                Material Mat = m_CurrentNode.Tag as Material;

                Mat.materialNumber = txtUPC.Text;
                Mat.volume = txtVolumn.Text;
                Mat.shortDescription = txtName.Text;
                Mat.materialType = cmbParent.Text;
                if( null != pbImage.Image )
                {
                    Mat.ImageData = Convert.ToBase64String( imageToByteArray( pbImage.Image ) );
                }

                //    cmbSubType.SelectedItem = PType;
            }
            else if( m_CurrentNode.Tag is MaterialType )
            {
                MaterialType Mat = m_CurrentNode.Tag as MaterialType;

                Mat.materialType = txtName.Text;
                Mat.parent = cmbParent.Text;
                if( null != pbImage.Image )
                {
                    Mat.ImageData = Convert.ToBase64String( imageToByteArray( pbImage.Image ) );
                }
            }
            m_CurrentNode.Text = txtName.Text;
        }

        private void btnImage_Click( object sender, EventArgs e )
        {
            if( DialogResult.OK == ofdImportFile.ShowDialog( ) )
            {
                Image I = new Bitmap( ofdImportFile.FileName );
                pbImage.Image = I;
            }
        }

        private void btnRemove_Click( object sender, EventArgs e )
        {
            if( DialogResult.OK == MessageBox.Show( string.Format( "Do you want to delete {0}?", m_CurrentNode.Text ), "Delete", MessageBoxButtons.OKCancel ) )
            {
                RemoveNodes( m_CurrentNode );
            }
        }

        protected void RemoveNodes( TreeNode _Node )
        {
            if( null != _Node )
            {
                if( _Node.Tag is Material )
                {
                    List<Material> Lst = m_Data.MaterialList.ToList( );
                    Lst.Remove( _Node.Tag as Material );
                    m_Data.MaterialList = Lst.ToArray( );
                }
                else if( _Node.Tag is MaterialType )
                {
                    if( 0 < _Node.Nodes.Count )
                    {
                        TreeNode[] NodeList = _Node.Nodes.ToListType<TreeNode>( ).ToArray( );
                        for( int i = NodeList.Count( ) - 1; i >= 0; --i )
                        {
                            RemoveNodes( NodeList[i] );
                        }
                    }
                    List<MaterialType> Lst = m_Data.MaterialTypeList.ToList( );
                    Lst.Remove( _Node.Tag as MaterialType );
                    m_Data.MaterialTypeList = Lst.ToArray( );
                }

                tvMaterial.Nodes.Remove( _Node );
            }
        }


        private void btnAddGroup_Click( object sender, EventArgs e )
        {
            MaterialType M = null;
            if( m_CurrentNode.Tag is Material )
            {
                Material Mat = m_CurrentNode.Tag as Material;
                M = Mat.ToType<MaterialType>( );

                TreeNode Node = new TreeNode( M.materialType );
                Node.Tag = M;
                m_CurrentNode.Parent.Nodes.Add( Node );

            }
            else if( m_CurrentNode.Tag is MaterialType )
            {
                MaterialType Mat = m_CurrentNode.Tag as MaterialType;
                M = Mat.ToType<MaterialType>( );
                M.parent = Mat.materialType;

                TreeNode Node = new TreeNode( M.materialType );
                Node.Tag = M;
                m_CurrentNode.Nodes.Add( Node );
            }
            List<MaterialType> Lst = m_Data.MaterialTypeList.ToList( );
            Lst.Add( M );
            m_Data.MaterialTypeList = Lst.ToArray( );
        }

        private void btnAddMaterial_Click( object sender, EventArgs e )
        {
            TreeNode Node = new TreeNode( );
            Material M = null;
            if( m_CurrentNode.Tag is Material )
            {
                Material Mat = m_CurrentNode.Tag as Material;
                M = Mat.ToType<Material>( );
                Node.Text = M.materialType;
                Node.Tag = M;
                m_CurrentNode.Parent.Nodes.Add( Node );

            }
            else if( m_CurrentNode.Tag is MaterialType )
            {
                MaterialType Mat = m_CurrentNode.Tag as MaterialType;
                M = Mat.ToType<Material>( );
                M.shortDescription = Mat.materialType;

                Node.Text = M.materialType;
                Node.Tag = M;
                m_CurrentNode.Nodes.Add( Node );
            }
            List<Material> Lst = m_Data.MaterialList.ToList( );
            Lst.Add( M );
            m_Data.MaterialList = Lst.ToArray( );

            tvMaterial.SelectedNode = Node;
        }

        private void btnSave_Click_1( object sender, EventArgs e )
        {
            Save( txtPath.Text + ".out.xml", m_Data );
        }

        private void tvMaterial_AfterSelect( object sender, TreeViewEventArgs e )
        {
            m_CurrentNode = e.Node;
            MaterialType PType = null;
            if( e.Node.Tag is Material )
            {
                Material Mat = e.Node.Tag as Material;

                txtUPC.Text = Mat.materialNumber;
                txtVolumn.Text = Mat.volume;
                txtName.Text = Mat.shortDescription;

                IEnumerable<MaterialType> Parent = m_Data.MaterialTypeList.Where( M => M.materialType == Mat.materialType );
                if( 0 < Parent.Count( ) )
                {
                    int Count = 0;
                    while( 100 > Count )
                    {
                        if( 0 < Parent.Count( ) )
                        {
                            PType = Parent.First( );
                            if( PType != null )
                            {
                                IEnumerable<MaterialType> SubType = m_SubTypes.Where( M => M.materialType == PType.materialType );
                                if( 0 == SubType.Count( ) )
                                {
                                    Parent = m_Data.MaterialTypeList.Where( M => M.materialType == PType.parent );
                                }
                                else
                                {
                                    PType = SubType.First( );
                                    break;
                                }
                            }
                        }
                        ++Count;
                    }
                    cmbSubType.DataSource = m_SubTypes.Where( T => T.parent == PType.parent ).ToList( );
                    cmbSubType.DisplayMember = "materialType";
                    cmbSubType.SelectedItem = PType;

                    cmbParent.DataSource = m_Data.MaterialTypeList.Where( M => M.materialType == Mat.materialType ).ToList( );
                    cmbParent.DisplayMember = "materialType";

                    if( !string.IsNullOrWhiteSpace( Mat.ImageData ) )
                    {
                        Bitmap Pict;
                        GetBmpFileFromXmlEx( Mat.ImageData, out Pict );
                        pbImage.Image = Pict;
                    }
                }
            }
            else if( e.Node.Tag is MaterialType )
            {
                MaterialType Mat = e.Node.Tag as MaterialType;

                txtUPC.Text = string.Empty;
                txtVolumn.Text = string.Empty;
                txtName.Text = Mat.materialType;

                IEnumerable<MaterialType> Parent = m_Data.MaterialTypeList.Where( M => M.materialType == Mat.materialType );
                if( 0 < Parent.Count( ) )
                {
                    int Count = 0;
                    while( 100 > Count )
                    {
                        PType = Parent.First( );
                        if( PType != null )
                        {
                            IEnumerable<MaterialType> SubType = m_SubTypes.Where( M => M.materialType == PType.materialType );
                            if( 0 == SubType.Count( ) )
                            {
                                Parent = m_Data.MaterialTypeList.Where( M => M.materialType == PType.parent );
                                ++Count;
                            }
                            else
                            {
                                PType = SubType.First( );
                                break;
                            }
                        }
                    }
                    cmbSubType.DataSource = null;

                    cmbParent.DataSource = m_Data.MaterialTypeList.Where( M => M.materialType == Mat.parent ).ToList( );
                    cmbParent.DisplayMember = "materialType";

                    if( !string.IsNullOrWhiteSpace( Mat.ImageData ) )
                    {
                        Bitmap Pict;
                        GetBmpFileFromXmlEx( Mat.ImageData, out Pict );
                        pbImage.Image = Pict;
                    }
                    else
                    {
                        pbImage.Image = null;
                    }
                }
            }
        }

        private void btnOpenExcel_Click( object sender, EventArgs e )
        {
            if( DialogResult.OK == ofdImportFile.ShowDialog( ) )
            {
                txtXLS.Text = ofdImportFile.FileName;
            }
        }

        private void btnImport_Click( object sender, EventArgs e )
        {
            string connectionString = string.Format( @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=""{0}"";Extended Properties= ""Excel 8.0;HDR=YES;""", txtXLS.Text );

            DbProviderFactory factory = DbProviderFactories.GetFactory( "System.Data.OleDb" );

            using( DbConnection connection = factory.CreateConnection( ) )
            {
                connection.ConnectionString = connectionString;

                using( DbCommand command = connection.CreateCommand( ) )
                {
                    // Cities$ comes from the name of the worksheet
                    command.CommandText = "SELECT * FROM [Sheet1$]";

                    connection.Open( );

                    using( DbDataReader dr = command.ExecuteReader( ) )
                    {
                        while( dr.Read( ) )
                        {
                            CreateMaterial( dr );
                        }
                    }
                }
                Reload( );
            }
        }

        protected void CreateMaterial( IDataReader _Reader )
        {
            string Volume = _Reader[5].ToString( );
            string UPC = _Reader[0].ToString( );
            string Name = _Reader[2].ToString( ).Replace( "ñ", "n" ).Replace( "\'", "" );
            string ParentName = string.Empty;

            IEnumerable<MaterialType> MTItems = m_Data.MaterialTypeList.Where( M => M.materialType == Name );
            if( 0 == MTItems.Count( ) )
            {
                string Parent = _Reader[3].ToString( );

                IEnumerable<MaterialType> ParentMT = m_Data.MaterialTypeList.Where( M => M.materialType == Name );
                if( 0 < ParentMT.Count( ) )
                {
                    MaterialType MT = new MaterialType( );
                    MT.description = Name;
                    MT.status = "ACTIVE";
                    MT.saasOrg = txtSAASOrg.Text;
                    MT.root = "Beverage";
                    MT.parent = Parent;
                    MT.levelToRoot = "3";
                    MT.headerTable = "ct_material";
                    MT.trackable = "True";
                    MT.materialType = Name;

                    List<MaterialType> MList = m_Data.MaterialTypeList.ToList( );
                    MList.Add( MT );
                    m_Data.MaterialTypeList = MList.ToArray( );

                    ParentName = MT.materialType;
                }
            }
            else
            {
                ParentName = MTItems.First( ).materialType;
            }

            IEnumerable<Material> Items = m_Data.MaterialList.Where( M => M.materialNumber == UPC );
            if( 0 == Items.Count( ) )
            {

                Material Mat = new Material( );
                Mat.status = "ACTIVE";
                Mat.saasOrg = txtSAASOrg.Text;
                Mat.attribute5 = "True";
                Mat.materialNumber = UPC;
                Mat.materialType = ParentName;

                Volume = Volume.Replace( "L.", "" ).Replace( "ml.", "" );
                Mat.volume = ( 3 <= Volume.Length && !Volume.Contains( '.' ) ) ? Volume : (Convert.ToDouble( Volume ) * 1000).ToString( );
                Mat.volumeUom = "mL";

                Mat.shortDescription = string.Format( "{0} {1} {2}", Name, Mat.volume, Mat.volumeUom );

                List<Material> MList = m_Data.MaterialList.ToList( );
                MList.Add( Mat );
                m_Data.MaterialList = MList.ToArray( );
            }

        }

        private void btnAssignSAAS_Click( object sender, EventArgs e )
        {
            foreach( MaterialType T in m_Data.MaterialTypeList )
            {
                T.saasOrg = txtSAASOrg.Text;
            }

            foreach( Material T in m_Data.MaterialList )
            {
                T.saasOrg = txtSAASOrg.Text;
            }

            foreach( Rule T in m_Data.RuleList )
            {
                T.saasOrg = txtSAASOrg.Text;
            }

            foreach( EntityWorkObject T in m_Data.EntityWorkObjectList )
            {
                T.saasOrg = txtSAASOrg.Text;
            }

        }

        private void tvMaterial_DragEnter( object sender, DragEventArgs e )
        {
            e.Effect = DragDropEffects.Move;
        }

        private void tvMaterial_ItemDrag( object sender, ItemDragEventArgs e )
        {
            DoDragDrop( e.Item, DragDropEffects.Move );
        }

        private void tvMaterial_DragDrop( object sender, DragEventArgs e )
        {
            TreeNode NewNode;

            if( e.Data.GetDataPresent( "System.Windows.Forms.TreeNode", false ) )
            {
                Point pt = ( (TreeView)sender ).PointToClient( new Point( e.X, e.Y ) );
                TreeNode DestinationNode = ( (TreeView)sender ).GetNodeAt( pt );
                NewNode = (TreeNode)e.Data.GetData( "System.Windows.Forms.TreeNode" );

                bool Success = false;
                if( NewNode.Tag is Material )
                {
                    MaterialType MT = DestinationNode.Tag as MaterialType;
                    if( null != MT )
                    {
                        Material M = NewNode.Tag as Material;
                        M.materialType = MT.materialType;
                        Success = true;
                    }
                }
                else if( NewNode.Tag is MaterialType )
                {

                    MaterialType MT = DestinationNode.Tag as MaterialType;
                    if( null != MT )
                    {
                        MaterialType M = NewNode.Tag as MaterialType;
                        M.parent = MT.materialType;
                        Success = true;
                    }

                }
                if( true == Success )
                {
                    NewNode.Parent.Nodes.Remove( NewNode );
                    DestinationNode.Nodes.Add( NewNode );
                }


            }
        }
    }
}