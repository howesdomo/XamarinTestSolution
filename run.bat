echo 在RAMDISK创建本项目的文件夹>nul
md "X:\Cache_SC\XamarinTestSolution\Client.Android\"
md "X:\Cache_SC\XamarinTestSolution\Client.iOS\"
echo 删除本项目的bin和obj文件夹>nul
rd "D:\SC_Github\XamarinTestSolution\SC\Client\Client.Android\bin"
rd "D:\SC_Github\XamarinTestSolution\SC\Client\Client.Android\obj"
rd "D:\SC_Github\XamarinTestSolution\SC\Client\Client.iOS\bin"
rd "D:\SC_Github\XamarinTestSolution\SC\Client\Client.iOS\obj"
echo 创建mklink>nul
mklink /D "D:\SC_Github\XamarinTestSolution\SC\Client\Client.Android\bin" "X:\Cache_SC\XamarinTestSolution\Client.Android\bin"
mklink /D "D:\SC_Github\XamarinTestSolution\SC\Client\Client.Android\obj" "X:\Cache_SC\XamarinTestSolution\Client.Android\obj"
mklink /D "D:\SC_Github\XamarinTestSolution\SC\Client\Client.iOS\bin" "X:\Cache_SC\XamarinTestSolution\Client.iOS\bin"
mklink /D "D:\SC_Github\XamarinTestSolution\SC\Client\Client.iOS\obj" "X:\Cache_SC\XamarinTestSolution\Client.iOS\obj"
echo 创建RAMDISK的bin和obj文件夹>nul
md "X:\Cache_SC\XamarinTestSolution\Client.Android\bin"
md "X:\Cache_SC\XamarinTestSolution\Client.Android\obj"
md "X:\Cache_SC\XamarinTestSolution\Client.iOS\bin"
md "X:\Cache_SC\XamarinTestSolution\Client.iOS\obj"