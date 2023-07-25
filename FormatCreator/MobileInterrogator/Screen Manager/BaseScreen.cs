using System.Windows.Forms;

namespace LFI.Mobile.Controls
{
    public partial class BaseScreen : UserControl, IScreen
    {
		//----------------------------------------------------------------------
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseScreen"/> class.
        /// </summary>
        public BaseScreen()
        {
            InitializeComponent();

            Init(string.Empty);
        }

		//----------------------------------------------------------------------
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseScreen"/> class.
        /// </summary>
        /// <param name="refTag">The reference name of the screen.</param>
        public BaseScreen(string refTag)
        {
            InitializeComponent();

            Init(refTag);
        }

		//----------------------------------------------------------------------
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseScreen"/> class.
        /// </summary>
        /// <param name="refTag">The reference name of the screen.</param>
        /// <param name="isModel">if set to <c>true</c> [the control is model].</param>
        public BaseScreen(string refTag, bool isModel)
        {
            InitializeComponent();

            Init(refTag);
            IsModal = isModel;
        }

        #region IScreen Members

        /// <summary>
        /// Gets or sets the header text.
        /// </summary>
        /// <value>The header text.</value>
        public string HeaderText { get; set; }

        /// <summary>
        /// Gets or sets the ref tag.
        /// </summary>
        /// <value>The ref tag.</value>
        public string RefTag { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is modal.
        /// </summary>
        /// <value><c>true</c> if this instance is modal; otherwise, <c>false</c>.</value>
        public bool IsModal { get; protected set; }

        /// <summary>
        /// Gets or sets the screen MGR.
        /// </summary>
        /// <value>The screen MGR.</value>
        public ScreenManager ScreenMgr { get; set; }

        /// <summary>
        /// Gets the control.
        /// </summary>
        /// <value>The control.</value>
        public Control Control 
        {
            get { return this; }
        }

        /// <summary>
        /// Gets or sets the screen results.
        /// </summary>
        /// <value>The screen results.</value>
        public ScreenResult ScreenResult { get; private set; }

        /// <summary>
        /// Activates this instance.
        /// </summary>
        /// <returns></returns>
        public virtual bool Activate()
        {
            return true;
        }

        /// <summary>
        /// Deactivates this instance.
        /// </summary>
        public virtual void Deactivate()
        {
            return;
        }

        /// <summary>
        /// Reactivates this instance.
        /// </summary>
        public virtual void Reactivate()
        {
            Deactivate();
            Activate();
        }

		//----------------------------------------------------------------------
        /// <summary>
        /// Handles the navigation result.
        /// </summary>
        /// <param name="result">The results.</param>
        public virtual void HandleNavigationResults(ScreenResult result)
        {
            return;
        }

		//----------------------------------------------------------------------
        /// <summary>
        /// Builds the left menu.
        /// </summary>
        /// <returns></returns>
        public virtual MenuItem BuildLeftMenu()
        {
            return null;
        }

		//----------------------------------------------------------------------
        /// <summary>
        /// Builds the right menu.
        /// </summary>
        /// <returns></returns>
        public virtual MenuItem BuildRightMenu()
        {
            return null;
        }

        #endregion

		//----------------------------------------------------------------------
        /// <summary>
        /// Inits the specified ref tag.
        /// </summary>
        /// <param name="refTag">The reference name of the screen.</param>
        private void Init(string refTag)
        {
            ScreenResult = new ScreenResult();
            RefTag = refTag;
        }
    }
}