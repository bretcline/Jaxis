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
    /// Interaction logic for ContactTypePanel.xaml
    /// </summary>
    public partial class ContactTypePanel : UserControl
    {
        protected IContactType m_ContactTypeData;
        public IContactType ContactTypeData
        {
            get
            {
                m_ContactTypeData.Name = txtName.Text;
                m_ContactTypeData.Description = txtDescription.Text;
                return m_ContactTypeData;
            }
            set
            {
                m_ContactTypeData = value;
                txtName.Text = m_ContactTypeData.Name;
                txtDescription.Text = m_ContactTypeData.Description;
            }
        }

        public ContactTypePanel()
        {
            InitializeComponent();
        }
    }
}
