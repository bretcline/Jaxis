object frmMain: TfrmMain
  Left = 76
  Top = 108
  Width = 930
  Height = 652
  Caption = 'MiFDemo'
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  Icon.Data = {
    0000010002002020100000000000E80200002600000010101000000000002801
    00000E0300002800000020000000400000000100040000000000800200000000
    0000000000000000000000000000000000000000800000800000008080008000
    0000800080008080000080808000C0C0C0000000FF0000FF000000FFFF00FF00
    0000FF00FF00FFFF0000FFFFFF00888888880BBBBBB078888888888888888888
    88880BBBBB007888888888888888888888880BBBBBB078888888888888888888
    88880BBB00007888888888888888888888880BBBBBB078888888888888888888
    88880BBBBB007888888888888888888888880BBBBBB078888888888888888888
    88880BBBB0007888888888888888888888880BBBBBB078888888888888888888
    88880BBBBB007888888888888888888888880BBBBBB078888888888888888888
    88880BBB00007888888888888888888888880BBBBBB078888888888888888888
    88880BBBBB007888888888888888777777770333333077777777777777770000
    00000000000000000000000000000BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB0BBB
    BBBBBBBBBBBBBBBBBBBBBBBBBBBB0BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB0BBB
    BBBBBBBBBBBBBBBBBBBBBBBBBBBB0BBBBBBBBBBBBBBB0BBBBBBB0BBBBBBB0BBB
    0BBB0BBB0BBB0BBB0BBB0BBB0BBB0B0B0B0B0B0B0B0B0B0B0B0B0B0B0B0B0000
    0000000000000000000000000000888888880BBBBBB078888888888888888888
    88880BBBBB007888888888888888888888880BBBBBB078888888888888888888
    88880BBBB0007888888888888888888888880BBBBBB078888888888888888888
    88880BBBBB007888888888888888888888880BBBBBB078888888888888888888
    8888000000007888888888888888000000000000000000000000000000000000
    0000000000000000000000000000000000000000000000000000000000000000
    0000000000000000000000000000000000000000000000000000000000000000
    0000000000000000000000000000000000000000000000000000000000000000
    0000000000000000000000000000280000001000000020000000010004000000
    0000C00000000000000000000000000000000000000000000000000080000080
    00000080800080000000800080008080000080808000C0C0C0000000FF0000FF
    000000FFFF00FF000000FF00FF00FFFF0000FFFFFF008880BBB0788888888880
    B000788888888880BBB0788888888880BB00788888888880BBB0788888888880
    B000788888887770BBB07777777700000000000000000BBBBBBBBBBBBBBB0B0B
    BB0BBB0BBB0B0B0B0B0B0B0B0B0B00000000000000008880BBB0788888888880
    B000788888888880BBB078888888888000007888888800000000000000000000
    0000000000000000000000000000000000000000000000000000000000000000
    00000000000000000000000000000000000000000000}
  Menu = MainMenu1
  OldCreateOrder = False
  OnActivate = FormActivate
  PixelsPerInch = 96
  TextHeight = 13
  object pgMain: TPageControl
    Left = 0
    Top = 5
    Width = 922
    Height = 575
    ActivePage = TabSheet5
    Align = alClient
    Images = imgTab
    MultiLine = True
    Style = tsFlatButtons
    TabIndex = 4
    TabOrder = 0
    OnChange = pgMainChange
    object TabSheet1: TTabSheet
      Caption = 'System Setup'
      object Panel1: TPanel
        Left = 0
        Top = 0
        Width = 914
        Height = 543
        Align = alClient
        BevelInner = bvLowered
        BevelOuter = bvNone
        Color = clMedGray
        TabOrder = 0
        object GroupBox1: TGroupBox
          Left = 40
          Top = 40
          Width = 409
          Height = 153
          Caption = #36890#35759#21442#25968#35774#32622
          TabOrder = 0
          object StaticText1: TStaticText
            Left = 32
            Top = 48
            Width = 100
            Height = 17
            Caption = #36890#35759#31471#21475#65288'COM'#65289
            TabOrder = 0
          end
          object StaticText2: TStaticText
            Left = 32
            Top = 96
            Width = 40
            Height = 17
            Caption = #27874#29305#29575
            TabOrder = 1
          end
          object cmbPort: TComboBox
            Left = 136
            Top = 48
            Width = 225
            Height = 21
            Style = csDropDownList
            ImeName = #25340#38899#21152#21152
            ItemHeight = 13
            TabOrder = 2
          end
          object cmbBaudrate: TComboBox
            Left = 136
            Top = 96
            Width = 225
            Height = 21
            Style = csDropDownList
            ImeName = #25340#38899#21152#21152
            ItemHeight = 13
            TabOrder = 3
          end
        end
        object cmbCOM: TBitBtn
          Left = 376
          Top = 208
          Width = 75
          Height = 25
          Caption = #25171#24320
          TabOrder = 1
          OnClick = cmbCOMClick
        end
        object GroupBox24: TGroupBox
          Left = 32
          Top = 264
          Width = 793
          Height = 201
          Caption = 'GroupBox24'
          TabOrder = 2
          object Label1: TLabel
            Left = 48
            Top = 40
            Width = 67
            Height = 13
            Caption = 'Block Number'
          end
          object Label2: TLabel
            Left = 308
            Top = 44
            Width = 68
            Height = 13
            Caption = 'Block Address'
          end
          object Label3: TLabel
            Left = 52
            Top = 116
            Width = 62
            Height = 13
            Caption = 'Return Value'
          end
          object Label4: TLabel
            Left = 324
            Top = 116
            Width = 36
            Height = 13
            Caption = 'CardNo'
          end
          object Label5: TLabel
            Left = 324
            Top = 72
            Width = 51
            Height = 13
            Caption = 'Write Data'
          end
          object Label6: TLabel
            Left = 88
            Top = 72
            Width = 25
            Height = 13
            Caption = 'KeyA'
          end
          object BitBtn5: TBitBtn
            Left = 192
            Top = 164
            Width = 75
            Height = 21
            Caption = 'Read'
            TabOrder = 0
          end
          object ComboBox1: TComboBox
            Left = 144
            Top = 36
            Width = 121
            Height = 21
            Style = csDropDownList
            ImeName = #25340#38899#21152#21152
            ItemHeight = 13
            TabOrder = 1
            Items.Strings = (
              '1'
              '2'
              '3'
              '4')
          end
          object Edit1: TEdit
            Left = 392
            Top = 40
            Width = 121
            Height = 21
            ImeName = #25340#38899#21152#21152
            TabOrder = 2
          end
          object Edit2: TEdit
            Left = 144
            Top = 112
            Width = 65
            Height = 21
            ImeName = #25340#38899#21152#21152
            TabOrder = 3
          end
          object GroupBox25: TGroupBox
            Left = 56
            Top = 92
            Width = 681
            Height = 9
            TabOrder = 4
          end
          object Edit3: TEdit
            Left = 392
            Top = 112
            Width = 121
            Height = 21
            ImeName = #25340#38899#21152#21152
            TabOrder = 5
          end
          object Edit4: TEdit
            Left = 392
            Top = 68
            Width = 321
            Height = 21
            ImeName = #25340#38899#21152#21152
            TabOrder = 6
          end
          object BitBtn1: TBitBtn
            Left = 324
            Top = 160
            Width = 75
            Height = 25
            Caption = 'Write'
            TabOrder = 7
          end
          object Edit5: TEdit
            Left = 144
            Top = 68
            Width = 121
            Height = 21
            ImeName = #25340#38899#21152#21152
            TabOrder = 8
            Text = 'FFFFFFFFFFFF'
          end
        end
      end
    end
    object TabSheet2: TTabSheet
      Caption = 'MaiFare Command'
      ImageIndex = 1
      object Panel2: TPanel
        Left = 0
        Top = 0
        Width = 914
        Height = 543
        Align = alClient
        BevelOuter = bvLowered
        Color = clMedGray
        TabOrder = 0
        object GroupBox4: TGroupBox
          Left = 36
          Top = 40
          Width = 773
          Height = 221
          Caption = #21629#20196#21442#25968
          TabOrder = 0
          object StaticText5: TStaticText
            Left = 396
            Top = 36
            Width = 52
            Height = 17
            Caption = #36215#22987#22359#21495
            TabOrder = 0
          end
          object txtReadStart: TEdit
            Left = 396
            Top = 56
            Width = 53
            Height = 21
            ImeName = #25340#38899#21152#21152
            TabOrder = 1
            OnKeyPress = txtReadStartKeyPress
          end
          object txtReadNum: TEdit
            Left = 456
            Top = 56
            Width = 57
            Height = 21
            ImeName = #25340#38899#21152#21152
            TabOrder = 2
            Text = '1'
            OnKeyPress = txtReadNumKeyPress
          end
          object txtWriteStart: TEdit
            Left = 280
            Top = 152
            Width = 45
            Height = 21
            ImeName = #25340#38899#21152#21152
            TabOrder = 3
            Text = '8'
            OnKeyPress = txtWriteStartKeyPress
          end
          object txtWriteKey: TEdit
            Left = 172
            Top = 152
            Width = 101
            Height = 21
            ImeName = #25340#38899#21152#21152
            TabOrder = 4
            Text = 'ffffffffffff'
            OnKeyPress = txtWriteKeyKeyPress
          end
          object GroupBox5: TGroupBox
            Left = 36
            Top = 48
            Width = 713
            Height = 9
            TabOrder = 5
          end
          object StaticText3: TStaticText
            Left = 172
            Top = 36
            Width = 28
            Height = 17
            Caption = #23494#38053
            TabOrder = 6
          end
          object lab: TStaticText
            Left = 456
            Top = 36
            Width = 28
            Height = 17
            Caption = #22359#25968
            TabOrder = 7
          end
          object txtReadKey: TEdit
            Left = 172
            Top = 57
            Width = 97
            Height = 21
            ImeName = #25340#38899#21152#21152
            TabOrder = 8
            Text = 'ffffffffffff'
            OnKeyPress = txtReadKeyKeyPress
          end
          object Panel6: TPanel
            Left = 36
            Top = 56
            Width = 125
            Height = 25
            TabOrder = 9
            object butReadA: TRadioButton
              Left = 8
              Top = 4
              Width = 57
              Height = 17
              Caption = #23494#38053'A'
              Checked = True
              TabOrder = 0
              TabStop = True
            end
            object butReadB: TRadioButton
              Left = 64
              Top = 4
              Width = 57
              Height = 17
              Caption = #23494#38053'B'
              TabOrder = 1
            end
          end
          object StaticText30: TStaticText
            Left = 36
            Top = 36
            Width = 52
            Height = 17
            Caption = #23494#38053#31867#22411
            TabOrder = 10
          end
          object butRead: TBitBtn
            Left = 676
            Top = 56
            Width = 75
            Height = 25
            Caption = #21345#29255#35835
            TabOrder = 11
            OnClick = butReadClick
          end
          object StaticText31: TStaticText
            Left = 40
            Top = 132
            Width = 52
            Height = 17
            Caption = #23494#38053#31867#22411
            TabOrder = 12
          end
          object StaticText32: TStaticText
            Left = 280
            Top = 132
            Width = 52
            Height = 17
            Caption = #36215#22987#22359#21495
            TabOrder = 13
          end
          object StaticText33: TStaticText
            Left = 172
            Top = 132
            Width = 28
            Height = 17
            Caption = #23494#38053
            TabOrder = 14
          end
          object StaticText34: TStaticText
            Left = 396
            Top = 132
            Width = 28
            Height = 17
            Caption = #25968#25454
            TabOrder = 15
          end
          object txtWriteData: TEdit
            Left = 396
            Top = 152
            Width = 277
            Height = 21
            ImeName = #25340#38899#21152#21152
            TabOrder = 16
            Text = '00000000000000000000000000000000'
            OnKeyPress = txtWriteDataKeyPress
          end
          object GroupBox2: TGroupBox
            Left = 40
            Top = 144
            Width = 713
            Height = 9
            TabOrder = 17
          end
          object Panel7: TPanel
            Left = 40
            Top = 152
            Width = 125
            Height = 25
            TabOrder = 18
            object butWriteA: TRadioButton
              Left = 4
              Top = 4
              Width = 57
              Height = 17
              Caption = #23494#38053'A'
              Checked = True
              TabOrder = 0
              TabStop = True
            end
            object butWriteB: TRadioButton
              Left = 64
              Top = 4
              Width = 57
              Height = 17
              Caption = #23494#38053'B'
              TabOrder = 1
            end
          end
          object BitBtn2: TBitBtn
            Left = 676
            Top = 152
            Width = 75
            Height = 25
            Caption = #21345#29255#20889
            TabOrder = 19
            OnClick = BitBtn2Click
          end
          object StaticText35: TStaticText
            Left = 332
            Top = 132
            Width = 28
            Height = 17
            Caption = #22359#25968
            TabOrder = 20
          end
          object txtWriteBlk: TEdit
            Left = 332
            Top = 152
            Width = 57
            Height = 21
            ImeName = #25340#38899#21152#21152
            ReadOnly = True
            TabOrder = 21
            Text = '1'
            OnKeyPress = txtWriteBlkKeyPress
          end
        end
        object GroupBox3: TGroupBox
          Left = 32
          Top = 300
          Width = 777
          Height = 201
          Caption = #21709#24212#21442#25968
          TabOrder = 1
          object txtRet1: TEdit
            Left = 80
            Top = 72
            Width = 473
            Height = 21
            ImeName = #25340#38899#21152#21152
            TabOrder = 0
          end
          object txtRet2: TEdit
            Left = 80
            Top = 100
            Width = 473
            Height = 21
            ImeName = #25340#38899#21152#21152
            TabOrder = 1
          end
          object txtRet3: TEdit
            Left = 80
            Top = 132
            Width = 473
            Height = 21
            ImeName = #25340#38899#21152#21152
            TabOrder = 2
          end
          object txtRet41: TEdit
            Left = 80
            Top = 160
            Width = 169
            Height = 21
            ImeName = #25340#38899#21152#21152
            TabOrder = 3
          end
          object txtRet42: TEdit
            Left = 256
            Top = 160
            Width = 117
            Height = 21
            ImeName = #25340#38899#21152#21152
            TabOrder = 4
          end
          object txtRet43: TEdit
            Left = 384
            Top = 160
            Width = 169
            Height = 21
            ImeName = #25340#38899#21152#21152
            TabOrder = 5
          end
          object txtRet: TEdit
            Left = 80
            Top = 32
            Width = 121
            Height = 21
            Color = clSilver
            ImeName = #25340#38899#21152#21152
            TabOrder = 6
          end
          object StaticText4: TStaticText
            Left = 28
            Top = 36
            Width = 28
            Height = 17
            Caption = #32467#26524
            TabOrder = 7
          end
          object labBlk1: TStaticText
            Left = 28
            Top = 72
            Width = 36
            Height = 17
            AutoSize = False
            Caption = 'xx'
            TabOrder = 8
          end
          object labBlk2: TStaticText
            Left = 28
            Top = 104
            Width = 36
            Height = 17
            AutoSize = False
            Caption = 'xx'
            TabOrder = 9
          end
          object labBlk3: TStaticText
            Left = 28
            Top = 132
            Width = 36
            Height = 21
            AutoSize = False
            Caption = 'xx'
            TabOrder = 10
          end
          object labBlk4: TStaticText
            Left = 28
            Top = 164
            Width = 36
            Height = 21
            AutoSize = False
            Caption = 'xx'
            TabOrder = 11
          end
        end
        object BitBtn6: TBitBtn
          Left = 676
          Top = 272
          Width = 131
          Height = 25
          Caption = 'GetCardNumber'
          TabOrder = 2
          OnClick = BitBtn6Click
        end
      end
    end
    object TabSheet3: TTabSheet
      Caption = '15693 Command'
      ImageIndex = 2
      object Panel5: TPanel
        Left = 0
        Top = 0
        Width = 914
        Height = 543
        Align = alClient
        BevelOuter = bvLowered
        Color = clMedGray
        TabOrder = 0
        object GroupBox10: TGroupBox
          Left = 32
          Top = 24
          Width = 741
          Height = 285
          Caption = #21629#20196
          TabOrder = 0
          object StaticText17: TStaticText
            Left = 28
            Top = 26
            Width = 29
            Height = 17
            Caption = 'Flags'
            TabOrder = 0
          end
          object StaticText18: TStaticText
            Left = 108
            Top = 22
            Width = 40
            Height = 17
            Caption = #39318#22359#21495
            TabOrder = 1
          end
          object StaticText19: TStaticText
            Left = 176
            Top = 22
            Width = 40
            Height = 17
            Caption = #22359#25968#30446
            TabOrder = 2
          end
          object StaticText20: TStaticText
            Left = 248
            Top = 22
            Width = 99
            Height = 17
            Caption = 'SNR'#65288#22320#22336#27169#24335#65289
            TabOrder = 3
          end
          object txtReadFlags2: TEdit
            Left = 24
            Top = 48
            Width = 73
            Height = 21
            ImeName = #25340#38899#21152#21152
            TabOrder = 4
            OnKeyPress = txtReadFlags2KeyPress
          end
          object txtReadStart2: TEdit
            Left = 108
            Top = 48
            Width = 61
            Height = 21
            ImeName = #25340#38899#21152#21152
            TabOrder = 5
            OnKeyPress = txtReadStart2KeyPress
          end
          object txtReadNum2: TEdit
            Left = 176
            Top = 48
            Width = 61
            Height = 21
            ImeName = #25340#38899#21152#21152
            TabOrder = 6
            OnKeyPress = txtReadNum2KeyPress
          end
          object txtReadSNR2: TEdit
            Left = 248
            Top = 48
            Width = 121
            Height = 21
            ImeName = #25340#38899#21152#21152
            TabOrder = 7
            OnKeyPress = txtReadSNR2KeyPress
          end
          object GroupBox11: TGroupBox
            Left = 24
            Top = 40
            Width = 685
            Height = 9
            TabOrder = 8
          end
          object butRead2: TBitBtn
            Left = 632
            Top = 48
            Width = 75
            Height = 25
            Caption = #35835#25968#25454
            TabOrder = 9
          end
          object StaticText21: TStaticText
            Left = 32
            Top = 122
            Width = 29
            Height = 17
            Caption = 'Flags'
            TabOrder = 10
          end
          object StaticText22: TStaticText
            Left = 176
            Top = 122
            Width = 40
            Height = 17
            Caption = #22359#25968#30446
            TabOrder = 11
          end
          object StaticText23: TStaticText
            Left = 248
            Top = 122
            Width = 99
            Height = 17
            Caption = 'SNR'#65288#22320#22336#27169#24335#65289
            TabOrder = 12
          end
          object StaticText25: TStaticText
            Left = 378
            Top = 121
            Width = 28
            Height = 17
            Caption = #25968#25454
            TabOrder = 13
          end
          object GroupBox13: TGroupBox
            Left = 24
            Top = 136
            Width = 685
            Height = 9
            TabOrder = 14
          end
          object txtWriteSNR2: TEdit
            Left = 248
            Top = 145
            Width = 121
            Height = 21
            ImeName = #25340#38899#21152#21152
            TabOrder = 15
            OnKeyPress = txtWriteSNR2KeyPress
          end
          object txtWriteNum2: TEdit
            Left = 176
            Top = 145
            Width = 65
            Height = 21
            ImeName = #25340#38899#21152#21152
            ReadOnly = True
            TabOrder = 16
            Text = '1'
            OnKeyPress = txtWriteNum2KeyPress
          end
          object txtWriteFlags2: TEdit
            Left = 28
            Top = 145
            Width = 73
            Height = 21
            ImeName = #25340#38899#21152#21152
            TabOrder = 17
            OnKeyPress = txtWriteFlags2KeyPress
          end
          object txtWriteData2: TEdit
            Left = 376
            Top = 145
            Width = 245
            Height = 21
            ImeName = #25340#38899#21152#21152
            TabOrder = 18
            OnKeyPress = txtWriteData2KeyPress
          end
          object butWrite2: TBitBtn
            Left = 632
            Top = 144
            Width = 75
            Height = 25
            Caption = #20889#25968#25454
            TabOrder = 19
          end
          object StaticText24: TStaticText
            Left = 32
            Top = 206
            Width = 29
            Height = 17
            Caption = 'Flags'
            TabOrder = 20
          end
          object StaticText26: TStaticText
            Left = 108
            Top = 206
            Width = 40
            Height = 17
            Caption = #39318#22359#21495
            TabOrder = 21
          end
          object StaticText27: TStaticText
            Left = 248
            Top = 206
            Width = 99
            Height = 17
            Caption = 'SNR'#65288#22320#22336#27169#24335#65289
            TabOrder = 22
          end
          object GroupBox15: TGroupBox
            Left = 24
            Top = 224
            Width = 685
            Height = 9
            TabOrder = 23
          end
          object txtLockStart2: TEdit
            Left = 108
            Top = 233
            Width = 89
            Height = 21
            ImeName = #25340#38899#21152#21152
            TabOrder = 24
            OnKeyPress = txtLockStart2KeyPress
          end
          object txtLockSNR2: TEdit
            Left = 248
            Top = 233
            Width = 121
            Height = 21
            ImeName = #25340#38899#21152#21152
            TabOrder = 25
            OnKeyPress = txtLockSNR2KeyPress
          end
          object txtLockFlags2: TEdit
            Left = 27
            Top = 233
            Width = 73
            Height = 21
            ImeName = #25340#38899#21152#21152
            TabOrder = 26
            OnKeyPress = txtLockFlags2KeyPress
          end
          object butLock2: TBitBtn
            Left = 632
            Top = 234
            Width = 75
            Height = 25
            Caption = #38145#23450
            TabOrder = 27
          end
          object StaticText36: TStaticText
            Left = 108
            Top = 122
            Width = 40
            Height = 17
            Caption = #39318#22359#21495
            TabOrder = 28
          end
          object txtWriteStart2: TEdit
            Left = 108
            Top = 145
            Width = 61
            Height = 21
            ImeName = #25340#38899#21152#21152
            TabOrder = 29
            OnKeyPress = txtWriteStart2KeyPress
          end
        end
        object GroupBox12: TGroupBox
          Left = 32
          Top = 340
          Width = 745
          Height = 201
          Caption = #21709#24212#21442#25968
          TabOrder = 1
          object StaticText37: TStaticText
            Left = 28
            Top = 36
            Width = 28
            Height = 17
            Caption = #32467#26524
            TabOrder = 0
          end
          object mm15693: TMemo
            Left = 76
            Top = 36
            Width = 629
            Height = 141
            ImeName = #25340#38899#21152#21152
            Lines.Strings = (
              '')
            TabOrder = 1
          end
        end
      end
    end
    object TabSheet4: TTabSheet
      Caption = 'ISO14443 TypeB Command'
      ImageIndex = 3
      object Panel3: TPanel
        Left = 0
        Top = 0
        Width = 914
        Height = 543
        Align = alClient
        BevelOuter = bvLowered
        Color = clMedGray
        TabOrder = 0
        object GroupBox6: TGroupBox
          Left = 36
          Top = 48
          Width = 625
          Height = 177
          Caption = #21629#20196#20449#24687
          TabOrder = 0
          object StaticText6: TStaticText
            Left = 24
            Top = 92
            Width = 98
            Height = 17
            Caption = 'Transfer_COS '#21629#20196
            TabOrder = 0
          end
          object txtCOS: TEdit
            Left = 24
            Top = 112
            Width = 453
            Height = 21
            ImeName = #25340#38899#21152#21152
            TabOrder = 1
            OnKeyPress = txtCOSKeyPress
          end
          object GroupBox7: TGroupBox
            Left = 24
            Top = 104
            Width = 569
            Height = 9
            TabOrder = 2
          end
          object BitBtn3: TBitBtn
            Left = 516
            Top = 112
            Width = 75
            Height = 25
            Caption = #21457#36865
            TabOrder = 3
          end
          object BitBtn4: TBitBtn
            Left = 514
            Top = 48
            Width = 75
            Height = 25
            Caption = #22797#20301
            TabOrder = 4
          end
          object GroupBox14: TGroupBox
            Left = 24
            Top = 40
            Width = 569
            Height = 9
            TabOrder = 5
          end
          object StaticText7: TStaticText
            Left = 24
            Top = 28
            Width = 28
            Height = 17
            Caption = #22797#20301
            TabOrder = 6
          end
        end
        object GroupBox8: TGroupBox
          Left = 40
          Top = 260
          Width = 621
          Height = 173
          Caption = #36820#22238#20449#24687
          TabOrder = 1
          object txtCosRet: TStaticText
            Left = 28
            Top = 44
            Width = 52
            Height = 17
            Caption = #36820#22238#32467#26524
            TabOrder = 0
          end
          object mmRet: TMemo
            Left = 100
            Top = 44
            Width = 465
            Height = 101
            ImeName = #25340#38899#21152#21152
            TabOrder = 1
          end
        end
      end
    end
    object TabSheet5: TTabSheet
      Caption = 'WatachCard Command'
      ImageIndex = 4
      object Panel4: TPanel
        Left = 0
        Top = 0
        Width = 914
        Height = 543
        Align = alClient
        BevelOuter = bvLowered
        Color = clMedGray
        TabOrder = 0
        object GroupBox9: TGroupBox
          Left = 28
          Top = 24
          Width = 913
          Height = 505
          TabOrder = 0
          object butReset: TBitBtn
            Left = 12
            Top = 108
            Width = 75
            Height = 25
            Caption = #22797#20301
            TabOrder = 0
            OnClick = butResetClick
          end
          object txtKey4: TEdit
            Left = 12
            Top = 40
            Width = 217
            Height = 21
            ImeName = #25340#38899#21152#21152
            TabOrder = 1
            OnKeyPress = txtKey4KeyPress
          end
          object butKeyVerify: TBitBtn
            Left = 244
            Top = 110
            Width = 75
            Height = 25
            Caption = #23494#38053#35748#35777
            TabOrder = 2
          end
          object StaticText11: TStaticText
            Left = 12
            Top = 19
            Width = 28
            Height = 17
            Caption = #23494#38053
            TabOrder = 3
          end
          object StaticText12: TStaticText
            Left = 12
            Top = 160
            Width = 40
            Height = 17
            Caption = #25991#20214#21495
            TabOrder = 4
          end
          object butCreateFile: TBitBtn
            Left = 244
            Top = 185
            Width = 75
            Height = 25
            Caption = #24314#31435#25991#20214
            TabOrder = 5
            OnClick = butCreateFileClick
          end
          object txtFileNo4: TEdit
            Left = 12
            Top = 184
            Width = 57
            Height = 21
            ImeName = #25340#38899#21152#21152
            TabOrder = 6
            OnKeyPress = txtFileNo4KeyPress
          end
          object butSelectFile: TBitBtn
            Left = 244
            Top = 248
            Width = 75
            Height = 25
            Caption = #36873#25321#25991#20214
            TabOrder = 7
            OnClick = butSelectFileClick
          end
          object StaticText13: TStaticText
            Left = 12
            Top = 228
            Width = 40
            Height = 17
            Caption = #25991#20214#21495
            TabOrder = 8
          end
          object txtSelectFile4: TEdit
            Left = 12
            Top = 251
            Width = 57
            Height = 21
            ImeName = #25340#38899#21152#21152
            TabOrder = 9
            OnKeyPress = txtSelectFile4KeyPress
          end
          object StaticText14: TStaticText
            Left = 152
            Top = 428
            Width = 64
            Height = 17
            Caption = #24453#20889#20837#20869#23481
            TabOrder = 10
          end
          object txtRecord: TEdit
            Left = 152
            Top = 448
            Width = 597
            Height = 21
            ImeName = #25340#38899#21152#21152
            TabOrder = 11
            OnKeyPress = txtRecordKeyPress
          end
          object butWriteRecord: TBitBtn
            Left = 800
            Top = 448
            Width = 75
            Height = 25
            Caption = #20889#35760#24405
            TabOrder = 12
          end
          object butReadRecord: TBitBtn
            Left = 244
            Top = 381
            Width = 75
            Height = 25
            Caption = #35835#35760#24405
            TabOrder = 13
          end
          object txtModifyRecord: TBitBtn
            Left = 240
            Top = 320
            Width = 75
            Height = 25
            Caption = #20462#25913#23494#38053
            Enabled = False
            TabOrder = 14
            OnClick = txtModifyRecordClick
          end
          object txtModifyKey4: TEdit
            Left = 13
            Top = 318
            Width = 208
            Height = 21
            ImeName = #25340#38899#21152#21152
            TabOrder = 15
            OnKeyPress = txtModifyKey4KeyPress
          end
          object StaticText16: TStaticText
            Left = 12
            Top = 296
            Width = 28
            Height = 17
            Caption = #23494#38053
            TabOrder = 16
          end
          object GroupBox16: TGroupBox
            Left = 12
            Top = 32
            Width = 305
            Height = 9
            TabOrder = 17
          end
          object GroupBox17: TGroupBox
            Left = 12
            Top = 440
            Width = 865
            Height = 9
            TabOrder = 18
          end
          object StaticText28: TStaticText
            Left = 80
            Top = 160
            Width = 52
            Height = 17
            Caption = #35760#24405#26465#25968
            TabOrder = 19
          end
          object StaticText29: TStaticText
            Left = 144
            Top = 160
            Width = 88
            Height = 17
            Caption = #27599#26465#35760#24405#30340#31354#38388
            TabOrder = 20
          end
          object txtRecord4: TEdit
            Left = 80
            Top = 185
            Width = 45
            Height = 21
            ImeName = #25340#38899#21152#21152
            TabOrder = 21
            OnKeyPress = txtRecord4KeyPress
          end
          object txtSpace4: TEdit
            Left = 144
            Top = 185
            Width = 85
            Height = 21
            ImeName = #25340#38899#21152#21152
            TabOrder = 22
            OnKeyPress = txtSpace4KeyPress
          end
          object GroupBox18: TGroupBox
            Left = 12
            Top = 241
            Width = 313
            Height = 9
            TabOrder = 23
          end
          object GroupBox19: TGroupBox
            Left = 12
            Top = 176
            Width = 313
            Height = 9
            TabOrder = 24
          end
          object GroupBox20: TGroupBox
            Left = 13
            Top = 371
            Width = 308
            Height = 9
            TabOrder = 25
          end
          object GroupBox21: TGroupBox
            Left = 14
            Top = 310
            Width = 303
            Height = 9
            TabOrder = 26
          end
          object GroupBox22: TGroupBox
            Left = 12
            Top = 100
            Width = 309
            Height = 9
            TabOrder = 27
          end
          object StaticText9: TStaticText
            Left = 8
            Top = 428
            Width = 40
            Height = 17
            Caption = #35760#24405#21495
            TabOrder = 28
          end
          object StaticText10: TStaticText
            Left = 76
            Top = 428
            Width = 52
            Height = 17
            Caption = #35760#24405#38271#24230
            TabOrder = 29
          end
          object txtRecordNo: TEdit
            Left = 12
            Top = 448
            Width = 53
            Height = 21
            ImeName = #25340#38899#21152#21152
            TabOrder = 30
          end
          object txtRecordLen: TEdit
            Left = 76
            Top = 448
            Width = 61
            Height = 21
            ImeName = #25340#38899#21152#21152
            TabOrder = 31
            OnKeyPress = txtRecordLenKeyPress
          end
          object StaticText15: TStaticText
            Left = 12
            Top = 360
            Width = 40
            Height = 17
            Caption = #35760#24405#21495
            TabOrder = 32
          end
          object StaticText38: TStaticText
            Left = 88
            Top = 360
            Width = 52
            Height = 17
            Caption = #35760#24405#38271#24230
            TabOrder = 33
          end
          object txtReadNo: TEdit
            Left = 12
            Top = 380
            Width = 53
            Height = 21
            ImeName = #25340#38899#21152#21152
            TabOrder = 34
            OnKeyPress = txtReadNoKeyPress
          end
          object txtReadLen: TEdit
            Left = 80
            Top = 380
            Width = 61
            Height = 21
            ImeName = #25340#38899#21152#21152
            TabOrder = 35
          end
          object GroupBox23: TGroupBox
            Left = 404
            Top = 64
            Width = 457
            Height = 221
            Caption = #36820#22238#20449#24687
            TabOrder = 36
            object StaticText8: TStaticText
              Left = 36
              Top = 56
              Width = 52
              Height = 17
              Caption = #36820#22238#32467#26524
              TabOrder = 0
            end
            object mmWCRet: TMemo
              Left = 112
              Top = 48
              Width = 317
              Height = 125
              ImeName = #25340#38899#21152#21152
              Lines.Strings = (
                '')
              TabOrder = 1
            end
          end
          object butLoadKey: TBitBtn
            Left = 244
            Top = 40
            Width = 75
            Height = 25
            Caption = #19979#36733#23494#38053
            TabOrder = 37
          end
        end
      end
    end
  end
  object barMain: TStatusBar
    Left = 0
    Top = 580
    Width = 922
    Height = 24
    Hint = #29366#24577#20449#24687
    Panels = <
      item
        Bevel = pbRaised
        Style = psOwnerDraw
        Width = 30
      end
      item
        Width = 700
      end
      item
        Bevel = pbRaised
        Style = psOwnerDraw
        Width = 30
      end
      item
        Bevel = pbRaised
        Style = psOwnerDraw
        Width = 30
      end
      item
        Alignment = taCenter
        Width = 50
      end>
    ParentShowHint = False
    ShowHint = True
    SimplePanel = False
    OnDrawPanel = barMainDrawPanel
  end
  object CoolBar1: TCoolBar
    Left = 0
    Top = 0
    Width = 922
    Height = 5
    Bands = <>
  end
  object MainMenu1: TMainMenu
    Images = imgMenu
    Left = 720
    Top = 8
    object N2: TMenuItem
      Caption = #31995#32479
      object N4: TMenuItem
        Caption = #31995#32479#35774#32622
        ImageIndex = 0
        OnClick = N4Click
      end
      object N7: TMenuItem
        Caption = '-'
      end
      object N6: TMenuItem
        Caption = #36864#20986
        ImageIndex = 1
        OnClick = N6Click
      end
    end
    object N1: TMenuItem
      Caption = #27169#24335
      object MaiFareCommand1: TMenuItem
        Caption = 'MaiFare Command'
        OnClick = MaiFareCommand1Click
      end
      object l5693Command1: TMenuItem
        Caption = 'l5693 Command'
        OnClick = l5693Command1Click
      end
      object TypeBCommand1: TMenuItem
        Caption = 'TypeB Command'
        OnClick = TypeBCommand1Click
      end
      object WatchCardCommand1: TMenuItem
        Caption = 'WatchCard Command'
        OnClick = WatchCardCommand1Click
      end
    end
    object N3: TMenuItem
      Caption = #24110#21161
      object N5: TMenuItem
        Caption = #20851#20110'...'
        ImageIndex = 2
        OnClick = N5Click
      end
    end
  end
  object imgTab: TImageList
    Left = 748
    Top = 8
    Bitmap = {
      494C010105000900040010001000FFFFFFFFFF10FFFFFFFFFFFFFFFF424D3600
      0000000000003600000028000000400000003000000001002000000000000030
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000FFFFFF0084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C60084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C60000000000C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C60084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C60084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C60000000000C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C60084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600FFFFFF00C6C6C600C6C6
      C600C6C6C600C6C6C60084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C60000000000C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C60084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600FFFFFF00C6C6
      C600C6C6C600C6C6C60084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000FFFFFF00C6C6C600C6C6C600C6C6
      C60000000000C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C60084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600FFFF
      FF00C6C6C600C6C6C60084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000FFFFFF00C6C6C600C6C6C6000000
      0000C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C60084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C60084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C60084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C60084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000FFFFFF0084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      840084848400848484008484840000000000FFFFFF0084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      840084848400848484008484840000000000FFFFFF0084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      840084848400848484008484840000000000FFFFFF0084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      840084848400848484008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C60000000000C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C60000000000C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C60000000000C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C60000000000C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C60000000000C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C60000000000C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C60000000000C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C60000000000C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600FFFFFF00C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600FFFFFF00C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600FFFFFF00C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600FFFFFF00C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C60000000000C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C60000000000C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C60000000000C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C60000000000C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600FFFFFF00C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600FFFFFF00C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600FFFFFF00C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600FFFFFF00C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C60000000000C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C60000000000C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C60000000000C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C60000000000C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600FFFF
      FF00C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600FFFF
      FF00C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600FFFF
      FF00C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600FFFF
      FF00C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C6000000
      0000C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C6000000
      0000C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C6000000
      0000C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C6000000
      0000C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF0000000000FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF0000000000FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF0000000000FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF0000000000424D3E000000000000003E000000
      2800000040000000300000000100010000000000800100000000000000000000
      000000000000000000000000FFFFFF0000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000010000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000010001000100010000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000}
  end
  object tmMain: TTimer
    OnTimer = tmMainTimer
    Left = 776
    Top = 4
  end
  object imgBar: TImageList
    Left = 808
    Top = 4
    Bitmap = {
      494C010104000900040010001000FFFFFFFFFF10FFFFFFFFFFFFFFFF424D3600
      0000000000003600000028000000400000003000000001002000000000000030
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400848484008484840084848400000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000848484000000840000008400000084000000840084848400848484008484
      8400000000000000000000000000000000000000000000000000848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000008484840084848400FFFFFF0000FFFF00FFFFFF000000
      00000000000000FFFF00FFFFFF0000FFFF00FFFFFF0000FFFF00000000000000
      0000FFFFFF0000FFFF00FFFFFF00000000000000000000000000000000000000
      84000000FF000000FF000000FF000000FF000000FF000000FF000000FF008484
      8400848484008484840000000000000000000000000000000000000000008484
      8400000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000000084848400FFFFFF0000FFFF00FFFFFF0000FF
      FF00FFFFFF0000FFFF00FFFFFF0000FFFF00FFFFFF0000FFFF00FFFFFF0000FF
      FF00FFFFFF0000FFFF00FFFFFF000000000000000000000000000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF00000084008484840000000000000000000000000000000000000000000000
      00000000000000000000FFFFFF00C6C6C6000000000000000000000000000000
      0000000000000000000000000000000000000000000000008400FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF000000FF000000000084848400FFFFFF0000FFFF00FFFFFF0000FF
      FF00FFFFFF0000FFFF00FFFFFF0000FFFF00FFFFFF0000FFFF00FFFFFF0000FF
      FF00FFFFFF0000FFFF00FFFFFF000000000000000000000084000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF008484840084848400000000000000000000000000000000000000
      0000FFFFFF00C6C6C60000000000FFFFFF00C6C6C60000000000000000000000
      0000000000000000000000000000000000000000000000008400FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF000000FF000000000084848400FFFFFF0000FFFF00FFFFFF0000FF
      FF00FFFFFF0000FFFF00FFFFFF0000FFFF00FFFFFF0000FFFF00FFFFFF0000FF
      FF00FFFFFF0000FFFF00FFFFFF0000000000000084000000FF000000FF000000
      FF00FFFFFF000000FF000000FF000000FF000000FF00FFFFFF00FFFFFF000000
      FF000000FF000000FF0084848400000000000000000000000000000000000000
      000000000000C6C6C60000000000FFFFFF00FFFFFF00C6C6C600000000000000
      0000000000000000000000000000000000000000000000008400FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF000000FF000000000084848400FFFFFF0000FFFF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000FFFFFF0000000000000084000000FF000000FF000000
      FF00FFFFFF00FFFFFF000000FF000000FF00FFFFFF00FFFFFF00FFFFFF000000
      FF000000FF000000FF008484840084848400000000000000000000000000FFFF
      FF00FFFFFF00FFFFFF000000000000000000C6C6C600C6C6C600FFFFFF00C6C6
      C600000000000000000000000000000000000000000000008400FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF000000FF000000000084848400FFFFFF0000FFFF00FFFFFF0000FF
      FF00FFFFFF0000FFFF00FFFFFF0000FFFF00FFFFFF0000FFFF00FFFFFF0000FF
      FF00FFFFFF0000FFFF00FFFFFF00000000000000FF000000FF000000FF000000
      FF000000FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF000000FF000000
      FF000000FF000000FF000000840084848400000000008484000000000000C6C6
      C600FFFFFF00FFFFFF0000000000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF0000000000C6C6C60084848400000000000000000000008400FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF0000000000FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF000000FF000000000084848400FFFFFF0000FFFF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000FFFFFF00000000000000FF000000FF000000FF000000
      FF000000FF000000FF00FFFFFF00FFFFFF00FFFFFF000000FF000000FF000000
      FF000000FF000000FF00000084008484840084840000FFFF0000848400000000
      0000C6C6C6000000000000000000000000000000000000000000848484000000
      000000FFFF008484840084848400848484000000000000008400FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF0000000000FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF000000FF000000000084848400FFFFFF0000FFFF00FFFFFF0000FF
      FF00FFFFFF0000FFFF00FFFFFF0000FFFF00FFFFFF0000FFFF00FFFFFF0000FF
      FF00FFFFFF0000FFFF00FFFFFF00000000000000FF000000FF000000FF000000
      FF000000FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF000000FF000000FF000000
      FF000000FF000000FF0000008400848484008484000084840000FFFF00008484
      00000000000000000000000000000000000000000000000000000000000000FF
      FF00C6C6C6008484840084848400848484000000000000008400FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF0000000000FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF000000FF000000000084848400FFFFFF0000FFFF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000FFFFFF00000000000000FF000000FF000000FF000000
      FF00FFFFFF00FFFFFF00FFFFFF000000FF00FFFFFF00FFFFFF000000FF000000
      FF000000FF000000FF0000008400000000008484000084840000848400000000
      000000000000000000000000000000000000000000000000000000000000FFFF
      FF00FFFFFF00C6C6C60084848400000000000000000000008400FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF0000000000FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF000000FF000000000084848400FFFFFF0000FFFF00FFFFFF0000FF
      FF00FFFFFF0000FFFF00FFFFFF0000FFFF00FFFFFF0000FFFF00FFFFFF0000FF
      FF00FFFFFF0000FFFF00FFFFFF0000000000000084000000FF000000FF00FFFF
      FF00FFFFFF00FFFFFF000000FF000000FF000000FF00FFFFFF00FFFFFF000000
      FF000000FF000000FF0084848400000000008484000084840000848400000000
      000000000000000000000000000000000000000000000000000000000000FFFF
      FF00FFFFFF00FFFFFF00C6C6C600000000000000000000008400FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF0000000000FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF000000FF000000000084848400FFFFFF0000FFFF00FFFFFF0000FF
      FF00FFFFFF0000FFFF00FFFFFF0000FFFF00FFFFFF0000FFFF00FFFFFF0000FF
      FF00FFFFFF0000FFFF00FFFFFF0000000000000000000000FF000000FF000000
      FF00FFFFFF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000840000000000000000008484000084840000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF00FFFFFF00FFFFFF00000000000000000000008400FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF0000000000FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF000000FF000000000084848400FFFFFF0000FFFF00FFFFFF0000FF
      FF00FFFFFF0000FFFF0000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000000084000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000000000000000000000000000000084840000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF00FFFFFF0000000000000000000000000000008400FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF0000000000FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF000000FF000000000084848400FFFFFF0000FFFF00FFFFFF0000FF
      FF00FFFFFF0000FFFF0000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000084000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      8400000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000008400000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000FF0000000000848484000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000084000000FF000000FF000000FF000000FF0000008400000084000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000000000000000424D3E000000000000003E000000
      2800000040000000300000000100010000000000800100000000000000000000
      000000000000000000000000FFFFFF0000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000FFFFFC3FFFFFC0011011F00FDFFF8000
      1010E003EE7F00000000C003F81F000000008001E00F000000000001E00F0000
      00000000C0030000000000008001000000000000000000000000000007C00000
      000000010FC10000000000011FE10000000080031FE1000000018007BFF30000
      03FFC00FBFF70000FFFFF01FFFFF000100000000000000000000000000000000
      000000000000}
  end
  object imgMenu: TImageList
    Left = 688
    Top = 8
    Bitmap = {
      494C010103000400040010001000FFFFFFFFFF10FFFFFFFFFFFFFFFF424D3600
      0000000000003600000028000000400000001000000001002000000000000010
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400000000008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000008400000084000000840000FFFFFF000084000000840000008400000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000FFFFFF000084
      84000000000084848400C6C6C60000FF0000C6C6C600C6C6C600C6C6C6000000
      0000000000000000000084848400000000008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000084
      00000084000000840000FFFFFF00FFFFFF00FFFFFF0000840000008400000084
      0000008400000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000084848400008484000084
      84008484840084848400FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0084848400000000008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000008400000084
      00000084000000840000FFFFFF00FFFFFF00FFFFFF00FFFFFF00008400000084
      0000008400000084000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000848484000000
      0000000000000000000084848400000000000000000000000000000000000000
      0000000000000000000084848400000000008484840084848400000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000840000008400000084
      00000084000000840000FFFFFF00FFFFFF00FFFFFF0000840000008400000084
      0000008400000084000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000848484000000
      0000000000000000000084848400FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF008484840000000000000000008484840084848400000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000840000008400000084
      0000008400000084000000840000008400000084000000840000008400000084
      0000008400000084000000840000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000008484840084848400848484008484
      8400848484008484840084848400848484008484000084840000848400008484
      0000FFFFFF008484840000000000000000008484840084848400000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000084000000840000008400000084
      00000084000000840000FFFFFF00FFFFFF00FFFFFF0000840000008400000084
      0000008400000084000000840000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000FFFFFF0000FF0000C6C6C600C6C6
      C600C6C6C6000000000084848400848484008484000084840000848400008484
      0000FFFFFF008484840000000000000000008484840084848400848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000084000000840000008400000084
      0000008400000084000000840000FFFFFF00FFFFFF00FFFFFF00008400000084
      0000008400000084000000840000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0084848400848484008484000084840000848400008484
      0000FFFFFF00848484000000000000000000848484008484840084848400C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600000000000084000000840000008400000084
      0000008400000084000000840000FFFFFF00FFFFFF00FFFFFF00008400000084
      0000008400000084000000840000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400848484008484840084848400848484008484
      8400FFFFFF008484840000000000000000008484840084848400C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C60000000000000000000084000000840000008400000084
      000000840000008400000084000000840000FFFFFF00FFFFFF00FFFFFF000084
      0000008400000084000000840000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C60000000000000000008484840084848400C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600000000000000000000000000000000000000000000840000008400000084
      0000FFFFFF00FFFFFF00FFFFFF0000840000FFFFFF00FFFFFF00FFFFFF000084
      0000008400000084000000840000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000FFFFFF0000000000FF00
      0000FF000000FF00000084000000C6C6C6008484840000000000000000000000
      0000000000000000000000000000000000008484840084848400C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600000000000000
      0000000000000000000000000000000000000000000000840000008400000084
      0000FFFFFF00FFFFFF00FFFFFF0000840000FFFFFF00FFFFFF00FFFFFF000084
      0000008400000084000000840000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000FFFFFF0000000000FFFF
      0000FF000000FF00000084000000C6C6C6008484840000000000000000000000
      00000000000000000000000000000000000084848400C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C6000000000000000000000000000000
      0000000000000000000000000000000000000000000000840000008400000084
      0000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF000084
      0000008400000084000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000FFFFFF0000000000FF00
      0000FF000000FF00000084000000C6C6C6008484840000000000000000000000
      00000000000000000000000000000000000084848400C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C60000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000008400000084
      000000840000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF000084
      0000008400000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000FFFFFF00848484008484
      8400848484008484840084848400C6C6C6008484840000000000000000000000
      00000000000000000000000000000000000084848400C6C6C600C6C6C600C6C6
      C600000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000084
      00000084000000840000FFFFFF00FFFFFF00FFFFFF0000840000008400000084
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000084848400C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C6008484840000000000000000000000
      000000000000000000000000000000000000C6C6C600C6C6C600000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000840000008400000084000000840000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000424D3E000000000000003E000000
      2800000040000000100000000100010000000000800000000000000000000000
      000000000000000000000000FFFFFF00FFFFFFFFFC3F0000F8013FFFE00F0000
      80000FFFC0070000800003FF80030000CC0000FF80010000CC01003F00010000
      0001000F00000000000100030000000000010001000000008001000300000000
      8001000F00010000807F003F00010000807F00FF80010000807F03FFC0030000
      807F0FFFE0070000807F3FFFF01F000000000000000000000000000000000000
      000000000000}
  end
end
