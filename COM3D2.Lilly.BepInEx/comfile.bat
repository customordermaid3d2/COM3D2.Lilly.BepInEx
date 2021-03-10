"C:\Windows\Microsoft.NET\Framework\v3.5\csc" /out:COM3D2.Lilly.plugin.dll ^
/t:library /lib:"F:\COM3D2\COM3D2x64_Data\Managed","F:\COM3D2\BepInEx\core" ^
/r:UnityEngine.dll /r:BepInEx.dll /r:0Harmony20.dll ^
/r:Assembly-CSharp.dll /r:Assembly-CSharp-firstpass.dll /r:ExIni.dll ^
/recurse:*.cs
rem pause 