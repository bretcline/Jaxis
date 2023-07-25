#pragma once

class CAceReader
{

	int mCommPort,mBandRate;
	HANDLE    hComm;
	HINSTANCE m_hInstMaster;
public:
	int DeviceAddress;
	CAceReader();
	~CAceReader();
	int LoadDll();
	int CloseDll();
	int OpenComm(int com,int bp);
	int OpenComm(int bp);
	int CloseComm();
	int SetDeviceAddress(int da);

	HANDLE GetHComm();
	CString GetCardStrSerial();
	CString GetSnr();
	int     SetSnr(BYTE  *pSnr);

	int GetCardByteSerial(BYTE  *pSerial);
	int GetCardByteSerial1(BYTE  *pSerial);
	int ReadBLOCK(int add,BYTE *pData);
	int ReadBlock(int add,int mod,BYTE *pData,BYTE *pCode,int len);
	int ReadBLOCKWithCode(int add,BYTE *pData,BYTE *pCode);
	int ReadWORD(int add,DWORD &revData);
	int ReadBYTE(int add,BYTE  &revData);
	int WriteBLOCK(BYTE add,BYTE *pData);
	int WriteBlock(BYTE add,int mod,BYTE *pData,int len,BYTE *pCode);
	int WriteWORD( int add,DWORD revData);
	int WriteBYTE( int add,BYTE  revData);
	void Buzzer(int time,int count);
};

