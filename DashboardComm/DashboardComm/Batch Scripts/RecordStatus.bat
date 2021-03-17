REM Usage: RecordStatus.bat Identifier [Stg | Prd] [Success | Warning | Error | Information]
echo @off

2>NUL CALL :CASE_%3% 
IF ERRORLEVEL 1 CALL :DEFAULT_CASE 

echo '%1':{'AppId':'%1','Name':'%1','Value':'%3','Display':true,'Status':%_Status%},> C:\UMPH_Programs\UMPH_DashboardComm\Statuses-%2\%1.status

EXIT /B

:CASE_Success
  set _Status=0
  GOTO END_CASE
:CASE_Warning
  set _Status=1
  GOTO END_CASE
:CASE_Error
  set _Status=2
  GOTO END_CASE
:CASE_Information
  set _Status=3
  GOTO END_CASE
:DEFAULT_CASE
  set _Status=2
  GOTO END_CASE
:END_CASE
  VER > NUL # reset ERRORLEVEL
  GOTO :EOF # return from CALL
