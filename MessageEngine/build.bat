cls

svn info --xml -r head > c:\temp\JaxisVersionSVN.xml

msbuild ".\Version.msbuild" /p:AssemblyInfoPath=AssemblyInfo.cs
msbuild ".\Engine.msbuild" /p:Configuration=Release 
msbuild ".\Plugins\Plugins.msbuild" /p:Configuration=Release

REM "C:\Program Files (x86)\Eziriz\.NET Reactor\dotNET_Reactor.exe" -project "C:\Source\Jaxis\Builds\JaxisEngine\JaxisEngine.nrproj"
 