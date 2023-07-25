program cs;

uses

  Windows, Messages,Dialogs,
  Forms,
  Unit1 in 'Unit1.pas' {Form1};

var
 MutexHandle: THandle;

{$R *.res}

begin
  Application.Initialize;
  Application.Title := 'RFID';
  MutexHandle:=0;

  CreateMutex(nil, true, 'Project1');
  if GetLastError = ERROR_ALREADY_EXISTS then
  begin
    showmessage('The program is already opened , please donot open again.. ');
    Exit;
  end;

  Application.CreateForm(TForm1, Form1);
  Application.Run;
end.
