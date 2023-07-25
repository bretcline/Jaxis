#pragma once

#include "resourceppc.h"
// CSetupDlg dialog

class CSetupDlg : public CDialog
{
	DECLARE_DYNAMIC(CSetupDlg)

public:
	CSetupDlg(CWnd* pParent = NULL);   // standard constructor
	virtual ~CSetupDlg();

	int StopBits;
	int PortNo;
	int Parity;
	int DataBits;
	int BaudRate,Separator;
	BOOL Enter,EN;

// Dialog Data
	enum { IDD = IDD_SETUP_DIALOG };
	CComboBox	m_StopBits;
	CComboBox	m_PortNo;
	CComboBox	m_Parity;
	CComboBox	m_DataBits;
	CComboBox	m_BaudRate;
	CComboBox	m_Separator;

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	virtual BOOL OnInitDialog();

	afx_msg void OnSelchangePort();
	afx_msg void OnSelchangeBaud();
	afx_msg void OnSelchangeData();
	afx_msg void OnSelchangeParity();
	afx_msg void OnSelchangeStop();
	afx_msg void OnSelchangeSp();

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedCheckEnter();
public:
	BOOL m_Enter,m_EN;
public:
	afx_msg void OnBnClickedOk();
public:
	afx_msg void OnBnClickedEn();
};
