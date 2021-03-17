cd C:\UMPH_Programs\UMPH_DashboardComm\Statuses-Stg
call :SUB_GenerateStatusFile
C:\UMPH_Programs\UMPH_DashboardComm\DashboardComm.exe --TargetEnvironment=Stg --statusesfile=C:\UMPH_Programs\UMPH_DashboardComm\Statuses-Stg\Statuses.json

cd C:\UMPH_Programs\UMPH_DashboardComm\Statuses-Prd
call :SUB_GenerateStatusFile
C:\UMPH_Programs\UMPH_DashboardComm\DashboardComm.exe --TargetEnvironment=Prd --statusesfile=C:\UMPH_Programs\UMPH_DashboardComm\Statuses-Prd\Statuses.json


goto :EOF

:SUB_GenerateStatusFile
del Statuses.json
del Statuses_Temp.json
echo {> Statuses.json
type *.status >> Statuses.json
echo }>> Statuses.json
"C:\cygwin64\bin\tr.exe" -d '\r\n' < Statuses.json > Statuses_Temp.json 
"C:\cygwin64\bin\sed" 's/, *\}/\}/' Statuses_Temp.json > Statuses.json
exit /B

:EOF
