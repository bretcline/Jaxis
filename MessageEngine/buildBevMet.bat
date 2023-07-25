cls

svn info --xml -r head > c:\temp\JaxisVersionSVN.xml

msbuild ".\Version.msbuild" /p:AssemblyInfoPath=AssemblyInfo.cs
msbuild ".\BevMetEngine.msbuild" /p:Configuration=Release /p:DefineConstants=BEV_MET
msbuild ".\Plugins\Plugins.msbuild" /p:Configuration=Release /p:DefineConstants=BEV_MET

"C:\Program Files (x86)\Eziriz\.NET Reactor\dotNET_Reactor.exe" -project "C:\Source\Jaxis\Builds\BevMet.MessageEngine\BevMetEngine.nrproj"
 