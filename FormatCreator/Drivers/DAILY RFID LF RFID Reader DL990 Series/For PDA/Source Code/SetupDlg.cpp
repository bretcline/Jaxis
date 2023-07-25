// SetupDlg.cpp : implementation file
//

#include "stdafx.h"
#include "SerialPortWM5.h"
#include "SetupDlg.h"


// CSetupDlg dialog

IMPLEMENT_DYNAMIC(CSetupDlg, CDialog)

CSetupDlg::CSetupDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CSetupDlg::IDD, pParent)
	, m_Enter(FALSE)
{

}

CSetupDlg::~CSetupDlg()
{
}

void CSetupDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_STOP, m_StopBits);
	DDX_Control(pDX, IDC_PORT, m_PortNo);
	DDX_Control(pDX, IDC_PARITY, m_Parity);
	DDX_Control(pDX, IDC_DATA, m_DataBits);
	DDX_Control(pDX, IDC_BAUD, m_BaudRate);
	DDX_Control(pDX, IDC_SP, m_Separator);
	DDX_Check(pDX, IDC_CHECK_ENTER, m_Enter);
	DDX_Check(pDX, IDC_EN, m_EN);
}


BEGIN_MESSAGE_MAP(CSetupDlg, CDialog)
	ON_CBN_SELCHANGE(IDC_PORT, OnSelchangePort)
	ON_CBN_SELCHANGE(IDC_BAUD, OnSelchangeBaud)
	ON_CBN_SELCHANGE(IDC_DATA, OnSelchangeData)
	ON_CBN_SELCHANGE(IDC_PARITY, OnSelchangeParity)
	ON_CBN_SELCHANGE(IDC_STOP, OnSelchangeStop)
	ON_BN_CLICKED(IDC_CHECK_ENTER, &CSetupDlg::OnBnClickedCheckEnter)
	ON_BN_CLICKED(IDOK, &CSetupDlg::OnBnClickedOk)
	ON_CBN_SELCHANGE(IDC_SP,OnSelchangeSp)
	ON_BN_CLICKED(IDC_EN, &CSetupDlg::OnBnClickedEn)
END_MESSAGE_MAP()


// CSetupDlg message handlers

BOOL CSetupDlg::OnInitDialog() 
{
	CDialog::OnInitDialog();
	
	m_StopBits.SetCurSel(StopBits);
	m_PortNo.SetCurSel(PortNo);
	m_Parity.SetCurSel(Parity);
	m_BaudRate.SetCurSel(BaudRate);
	m_DataBits.SetCurSel(DataBits);
	m_Separator.SetCurSel(Separator);
	m_Enter=Enter;
	m_EN=EN;
	((CButton*)GetDlgItem(IDC_CHECK_ENTER))->SetCheck(Enter);
	((CButton*)GetDlgItem(IDC_EN))->SetCheck(EN);
	CWnd *tmp=GetDlgItem(IDC_SP); 
	tmp->EnableWindow(Enter); 
	return TRUE;  // return TRUE unless you set the focus to a control
	              // EXCEPTION: OCX Property Pages should return FALSE
}

void CSetupDlg::OnSelchangePort() 
{
	if(m_PortNo.GetCurSel()!=CB_ERR)
	{
		PortNo=m_PortNo.GetCurSel();
	}
}

void CSetupDlg::OnSelchangeBaud() 
{
	if(m_BaudRate.GetCurSel()!=CB_ERR)
	{
		BaudRate=m_BaudRate.GetCurSel();
	}
}

void CSetupDlg::OnSelchangeData() 
{
	if(m_DataBits.GetCurSel()!=CB_ERR)
	{
		DataBits=m_DataBits.GetCurSel();
	}
}

void CSetupDlg::OnSelchangeParity() 
{
	if(m_Parity.GetCurSel()!=CB_ERR)
	{
		Parity=m_Parity.GetCurSel();
	}
}

void CSetupDlg::OnSelchangeStop() 
{
	if(m_StopBits.GetCurSel()!=CB_ERR)
	{
		StopBits=m_StopBits.GetCurSel();
	}
}

void CSetupDlg::OnBnClickedCheckEnter()
{
	Enter=!Enter;
	CWnd *tmp=GetDlgItem(IDC_SP); 
	tmp->EnableWindow(Enter); 
}

void CSetupDlg::OnBnClickedOk()
{
	// TODO: 在此添加控件通知处理程序代码
	OnOK();
}

void CSetupDlg::OnSelchangeSp()
{
	if(m_Separator.GetCurSel()!=CB_ERR)
	{
		Separator=m_Separator.GetCurSel();
	}
}

void CSetupDlg::OnBnClickedEn()
{
	EN=!EN;
	// TODO: 在此添加控件通知处理程序代码
}
