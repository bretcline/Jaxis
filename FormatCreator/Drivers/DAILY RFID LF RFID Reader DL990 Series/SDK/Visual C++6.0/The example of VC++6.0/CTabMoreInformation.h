#if !defined(AFX_CTABMOREINFORMATION_H__F0D50237_9BD0_49F3_AF10_5ABFCE0ADA07__INCLUDED_)
#define AFX_CTABMOREINFORMATION_H__F0D50237_9BD0_49F3_AF10_5ABFCE0ADA07__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// CTabMoreInformation.h : header file
//

/////////////////////////////////////////////////////////////////////////////
// CCTabMoreInformation dialog

class CCTabMoreInformation : public CDialog
{
// Construction
public:
	CCTabMoreInformation(CWnd* pParent = NULL);   // standard constructor
	CAceReader *pReader;

	void SetReader(CAceReader *pReader);

// Dialog Data
	//{{AFX_DATA(CCTabMoreInformation)
	enum { IDD = IDD_MORE_INFORMATION };
	CComboBox	m_port;
	CComboBox	m_bp;
	CComboBox	m_adress;
		// NOTE: the ClassWizard will add data members here
	//}}AFX_DATA


// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CCTabMoreInformation)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:

	// Generated message map functions
	//{{AFX_MSG(CCTabMoreInformation)
	afx_msg void OnOk();
	virtual BOOL OnInitDialog();
	afx_msg void OnOpen();
	afx_msg void OnClose();
		// NOTE: the ClassWizard will add member functions here
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_CTABMOREINFORMATION_H__F0D50237_9BD0_49F3_AF10_5ABFCE0ADA07__INCLUDED_)
