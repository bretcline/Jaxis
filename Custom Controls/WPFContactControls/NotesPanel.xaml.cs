using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ContactDataInterfaces;

namespace WPFContactControls
{
    /// <summary>
    /// Interaction logic for NotesPanel.xaml
    /// </summary>
    public partial class NotesPanel : UserControl
    {
        protected INotes m_NotesData;
        public INotes NotesData
        {
            get
            {
                m_NotesData.NoteTitle = txtNoteTitle.Text;
                m_NotesData.Note = txtNote.Text;
                return m_NotesData;
            }
            set
            {
                m_NotesData = value;
                txtNoteTitle.Text = m_NotesData.NoteTitle;
                txtNote.Text = m_NotesData.Note;
            }
        }

        public NotesPanel()
        {
            InitializeComponent();
        }
    }
}
