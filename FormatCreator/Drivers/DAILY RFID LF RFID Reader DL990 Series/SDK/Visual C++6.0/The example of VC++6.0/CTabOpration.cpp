// CTabOpration.cpp : implementation file
//

#include "stdafx.h"
#include "friend1.h"
#include "CTabOpration.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CCTabOpration dialog

int Getbp(int index)
{
	switch(index)
	{
	case 1: return 9600;
	case 2: return 19200;
	case 3: return 38400;
	case 4: return 57600;
	case 5: return 115200;
	};
	return 9600;

}

CCTabOpration::CCTabOpration(CWnd* pParent /*=NULL*/)
	: CDialog(CCTabOpration::IDD, pParent)
{
	//{{AFX_DATA_INIT(CCTabOpration)
	m_ser = _T("AA BB AA BB AA BB AA BB");
	m_data = _T("FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF");
	//}}AFX_DATA_INIT
	pReader = NULL;
	pMem    = NULL;
}


void CCTabOpration::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CCTabOpration)
	DDX_Control(pDX, IDC_COMBO13, m_w_start);
	DDX_Control(pDX, IDC_COMBO14, m_w_key);
	DDX_Control(pDX, IDC_COMBO10, m_w_count);
	DDX_Control(pDX, IDC_COMBO11, m_w_key_type);
	DDX_Control(pDX, IDC_COMBO12, m_w_amount_type);
	DDX_Control(pDX, IDC_COMBO9, m_key);
	DDX_Control(pDX, IDC_COMBO8, m_start);
	DDX_Control(pDX, IDC_COMBO5, m_count);
	DDX_Control(pDX, IDC_COMBO6, m_key_type);
	DDX_Control(pDX, IDC_COMBO7, m_amount_type);
	DDX_Control(pDX, IDC_SERIAL, m_serial);
	DDX_Control(pDX, IDC_COMBO4, m_adress);
	DDX_Control(pDX, IDC_COMBO3, m_bp);
	DDX_Text(pDX, IDC_EDIT3, m_ser);
	DDV_MaxChars(pDX, m_ser, 23);
	DDX_Text(pDX, IDC_EDIT1, m_data);
	//}}AFX_DATA_MAP
}


BEGIN_MESSAGE_MAP(CCTabOpration, CDialog)
	//{{AFX_MSG_MAP(CCTabOpration)
	ON_BN_CLICKED(IDC_BUTTON1, OnSetAdress)
	ON_BN_CLICKED(IDC_BUTTON2, OnReadSer)
	ON_BN_CLICKED(IDC_BUTTON4, OnSetSer)
	ON_BN_CLICKED(IDC_BUTTON3, OnButton3)
	ON_BN_CLICKED(IDC_BUTTON5, OnRead)
	ON_BN_CLICKED(IDC_BUTTON6, OnWrite)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CCTabOpration message handlers
void CCTabOpration::SetReader(CAceReader *pReader)
{
	if(pReader == NULL)
		return;
	else
		this->pReader = pReader;
}

BOOL CCTabOpration::OnInitDialog() 
{
	CDialog::OnInitDialog();

	m_bp.AddString("9600");
	m_bp.AddString("19200");
	m_bp.AddString("38400");
	m_bp.AddString("57600");
	m_bp.AddString("115200");
	m_bp.SetCurSel(3);

	m_adress.AddString("00");
	m_adress.AddString("01");
	m_adress.AddString("02");
	m_adress.AddString("03");
	m_adress.AddString("04");
	m_adress.AddString("05");
	m_adress.AddString("06");
	m_adress.AddString("07");
	m_adress.AddString("08");
	m_adress.SetCurSel(0);

	m_amount_type.AddString("ldle");
	m_amount_type.AddString("all");
	m_amount_type.SetCurSel(0);

	m_key_type.AddString("keyA");
	m_key_type.AddString("keyB");
	m_key_type.SetCurSel(0);

	CString str;
	for(int i=0; i<64; i++)
	{
		
		str.Format("%d",i);
		m_count.AddString(str);
	}
	m_count.SetCurSel(10);
	
	m_start.AddString("1");
	m_start.SetCurSel(0);

	m_key.AddString("A0 A1 A2 A3 A4 A5");
	m_key.AddString("B0 B1 B2 B3 B4 B5");
	m_key.AddString("FF FF FF FF FF FF");
	m_key.SetCurSel(2);


	m_w_amount_type.AddString("ldle");
	m_w_amount_type.AddString("all");
	m_w_amount_type.SetCurSel(0);

	m_w_key_type.AddString("keyA");
	m_w_key_type.AddString("keyB");
	m_w_key_type.SetCurSel(0);
	
	for(int i=0; i<64; i++)
	{
		
		str.Format("%d",i);
		m_w_count.AddString(str);
	}
	m_w_count.SetCurSel(10);
	
	m_w_start.AddString("1");
	m_w_start.SetCurSel(0);

	m_w_key.AddString("A0 A1 A2 A3 A4 A5");
	m_w_key.AddString("B0 B1 B2 B3 B4 B5");
	m_w_key.AddString("FF FF FF FF FF FF");
	m_w_key.SetCurSel(2);


	return TRUE;  // return TRUE unless you set the focus to a control
	              // EXCEPTION: OCX Property Pages should return FALSE
}

void CCTabOpration::OnSetAdress() 
{
//	int last = pReader->DeviceAddress;
	pReader->DeviceAddress = m_adress.GetCurSel();
	CString str,temp;
	str.Format("Reader DeviceAddress change to %d",m_adress.GetCurSel());
	pMem->GetWindowText(temp);
	pMem->SetWindowText(temp+"\n"+str);
}

void CCTabOpration::OnReadSer() 
{
	m_serial.SetWindowText(pReader->GetSnr());
}

void CCTabOpration::OnSetSer() 
{
	UpdateData(true);

	CString temp;
	pMem->GetWindowText(temp);

	BYTE buf[128];
	memset(buf,0,128);
	if(GetData(buf,m_ser,23))
	{
		pMem->SetWindowText(temp+"\nThe ser to SetSer is not validity!");
		return;
	}	
	if(pReader->SetSnr(buf) == 0)
	{
		pMem->SetWindowText(temp+"\nSetSer OK: "+ m_ser);
		return;
	}
	else
	{
		pMem->SetWindowText(temp+"\nSetSer Error: "+ m_ser);
		return;
	}


	
}

void CCTabOpration::OnButton3() 
{
	CString temp;
	pMem->GetWindowText(temp);
	if(pReader->GetHComm() == NULL)
	{
		pMem->SetWindowText(temp+"\nRead is not open!");
		return;
	}	
	int bp = Getbp(m_bp.GetCurSel()+1);
	pReader->CloseComm();
	int re = pReader->OpenComm(bp);	
	if(re == 0)
	{
		CString str,temp;
		str.Format("Reader Baderate change to %d ok!",bp);
		pMem->GetWindowText(temp);
		pMem->SetWindowText(temp+"\n"+str);
	}
	else
	{
		CString str,temp;
		str.Format("Reader Baderate change to %d error!",bp);
		pMem->GetWindowText(temp);
		pMem->SetWindowText(temp+"\n"+str);
	}
}

void CCTabOpration::OnRead() 
{
	CString temp;	
	pMem->GetWindowText(temp);
	if(pReader->GetHComm() == NULL)
	{
		pMem->SetWindowText(temp+"\nRead is not open!");
		return;
	}	
	int add = m_count.GetCurSel();
	int len = m_start.GetCurSel()+1;
	int mod = m_amount_type.GetCurSel()*2+m_key_type.GetCurSel();

	CString strSer = pReader->GetCardStrSerial();
	BYTE buf[1024];
	memset(buf,0,1024);

	BYTE pCode[6];
	if(m_key.GetCurSel() == 0)
	{
		pCode[0] = 0xa0;
		pCode[1] = 0xa1;
		pCode[2] = 0xa2;
		pCode[3] = 0xa3;
		pCode[4] = 0xa4;
		pCode[5] = 0xa5;
	}
	else if(m_key.GetCurSel() == 1)
	{
		pCode[0] = 0xb0;
		pCode[1] = 0xb1;
		pCode[2] = 0xb2;
		pCode[3] = 0xb3;
		pCode[4] = 0xb4;
		pCode[5] = 0xb5;
	}
	else
		memset(pCode,0xff,6);

	int re = pReader->ReadBlock(add,mod,buf,pCode,len);
	if(re == 0)
	{
		CString str,t;
		str.Format("Read data:[%s]",strSer.GetBuffer(0));
		for(int i=0; i<len; i++)
		{
			t.Format(" %02x %02x %02x %02x %02x %02x %02x %02x %02x %02x %02x %02x %02x %02x %02x %02x ",
				buf[i*16+0],buf[i*16+1],buf[i*16+2],buf[i*16+3],
				buf[i*16+4],buf[i*16+5],buf[i*16+6],buf[i*16+7],
				buf[i*16+8],buf[i*16+9],buf[i*16+10],buf[i*16+11],
				buf[i*16+12],buf[i*16+13],buf[i*16+14],buf[i*16+15]);
			str += t;
			t="";
		}
		
		pMem->SetWindowText(temp+"\n"+str);
	}
	else
	{
		pMem->SetWindowText(temp+"\nRead error!");
	}

}
int gethexvalue(char p)
{
	switch(p)
	{
	case '0': return 0; 
	case '1': return 1;
	case '2': return 2; 
	case '3': return 3;
	case '4': return 4; 
	case '5': return 5;
	case '6': return 6; 
	case '7': return 7;
	case '8': return 8; 
	case '9': return 9;
	case 'a': 
	case 'A': return 10;
	case 'b':  
	case 'B': return 11;
	case 'c': ; 
	case 'C': return 12;
	case 'd':  
	case 'D': return 13;
	case 'e': 
	case 'E': return 14;
	case 'f': 
	case 'F': return 15;
	};
	return -1;
}
int CCTabOpration::GetData(BYTE *pData,CString str,int maxlen)
{
	int i=0;
	int len = str.GetLength();
	char *p = str.GetBuffer(0);
	if(len < maxlen)
		return 1;
	for(i=0; i<16; i++)
	{
		int value = 0;
		value = gethexvalue(p[i*3])*16;
		if(value == -1)
			return 2;
		value += gethexvalue(p[i*3+1]);
		if(value == -1)
			return 2;
		pData[i] = value;
	}
	return 0;
}
void CCTabOpration::OnWrite() 
{
	CString temp;	
	pMem->GetWindowText(temp);
	if(pReader->GetHComm() == NULL)
	{
		pMem->SetWindowText(temp+"\nReader is not open!");
		return;
	}	
	int add = m_w_count.GetCurSel();
	int len = m_w_start.GetCurSel()+1;
	int mod = m_w_amount_type.GetCurSel()*2+m_w_key_type.GetCurSel();

	CString strSer = pReader->GetCardStrSerial();
	BYTE buf[1024];
	memset(buf,0,1024);

	BYTE pCode[6];
	if(m_key.GetCurSel() == 0)
	{
		pCode[0] = 0xa0;
		pCode[1] = 0xa1;
		pCode[2] = 0xa2;
		pCode[3] = 0xa3;
		pCode[4] = 0xa4;
		pCode[5] = 0xa5;
	}
	else if(m_key.GetCurSel() == 1)
	{
		pCode[0] = 0xb0;
		pCode[1] = 0xb1;
		pCode[2] = 0xb2;
		pCode[3] = 0xb3;
		pCode[4] = 0xb4;
		pCode[5] = 0xb5;
	}
	else
		memset(pCode,0xff,6);

	UpdateData(true);
	if(GetData(buf,m_data,47))
	{
		pMem->SetWindowText(temp+"\nThe data to wrote is not validity!");
		return;
	}	
	int re = pReader->WriteBlock(add,mod,pCode,len,buf);
	if(re == 0)
	{
		CString str,t;
		str.Format("Write data:[%s]",strSer.GetBuffer(0));
		int i = 0;
		t.Format(" %02x %02x %02x %02x %02x %02x %02x %02x %02x %02x %02x %02x %02x %02x %02x %02x ",
				buf[i*16+0],buf[i*16+1],buf[i*16+2],buf[i*16+3],
				buf[i*16+4],buf[i*16+5],buf[i*16+6],buf[i*16+7],
				buf[i*16+8],buf[i*16+9],buf[i*16+10],buf[i*16+11],
				buf[i*16+12],buf[i*16+13],buf[i*16+14],buf[i*16+15]);
			str += t;
		pMem->SetWindowText(temp+"\n"+str);
	}
	else
	{
		pMem->SetWindowText(temp+"\nWrite error!");
	}	
}
