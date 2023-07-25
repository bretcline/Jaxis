//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "stdio.h"
#include "main.h"
#include "aboutfrm.h"
#include "e:\usr\rpj\app\demo\dllsrc\mi.h"

//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TfrmMain *frmMain;
//---------------------------------------------------------------------------
__fastcall TfrmMain::TfrmMain(TComponent* Owner)
        : TForm(Owner)
{
    cmbPort->AddItem("COM1", NULL);
    cmbPort->AddItem("COM2", NULL);
    cmbPort->AddItem("COM3", NULL);
    cmbPort->AddItem("COM4", NULL);

    cmbBaudrate->AddItem("9600", NULL);


    sprintf(m_szPort, "COM1");
    m_nBaudrate = 9600;


    m_bOpen = FALSE;
}
//---------------------------------------------------------------------------




void __fastcall TfrmMain::FormActivate(TObject *Sender)
{
    for (int n = 0; n < cmbPort->Items->Count; n ++)
    {
        if (cmbPort->Items->Strings[n] == m_szPort)
        {
            cmbPort->ItemIndex = n;
            break;
        }
    }

    for (int n = 0; n < cmbBaudrate->Items->Count; n ++)
    {
        if (cmbBaudrate->Items->Strings[n] == m_nBaudrate)
        {
            cmbBaudrate->ItemIndex = n;
            break;
        }

    }
}
//---------------------------------------------------------------------------

void __fastcall TfrmMain::pgMainChange(TObject *Sender)
{
    switch (pgMain->ActivePageIndex)
    {
        case 0:
            if (m_bOpen)
            {
                cmbCOM->Caption = "关闭";
            }
            else
            {
                cmbCOM->Caption = "打开";
            }

            MaiFareCommand1->Checked = FALSE;
            l5693Command1->Checked = FALSE;
            TypeBCommand1->Checked = FALSE;
            WatchCardCommand1->Checked = FALSE;

            break;
        case 1:
            MaiFareCommand1->Checked = TRUE;
            l5693Command1->Checked = FALSE;
            TypeBCommand1->Checked = FALSE;
            WatchCardCommand1->Checked = FALSE;

            break;
        case 2:
            MaiFareCommand1->Checked = FALSE;
            l5693Command1->Checked = TRUE;
            TypeBCommand1->Checked = FALSE;
            WatchCardCommand1->Checked = FALSE;

            break;

        case 3:
            MaiFareCommand1->Checked = FALSE;
            l5693Command1->Checked = FALSE;
            TypeBCommand1->Checked = TRUE;
            WatchCardCommand1->Checked = FALSE;

            break;

        case 4:
            MaiFareCommand1->Checked = FALSE;
            l5693Command1->Checked = FALSE;
            TypeBCommand1->Checked = FALSE;
            WatchCardCommand1->Checked = TRUE;

            break;

        default:
            break;
    }
}
//---------------------------------------------------------------------------

void __fastcall TfrmMain::cmbCOMClick(TObject *Sender)
{
     if (cmbPort->ItemIndex < 0)
        return;

     sprintf(m_szPort, "%s", cmbPort->Text);

    if (!m_bOpen)
    {
        m_commHandle = API_OpenComm(m_szPort, m_nBaudrate);
        if (m_commHandle != NULL)
        {
            cmbCOM->Caption = "关闭";
            m_bOpen = TRUE;
        }
    }
    else
    {
        int nRet = API_CloseComm(m_commHandle);

        if (!nRet)
        {
            cmbCOM->Caption = "打开";
            m_bOpen = FALSE;
        }
    }

    barMain->Invalidate();
}
//---------------------------------------------------------------------------

void __fastcall TfrmMain::tmMainTimer(TObject *Sender)
{
    barMain->Panels->Items[1]->Text = "演示系统 版本V1.00 （2004.05.01）";
    barMain->Panels->Items[4]->Text = FormatDateTime("yyyy.mm.dd   hh:mm:ss AM/PM    dddd", Now());
}
//---------------------------------------------------------------------------

void __fastcall TfrmMain::barMainDrawPanel(TStatusBar *StatusBar,
      TStatusPanel *Panel, const TRect &Rect)
{
    //
    TCanvas *pCanvas = StatusBar->Canvas;

    switch (Panel->Index)
    {
    case 0:
        imgBar->Draw(pCanvas, Rect.left + 5, Rect.top + 2, 0, TRUE);
        break;
    case 2:
        {
            int nIndex = (m_bOpen == TRUE) ? 2 : 1;
            imgBar->Draw(pCanvas, Rect.left + 5, Rect.top + 2, nIndex, TRUE);
        }
        break;
    case 3:
        imgBar->Draw(pCanvas, Rect.left + 5, Rect.top + 2, 3, TRUE);
        break;
    default:
        break;
    }


}
//---------------------------------------------------------------------------



void __fastcall TfrmMain::N4Click(TObject *Sender)
{
    pgMain->ActivePageIndex = 0;

    MaiFareCommand1->Checked = FALSE;
    l5693Command1->Checked = FALSE;
    TypeBCommand1->Checked = TRUE;
    WatchCardCommand1->Checked = FALSE;

}
//---------------------------------------------------------------------------

void __fastcall TfrmMain::MaiFareCommand1Click(TObject *Sender)
{
    pgMain->ActivePageIndex = 1;
    MaiFareCommand1->Checked = TRUE;
    l5693Command1->Checked = FALSE;
    TypeBCommand1->Checked = FALSE;
    WatchCardCommand1->Checked = FALSE;
}
//---------------------------------------------------------------------------

void __fastcall TfrmMain::l5693Command1Click(TObject *Sender)
{
    pgMain->ActivePageIndex = 2;
    MaiFareCommand1->Checked = FALSE;
    l5693Command1->Checked = TRUE;
    TypeBCommand1->Checked = FALSE;
    WatchCardCommand1->Checked = FALSE;

}
//---------------------------------------------------------------------------

void __fastcall TfrmMain::TypeBCommand1Click(TObject *Sender)
{
    pgMain->ActivePageIndex = 3;
    MaiFareCommand1->Checked = FALSE;
    l5693Command1->Checked = FALSE;
    TypeBCommand1->Checked = TRUE;
    WatchCardCommand1->Checked = FALSE;
}
//---------------------------------------------------------------------------

void __fastcall TfrmMain::WatchCardCommand1Click(TObject *Sender)
{
    pgMain->ActivePageIndex = 4;
    MaiFareCommand1->Checked = FALSE;
    l5693Command1->Checked = FALSE;
    TypeBCommand1->Checked = FALSE;
    WatchCardCommand1->Checked = TRUE;
}
//---------------------------------------------------------------------------

void __fastcall TfrmMain::N5Click(TObject *Sender)
{
    if (NULL == frmAbout)
    {
        frmAbout = new TfrmAbout(NULL);
    }

    frmAbout->ShowModal();
}
//---------------------------------------------------------------------------

void __fastcall TfrmMain::butReadClick(TObject *Sender)
{
    //
    int ret, keyIndex = -1;
    unsigned char addr;
    unsigned char mode, blkAdd, numBlk;
    unsigned char pSnr[256];
    unsigned char pBuffer[256];
    char pShow[256];

    if (butReadA->Checked)
        mode = 0;
    else
        mode = 1;

    if (txtReadStart->Text.Length() <= 0)
    {
        txtReadStart->SetFocus();
        return;
    }
    blkAdd = StrToInt(txtReadStart->Text);

    if (txtReadNum->Text.Length() <= 0)
    {
        txtReadNum->SetFocus();
        return;
    }
    numBlk = StrToInt(txtReadNum->Text);

    if (txtReadKey->Text.Length() != 12)
    {
        txtReadKey->SetFocus();
        return;
    }

    int nSize = strToHex(txtReadKey->Text.c_str(), txtReadKey->Text.Length(), pSnr, 255);

    addr = 0;
    txtRet1->Text = "";
    txtRet2->Text = "";
    txtRet3->Text = "";
    txtRet41->Text = "";
    txtRet42->Text = "";
    txtRet43->Text = "";

    labBlk1->Caption = "";
    labBlk2->Caption = "";
    labBlk3->Caption = "";
    labBlk4->Caption = "";

    for (int n = 0, m = 0; n < numBlk; n ++)
    {
        sprintf(pShow, "块 %d", blkAdd + n);
        if ((blkAdd + 1 + n) % 4 == 0)
        {
            labBlk4->Caption = pShow;
            keyIndex = n;
        }
        else
        {
            switch (m)
            {
                case 0:
                    labBlk1->Caption = pShow;
                    break;
                case 1:
                    labBlk2->Caption = pShow;
                    break;
                case 2:
                    labBlk3->Caption = pShow;
                    break;
            }
            m ++;
        }
    }

    if ((ret = API_PCDRead(addr, mode, blkAdd, numBlk, pSnr, pBuffer)) == 0)
    {
        //get sequence number
        nSize = hexToStr(pSnr, 4 , pShow, 256);
        txtRet->Text = pShow;

        for (int n = 0, m = 0; n < numBlk; n ++)
        {
            if (keyIndex == n)
            {
                nSize = hexToStr(pBuffer + 16 * n, 6, pShow, 256);
                txtRet41->Text = pShow;

                nSize = hexToStr(pBuffer + 16 * n + 6 , 4, pShow, 256);
                txtRet42->Text = pShow;

                nSize = hexToStr(pBuffer + 16 * n + 10, 6, pShow, 256);
                txtRet43->Text = pShow;
            }
            else
            {
               nSize = hexToStr(pBuffer + 16 * n, 16 , pShow, 256);

               switch (m)
               {
                case 0:
                    txtRet1->Text = pShow;
                    break;
                case 1:
                    txtRet2->Text = pShow;
                    break;

                case 2:
                    txtRet3->Text = pShow;
                    break;
               }

               m ++;
            }
        }
    }
    else
    {
        sprintf(pShow, "%02x", ret);
        txtRet->Text = pShow;
    }
}
//---------------------------------------------------------------------------


int __fastcall TfrmMain::strToHex(const char *pStr, int maxSize, unsigned char *pBuffer, int maxBuffer) const
{
    unsigned char charH, charL, charRet;
    int m = 0;

    for (int n = 0; n <  maxSize; n++ )
    {
        if (n % 2 == 0)
        {
            charH = charToHex(pStr[n]);
        }
        else
        {
            charL = charToHex(pStr[n]);

            charRet = (charH << 4) | charL;
            pBuffer[m ++] = charRet;

            if (m >= maxBuffer)
                break;
        }
    }

    return m;
}

unsigned char __fastcall TfrmMain::charToHex(const char srcChar)
{
    if (srcChar >= 'a')
        return srcChar - 97 + 10;
    else if (srcChar >= 'A')
        return srcChar - 65 + 10;
    else
        return srcChar - 48;
}




int __fastcall TfrmMain::hexToStr(const unsigned char *pHex, int maxSize, char *pBuffer, int maxBuffer) const
{
    char szTemp[16];
    pBuffer[0] = '\0';

    for (int n = 0; n < maxSize; n ++)
    {
        sprintf(szTemp, "%02x ", pHex[n]);
        strcat(pBuffer, szTemp);
    }
}



void __fastcall TfrmMain::BitBtn2Click(TObject *Sender)
{
    int ret;
    unsigned char addr;
    unsigned char mode, blkAdd, numBlk;
    unsigned char pSnr[256];
    unsigned char pBuffer[256];
    char pShow[256];

    if (butWriteA->Checked)
        mode = 0;
    else
        mode = 1;

    if (txtWriteStart->Text.Length() <= 0)
    {
        txtWriteStart->SetFocus();
        return;
    }

    if (txtWriteBlk->Text.Length() <= 0)
    {
        txtWriteBlk->SetFocus();
        return;
    }

    if (txtWriteKey->Text.Length() != 12)
    {
        txtWriteKey->SetFocus();
        return;
    }

    if (txtWriteData->Text.Length() != 32)
    {
        txtWriteData->SetFocus();
        return;
    }

    blkAdd = StrToInt(txtWriteStart->Text);
    numBlk = StrToInt(txtWriteBlk->Text);

    int nSize = strToHex(txtWriteKey->Text.c_str(), txtWriteKey->Text.Length(), pSnr, 255);
    nSize = strToHex(txtWriteData->Text.c_str(), txtWriteData->Text.Length(), pBuffer, 255);

    addr = 0;
    txtRet1->Text = "";
    txtRet2->Text = "";
    txtRet3->Text = "";
    txtRet41->Text = "";
    txtRet42->Text = "";
    txtRet43->Text = "";

    labBlk1->Caption = "";
    labBlk2->Caption = "";
    labBlk3->Caption = "";
    labBlk4->Caption = "";

    if ((ret = API_PCDWrite(addr, mode, blkAdd, numBlk, pSnr, pBuffer)) == 0)
    {
        //get sequence number
        nSize = hexToStr(pSnr, 4 , pShow, 256);
        txtRet->Text = pShow;
    }
    else
    {
        sprintf(pShow, "%02x", ret);
        txtRet->Text = pShow;
    }


}
//---------------------------------------------------------------------------








void __fastcall TfrmMain::butCreateFileClick(TObject *Sender)
{
    int ret;
    unsigned char addr = 0, record, space;
    unsigned char fileName[256];
      char pShow[1024], pTemp[16];



    if (txtFileNo4->Text.Length() != 4)
    {
        txtFileNo4->SetFocus();
        return;
    }

    strToHex(txtFileNo4->Text.c_str(), 4, fileName, 255);

    if (txtRecord4->Text.Length() <= 0)
    {
        txtRecord4->SetFocus();
        return;
    }
    record = StrToInt(txtRecord4->Text);


    if (txtSpace4->Text.Length() <= 0)
    {
        txtSpace4->SetFocus();
        return;
    }
    space = StrToInt(txtSpace4->Text);

    mmWCRet->Text = "";
    if ((ret = API_WCCreateFile(addr, fileName, record, space)) == 0)
    {
        sprintf(pShow, "");

        for (int n = 2; n < 4; n ++)
        {
            sprintf(pTemp, "%02x ", fileName[n]);
            strcat(pShow, pTemp);
        }
    }
    else
    {
        sprintf(pShow, "%02x", ret);
    }

    mmWCRet->Text = pShow;
}
//---------------------------------------------------------------------------

void __fastcall TfrmMain::butSelectFileClick(TObject *Sender)
{
    int ret;
    unsigned char addr = 0;
    unsigned char fileName[256];
    char pShow[1024], pTemp[16];

    if (txtSelectFile4->Text.Length() <= 0)
    {
        txtSelectFile4->SetFocus();
        return;
    }
    strToHex(txtSelectFile4->Text.c_str(), 4, fileName, 255);

    mmWCRet->Text = "";
    if ((ret == API_WCSelectFile(addr, fileName)) == 0)
    {
        sprintf(pShow, "");

        for (int n = 2; n < 4; n ++)
        {
            sprintf(pTemp, "%02x ", fileName[n]);
            strcat(pShow, pTemp);
        }
    }
    else
    {
        sprintf(pShow, "%02x", ret);
    }

    mmWCRet->Text = pShow;
}
//---------------------------------------------------------------------------




void __fastcall TfrmMain::txtModifyRecordClick(TObject *Sender)
{
    int ret;
    unsigned char addr = 0;
    unsigned char newKey[256];
    char pShow[1024], pTemp[16];


    if (txtModifyKey4->Text.Length() != 32)
    {
        txtModifyKey4->SetFocus();
        return;
    }
    strToHex(txtModifyKey4->Text.c_str(), 32, newKey, 255);
    
    if ((ret == API_WCModifyKey(addr, newKey)) == 0)
    {
       sprintf(pShow, "");
        for (int n = 2; n < 4; n ++)
        {
            sprintf(pTemp, "%02x ", newKey[n]);
            strcat(pShow, pTemp);
        }

    }
    else
    {
        sprintf(pShow, "%02x", ret);
    }

    mmRet->Text = pShow;
}
//---------------------------------------------------------------------------






void __fastcall TfrmMain::butResetClick(TObject *Sender)
{
    int ret = 0;
    unsigned char addr = 0;
    unsigned char buffer[256];
    char pShow[1024], pTemp[16];

    mmWCRet->Text = "";

    if ((ret = API_WCReset(addr, buffer)) == 0)
    {
       sprintf(pShow, "OK: ");

       for (int n = 0; n < 4; n ++)
        {
            sprintf(pTemp, "%02x ", buffer[n]);
            strcat(pShow, pTemp);
        }

    }
    else
    {
        sprintf(pShow, "%02x", ret);
    }

    mmWCRet->Text = pShow;
}
//---------------------------------------------------------------------------


void __fastcall TfrmMain::N6Click(TObject *Sender)
{
    {
        PostMessage(this->Handle, WM_QUIT, 0, 0);
    }
}
//---------------------------------------------------------------------------


void __fastcall TfrmMain::txtReadKeyKeyPress(TObject *Sender, char &Key)
{
    if (!((Key >= '0' && Key <= '9') || (Key >= 'a' && Key <= 'f') || Key == '\b'))
    {
        Key = 0;
    }
}
//---------------------------------------------------------------------------

void __fastcall TfrmMain::txtReadStartKeyPress(TObject *Sender, char &Key)
{
    if (!((Key >= '0' && Key <= '9') || Key == '\b'))
    {
        Key = 0;
    }
}
//---------------------------------------------------------------------------

void __fastcall TfrmMain::txtReadNumKeyPress(TObject *Sender, char &Key)
{
    if (!((Key >= '0' && Key <= '9') || Key == '\b'))
    {
        Key = 0;
    }
    
}
//---------------------------------------------------------------------------

void __fastcall TfrmMain::txtWriteStartKeyPress(TObject *Sender, char &Key)
{
    if (!((Key >= '0' && Key <= '9') || Key == '\b'))
    {
        Key = 0;
    }
}
//---------------------------------------------------------------------------

void __fastcall TfrmMain::txtWriteBlkKeyPress(TObject *Sender, char &Key)
{
    if (!((Key >= '0' && Key <= '9') || Key == '\b'))
    {
        Key = 0;
    }
}
//---------------------------------------------------------------------------

void __fastcall TfrmMain::txtWriteKeyKeyPress(TObject *Sender, char &Key)
{
    if (!((Key >= '0' && Key <= '9') || (Key >= 'a' && Key <= 'f') || Key == '\b'))
    {
        Key = 0;
    }
    
}
//---------------------------------------------------------------------------

void __fastcall TfrmMain::txtWriteDataKeyPress(TObject *Sender, char &Key)
{
    if (!((Key >= '0' && Key <= '9') || (Key >= 'a' && Key <= 'f') || Key == '\b'))
    {
        Key = 0;
    }
}
//---------------------------------------------------------------------------

void __fastcall TfrmMain::txtCOSKeyPress(TObject *Sender, char &Key)
{
    if (!((Key >= '0' && Key <= '9') || (Key >= 'a' && Key <= 'f') || Key == '\b'))
    {
        Key = 0;
    }
}
//---------------------------------------------------------------------------

void __fastcall TfrmMain::txtKey4KeyPress(TObject *Sender, char &Key)
{
    if (!((Key >= '0' && Key <= '9') || (Key >= 'a' && Key <= 'f') || Key == '\b'))
    {
        Key = 0;
    }
    
}
//---------------------------------------------------------------------------

void __fastcall TfrmMain::txtFileNo4KeyPress(TObject *Sender, char &Key)
{
    if (!((Key >= '0' && Key <= '9') || (Key >= 'a' && Key <= 'f') || Key == '\b'))
    {
        Key = 0;
    }
    
}
//---------------------------------------------------------------------------

void __fastcall TfrmMain::txtRecord4KeyPress(TObject *Sender, char &Key)
{
    if (!((Key >= '0' && Key <= '9') || Key == '\b'))
    {
        Key = 0;
    }

}
//---------------------------------------------------------------------------

void __fastcall TfrmMain::txtSpace4KeyPress(TObject *Sender, char &Key)
{
    if (!((Key >= '0' && Key <= '9') || Key == '\b'))
    {
        Key = 0;
    }
}
//---------------------------------------------------------------------------

void __fastcall TfrmMain::txtSelectFile4KeyPress(TObject *Sender,
      char &Key)
{
    if (!((Key >= '0' && Key <= '9') || (Key >= 'a' && Key <= 'f') || Key == '\b'))
    {
        Key = 0;
    }
    
}
//---------------------------------------------------------------------------

void __fastcall TfrmMain::txtModifyKey4KeyPress(TObject *Sender, char &Key)
{
    if (!((Key >= '0' && Key <= '9') || (Key >= 'a' && Key <= 'f') || Key == '\b'))
    {
        Key = 0;
    }
    
}
//---------------------------------------------------------------------------

void __fastcall TfrmMain::txtReadNoKeyPress(TObject *Sender, char &Key)
{
    if (!((Key >= '0' && Key <= '9') || Key == '\b'))
    {
        Key = 0;
    }

}
//---------------------------------------------------------------------------

void __fastcall TfrmMain::txtRecordLenKeyPress(TObject *Sender, char &Key)
{
    if (!((Key >= '0' && Key <= '9') || Key == '\b'))
    {
        Key = 0;
    }
}
//---------------------------------------------------------------------------

void __fastcall TfrmMain::txtRecordKeyPress(TObject *Sender, char &Key)
{
    if (!((Key >= '0' && Key <= '9') || (Key >= 'a' && Key <= 'f') || Key == '\b'))
    {
        Key = 0;
    }
}
//---------------------------------------------------------------------------

void __fastcall TfrmMain::txtReadFlags2KeyPress(TObject *Sender, char &Key)
{
    if (!((Key >= '0' && Key <= '9') || Key == '\b'))
    {
        Key = 0;
    }
}
//---------------------------------------------------------------------------

void __fastcall TfrmMain::txtReadStart2KeyPress(TObject *Sender, char &Key)
{
    if (!((Key >= '0' && Key <= '9') || Key == '\b'))
    {
        Key = 0;
    }
    
}
//---------------------------------------------------------------------------

void __fastcall TfrmMain::txtReadNum2KeyPress(TObject *Sender, char &Key)
{
    if (!((Key >= '0' && Key <= '9') || Key == '\b'))
    {
        Key = 0;
    }
    
}
//---------------------------------------------------------------------------

void __fastcall TfrmMain::txtWriteFlags2KeyPress(TObject *Sender,
      char &Key)
{
    if (!((Key >= '0' && Key <= '9') || Key == '\b'))
    {
        Key = 0;
    }
    
}
//---------------------------------------------------------------------------

void __fastcall TfrmMain::txtWriteStart2KeyPress(TObject *Sender,
      char &Key)
{
    if (!((Key >= '0' && Key <= '9') || Key == '\b'))
    {
        Key = 0;
    }
    
}
//---------------------------------------------------------------------------

void __fastcall TfrmMain::txtWriteNum2KeyPress(TObject *Sender, char &Key)
{
    if (!((Key >= '0' && Key <= '9') || Key == '\b'))
    {
        Key = 0;
    }
    
}
//---------------------------------------------------------------------------

void __fastcall TfrmMain::txtLockFlags2KeyPress(TObject *Sender, char &Key)
{
    if (!((Key >= '0' && Key <= '9') || Key == '\b'))
    {
        Key = 0;
    }
    
}
//---------------------------------------------------------------------------

void __fastcall TfrmMain::txtLockStart2KeyPress(TObject *Sender, char &Key)
{
    if (!((Key >= '0' && Key <= '9') || Key == '\b'))
    {
        Key = 0;
    }
    
}
//---------------------------------------------------------------------------

void __fastcall TfrmMain::txtReadSNR2KeyPress(TObject *Sender, char &Key)
{
    if (!((Key >= '0' && Key <= '9') || (Key >= 'a' && Key <= 'f') || Key == '\b'))
    {
        Key = 0;
    }

}
//---------------------------------------------------------------------------

void __fastcall TfrmMain::txtWriteSNR2KeyPress(TObject *Sender, char &Key)
{
    if (!((Key >= '0' && Key <= '9') || (Key >= 'a' && Key <= 'f') || Key == '\b'))
    {
        Key = 0;
    }
    
}
//---------------------------------------------------------------------------

void __fastcall TfrmMain::txtWriteData2KeyPress(TObject *Sender, char &Key)
{
    if (!((Key >= '0' && Key <= '9') || (Key >= 'a' && Key <= 'f') || Key == '\b'))
    {
        Key = 0;
    }
    
}
//---------------------------------------------------------------------------

void __fastcall TfrmMain::txtLockSNR2KeyPress(TObject *Sender, char &Key)
{
    if (!((Key >= '0' && Key <= '9') || (Key >= 'a' && Key <= 'f') || Key == '\b'))
    {
        Key = 0;
    }
}
//---------------------------------------------------------------------------



void __fastcall TfrmMain::BitBtn6Click(TObject *Sender)
{

    //
    unsigned char cardNo[32];
    unsigned devAddr = 0x00;

    char pShow[256];

    txtRet->Text = "";
    int nRet =  RDM_GetSnr(m_commHandle, devAddr, cardNo);

     hexToStr(cardNo, 5 , pShow, 256);
     txtRet->Text = pShow;
}
//---------------------------------------------------------------------------





