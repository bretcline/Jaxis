// SerialPortWM5Dlg.h : 头文件
//

#pragma once

#include "PSerialPort.h"
#include "resourceppc.h"



// CSerialPortWM5Dlg 对话框
class CSerialPortWM5Dlg : public CDialog
{
// 构造
public:
	CSerialPortWM5Dlg(CWnd* pParent = NULL);	// 标准构造函数

	BOOL SaveReceivedToFile(LPCTSTR FileName);

	void toTray();

	void SendK(CString sendstr);
	CString HexToBin(CString shex);
	CString HexToDec(CString shex);
	CString HexToDouble(CString shex);
	CString GetData(CString shex);


// 对话框数据
	enum { IDD = IDD_SERIALPORTWM5_DIALOG };
	CEdit	m_CtrlReceive;
	CString	m_strReceive;
	CString	m_strTransmit;
	CString is_str;
	NOTIFYICONDATA nid;

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV 支持

// 实现
protected:
	HICON m_hIcon;

	// 生成的消息映射函数
	virtual BOOL OnInitDialog();
#if defined(_DEVICE_RESOLUTION_AWARE) && !defined(WIN32_PLATFORM_WFSP)
	afx_msg void OnSize(UINT /*nType*/, int /*cx*/, int /*cy*/);
#endif
	afx_msg void OnSetup();
	afx_msg void OnOpen();
	afx_msg void OnClearRece();
	afx_msg void OnClearSend();
	afx_msg void OnRece();
	afx_msg void OnSend();
	afx_msg void OnDestroy();
	afx_msg void OnCheckHex();
	afx_msg void OnSendFile();
	afx_msg void OnSaveReceived();
	afx_msg void OnCheckHexSend();
	afx_msg void OnTimer(UINT nIDEvent);
	afx_msg LONG OnDataArrivedMsg(WPARAM wParam,LPARAM lParam);
	afx_msg LONG OnShowTask(WPARAM wParam,LPARAM lParam);
	DECLARE_MESSAGE_MAP()

private:
	//变量
	BOOL Open;
	BOOL Receive;
	BOOL wHide;
	BOOL Enter;
	BOOL EN;
	BOOL ib125;
	int PortNo,BaudRate,DataBits,StopBits,Parity,Separator;
	CString PortID,PortIDs[9];
	int BaudRates[8];
	BOOL HexDisplay,HexSend,DecDisplay;
	//串口类变量
	CPSerialPort* m_pSerial;
	
	CString m_strDataReceived;

	static void OnDataArrive(char *data,int length,DWORD userdata);
	afx_msg void OnBnClickedButton1();
	afx_msg void OnBnClickedShow();
	afx_msg void OnBnClickedCheckDec();
	afx_msg void OnBnClicked125();
	afx_msg void OnBnClicked134();
};
