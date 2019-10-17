@echo off
:: Author      : Howe
:: Version     : 1.4
:: CreateTime  : 2019-10-16 11:07:46
:: Description : RAMDisk link �����ű�, �޸� Step 1 �� 2 ����, �����ִ��

:: ******* Step 1 �����ڴ��̵�·�� *******
set RamDiskRootPath="X:\Cache_SC\XamarinTestSolution"

:: ******* Step 2 ���ð�����ƽ̨ *******
set AndroidProjectContain=1
set iOSProjectContain=1

echo ɾ������Ŀ��bin��obj�ļ���
::Android
if %AndroidProjectContain% EQU 1 (
rd /s/q "%~dp0\Client\Client.Android\bin"
rd /s/q "%~dp0\Client\Client.Android\obj"
rd /s/q "%~dp0\Client\Client.Android\buildAPK"
)
::iOS
if %iOSProjectContain% EQU 1 (
rd /s/q "%~dp0\Client\Client.iOS\bin"
rd /s/q "%~dp0\Client\Client.iOS\obj"
)

echo ����mklink
::Android
set RamDiskAndroidPath="%RamDiskRootPath%\Client\Client.Android\"

if %AndroidProjectContain% EQU 1 (
mklink /D "%~dp0\Client\Client.Android\bin" "%RamDiskAndroidPath%\bin"
mklink /D "%~dp0\Client\Client.Android\obj" "%RamDiskAndroidPath%\obj"
mklink /D "%~dp0\Client\Client.Android\buildAPK" "%RamDiskAndroidPath%\buildAPK"
)

::iOS
set RamDiskiOSPath="%RamDiskRootPath%\Client\Client.iOS\"
if %iOSProjectContain% EQU 1 (
mklink /D "%~dp0\Client\Client.iOS\bin" "%RamDiskiOSPath%\bin"
mklink /D "%~dp0\Client\Client.iOS\obj" "%RamDiskiOSPath%\obj"
)

echo ����RAMDISK��bin��obj�ļ���
::Android
if %AndroidProjectContain% EQU 1 (
md "%RamDiskAndroidPath%\bin"
md "%RamDiskAndroidPath%\obj"
md "%RamDiskAndroidPath%\buildAPK"
)

::iOS
if %iOSProjectContain% EQU 1 (
md "%RamDiskiOSPath%\bin"
md "%RamDiskiOSPath%\obj"
)

pause