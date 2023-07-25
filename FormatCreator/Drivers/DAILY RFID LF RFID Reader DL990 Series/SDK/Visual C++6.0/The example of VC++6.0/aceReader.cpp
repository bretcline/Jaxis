
#include "stdafx.h"
#include "aceReader.h"
#include "mi.h"

#define BTL_9600 9600
#define BTL_57600 57600


static char *GetCommStr(int com)
{
	switch(com)
	{
		case 1: return "COM1";
		case 2: return "COM2";
		case 3: return "COM3";
		case 4: return "COM4";
		case 5: return "COM5";
		case 6: return "COM6";
		case 7: return "COM7";
		case 8: return "COM8";
	}
	return "COM";
		
}
CAceReader::CAceReader()
{
	DeviceAddress = 0;
	m_hInstMaster = NULL;
	LoadDll();

}
CAceReader::~CAceReader()
{
	
	if(hComm)
		CloseComm();
	CloseDll();
}
int CAceReader::LoadDll()
{
	printf("sd");
	char buf[1024];
	GetCurrentDirectory(1024,buf);
	CString str;
	str.Format("%s",buf);
	m_hInstMaster = LoadLibrary(".\\MI.DLL");	// Loaded 'E:\EXAMEX4\RMD\mi.dll', no matching symbolic information found.
	if(!m_hInstMaster)
	{
		DWORD er = GetLastError();
		MessageBox(NULL,"调用读写器动态连接库(Mi.dll)失败!","警  告",MB_OK | MB_ICONERROR);
	    exit(0);
	}

	(FARPROC &)API_OpenComm   = GetProcAddress(m_hInstMaster,_T("API_OpenComm"));
	(FARPROC &)API_CloseComm  = GetProcAddress(m_hInstMaster,_T("API_CloseComm"));
	(FARPROC &)API_PCDRead    = GetProcAddress(m_hInstMaster,_T("API_PCDRead"));
	(FARPROC &)API_PCDWrite   = GetProcAddress(m_hInstMaster,_T("API_PCDWrite"));
	(FARPROC &)API_PCDInitVal = GetProcAddress(m_hInstMaster,_T("API_PCDInitVal"));
	(FARPROC &)API_PCDDec     = GetProcAddress(m_hInstMaster,_T("API_PCDDec"));
	(FARPROC &)API_PCDInc     = GetProcAddress(m_hInstMaster,_T("API_PCDInc"));
	(FARPROC &)RDM_GetSnr     = GetProcAddress(m_hInstMaster,_T("RDM_GetSnr"));
	(FARPROC &)MF_Request     = GetProcAddress(m_hInstMaster,_T("MF_Request"));
	(FARPROC &)MF_Anticoll    = GetProcAddress(m_hInstMaster,_T("MF_Anticoll"));
	(FARPROC &)MF_Select      = GetProcAddress(m_hInstMaster,_T("MF_Select"));
	(FARPROC &)MF_Halt        = GetProcAddress(m_hInstMaster,_T("MF_Halt"));
	(FARPROC &)MF_Restore             = GetProcAddress(m_hInstMaster,_T("MF_Restore"));
	(FARPROC &)API_ControlBuzzer      = GetProcAddress(m_hInstMaster,_T("API_ControlBuzzer"));
	(FARPROC &)GET_SNR                = GetProcAddress(m_hInstMaster,_T("GET_SNR"));
	(FARPROC &)GetVersionNum          = GetProcAddress(m_hInstMaster,_T("GetVersionNum"));
	(FARPROC &)API_SetDeviceAddress   = GetProcAddress(m_hInstMaster,_T("API_SetDeviceAddress"));
	(FARPROC &)API_SetSerNum          = GetProcAddress(m_hInstMaster,_T("API_SetSerNum"));
	(FARPROC &)API_GetSerNum          = GetProcAddress(m_hInstMaster,_T("API_GetSerNum"));
	if( API_OpenComm      == NULL || 
		API_CloseComm     == NULL ||
		API_PCDRead       == NULL ||
		API_PCDWrite      == NULL ||
		API_PCDInitVal    == NULL ||
		API_PCDDec        == NULL ||
		API_PCDInc        == NULL ||
		RDM_GetSnr        == NULL ||
		MF_Request        == NULL ||
		MF_Anticoll       == NULL ||
		MF_Select         == NULL ||
		MF_Restore        == NULL ||
		GET_SNR           == NULL ||
		GetVersionNum     == NULL ||
		API_SetDeviceAddress  == NULL ||
		API_SetSerNum         == NULL ||
		API_GetSerNum         == NULL ||
		API_ControlBuzzer == NULL ||
		MF_Halt           == NULL )
	{
		MessageBox(NULL,"调用读写器动态连接库的函数失败!","警  告",MB_OK|MB_ICONERROR);
		exit(0);
		return 0;
	}
  /* */
    return 1;
}
int CAceReader::CloseDll()
{
	if(m_hInstMaster)
	{
		FreeLibrary(m_hInstMaster);
		API_OpenComm      = NULL ; 
		API_CloseComm     = NULL ;
		API_PCDRead       = NULL ;
		API_PCDWrite      = NULL ;
		API_PCDInitVal    = NULL ;
		API_PCDDec        = NULL ;
		API_PCDInc        = NULL ;
		RDM_GetSnr        = NULL ;
		MF_Request        = NULL ;
		MF_Anticoll       = NULL ;
		MF_Select         = NULL ;
		MF_Restore        = NULL ;
		GET_SNR           = NULL ;
		API_ControlBuzzer = NULL ;
		MF_Halt           = NULL ;
		return 1;
	}
	return 0;
}
HANDLE CAceReader::GetHComm()
{ return hComm;}
int CAceReader::OpenComm(int com,int bp)
{
	if(hComm)
		return 1;  
	if(com<1 || com >8)
		return 3;
	hComm = API_OpenComm(GetCommStr(com),bp);
	if(!hComm)
		return 2;  
	Buzzer(10,1);
	mCommPort = com;
	mBandRate = bp;
	return 0;
}
int CAceReader::OpenComm(int bp)
{
	if(hComm)
		return 1;  
	if(mCommPort<1 || mCommPort >8)
		return 3;
	hComm = API_OpenComm(GetCommStr(mCommPort),bp);
	if(!hComm)
		return 2;  
	Buzzer(10,1);
	return 0;
}
int CAceReader::CloseComm()
{
	if(hComm)
		int i = API_CloseComm(hComm);	
	hComm = NULL;
	return 1;
}
int CAceReader::SetDeviceAddress(int da)
{
	int last = DeviceAddress;
	DeviceAddress = da;
	return last;
}
CString CAceReader::GetCardStrSerial()
{
	CString strre;
	BYTE buf[128];
	Buzzer(1,1);
	if(GetCardByteSerial(buf) == 0)
	{
		strre.Format("%02x%02x%02x%02x",buf[0],buf[1],buf[2],buf[3]);
		return strre;
	}
	return "";
}
int CAceReader::GetCardByteSerial(BYTE  *pSerial)
{
	BYTE buf[128];
	BYTE snr[16];
	int result = 0;
	memset(buf,0,128);
	memset(snr,0,16);
	if (hComm == INVALID_HANDLE_VALUE)
		return 1 ;
	if((result = GET_SNR(hComm, DeviceAddress, 0x26, 0x00,
		snr,buf)) != 0x00)
		return 2;
	memcpy(pSerial,buf,4);
	Buzzer(5,1);
	return 0;
}
//64个block
int CAceReader::ReadBLOCK(int add,BYTE *pData)
{
	if(!hComm)
		return 1; 
	if(add<0 || add>64)
		return 10;
	BYTE buf[128];
	int result = 0;
	memset(buf,0,128);
    if((result = GetCardByteSerial(buf)) != 0x00)
		return result;
	BYTE dataBuf[1024];
    memset(buf,0xff,6);
	memset(dataBuf,0,128);
	if((result = API_PCDRead(hComm,DeviceAddress,0x00,add,1,buf,dataBuf)) != 0x00)
		return 6;
	memcpy(pData,dataBuf,16);
	Buzzer(2,1);
	return 0;

}
int CAceReader::ReadBLOCKWithCode(int add,BYTE *pData,BYTE *pCode)
{
	if(!hComm)
		return 1; 
	if(add<0 || add>64)
		return 10;
	if(add >=4 && add<= 7)
		add = add;
	BYTE buf[128];
	int result = 0;
	memset(buf,0,128);
    if((result = GetCardByteSerial(buf)) != 0x00)
		return result;
	BYTE dataBuf[128];
    memcpy(buf,pCode,6);
	memset(dataBuf,0,128);
	if((result = API_PCDRead(hComm,DeviceAddress,0x00,add,1,buf,dataBuf)) != 0x00)
		return 6;
	memcpy(pData,dataBuf,16);
	return 0;

}
int CAceReader::ReadBlock(int add,int mod,BYTE *pData,BYTE *pCode,int len)
{
	if(!hComm)
		return 1; 
	if(add<0 || add>64)
		return 10;
	if(add >=4 && add<= 7)
		add = add;
	if(mod <0 || mod>1)
		return 101;
	if(len > 4)
		len = 4;
	if(len < 1)
		len = 1;
	BYTE buf[128];
	int result = 0;
	memset(buf,0,128);
    if((result = GetCardByteSerial(buf)) != 0x00)
		return result;
	BYTE dataBuf[128];
    memcpy(buf,pCode,6);
	memset(dataBuf,0,128);
	if((result = API_PCDRead(hComm,DeviceAddress,mod,add,len,buf,dataBuf)) != 0x00)
		return 6;
	memcpy(pData,dataBuf,16*len);
	return 0;

}
//一共256个DWORD，地址按找绝对编号编码
int CAceReader::ReadWORD(int add,DWORD &revData)
{
	if(!hComm)
		return 1; 
	BYTE bAdd = add/4;
	if(bAdd<0 || bAdd>256)
		return 10;
	BYTE buf[128];
	int result = 0;
	memset(buf,0,128);
    if((result = GetCardByteSerial(buf)) != 0x00)
		return result;
	BYTE dataBuf[1024];
	memset(buf,0xff,6);
	memset(dataBuf,0,128);
	if((result = API_PCDRead(hComm,DeviceAddress,0x00,bAdd,2,buf,dataBuf)) != 0x00)
		return 6;
	else
	{
		revData = 0;
		bAdd = (add%4)*4;
		revData |= dataBuf[bAdd++];		revData = revData<<8;
		revData |= dataBuf[bAdd++];		revData = revData<<8;
		revData |= dataBuf[bAdd++];		revData = revData<<8;
		revData |= dataBuf[bAdd];	
		return 0;
	}
}

//一共1024个BTYE，地址按找绝对编号编码
int CAceReader::ReadBYTE(int add,BYTE  &revData)
{
	if(!hComm)
		return 1; 
	BYTE bAdd = add/16 + 1;
	if(bAdd<0 || bAdd>15)
		return 10;
	BYTE buf[128];
	int result = 0;
	memset(buf,0,128);
    if((result = GetCardByteSerial(buf)) != 0x00)
		return result;
	if((result = MF_Select(hComm, DeviceAddress, buf)) != 0x00)
		return 5 ;
	BYTE dataBuf[1028];
	memset(buf,0xff,6);
	memset(dataBuf,0,128);
	if((result = API_PCDRead(hComm,DeviceAddress,0x00,bAdd,1,buf,dataBuf)) != 0x00)
		return 6;
	else
	{
		bAdd = add%16;
		revData = dataBuf[bAdd++];
		return 0;
	}
}
int CAceReader::WriteBLOCK(BYTE add,BYTE *pData)
{
	if(!hComm)
		return 1; 
	if(add<0 || add>64)
		return 10;
	if(add%4 == 3)
		return 11;
	BYTE buf[128];
	int result = 0;
	memset(buf,0,128);
    if((result = GetCardByteSerial(buf)) != 0x00)
		return result;
	memset(buf,0xff,16);
	if((result = API_PCDWrite(hComm,DeviceAddress,0,add,1,buf,pData)) != 0x00)
		return 6;
	Buzzer(5,1);
	return 0;
}
int CAceReader::WriteBlock(BYTE add,int mod,BYTE *pData,int len,BYTE *pCode)
{
	if(!hComm)
		return 1; 
	if(add<0 || add>64)
		return 10;
	if(add%4 == 3)
		return 11;
	BYTE buf[128];
	int result = 0;
	memset(buf,0,128);
    if((result = GetCardByteSerial(buf)) != 0x00)
		return result;
	memset(buf,0xff,16);
	if((result = API_PCDWrite(hComm,DeviceAddress,mod,add,len,pCode,pData)) != 0x00)
		return 6;
	Buzzer(5,1);
	return 0;
}
int CAceReader::WriteWORD(int add,DWORD wData)
{
	return 2;
}
int CAceReader::WriteBYTE(int add,BYTE  wData)
{return 0;}
void CAceReader::Buzzer(int time,int count)
{
	if(!hComm)
		return ;  
	BYTE buf[128];
	int re = API_ControlBuzzer(hComm,DeviceAddress,time,count,buf);

}
CString CAceReader::GetSnr()
{
	BYTE buf[128];
	memset(buf,1,128);
	int re = 0;
	if((re = API_GetSerNum(hComm,DeviceAddress,buf)) == 0)
	{
		CString str;
		str.Format("%02x %02x %02x %02x %02x %02x %02x %02x",
				buf[1],buf[2],buf[3],
				buf[4],buf[5],buf[6],buf[7],buf[8]);
		return str;
	}
	else
		return "";
}
int CAceReader::SetSnr(BYTE  *pSnr)
{
	if(!pSnr) return 1;
	BYTE buf[128];
	memset(buf,1,128);
	int re = API_SetSerNum(hComm,DeviceAddress,pSnr,buf);
	return re;
}












