#ifndef INC_STABCTRL
#define INC_STABCTRL

#include <afxtempl.h>
class CSTabCtrl : public CTabCtrl
{
public:
	CSTabCtrl();
public:

public:
	virtual ~CSTabCtrl();
	virtual BOOL AttachControlToTab(CWnd * _pControl,INT _nTabNum);
	virtual int SetCurSel( int nItem );

	// Generated message map functions
protected:
	//{{AFX_MSG(CSTabCtrl)
	afx_msg BOOL OnSelchange(NMHDR* pNMHDR, LRESULT* pResult);
	afx_msg void OnDestroy();
	afx_msg int OnCreate(LPCREATESTRUCT lpCreateStruct);
	afx_msg void OnEnable( BOOL bEnable );
	//}}AFX_MSG

	DECLARE_MESSAGE_MAP()

private:
	class CSTabPage
	{
	private:
		CList <CWnd *, CWnd *> m_ControlList;

	public:
		CSTabPage();
		~CSTabPage();

		BOOL AttachControl (CWnd * _pControl);
		BOOL ShowWindows ( INT _nCmdShow );
		BOOL EnableWindows ( BOOL _bEnable );
	};

	class CPageToControlsMap : public CMap <INT, INT&,CSTabPage *, CSTabPage *>
	{
	public:
		CPageToControlsMap( );
		~CPageToControlsMap( );


		BOOL AttachControl(INT _nTabNum,CWnd * _pControl);
		BOOL ShowWindows ( INT _nCurrPage, INT _nCmdShow );
		BOOL EnableWindows ( INT _nCurrPage, BOOL _bEnable );
		void RemoveAll( );
	};

	INT m_nPrevSel;
	CPageToControlsMap m_TabPagesMap;
};

#endif // INC_STABCTRL
/////////////////////////////////////////////////////////////////////////////
