// friend1.h : main header file for the FRIEND1 application
//

#if !defined(AFX_FRIEND1_H__ECD7EBE8_2100_4422_99D3_CED406408F69__INCLUDED_)
#define AFX_FRIEND1_H__ECD7EBE8_2100_4422_99D3_CED406408F69__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CFriend1App:
// See friend1.cpp for the implementation of this class
//

class CFriend1App : public CWinApp
{
public:
	CFriend1App();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CFriend1App)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CFriend1App)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_FRIEND1_H__ECD7EBE8_2100_4422_99D3_CED406408F69__INCLUDED_)
