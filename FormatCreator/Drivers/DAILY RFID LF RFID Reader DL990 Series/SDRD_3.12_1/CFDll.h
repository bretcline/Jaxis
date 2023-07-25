#ifndef _EXnPort_CFFUNC_H_
#define _EXnPort_CFFUNC_H_



#define  LIB_SUCCESS      0		//return successful result
#define  LIB_FAILED       1		//return failed result


#define  MAX_RF_BUFFER    512

// 工作模式定义
#define		WORKMODE_CF		 0
#define		WORKMODE_SD		 1


/******** 功能：设定工作模式 *******************/
//	参数	: bWorkMode 
//				0	- CF 工作模式
//				1	- SD 工作模式
/*********************************************************/
int WINAPI SetWorkMode(BYTE bWorkMode);


/******** 功能：获取动态库版本号 2字节 *******************/
//  返回: 成功返回0
/*********************************************************/
int WINAPI LibVersion(unsigned int *pVer);


/********* 功能：初始化端口 ****************/
//参数：nPort：串口号，取值为0～
//baud：为通讯波特率4800～115200
//返回：成功则返回0
/*******************************************/
int WINAPI CFInitCom(int nPort, long nBaud);

/********* 功能：关闭端口 ****************/
int WINAPI CFCloseCom();

/******** 功能：指定设备标识 *******************************/
//  参数：wDevID：通讯设备标识符，0-65536
//  返回：成功则返回0
/*********************************************************/
int WINAPI CFSetDeviceNumber(WORD wDevID);

/******** 功能：取得设备标识 *****************************/
//  参数：pDevID：通讯设备标识符，0-65536
//  返回：成功返回0
/*********************************************************/
int WINAPI CFGetDeviceNumber(WORD *pDevID);

/******** 功能：取得读写卡器硬件版本号，2 字节 ***********/
//  参数：wDevID	：  通讯设备标识符
//        pVersion	：	存放返回版本信息如0x400, 表示为4.0
//  返回：成功则返回0
/*********************************************************/
int WINAPI CFGetHardwareVersion(WORD wDevID, WORD *pVersion);

/******** 功能：取得读写卡器产品序列号，8 字节 ***********/
//  参数：wDevID：通讯设备标识符
//        pSnr：  存放返回读写卡器产品序列号,如0xA1, 0xA2, 格式化后表示为"A1A2"
//  返回：成功则返回0
/*********************************************************/
int WINAPI CFGetReaderNo(WORD wDevID, BYTE *pSnr);

/******** 功能：蜂鸣 *************************************/
//  参数：wDevID：通讯设备标识符
//        bMs： 蜂鸣时限，单位是10 毫秒
//  返回：成功则返回0
/*********************************************************/
int	WINAPI CFBeep(WORD wDevID, BYTE bMs);

/******** 功能：设置读写卡器sam 卡通讯波特率 *************/
//  参数：wDevID：通讯设备标识符
//        bound: sam 卡波特率，取值为9600、38400
//  返回：成功则返回0
//  说明：bound=0:9600
//        bound=1:38400
/*********************************************************/
int WINAPI CFSetSamBaud(WORD wDevID, BYTE bBaud);

/******* 功能：复位sam 卡 ********************************/
//  参数：wDevID： 通讯设备标识符
//        pData： 返回的复位信息内容,如0xA1, 0xA2, 格式化后表示为"A1A2"
//        pLength：返回复位信息的长度
//  返回：成功则返回0
/*********************************************************/
int WINAPI CFSamReset(WORD wDevID, BYTE *pData, BYTE* pLength);




/******** 功能：向SAM 卡发送COS 命令 *********************/
//  参数：wDevID：  通讯设备标识符
//        pCommand：cos 命令
//        bCmdLen:  cos 命令长度
//        pDate：  卡片返回的数据，含SW1、SW2
//        pLength： 返回数据长度
//  返回：成功则返回0
/*********************************************************/

int WINAPI CFSamCos(WORD wDevID, BYTE *pCommand, BYTE bCmdLen, BYTE *pData, BYTE *pLength);



/*******  功能：设置读写卡器非接触工作方式为 *************/ 
//              ISO14443 TYPE A OR ISO14443 TYPE B
//  参数：wDevID：通讯设备标识符
//        bType:  读写卡器工作方式
//  返回：成功则返回0
//  说明：bType=0:设置为TYPE_A方式
//        bType=1':设置为TYPE_B方式
//        bType=2:设置为AT88RF020卡方式
//        bType=3:设置为ISO15693卡方式
/*********************************************************/
int WINAPI CFSetWorkMode(WORD wDevID, BYTE bType);

/*******  功能：关闭或启动读写卡器天线发射 ***************/
//  参数：wDevID：通讯设备标识符
//        bMode：天线状态
//  返回：成功则返回0
//  说明：bMode=0:关闭天线
//        bMode=1:开启天线
/*********************************************************/
int WINAPI CFSetAntennaStatus(WORD wDevID, BYTE bMode);

/******** 功能：寻ISO14443-3 TYPE_A 卡 *******************/
//  参数：wDevID：  通讯设备标识符
//        bMode：  寻卡模式
//        pTagType：返回卡类型值
//		  pLength	: 返回卡类型值长度
//  返回：成功则返回0
//  说明：bMode=0x26:寻未进入休眠状态的卡
//        bMode=0x52:寻所有状态的卡
/*********************************************************/
int WINAPI CFISO14443_3ARequest(WORD wDevID, BYTE bMode, BYTE *pTagType, BYTE *pLength);

/********* 功能：ISO14443-3 TYPE_A 卡防冲撞 **************/
//  参数：wDevID	：  通讯设备标识符
//        pSnr		：  返回的卡序列号
//        pLength	：	卡序列号长度
//  返回：成功则返回0
/*********************************************************/
//int WINAPI CFISO14443_3AAnticoll(WORD wDevID, BYTE bcnt, BYTE *pSnr,BYTE* pRLength);
int WINAPI CFISO14443_3AAnticoll(WORD wDevID, BYTE *pSnr, BYTE* pLength);

    



/******** 功能：锁定一张ISO14443-3 TYPE_A 卡 *************/
//  参数：wDevID：通讯设备标识符
//        pSnr： 卡序列号
//        bLen:卡序列号长度，MifareOne卡该值等于4
//        pLength 返回卡容量
//  返回：成功则返回0
/*********************************************************/
int WINAPI CFISO14443_3ASelect(WORD wDevID, BYTE *pSnr, BYTE bLen, BYTE *pLength);

/******* 功能：命令已激活的ISO14443-3 TYPE_A卡进入休眠状态*/
//  参数：wDevID：通讯设备标识符
//  返回：成功则返回0
/**********************************************************/
int WINAPI CFISO14443_3AHalt(WORD wDevID);


/***** 功能：用指定的密钥验证Mifare One 卡*****************/
/*******  功能：读取Mifare One 卡一块数据 ****************/
//  参数：wDevID： 通讯设备标识符
//		  bMode	:	密钥属性, 0x60 ='A', 0x61 = 'B'
//        bBlock： M1卡绝对块号
//        pKey	: 6字节密钥
//  返回：成功则返回0
/*********************************************************/
int WINAPI CFISO14443_3AAuthentication2(WORD wDevID, BYTE bMode, BYTE bBlock, BYTE *pKey);



/*******  功能：读取Mifare One、UltraLight 卡一块数据 ****************/
//  参数：wDevID：通讯设备标识符
//        bBlock：M1卡绝对块号
//        pData	：读取的数据，16 字节
//  返回：成功则返回0
/*********************************************************/
int WINAPI CFISO14443_3ARead(WORD wDevID, BYTE bBlock, BYTE *pData);


/*******  功能：向Mifare One 卡中写入一块数据 ************/
//  参数：wDevID：通讯设备标识符
//        bBlock：M1卡绝对块号
//        pData	：写入的数据，16 字节
//  返回：成功则返回0
/*********************************************************/
int WINAPI CFISO14443_3AWrite(WORD wDevID, BYTE bBlock, BYTE *pData);


/*******  功能：将Mifare One 卡某一扇区初始化为钱包 *******/
//  参数：wDevID：通讯设备标识符
//        bBlock：M1 卡块地址
//        lValue：初始值
//  返回：成功则返回0
/*********************************************************/
int WINAPI CFISO14443_3APurseInit(WORD wDevID, BYTE bBlock, long lValue);

/*******  功能：读Mifare One 钱包值 **********************/
//  参数：wDevID： 通讯设备标识符
//        bBlock： M1 卡块地址
//        plValue：返回的值
//  返回：成功则返回0
/*********************************************************/
int WINAPI CFISO14443_3AReadVal(WORD wDevID, BYTE bBlock, long *plValue);


/*******  功能：Mifare One 扣款 **************************/
//  参数：wDevID：通讯设备标识符
//        bBlock：M1 卡块地址
//        lValue：要扣的值
//  返回：成功则返回0
//  说明：此函数执行成功后，结果保存在卡片的BUFFER 内，
//        尚未改写相应块的内容，若要将结果保存到卡片
//        相应块中需紧跟执行 CFISO14443_3ARestore 函数
/*********************************************************/
int WINAPI CFISO14443_3ADecrement(WORD wDevID, BYTE bBlock, long lValue);

/******** 功能：Mifare One 充值 **************************/
//  参数：wDevID：通讯设备标识符
//        bBlock：M1 卡块地址
//        lValue：要增加的值
//  返回：成功则返回0
/*********************************************************/
int WINAPI CFISO14443_3AIncrement(WORD wDevID, BYTE bBlock, long lValue);

/******** 功能：Mifare One 卡值回传 **********************/
//  参数：wDevID：通讯设备标识符
//        bBlock：M1 卡块地址
//  返回：成功则返回0
//  说明：用此函数将指定的块内容传入卡的buffer，然后可用
//        CFISO14443_3ATransfer()函数将buffer 中数据再传送到另一块中去
/*********************************************************/
int WINAPI CFISO14443_3ARestore(WORD wDevID, BYTE bBlock);

/****** 功能：将Mifare One数据传送 ***********************/
//  参数：wDevID：通讯设备标识符
//        bBlock：M1 卡块地址
//  返回：成功则返回0
//  说明：该函数仅在 CFISO14443_3AIncrement、CFISO14443_3ADecrement和CFISO14443_3ARestore 命令之后调用。
/*********************************************************/
int WINAPI CFISO14443_3ATransfer(WORD wDevID, BYTE bBlock);


/****** 功能：锁定一张ultra light卡 **********************/
//  参数：wDevID：通讯设备标识符
//        pSnr： 返回7字节卡序列号
//  返回：成功则返回0
/*********************************************************/
int WINAPI CFULSelect(WORD wDevID, BYTE *pSnr);



/******** 功能：向ultra light卡中写入一块数据 ************/
//  参数：wDevID： 通讯设备标识符
//        bPage：  ultra light卡页地址（0～0x0f）
//        pData：  写入的数据，16字节
//        pRev：   返回7字节序列号
//  返回：成功则返回0
/*********************************************************/
int WINAPI CFULWrite(WORD wDevID, BYTE bPage, BYTE *pData, BYTE *pRev);


/****** 功能：复位符合ISO14443-A标准的CPU卡 **********************/
//  参数：wDevID：通讯设备标识符
//		  bMode: 寻卡方式0x52＝std,0x26=WUPa
//        pData	：返回的复位信息内容
//        pLength：序列号长度
//  返回：成功则返回0
/*********************************************************/
int WINAPI CFTypeAReset(WORD wDevID, BYTE bMode, BYTE *pData, BYTE *pLength);

/******** 功能：向符合ISO14443-A 标准的CPU 卡发送COS 命令 ********/
//参数：	wDevID：通讯设备标识符
//			pCommand：cos 命令
//			bCLen	:命令长度
//			pData：卡片返回的数据，含SW1、SW2
//			pLength：返回数据长度
//			返回：成功则返回0
/*****************************************************************/
int WINAPI CFCosCommand(WORD wDevID, BYTE*pCommand, BYTE bCLen,BYTE *pData, BYTE *pLength);

/******** 功能：寻符合ISO14443-B 标准的卡 ************************/
//参数：	wDevID	：通讯设备标识符
//			bMode	：寻卡方式0＝REQB,1=WUPB
//			pData	：返回数据长度(12个字节)
//返回：	成功则返回0
/*****************************************************************/
int WINAPI CFSearchISO14443_3B(WORD wDevID, BYTE bMode, BYTE *pData);





/******** 功能：CFISO15693_Inventory ***********************/
//  参数：wDevID：通讯设备标识符
//        Pdata: 返回的数据，1字节DSFID+8字节UID
//        pLength:	 Pdata长度
//  返回：成功则返回0
/*********************************************************/
int WINAPI CFISO15693_Inventory(WORD wDevID, BYTE *pData, BYTE *pLength);


/******** 功能：CFISO15693_Stay_Quiet **********************/
//  参数：wDevID：通讯设备标识符
//        UID:	 UID 8字节
//  返回：成功则返回0
/*********************************************************/
int WINAPI CFISO15693_Stay_Quiet(WORD wDevID, BYTE *pUID);


/******** 功能：CFISO15693_Select **************************/
//  参数：wDevID：通讯设备标识符
//        UID:	 UID 8字节
//  返回：成功则返回0
/*********************************************************/
int WINAPI CFISO15693_Select(WORD wDevID, BYTE *pUID);


/******** 功能：CFISO15693_ResetToReady ******************/
//  参数：wDevID：通讯设备标识符
//        model: bit0=Select_flags,bit1=Addres_flags
//        UID:	 UID 8字节
//  返回：成功则返回0
/*********************************************************/
int WINAPI CFISO15693_ResetToReady(WORD wDevID, BYTE bMode, BYTE *pUID);


/******** 功能：CFISO15693Read ***************************/
//  参数：wDevID：通讯设备标识符
//        bMode	: bit0=Select_flags,bit1=Addres_flags
//        pUID	: UID 8字节
//        bBlock: 块号
//        bNumber:要读取的块数，< 0x10
//        pData: 返回的数据
//        pLength:  返回数据的长度
//  返回：成功则返回0
/*********************************************************/
int WINAPI CFISO15693Read(WORD wDevID, 
							 BYTE  bMode,
                             BYTE  *pUID,
                             BYTE  bBlock,
                             BYTE  bNumber,
                             BYTE  *pData,
                             BYTE  *pLength);


/******** 功能：CFISO15693Write ***************************/
//  参数：wDevID：通讯设备标识符
//        model: bit0=Select_flags,bit1=Addres_flags
//        UID:	 UID 8字节
//        bBlock: 块号
//        data:  要写入的数据，4字节
//  返回：成功则返回0
/*********************************************************/
int WINAPI CFISO15693Write(WORD wDevID, 
					BYTE  bMode,
					BYTE  *pUID,
					BYTE  bBlock,
					BYTE *pData);
			      
			 
/******** 功能：CFISO15693LockBlock **********************/
//  参数：wDevID：通讯设备标识符
//        model: bit0=Select_flags,bit1=Addres_flags
//        UID:	 UID 8字节
//        bBlock: 块号
//  返回：成功则返回0
/*********************************************************/
int WINAPI CFISO15693LockBlock(WORD wDevID, BYTE  bMode, BYTE  *pUID, BYTE  bBlock);


/******** 功能：CFISO15693WriteAFI ***********************/
//  参数：wDevID：通讯设备标识符
//        model: bit0=Select_flags,bit1=Addres_flags
//        UID:	 UID 8字节
//        AFI:   要写入的AFI
//  返回：成功则返回0
/*********************************************************/
int WINAPI CFISO15693WriteAFI(WORD wDevID, BYTE  bMode, BYTE  *pUID, BYTE bAFI);


/******** 功能：CFISO15693LockAFI ************************/
//  参数：wDevID：通讯设备标识符
//        model: bit0=Select_flags,bit1=Addres_flags
//        UID:	 UID 8字节
//  返回：成功则返回0
/*********************************************************/
int WINAPI CFISO15693LockAFI(WORD wDevID, BYTE  bMode, BYTE  *pUID);


/******** 功能：CFISO15693WriteDSFID *********************/
//  参数：wDevID：通讯设备标识符
//        model	: bit0=Select_flags,bit1=Addres_flags
//        pUID	: UID 8字节
//        bDSFID: 要写入的DSFID
//  返回：成功则返回0
/*********************************************************/
int WINAPI CFISO15693WriteDSFID(WORD wDevID, BYTE bMode, BYTE *pUID, BYTE bDSFID);


/******** 功能：CFISO15693LockDSFID **********************/
//  参数：wDevID：通讯设备标识符
//        model: bit0=Select_flags,bit1=Addres_flags
//        UID:	 UID 8字节
//  返回：成功则返回0
/*********************************************************/
int WINAPI CFISO15693LockDSFID(WORD wDevID, BYTE bMode, BYTE *pUID);


/******** 功能：CFISO15693GetSystemInformation **********/
//  参数：wDevID：通讯设备标识符
//        bMode	: bit0=Select_flags,bit1=Addres_flags
//        pUID	: UID 8字节
//        bDSFID: 1字节DSFID
//        bAFI	: 1字节AFI
//  返回：成功则返回0
/*********************************************************/
int WINAPI CFISO15693GetSystemInformation(WORD wDevID, 
					       BYTE  bMode,
					       BYTE  *pUID,
					       BYTE  *pDSFID, 
					       BYTE  *pAFI);
					       

/******** 功能：CFISO15693GetBlockSecurity **************/
//  参数：wDevID：通讯设备标识符
//        bMode	: bit0=Select_flags,bit1=Addres_flags
//        pUID	: UID 8字节
//        bBlock: 块号
//        bNumber:要读取的块数，< 0x40
//        pData: 返回的数据
//        pLength:  返回数据的长度
//  返回：成功则返回0
/*********************************************************/
int WINAPI CFISO15693GetBlockSecurity(WORD wDevID,
					   BYTE  bMode,
					   BYTE  *pUID,
					   BYTE  bBlock,
					   BYTE  bNumber, 
                       BYTE  *pData,
                       BYTE  *pLength);


/******** 功能：寻片 *************************************/
//  参数：wDevID	：通讯设备标识符
//        pType		：卡片类型: 0x01	-	Mifare A
//								0x02	-	Utra Light
//								0x03	-	IS015693
//		  pSn		:	卡片序列号
//  返回：0：成功
//        1：失败
/********************************************************************/
int WINAPI RFRequest(WORD wDevID, BYTE *pType, BYTE* pSn);



#endif