// friend1Dlg.h : header file
//

#if !defined(AFX_FRIEND1DLG_H__4714EA44_0F84_4975_A304_ECDA94D6537B__INCLUDED_)
#define AFX_FRIEND1DLG_H__4714EA44_0F84_4975_A304_ECDA94D6537B__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CFriend1Dlg dialog
#include "CTabMoreInformation.h"
#include "CTabOpration.h"
#include "STabCtrl.h"
class CFriend1Dlg : public CDialog
{
// Construction
public:
	CFriend1Dlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CFriend1Dlg)
	enum { IDD = IDD_FRIEND1_DIALOG };
	CRichEditCtrl	m_mem;
	CSTabCtrl m_TabCtrl;
	CString	m_s;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CFriend1Dlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON m_hIcon;
	CCTabMoreInformation *m_pTabInformation;
	CCTabOpration *m_pTabOpration;

	// Generated message map functions
	//{{AFX_MSG(CFriend1Dlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnSelchangeTab1(NMHDR* pNMHDR, LRESULT* pResult);
	afx_msg void OnEditchangeComboboxex1();
	afx_msg void OnButton1();
	afx_msg void OnTimer(UINT nIDEvent);
	afx_msg void OnClickRichedit1(NMHDR* pNMHDR, LRESULT* pResult);
	afx_msg void OnRclickRichedit1(NMHDR* pNMHDR, LRESULT* pResult);
	afx_msg void OnRdblclkRichedit1(NMHDR* pNMHDR, LRESULT* pResult);
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_FRIEND1DLG_H__4714EA44_0F84_4975_A304_ECDA94D6537B__INCLUDED_)
