// friend1Dlg.cpp : implementation file
//

#include "stdafx.h"
#include "friend1.h"
#include "friend1Dlg.h"
#include "CTabMoreInformation.h"
//#include "STabCtrl.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CAboutDlg dialog used for App About

CAceReader mAceReader;
class CAboutDlg : public CDialog
{
public:
	CAboutDlg();

// Dialog Data
	//{{AFX_DATA(CAboutDlg)
	enum { IDD = IDD_ABOUTBOX };
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CAboutDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	//{{AFX_MSG(CAboutDlg)
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialog(CAboutDlg::IDD)
{
	//{{AFX_DATA_INIT(CAboutDlg)
	//}}AFX_DATA_INIT
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CAboutDlg)
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialog)
	//{{AFX_MSG_MAP(CAboutDlg)
		// No message handlers
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CFriend1Dlg dialog

CFriend1Dlg::CFriend1Dlg(CWnd* pParent /*=NULL*/)
	: CDialog(CFriend1Dlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CFriend1Dlg)
	m_s = _T("");
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);

	m_pTabInformation = NULL;
	m_pTabOpration    = NULL;
}

void CFriend1Dlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CFriend1Dlg)
	DDX_Control(pDX, IDC_RICHEDIT1, m_mem);
	DDX_Control(pDX, IDC_TAB1, m_TabCtrl);
	DDX_Text(pDX, IDC_S, m_s);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CFriend1Dlg, CDialog)
	//{{AFX_MSG_MAP(CFriend1Dlg)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_NOTIFY(TCN_SELCHANGE, IDC_TAB1, OnSelchangeTab1)
	ON_CBN_EDITCHANGE(IDC_COMBOBOXEX1, OnEditchangeComboboxex1)
	ON_BN_CLICKED(IDC_BUTTON1, OnButton1)
	ON_WM_TIMER()
	ON_NOTIFY(NM_CLICK, IDC_RICHEDIT1, OnClickRichedit1)
	ON_NOTIFY(NM_RCLICK, IDC_RICHEDIT1, OnRclickRichedit1)
	ON_NOTIFY(NM_RDBLCLK, IDC_RICHEDIT1, OnRdblclkRichedit1)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CFriend1Dlg message handlers

BOOL CFriend1Dlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Add "About..." menu item to system menu.

	// IDM_ABOUTBOX must be in the system command range.
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		CString strAboutMenu;
		strAboutMenu.LoadString(IDS_ABOUTBOX);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon
	

	m_pTabInformation = new CCTabMoreInformation;
	m_pTabInformation->Create(CCTabMoreInformation::IDD, &m_TabCtrl);

	m_pTabOpration = new CCTabOpration;
	m_pTabOpration->Create(CCTabOpration::IDD, &m_TabCtrl);

	PSTR pszTabItems[] =
	{
		"System Setting",
		"Commands",
		NULL
	};
	CWnd* pWnd=NULL;m_pTabInformation;
	TCITEM item;
	for(int i=0;i<2;i++)
	{
		item.mask = TCIF_TEXT|TCIF_PARAM|TCIF_IMAGE;
		item.lParam = (LPARAM) pWnd;
		item.pszText = pszTabItems[i];
		int iIndex = 1;
		m_TabCtrl.InsertItem(i, &item);
		if(i==0)
		{
			pWnd = m_pTabInformation;
			pWnd->SetWindowPos(NULL, 10, 40 , 0, 0,
						SWP_FRAMECHANGED | SWP_NOSIZE | SWP_NOZORDER);	
			pWnd->ShowWindow(iIndex ? SW_HIDE : SW_SHOW);
		}
		else
		{	
			pWnd = m_pTabOpration;
			pWnd->SetWindowPos(NULL, 10, 40 , 0, 0,
						SWP_FRAMECHANGED | SWP_NOSIZE | SWP_NOZORDER);	
			pWnd->ShowWindow(iIndex ? SW_HIDE : SW_SHOW);

		}
	}
	m_TabCtrl.AttachControlToTab(m_pTabInformation,0);	// attach first static text to first page
	m_TabCtrl.AttachControlToTab(m_pTabOpration,1);	// attach second static text to second page

	m_TabCtrl.SetCurSel(0);

	CWnd* uu=&m_TabCtrl;
	//m_pTabInformation->m_hWnd;

	m_pTabInformation->pReader = &mAceReader;
	m_pTabOpration->pReader = &mAceReader;
	m_pTabOpration->pMem = &m_mem;

	SetTimer(1,300,NULL);

	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CFriend1Dlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialog::OnSysCommand(nID, lParam);
	}
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CFriend1Dlg::OnPaint() 
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, (WPARAM) dc.GetSafeHdc(), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}

// The system calls this to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CFriend1Dlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIcon;
}

void CFriend1Dlg::OnSelchangeTab1(NMHDR* pNMHDR, LRESULT* pResult) 
{
	// TODO: Add your control notification handler code here
	
	*pResult = 0;
}

void CFriend1Dlg::OnEditchangeComboboxex1() 
{
	// TODO: Add your control notification handler code here
	
}

void CFriend1Dlg::OnButton1() 
{
	m_mem.SetWindowText("");	
}

void CFriend1Dlg::OnTimer(UINT nIDEvent) 
{
	if(mAceReader.GetHComm() == NULL)
		m_s = "reader state: not open.";
	else
		m_s = "reader state: is open.";
	UpdateData(false);

	CDialog::OnTimer(nIDEvent);
}

void CFriend1Dlg::OnClickRichedit1(NMHDR* pNMHDR, LRESULT* pResult) 
{
   
	*pResult = 0;
}

void CFriend1Dlg::OnRclickRichedit1(NMHDR* pNMHDR, LRESULT* pResult) 
{
	m_mem.Copy();
	
	*pResult = 0;
}

void CFriend1Dlg::OnRdblclkRichedit1(NMHDR* pNMHDR, LRESULT* pResult) 
{
	// TODO: Add your control notification handler code here
	 m_mem.Paste();	
	*pResult = 0;
}
