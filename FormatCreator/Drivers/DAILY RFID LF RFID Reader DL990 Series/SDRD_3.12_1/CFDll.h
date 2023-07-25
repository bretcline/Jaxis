#ifndef _EXnPort_CFFUNC_H_
#define _EXnPort_CFFUNC_H_



#define  LIB_SUCCESS      0		//return successful result
#define  LIB_FAILED       1		//return failed result


#define  MAX_RF_BUFFER    512

// ����ģʽ����
#define		WORKMODE_CF		 0
#define		WORKMODE_SD		 1


/******** ���ܣ��趨����ģʽ *******************/
//	����	: bWorkMode 
//				0	- CF ����ģʽ
//				1	- SD ����ģʽ
/*********************************************************/
int WINAPI SetWorkMode(BYTE bWorkMode);


/******** ���ܣ���ȡ��̬��汾�� 2�ֽ� *******************/
//  ����: �ɹ�����0
/*********************************************************/
int WINAPI LibVersion(unsigned int *pVer);


/********* ���ܣ���ʼ���˿� ****************/
//������nPort�����ںţ�ȡֵΪ0��
//baud��ΪͨѶ������4800��115200
//���أ��ɹ��򷵻�0
/*******************************************/
int WINAPI CFInitCom(int nPort, long nBaud);

/********* ���ܣ��رն˿� ****************/
int WINAPI CFCloseCom();

/******** ���ܣ�ָ���豸��ʶ *******************************/
//  ������wDevID��ͨѶ�豸��ʶ����0-65536
//  ���أ��ɹ��򷵻�0
/*********************************************************/
int WINAPI CFSetDeviceNumber(WORD wDevID);

/******** ���ܣ�ȡ���豸��ʶ *****************************/
//  ������pDevID��ͨѶ�豸��ʶ����0-65536
//  ���أ��ɹ�����0
/*********************************************************/
int WINAPI CFGetDeviceNumber(WORD *pDevID);

/******** ���ܣ�ȡ�ö�д����Ӳ���汾�ţ�2 �ֽ� ***********/
//  ������wDevID	��  ͨѶ�豸��ʶ��
//        pVersion	��	��ŷ��ذ汾��Ϣ��0x400, ��ʾΪ4.0
//  ���أ��ɹ��򷵻�0
/*********************************************************/
int WINAPI CFGetHardwareVersion(WORD wDevID, WORD *pVersion);

/******** ���ܣ�ȡ�ö�д������Ʒ���кţ�8 �ֽ� ***********/
//  ������wDevID��ͨѶ�豸��ʶ��
//        pSnr��  ��ŷ��ض�д������Ʒ���к�,��0xA1, 0xA2, ��ʽ�����ʾΪ"A1A2"
//  ���أ��ɹ��򷵻�0
/*********************************************************/
int WINAPI CFGetReaderNo(WORD wDevID, BYTE *pSnr);

/******** ���ܣ����� *************************************/
//  ������wDevID��ͨѶ�豸��ʶ��
//        bMs�� ����ʱ�ޣ���λ��10 ����
//  ���أ��ɹ��򷵻�0
/*********************************************************/
int	WINAPI CFBeep(WORD wDevID, BYTE bMs);

/******** ���ܣ����ö�д����sam ��ͨѶ������ *************/
//  ������wDevID��ͨѶ�豸��ʶ��
//        bound: sam �������ʣ�ȡֵΪ9600��38400
//  ���أ��ɹ��򷵻�0
//  ˵����bound=0:9600
//        bound=1:38400
/*********************************************************/
int WINAPI CFSetSamBaud(WORD wDevID, BYTE bBaud);

/******* ���ܣ���λsam �� ********************************/
//  ������wDevID�� ͨѶ�豸��ʶ��
//        pData�� ���صĸ�λ��Ϣ����,��0xA1, 0xA2, ��ʽ�����ʾΪ"A1A2"
//        pLength�����ظ�λ��Ϣ�ĳ���
//  ���أ��ɹ��򷵻�0
/*********************************************************/
int WINAPI CFSamReset(WORD wDevID, BYTE *pData, BYTE* pLength);




/******** ���ܣ���SAM ������COS ���� *********************/
//  ������wDevID��  ͨѶ�豸��ʶ��
//        pCommand��cos ����
//        bCmdLen:  cos �����
//        pDate��  ��Ƭ���ص����ݣ���SW1��SW2
//        pLength�� �������ݳ���
//  ���أ��ɹ��򷵻�0
/*********************************************************/

int WINAPI CFSamCos(WORD wDevID, BYTE *pCommand, BYTE bCmdLen, BYTE *pData, BYTE *pLength);



/*******  ���ܣ����ö�д�����ǽӴ�������ʽΪ *************/ 
//              ISO14443 TYPE A OR ISO14443 TYPE B
//  ������wDevID��ͨѶ�豸��ʶ��
//        bType:  ��д����������ʽ
//  ���أ��ɹ��򷵻�0
//  ˵����bType=0:����ΪTYPE_A��ʽ
//        bType=1':����ΪTYPE_B��ʽ
//        bType=2:����ΪAT88RF020����ʽ
//        bType=3:����ΪISO15693����ʽ
/*********************************************************/
int WINAPI CFSetWorkMode(WORD wDevID, BYTE bType);

/*******  ���ܣ��رջ�������д�������߷��� ***************/
//  ������wDevID��ͨѶ�豸��ʶ��
//        bMode������״̬
//  ���أ��ɹ��򷵻�0
//  ˵����bMode=0:�ر�����
//        bMode=1:��������
/*********************************************************/
int WINAPI CFSetAntennaStatus(WORD wDevID, BYTE bMode);

/******** ���ܣ�ѰISO14443-3 TYPE_A �� *******************/
//  ������wDevID��  ͨѶ�豸��ʶ��
//        bMode��  Ѱ��ģʽ
//        pTagType�����ؿ�����ֵ
//		  pLength	: ���ؿ�����ֵ����
//  ���أ��ɹ��򷵻�0
//  ˵����bMode=0x26:Ѱδ��������״̬�Ŀ�
//        bMode=0x52:Ѱ����״̬�Ŀ�
/*********************************************************/
int WINAPI CFISO14443_3ARequest(WORD wDevID, BYTE bMode, BYTE *pTagType, BYTE *pLength);

/********* ���ܣ�ISO14443-3 TYPE_A ������ײ **************/
//  ������wDevID	��  ͨѶ�豸��ʶ��
//        pSnr		��  ���صĿ����к�
//        pLength	��	�����кų���
//  ���أ��ɹ��򷵻�0
/*********************************************************/
//int WINAPI CFISO14443_3AAnticoll(WORD wDevID, BYTE bcnt, BYTE *pSnr,BYTE* pRLength);
int WINAPI CFISO14443_3AAnticoll(WORD wDevID, BYTE *pSnr, BYTE* pLength);

    



/******** ���ܣ�����һ��ISO14443-3 TYPE_A �� *************/
//  ������wDevID��ͨѶ�豸��ʶ��
//        pSnr�� �����к�
//        bLen:�����кų��ȣ�MifareOne����ֵ����4
//        pLength ���ؿ�����
//  ���أ��ɹ��򷵻�0
/*********************************************************/
int WINAPI CFISO14443_3ASelect(WORD wDevID, BYTE *pSnr, BYTE bLen, BYTE *pLength);

/******* ���ܣ������Ѽ����ISO14443-3 TYPE_A����������״̬*/
//  ������wDevID��ͨѶ�豸��ʶ��
//  ���أ��ɹ��򷵻�0
/**********************************************************/
int WINAPI CFISO14443_3AHalt(WORD wDevID);


/***** ���ܣ���ָ������Կ��֤Mifare One ��*****************/
/*******  ���ܣ���ȡMifare One ��һ������ ****************/
//  ������wDevID�� ͨѶ�豸��ʶ��
//		  bMode	:	��Կ����, 0x60 ='A', 0x61 = 'B'
//        bBlock�� M1�����Կ��
//        pKey	: 6�ֽ���Կ
//  ���أ��ɹ��򷵻�0
/*********************************************************/
int WINAPI CFISO14443_3AAuthentication2(WORD wDevID, BYTE bMode, BYTE bBlock, BYTE *pKey);



/*******  ���ܣ���ȡMifare One��UltraLight ��һ������ ****************/
//  ������wDevID��ͨѶ�豸��ʶ��
//        bBlock��M1�����Կ��
//        pData	����ȡ�����ݣ�16 �ֽ�
//  ���أ��ɹ��򷵻�0
/*********************************************************/
int WINAPI CFISO14443_3ARead(WORD wDevID, BYTE bBlock, BYTE *pData);


/*******  ���ܣ���Mifare One ����д��һ������ ************/
//  ������wDevID��ͨѶ�豸��ʶ��
//        bBlock��M1�����Կ��
//        pData	��д������ݣ�16 �ֽ�
//  ���أ��ɹ��򷵻�0
/*********************************************************/
int WINAPI CFISO14443_3AWrite(WORD wDevID, BYTE bBlock, BYTE *pData);


/*******  ���ܣ���Mifare One ��ĳһ������ʼ��ΪǮ�� *******/
//  ������wDevID��ͨѶ�豸��ʶ��
//        bBlock��M1 �����ַ
//        lValue����ʼֵ
//  ���أ��ɹ��򷵻�0
/*********************************************************/
int WINAPI CFISO14443_3APurseInit(WORD wDevID, BYTE bBlock, long lValue);

/*******  ���ܣ���Mifare One Ǯ��ֵ **********************/
//  ������wDevID�� ͨѶ�豸��ʶ��
//        bBlock�� M1 �����ַ
//        plValue�����ص�ֵ
//  ���أ��ɹ��򷵻�0
/*********************************************************/
int WINAPI CFISO14443_3AReadVal(WORD wDevID, BYTE bBlock, long *plValue);


/*******  ���ܣ�Mifare One �ۿ� **************************/
//  ������wDevID��ͨѶ�豸��ʶ��
//        bBlock��M1 �����ַ
//        lValue��Ҫ�۵�ֵ
//  ���أ��ɹ��򷵻�0
//  ˵�����˺���ִ�гɹ��󣬽�������ڿ�Ƭ��BUFFER �ڣ�
//        ��δ��д��Ӧ������ݣ���Ҫ��������浽��Ƭ
//        ��Ӧ���������ִ�� CFISO14443_3ARestore ����
/*********************************************************/
int WINAPI CFISO14443_3ADecrement(WORD wDevID, BYTE bBlock, long lValue);

/******** ���ܣ�Mifare One ��ֵ **************************/
//  ������wDevID��ͨѶ�豸��ʶ��
//        bBlock��M1 �����ַ
//        lValue��Ҫ���ӵ�ֵ
//  ���أ��ɹ��򷵻�0
/*********************************************************/
int WINAPI CFISO14443_3AIncrement(WORD wDevID, BYTE bBlock, long lValue);

/******** ���ܣ�Mifare One ��ֵ�ش� **********************/
//  ������wDevID��ͨѶ�豸��ʶ��
//        bBlock��M1 �����ַ
//  ���أ��ɹ��򷵻�0
//  ˵�����ô˺�����ָ���Ŀ����ݴ��뿨��buffer��Ȼ�����
//        CFISO14443_3ATransfer()������buffer �������ٴ��͵���һ����ȥ
/*********************************************************/
int WINAPI CFISO14443_3ARestore(WORD wDevID, BYTE bBlock);

/****** ���ܣ���Mifare One���ݴ��� ***********************/
//  ������wDevID��ͨѶ�豸��ʶ��
//        bBlock��M1 �����ַ
//  ���أ��ɹ��򷵻�0
//  ˵�����ú������� CFISO14443_3AIncrement��CFISO14443_3ADecrement��CFISO14443_3ARestore ����֮����á�
/*********************************************************/
int WINAPI CFISO14443_3ATransfer(WORD wDevID, BYTE bBlock);


/****** ���ܣ�����һ��ultra light�� **********************/
//  ������wDevID��ͨѶ�豸��ʶ��
//        pSnr�� ����7�ֽڿ����к�
//  ���أ��ɹ��򷵻�0
/*********************************************************/
int WINAPI CFULSelect(WORD wDevID, BYTE *pSnr);



/******** ���ܣ���ultra light����д��һ������ ************/
//  ������wDevID�� ͨѶ�豸��ʶ��
//        bPage��  ultra light��ҳ��ַ��0��0x0f��
//        pData��  д������ݣ�16�ֽ�
//        pRev��   ����7�ֽ����к�
//  ���أ��ɹ��򷵻�0
/*********************************************************/
int WINAPI CFULWrite(WORD wDevID, BYTE bPage, BYTE *pData, BYTE *pRev);


/****** ���ܣ���λ����ISO14443-A��׼��CPU�� **********************/
//  ������wDevID��ͨѶ�豸��ʶ��
//		  bMode: Ѱ����ʽ0x52��std,0x26=WUPa
//        pData	�����صĸ�λ��Ϣ����
//        pLength�����кų���
//  ���أ��ɹ��򷵻�0
/*********************************************************/
int WINAPI CFTypeAReset(WORD wDevID, BYTE bMode, BYTE *pData, BYTE *pLength);

/******** ���ܣ������ISO14443-A ��׼��CPU ������COS ���� ********/
//������	wDevID��ͨѶ�豸��ʶ��
//			pCommand��cos ����
//			bCLen	:�����
//			pData����Ƭ���ص����ݣ���SW1��SW2
//			pLength���������ݳ���
//			���أ��ɹ��򷵻�0
/*****************************************************************/
int WINAPI CFCosCommand(WORD wDevID, BYTE*pCommand, BYTE bCLen,BYTE *pData, BYTE *pLength);

/******** ���ܣ�Ѱ����ISO14443-B ��׼�Ŀ� ************************/
//������	wDevID	��ͨѶ�豸��ʶ��
//			bMode	��Ѱ����ʽ0��REQB,1=WUPB
//			pData	���������ݳ���(12���ֽ�)
//���أ�	�ɹ��򷵻�0
/*****************************************************************/
int WINAPI CFSearchISO14443_3B(WORD wDevID, BYTE bMode, BYTE *pData);





/******** ���ܣ�CFISO15693_Inventory ***********************/
//  ������wDevID��ͨѶ�豸��ʶ��
//        Pdata: ���ص����ݣ�1�ֽ�DSFID+8�ֽ�UID
//        pLength:	 Pdata����
//  ���أ��ɹ��򷵻�0
/*********************************************************/
int WINAPI CFISO15693_Inventory(WORD wDevID, BYTE *pData, BYTE *pLength);


/******** ���ܣ�CFISO15693_Stay_Quiet **********************/
//  ������wDevID��ͨѶ�豸��ʶ��
//        UID:	 UID 8�ֽ�
//  ���أ��ɹ��򷵻�0
/*********************************************************/
int WINAPI CFISO15693_Stay_Quiet(WORD wDevID, BYTE *pUID);


/******** ���ܣ�CFISO15693_Select **************************/
//  ������wDevID��ͨѶ�豸��ʶ��
//        UID:	 UID 8�ֽ�
//  ���أ��ɹ��򷵻�0
/*********************************************************/
int WINAPI CFISO15693_Select(WORD wDevID, BYTE *pUID);


/******** ���ܣ�CFISO15693_ResetToReady ******************/
//  ������wDevID��ͨѶ�豸��ʶ��
//        model: bit0=Select_flags,bit1=Addres_flags
//        UID:	 UID 8�ֽ�
//  ���أ��ɹ��򷵻�0
/*********************************************************/
int WINAPI CFISO15693_ResetToReady(WORD wDevID, BYTE bMode, BYTE *pUID);


/******** ���ܣ�CFISO15693Read ***************************/
//  ������wDevID��ͨѶ�豸��ʶ��
//        bMode	: bit0=Select_flags,bit1=Addres_flags
//        pUID	: UID 8�ֽ�
//        bBlock: ���
//        bNumber:Ҫ��ȡ�Ŀ�����< 0x10
//        pData: ���ص�����
//        pLength:  �������ݵĳ���
//  ���أ��ɹ��򷵻�0
/*********************************************************/
int WINAPI CFISO15693Read(WORD wDevID, 
							 BYTE  bMode,
                             BYTE  *pUID,
                             BYTE  bBlock,
                             BYTE  bNumber,
                             BYTE  *pData,
                             BYTE  *pLength);


/******** ���ܣ�CFISO15693Write ***************************/
//  ������wDevID��ͨѶ�豸��ʶ��
//        model: bit0=Select_flags,bit1=Addres_flags
//        UID:	 UID 8�ֽ�
//        bBlock: ���
//        data:  Ҫд������ݣ�4�ֽ�
//  ���أ��ɹ��򷵻�0
/*********************************************************/
int WINAPI CFISO15693Write(WORD wDevID, 
					BYTE  bMode,
					BYTE  *pUID,
					BYTE  bBlock,
					BYTE *pData);
			      
			 
/******** ���ܣ�CFISO15693LockBlock **********************/
//  ������wDevID��ͨѶ�豸��ʶ��
//        model: bit0=Select_flags,bit1=Addres_flags
//        UID:	 UID 8�ֽ�
//        bBlock: ���
//  ���أ��ɹ��򷵻�0
/*********************************************************/
int WINAPI CFISO15693LockBlock(WORD wDevID, BYTE  bMode, BYTE  *pUID, BYTE  bBlock);


/******** ���ܣ�CFISO15693WriteAFI ***********************/
//  ������wDevID��ͨѶ�豸��ʶ��
//        model: bit0=Select_flags,bit1=Addres_flags
//        UID:	 UID 8�ֽ�
//        AFI:   Ҫд���AFI
//  ���أ��ɹ��򷵻�0
/*********************************************************/
int WINAPI CFISO15693WriteAFI(WORD wDevID, BYTE  bMode, BYTE  *pUID, BYTE bAFI);


/******** ���ܣ�CFISO15693LockAFI ************************/
//  ������wDevID��ͨѶ�豸��ʶ��
//        model: bit0=Select_flags,bit1=Addres_flags
//        UID:	 UID 8�ֽ�
//  ���أ��ɹ��򷵻�0
/*********************************************************/
int WINAPI CFISO15693LockAFI(WORD wDevID, BYTE  bMode, BYTE  *pUID);


/******** ���ܣ�CFISO15693WriteDSFID *********************/
//  ������wDevID��ͨѶ�豸��ʶ��
//        model	: bit0=Select_flags,bit1=Addres_flags
//        pUID	: UID 8�ֽ�
//        bDSFID: Ҫд���DSFID
//  ���أ��ɹ��򷵻�0
/*********************************************************/
int WINAPI CFISO15693WriteDSFID(WORD wDevID, BYTE bMode, BYTE *pUID, BYTE bDSFID);


/******** ���ܣ�CFISO15693LockDSFID **********************/
//  ������wDevID��ͨѶ�豸��ʶ��
//        model: bit0=Select_flags,bit1=Addres_flags
//        UID:	 UID 8�ֽ�
//  ���أ��ɹ��򷵻�0
/*********************************************************/
int WINAPI CFISO15693LockDSFID(WORD wDevID, BYTE bMode, BYTE *pUID);


/******** ���ܣ�CFISO15693GetSystemInformation **********/
//  ������wDevID��ͨѶ�豸��ʶ��
//        bMode	: bit0=Select_flags,bit1=Addres_flags
//        pUID	: UID 8�ֽ�
//        bDSFID: 1�ֽ�DSFID
//        bAFI	: 1�ֽ�AFI
//  ���أ��ɹ��򷵻�0
/*********************************************************/
int WINAPI CFISO15693GetSystemInformation(WORD wDevID, 
					       BYTE  bMode,
					       BYTE  *pUID,
					       BYTE  *pDSFID, 
					       BYTE  *pAFI);
					       

/******** ���ܣ�CFISO15693GetBlockSecurity **************/
//  ������wDevID��ͨѶ�豸��ʶ��
//        bMode	: bit0=Select_flags,bit1=Addres_flags
//        pUID	: UID 8�ֽ�
//        bBlock: ���
//        bNumber:Ҫ��ȡ�Ŀ�����< 0x40
//        pData: ���ص�����
//        pLength:  �������ݵĳ���
//  ���أ��ɹ��򷵻�0
/*********************************************************/
int WINAPI CFISO15693GetBlockSecurity(WORD wDevID,
					   BYTE  bMode,
					   BYTE  *pUID,
					   BYTE  bBlock,
					   BYTE  bNumber, 
                       BYTE  *pData,
                       BYTE  *pLength);


/******** ���ܣ�ѰƬ *************************************/
//  ������wDevID	��ͨѶ�豸��ʶ��
//        pType		����Ƭ����: 0x01	-	Mifare A
//								0x02	-	Utra Light
//								0x03	-	IS015693
//		  pSn		:	��Ƭ���к�
//  ���أ�0���ɹ�
//        1��ʧ��
/********************************************************************/
int WINAPI RFRequest(WORD wDevID, BYTE *pType, BYTE* pSn);



#endif