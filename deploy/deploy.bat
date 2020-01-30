set setup_bin_folder=..\src\WineMsSetup\Debug

@echo off

set /p build_version="Build version (e.g.: 19 for 1.19): "
set /p sdk_version="Evolution SDK version (e.g.: 720): "
set zip_file_name=WineMsSetup.1.%build_version%-%sdk_version%.zip

echo %zip_file_name%

7z.exe a %zip_file_name% %setup_bin_folder%\*.*

pause