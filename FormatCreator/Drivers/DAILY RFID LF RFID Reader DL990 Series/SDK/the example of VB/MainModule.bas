Attribute VB_Name = "Module2"
  
  Declare Function OpenComm Lib "mi.dll" _
        Alias "API_OpenComm" (ByRef comm As Byte, ByVal nBoudrate As Long) As Long

    Declare Function CloseComm Lib "mi.dll" _
        Alias "API_CloseComm" (ByVal handle As Long) As Integer


   Declare Function API_PCDRead Lib "mi.dll" _
        (ByVal handle As Integer, ByVal deviceAddr As Integer, _
        ByVal mode As Byte, ByVal blk_Addr As Byte, ByVal Num_blk As Byte, _
        ByRef snr As Byte, _
        ByRef Buffer As Byte) As Integer



    Declare Function API_PCDWrite Lib "mi.dll" _
        (ByVal handle As Integer, ByVal deviceAddr As Integer, _
        ByVal mode As Byte, ByVal blk_Addr As Byte, ByVal Num_blk As Byte, _
        ByRef snr As Byte, _
        ByRef Buffer As Byte) As Integer
        
  '设置通讯地址
   Declare Function API_SetDeviceAddress Lib "mi.dll" _
        (ByVal handle As Integer, ByVal deviceAddr As Integer, _
        ByVal new_Addr As Byte, _
        ByRef Buffer As Byte) As Integer
        
  '计算机的比特率
   Declare Function API_SetBandrate Lib "mi.dll" _
        (ByVal handle As Integer, ByVal deviceAddr As Integer, _
        ByVal newBaud As Byte, _
        ByRef Buffer As Byte) As Integer



'设置灯的工作状态，包括，灯亮的周期以及循环的次数
Declare Function API_ControlLED Lib "mi.dll" _
        (ByVal handle As Integer, ByVal deviceAddr As Integer, _
        ByVal freq As Byte, ByVal duration As Byte, _
        ByRef Buffer As Byte) As Integer
        
        
'设置灯的工作状态，包括，灯亮的周期以及循环的次数
Declare Function API_ControlBuzzer Lib "mi.dll" _
        (ByVal handle As Integer, ByVal deviceAddr As Integer, _
        ByVal freq As Byte, ByVal duration As Byte, _
        ByRef Buffer As Byte) As Integer
        
'读取由厂家预设的1个字节的读卡器地址和8个字节序列号
Declare Function API_GetSerNum Lib "mi.dll" _
        (ByVal handle As Integer, ByVal deviceAddr As Integer, _
        ByRef Buffer As Byte) As Integer
        
        '读取由厂家预设的1个字节的读卡器地址和8个字节序列号
Declare Function API_SetSerNum Lib "mi.dll" _
        (ByVal handle As Integer, ByVal deviceAddr As Integer, _
        ByRef newvalue As Byte, ByRef Buffer As Byte) As Integer
        
        
'自动读取卡Ultralight的卡号
Declare Function UL_Request Lib "mi.dll" _
        (ByVal handle As Integer, ByVal deviceAddr As Integer, _
        ByVal mode As Byte, _
        ByRef snr As Byte) As Integer
        
        '选择卡，使卡进入被中断的状态…
Declare Function MF_Halt Lib "mi.dll" _
        (ByVal handle As Integer, ByVal deviceAddr As Integer) As Integer
'读取卡中指定扇区的数据
Declare Function UL_HLRead Lib "mi.dll" _
        (ByVal handle As Integer, ByVal deviceAddr As Integer, _
        ByVal mode As Byte, ByVal blk_Addr As Byte, _
        ByRef snr As Byte, _
        ByRef Buffer As Byte) As Integer
        
        '写取卡中指定扇区的数据
Declare Function UL_HLWrite Lib "mi.dll" _
        (ByVal handle As Integer, ByVal deviceAddr As Integer, _
        ByVal mode As Byte, ByVal blk_Addr As Byte, _
        ByRef snr As Byte, _
        ByRef Buffer As Byte) As Integer
        
          '写取卡中指定扇区的数据
Declare Function ISO15693_Inventory Lib "mi.dll" _
        (ByVal handle As Integer, ByVal deviceAddr As Integer, _
        ByRef snr As Byte, _
        ByRef Buffer As Byte) As Integer
        
          '读取数据
Declare Function API_ISO15693Read Lib "mi.dll" _
        (ByVal handle As Integer, ByVal deviceAddr As Integer, _
        ByVal flags As Byte, ByVal blk_Addr As Integer, ByVal Num_blk As Integer, _
        ByRef uid As Byte, _
        ByRef Buffer As Byte) As Integer
        
          '写数据
Declare Function API_ISO15693Write Lib "mi.dll" _
        (ByVal handle As Integer, ByVal deviceAddr As Integer, _
        ByVal flags As Byte, ByVal blk_Addr As Integer, ByVal Num_blk As Integer, _
        ByRef uid As Byte, _
        ByRef Buffer As Byte) As Integer
        
        
Declare Function API_PCDInitVal Lib "mi.dll" _
        (ByVal handle As Integer, ByVal deviceAddr As Integer, _
        ByVal mode As Byte, ByVal Num_blk As Byte, _
        ByRef snr As Byte, _
        ByRef Buffer As Byte) As Integer
        
Declare Function API_PCDDec Lib "mi.dll" _
        (ByVal handle As Integer, ByVal deviceAddr As Integer, _
        ByVal mode As Byte, ByVal Num_blk As Byte, _
        ByRef snr As Byte, _
        ByRef Buffer As Byte) As Integer
        
Declare Function API_PCDInc Lib "mi.dll" _
        (ByVal handle As Integer, ByVal deviceAddr As Integer, _
        ByVal mode As Byte, ByVal Num_blk As Byte, _
        ByRef snr As Byte, _
        ByRef Buffer As Byte) As Integer
        
        
Declare Function GET_SNR Lib "mi.dll" _
        (ByVal handle As Integer, ByVal deviceAddr As Integer, _
        ByVal mode As Byte, ByVal half As Byte, _
        ByRef snr As Byte, _
        ByRef Buffer As Byte) As Integer






