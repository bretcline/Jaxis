// CTabMoreInformation.cpp : implementation file
//

#include "stdafx.h"
#include "friend1.h"
#include "CTabMoreInformation.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CCTabMoreInformation dialog


CCTabMoreInformation::CCTabMoreInformation(CWnd* pParent /*=NULL*/)
	: CDialog(CCTabMoreInformation::IDD, pParent)
{
	//{{AFX_DATA_INIT(CCTabMoreInformation)
		// NOTE: the ClassWizard will add member initialization here
	//}}AFX_DATA_INIT

	pReader = NULL;
}


void CCTabMoreInformation::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CCTabMoreInformation)
		DDX_Control(pDX, IDC_COMBO1, m_port);
		DDX_Control(pDX, IDC_COMBO2, m_bp);
		DDX_Control(pDX, IDC_COMBO3, m_adress);
	//}}AFX_DATA_MAP
}


BEGIN_MESSAGE_MAP(CCTabMoreInformation, CDialog)
	//{{AFX_MSG_MAP(CCTabMoreInformation)
		// NOTE: the ClassWizard will add message map macros here
		ON_BN_CLICKED(IDC_BUTTON1, OnOpen)
		ON_BN_CLICKED(IDC_BUTTON2, OnClose)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CCTabMoreInformation message handlers
void CCTabMoreInformation::OnOk() 
{
	// TODO: Add your control notification handler code here
	
}

void CCTabMoreInformation::SetReader(CAceReader *pReader)
{
	if(pReader == NULL)
		return;
	else
		this->pReader = pReader;
}

BOOL CCTabMoreInformation::OnInitDialog() 
{
	CDialog::OnInitDialog();
	
	m_port.AddString("COM1");
	m_port.AddString("COM2");
	m_port.AddString("COM3");
	m_port.AddString("COM4");
	m_port.AddString("COM5");
	m_port.AddString("COM6");
	m_port.AddString("COM7");
	m_port.AddString("COM8");
	m_port.SetCurSel(0);

	m_bp.AddString("9600");
	m_bp.AddString("19200");
	m_bp.AddString("38400");
	m_bp.AddString("57600");
	m_bp.AddString("115200");
	m_bp.SetCurSel(3);

	m_adress.AddString("00");
	m_adress.AddString("01");
	m_adress.AddString("02");
	m_adress.AddString("03");
	m_adress.AddString("04");
	m_adress.AddString("05");
	m_adress.AddString("06");
	m_adress.AddString("07");
	m_adress.AddString("08");
	m_adress.SetCurSel(0);

	((CButton *)GetDlgItem(IDC_BUTTON2))->EnableWindow(false);
	((CButton *)GetDlgItem(IDC_BUTTON1))->EnableWindow(true);


	return TRUE;  // return TRUE unless you set the focus to a control
	              // EXCEPTION: OCX Property Pages should return FALSE
}
int GetBP(int index)
{
	switch(index)
	{
	case 1: return 9600;
	case 2: return 19200;
	case 3: return 38400;
	case 4: return 57600;
	case 5: return 115200;
	};
	return 9600;

}
void CCTabMoreInformation::OnOpen() 
{
	int bp = GetBP(m_bp.GetCurSel()+1);
	int re = pReader->OpenComm(m_port.GetCurSel()+1,bp);
	if(re == 0)
	{
		((CButton *)GetDlgItem(IDC_BUTTON1))->EnableWindow(false);
		((CButton *)GetDlgItem(IDC_BUTTON2))->EnableWindow(true);
	}
	else
	{
		((CButton *)GetDlgItem(IDC_BUTTON2))->EnableWindow(false);
		((CButton *)GetDlgItem(IDC_BUTTON1))->EnableWindow(true);
	}
	
}

void CCTabMoreInformation::OnClose() 
{
	int re = pReader->CloseComm();
	if(re == 1)
	{
		((CButton *)GetDlgItem(IDC_BUTTON1))->EnableWindow(true);
		((CButton *)GetDlgItem(IDC_BUTTON2))->EnableWindow(false);
	}
	else
	{
		((CButton *)GetDlgItem(IDC_BUTTON1))->EnableWindow(false);
		((CButton *)GetDlgItem(IDC_BUTTON2))->EnableWindow(true);
	}

//	re = pReader-
}