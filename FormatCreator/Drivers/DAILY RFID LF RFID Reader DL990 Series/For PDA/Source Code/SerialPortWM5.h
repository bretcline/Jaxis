// SerialPortWM5.h : PROJECT_NAME Ӧ�ó������ͷ�ļ�
//

#pragma once

#ifndef __AFXWIN_H__
	#error "�ڰ������ļ�֮ǰ������stdafx.h�������� PCH �ļ�"
#endif

#ifdef POCKETPC2003_UI_MODEL
#include "resourceppc.h"
#endif 

// CSerialPortWM5App:
// �йش����ʵ�֣������ SerialPortWM5.cpp
//

class CSerialPortWM5App : public CWinApp
{
public:
	CSerialPortWM5App();
	
// ��д
public:
	virtual BOOL InitInstance();

// ʵ��

	DECLARE_MESSAGE_MAP()
};

extern CSerialPortWM5App theApp;
