using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SecureRisk.Data.Interfaces;

namespace ContactControls
{
    public partial class NotesPanel : UserControl
    {
        #region Fields

        protected INotes m_NotesData;

        #endregion Fields

        #region Constructors

        public NotesPanel()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Properties

        public INotes NotesData
        {
            get
            {
                m_NotesData.NoteTitle = txtNoteTitle.Text;
                m_NotesData.NoteText = txtNote.Text;
                return m_NotesData;
            }
            set
            {
                m_NotesData = value;
                txtNoteTitle.Text = m_NotesData.NoteTitle;
                txtNote.Text = m_NotesData.NoteText;
            }
        }

        #endregion Properties
    }
}