/*
* File:         SRAPIV2.H
* Created:      03/09/2001
* Description:  The Function Prototype Header File for SRAPIV2.DLL
*					 (The API for Serial Reader (SR100, SR120, SR950 .. etc)
*
*****************************************************************************
*
* Version | Date     | Description
* --------+----------+------------------------------------------------
* V1.02   | 03/12/01 | Date type of the parameter Value in the MIFARE

*********************************************************************************/

/******************* System Functions ******************************/


//1. Set communciation baudrate.
 HANDLE (WINAPI * API_OpenComm)(char *com,int Baudrate);

//2. To close communication port.
 int (WINAPI * API_CloseComm)(HANDLE commHandle);



/******************* MIFARE High Level Functions ******************************/
 int (WINAPI * API_PCDRead)(HANDLE commHandle,int DeviceAddress,unsigned char mode,
                                                        unsigned char blk_add,unsigned char num_blk,
                                                        unsigned char *snr, unsigned char *buffer);

 int (WINAPI * API_PCDWrite)(HANDLE commHandle,int DeviceAddress,unsigned char mode,
                                                        unsigned char blk_add,unsigned char num_blk,
                                                        unsigned char *snr, unsigned char *buffer);

 int (WINAPI * API_PCDInitVal)(int DeviceAddress,unsigned char mode,
                                                         unsigned char SectNum,
                                                         unsigned char *snr, int value);

 int (WINAPI * API_PCDDec)(int DeviceAddress,unsigned char mode,
                                                   unsigned char SectNum,
                                                   unsigned char *snr, int *value);

 int (WINAPI * API_PCDInc)(int DeviceAddress,unsigned char mode,
                                                   unsigned char SectNum,
                                                   unsigned char *snr, int *value);


 int (WINAPI * RDM_GetSnr)(HANDLE commHandle, int deviceAddress,
                                unsigned char *pCardNo);

 // REQA发送寻卡指令.
 int (WINAPI * MF_Request)(HANDLE commHandle,
	 int DeviceAddress, 
	 unsigned char inf_mode,
	 unsigned char *Buffer) ;
//  Select 选择卡
int (WINAPI *  MF_Select)(HANDLE commHandle,
						  int DeviceAddress,
						  unsigned char *snr);
// Halt 中断卡  选择卡，使卡进入被中断的状态…
int (WINAPI *  MF_Halt)(HANDLE commHandle,
						int DeviceAddress);
//Anticoll 防冲突
int (WINAPI *  MF_Anticoll)(HANDLE commHandle,
						  int DeviceAddress,
						  unsigned char *snr,
						  unsigned char &status) ;
int (WINAPI *  MF_Restore)(HANDLE          commHandle,
						   int             DeviceAddress, 
						   unsigned char   mode, 
						   unsigned char   cardlength, 
						   unsigned char   *carddata );
int (WINAPI *  GET_SNR)(HANDLE  commHandle,
						int DeviceAddress, 
						unsigned char mode,
						unsigned char RDM_halt,
						unsigned char *snr, 
						unsigned char *value);
int (WINAPI *  API_ControlBuzzer)( HANDLE commHandle,
								  int DeviceAddress,
								  unsigned char freq,
								  unsigned char duration,
								  unsigned char *buffer);
int (WINAPI *  GetVersionNum)(
							  int DeviceAddress,
							  char *VersionNum);

int (WINAPI *  API_SetDeviceAddress)(
									 HANDLE commHandle,
									 int DeviceAddress,
									 unsigned char newAddr,
									 unsigned char *buffer);
int (WINAPI *  API_SetSerNum)(
							  HANDLE commHandle,
							  int DeviceAddress,
							  unsigned char *newValue,
							  unsigned char *buffer);
int (WINAPI *  API_GetSerNum)(
							  HANDLE commHandle,
							  int DeviceAddress,
							  unsigned char *buffer);
