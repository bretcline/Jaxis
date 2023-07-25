//---------------------------------------------------------------------------

#ifndef mainH
#define mainH
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
#include <ComCtrls.hpp>
#include <Menus.hpp>
#include <ToolWin.hpp>
#include <ExtCtrls.hpp>
#include <Buttons.hpp>
#include <ImgList.hpp>
#include <Mask.hpp>

const int MinBlkBegin = 0;
const int MaxBlkBegin = 256;

const int MinBlkNun = 1;
const int MaxBlkNum = 256;


//---------------------------------------------------------------------------
class TfrmMain : public TForm
{
__published:	// IDE-managed Components
        TMainMenu *MainMenu1;
        TMenuItem *N2;
        TMenuItem *N1;
        TMenuItem *N3;
        TMenuItem *N4;
        TMenuItem *N6;
    TPageControl *pgMain;
        TTabSheet *TabSheet1;
        TTabSheet *TabSheet2;
        TTabSheet *TabSheet3;
        TTabSheet *TabSheet4;
        TTabSheet *TabSheet5;
        TPanel *Panel1;
        TGroupBox *GroupBox1;
        TStaticText *StaticText1;
        TStaticText *StaticText2;
    TComboBox *cmbPort;
    TComboBox *cmbBaudrate;
    TStatusBar *barMain;
        TPanel *Panel2;
    TGroupBox *GroupBox4;
    TStaticText *StaticText5;
    TEdit *txtReadStart;
    TEdit *txtReadNum;
    TEdit *txtWriteStart;
    TEdit *txtWriteKey;
    TGroupBox *GroupBox5;
    TPanel *Panel3;
    TGroupBox *GroupBox6;
    TStaticText *StaticText6;
    TEdit *txtCOS;
    TGroupBox *GroupBox7;
    TPanel *Panel4;
    TGroupBox *GroupBox9;
    TBitBtn *butReset;
    TEdit *txtKey4;
    TBitBtn *butKeyVerify;
    TStaticText *StaticText11;
    TStaticText *StaticText12;
    TBitBtn *butCreateFile;
    TEdit *txtFileNo4;
    TBitBtn *butSelectFile;
    TStaticText *StaticText13;
    TEdit *txtSelectFile4;
    TStaticText *StaticText14;
    TEdit *txtRecord;
    TBitBtn *butWriteRecord;
    TBitBtn *butReadRecord;
    TBitBtn *txtModifyRecord;
    TEdit *txtModifyKey4;
    TStaticText *StaticText16;
    TPanel *Panel5;
    TGroupBox *GroupBox10;
    TStaticText *StaticText17;
    TStaticText *StaticText18;
    TStaticText *StaticText19;
    TStaticText *StaticText20;
    TEdit *txtReadFlags2;
    TEdit *txtReadNum2;
    TEdit *txtReadSNR2;
    TGroupBox *GroupBox11;
    TBitBtn *butRead2;
    TCoolBar *CoolBar1;
    TBitBtn *cmbCOM;
    TBitBtn *BitBtn3;
    TBitBtn *BitBtn4;
    TGroupBox *GroupBox16;
    TGroupBox *GroupBox17;
    TStaticText *StaticText28;
    TStaticText *StaticText29;
    TEdit *txtRecord4;
    TEdit *txtSpace4;
    TGroupBox *GroupBox18;
    TGroupBox *GroupBox19;
    TGroupBox *GroupBox20;
    TGroupBox *GroupBox21;
    TMenuItem *N7;
    TStaticText *StaticText3;
    TStaticText *lab;
    TEdit *txtReadKey;
    TPanel *Panel6;
    TRadioButton *butReadA;
    TRadioButton *butReadB;
    TStaticText *StaticText30;
    TBitBtn *butRead;
    TStaticText *StaticText31;
    TStaticText *StaticText32;
    TStaticText *StaticText33;
    TStaticText *StaticText34;
    TEdit *txtWriteData;
    TGroupBox *GroupBox2;
    TPanel *Panel7;
    TRadioButton *butWriteA;
    TRadioButton *butWriteB;
    TBitBtn *BitBtn2;
    TStaticText *StaticText21;
    TStaticText *StaticText22;
    TStaticText *StaticText23;
    TStaticText *StaticText25;
    TGroupBox *GroupBox13;
    TEdit *txtWriteSNR2;
    TEdit *txtWriteNum2;
    TEdit *txtWriteFlags2;
    TEdit *txtWriteData2;
    TBitBtn *butWrite2;
    TStaticText *StaticText24;
    TStaticText *StaticText26;
    TStaticText *StaticText27;
    TGroupBox *GroupBox15;
    TEdit *txtLockStart2;
    TEdit *txtLockSNR2;
    TEdit *txtLockFlags2;
    TBitBtn *butLock2;
    TImageList *imgTab;
    TTimer *tmMain;
    TImageList *imgBar;
    TMenuItem *MaiFareCommand1;
    TMenuItem *l5693Command1;
    TMenuItem *TypeBCommand1;
    TMenuItem *WatchCardCommand1;
    TImageList *imgMenu;
    TMenuItem *N5;
    TGroupBox *GroupBox3;
    TEdit *txtRet1;
    TEdit *txtRet2;
    TEdit *txtRet3;
    TEdit *txtRet41;
    TEdit *txtRet42;
    TEdit *txtRet43;
    TEdit *txtRet;
    TStaticText *StaticText4;
    TStaticText *labBlk1;
    TStaticText *labBlk2;
    TStaticText *labBlk3;
    TStaticText *labBlk4;
    TStaticText *StaticText35;
    TEdit *txtWriteBlk;
    TStaticText *StaticText36;
    TEdit *txtWriteStart2;
    TEdit *txtReadStart2;
    TGroupBox *GroupBox12;
    TStaticText *StaticText37;
    TGroupBox *GroupBox14;
    TGroupBox *GroupBox8;
    TStaticText *StaticText7;
    TStaticText *txtCosRet;
    TMemo *mmRet;
    TGroupBox *GroupBox22;
    TStaticText *StaticText9;
    TStaticText *StaticText10;
    TEdit *txtRecordNo;
    TEdit *txtRecordLen;
    TStaticText *StaticText15;
    TStaticText *StaticText38;
    TEdit *txtReadNo;
    TEdit *txtReadLen;
    TGroupBox *GroupBox23;
    TStaticText *StaticText8;
    TMemo *mmWCRet;
    TBitBtn *butLoadKey;
    TMemo *mm15693;
    TGroupBox *GroupBox24;
    TBitBtn *BitBtn5;
    TLabel *Label1;
    TComboBox *ComboBox1;
    TLabel *Label2;
    TEdit *Edit1;
    TLabel *Label3;
    TEdit *Edit2;
    TGroupBox *GroupBox25;
    TLabel *Label4;
    TEdit *Edit3;
    TLabel *Label5;
    TEdit *Edit4;
    TBitBtn *BitBtn1;
    TLabel *Label6;
    TEdit *Edit5;
    TBitBtn *BitBtn6;
    void __fastcall FormActivate(TObject *Sender);
    void __fastcall pgMainChange(TObject *Sender);
    void __fastcall cmbCOMClick(TObject *Sender);
    void __fastcall tmMainTimer(TObject *Sender);
    void __fastcall barMainDrawPanel(TStatusBar *StatusBar,
          TStatusPanel *Panel, const TRect &Rect);
    void __fastcall N4Click(TObject *Sender);
    void __fastcall MaiFareCommand1Click(TObject *Sender);
    void __fastcall l5693Command1Click(TObject *Sender);
    void __fastcall TypeBCommand1Click(TObject *Sender);
    void __fastcall WatchCardCommand1Click(TObject *Sender);
    void __fastcall N5Click(TObject *Sender);
    void __fastcall butReadClick(TObject *Sender);
    void __fastcall BitBtn2Click(TObject *Sender);
    void __fastcall butCreateFileClick(TObject *Sender);
    void __fastcall butSelectFileClick(TObject *Sender);
    void __fastcall txtModifyRecordClick(TObject *Sender);
    void __fastcall butResetClick(TObject *Sender);
    void __fastcall N6Click(TObject *Sender);
    void __fastcall txtReadKeyKeyPress(TObject *Sender, char &Key);
    void __fastcall txtReadStartKeyPress(TObject *Sender, char &Key);
    void __fastcall txtReadNumKeyPress(TObject *Sender, char &Key);
    void __fastcall txtWriteStartKeyPress(TObject *Sender, char &Key);
    void __fastcall txtWriteBlkKeyPress(TObject *Sender, char &Key);
    void __fastcall txtWriteKeyKeyPress(TObject *Sender, char &Key);
    void __fastcall txtWriteDataKeyPress(TObject *Sender, char &Key);
    void __fastcall txtCOSKeyPress(TObject *Sender, char &Key);
    void __fastcall txtKey4KeyPress(TObject *Sender, char &Key);
    void __fastcall txtFileNo4KeyPress(TObject *Sender, char &Key);
    void __fastcall txtRecord4KeyPress(TObject *Sender, char &Key);
    void __fastcall txtSpace4KeyPress(TObject *Sender, char &Key);
    void __fastcall txtSelectFile4KeyPress(TObject *Sender, char &Key);
    void __fastcall txtModifyKey4KeyPress(TObject *Sender, char &Key);
    void __fastcall txtReadNoKeyPress(TObject *Sender, char &Key);
    void __fastcall txtRecordLenKeyPress(TObject *Sender, char &Key);
    void __fastcall txtRecordKeyPress(TObject *Sender, char &Key);
    void __fastcall txtReadFlags2KeyPress(TObject *Sender, char &Key);
    void __fastcall txtReadStart2KeyPress(TObject *Sender, char &Key);
    void __fastcall txtReadNum2KeyPress(TObject *Sender, char &Key);
    void __fastcall txtWriteFlags2KeyPress(TObject *Sender, char &Key);
    void __fastcall txtWriteStart2KeyPress(TObject *Sender, char &Key);
    void __fastcall txtWriteNum2KeyPress(TObject *Sender, char &Key);
    void __fastcall txtLockFlags2KeyPress(TObject *Sender, char &Key);
    void __fastcall txtLockStart2KeyPress(TObject *Sender, char &Key);
    void __fastcall txtReadSNR2KeyPress(TObject *Sender, char &Key);
    void __fastcall txtWriteSNR2KeyPress(TObject *Sender, char &Key);
    void __fastcall txtWriteData2KeyPress(TObject *Sender, char &Key);
    void __fastcall txtLockSNR2KeyPress(TObject *Sender, char &Key);
    void __fastcall BitBtn6Click(TObject *Sender);
private:	// User declarations
public:		// User declarations

    // com port
    char m_szPort[128];

    // baudrate
    unsigned int m_nBaudrate;
    HANDLE m_commHandle;

    // current com state
    int m_bOpen;


    int __fastcall strToHex(const char *pStr, int maxSize, unsigned char *pBuffer, int maxBuffer) const;
    unsigned char  __fastcall charToHex(const char srcChar);


    int __fastcall hexToStr(const unsigned char *pHex, int maxSize, char *pBuffer, int maxBuffer) const;

        __fastcall TfrmMain(TComponent* Owner);
};
//---------------------------------------------------------------------------
extern PACKAGE TfrmMain *frmMain;
//---------------------------------------------------------------------------
#endif
