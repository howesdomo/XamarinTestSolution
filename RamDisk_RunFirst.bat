@echo off
:: Author      : Howe
:: Version     : 1.4
:: CreateTime  : 2019-10-16 11:07:46
:: Description : RAMDisk link 创建脚本, 修改 Step 1 与 2 参数, 保存后执行

:: ******* Step 1 设置内存盘的路径 *******
set RamDiskRootPath="X:\Cache_SC\XamarinTestSolution"

:: ******* Step 2 设置包含的平台 *******
set AndroidProjectContain=1
set iOSProjectContain=1

echo 删除本项目的bin和obj文件夹
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

echo 创建mklink
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

echo 创建RAMDISK的bin和obj文件夹
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