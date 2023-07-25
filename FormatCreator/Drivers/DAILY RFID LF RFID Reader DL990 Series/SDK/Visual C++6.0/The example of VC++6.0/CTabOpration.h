#if !defined(AFX_CTABOPRATION_H__B3D6F8CA_DEBC_4215_908E_B6C07B8D8BA3__INCLUDED_)
#define AFX_CTABOPRATION_H__B3D6F8CA_DEBC_4215_908E_B6C07B8D8BA3__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// CTabOpration.h : header file
//

/////////////////////////////////////////////////////////////////////////////
// CCTabOpration dialog

class CCTabOpration : public CDialog
{
// Construction
public:
	CCTabOpration(CWnd* pParent = NULL);   // standard constructor
	CAceReader *pReader;
	CRichEditCtrl *pMem;

	void SetReader(CAceReader *pReader);
	int  GetData(BYTE *pData,CString str,int maxlen);

// Dialog Data
	//{{AFX_DATA(CCTabOpration)
	enum { IDD = IDD_MORE_INFORMATION2 };
	CComboBox	m_w_start;
	CComboBox	m_w_key;
	CComboBox	m_w_count;
	CComboBox	m_w_key_type;
	CComboBox	m_w_amount_type;
	CComboBox	m_key;
	CComboBox	m_start;
	CComboBox	m_count;
	CComboBox	m_key_type;
	CComboBox	m_amount_type;
	CStatic	m_serial;
	CComboBox	m_adress;
	CComboBox	m_bp;
	CString	m_ser;
	CString	m_data;
	//}}AFX_DATA


// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CCTabOpration)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:

	// Generated message map functions
	//{{AFX_MSG(CCTabOpration)
	virtual BOOL OnInitDialog();
	afx_msg void OnSetAdress();
	afx_msg void OnReadSer();
	afx_msg void OnSetSer();
	afx_msg void OnButton3();
	afx_msg void OnRead();
	afx_msg void OnWrite();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_CTABOPRATION_H__B3D6F8CA_DEBC_4215_908E_B6C07B8D8BA3__INCLUDED_)
