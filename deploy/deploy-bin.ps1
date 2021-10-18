Clear-Host

$destination_root="C:\_ProjectDependencies\WineMS\bin"

$source_root="..\src"
$cli_root = "${source_root}\cli"
$gui_root = "${source_root}\gui"

$build_output="bin\Debug"

function Copy-Files {
  param([string]$Path)
  Get-ChildItem -Path "$($Path)\$($build_output)" -Filter "WineMS*.*" | %{ Copy-Item $_.FullName -Destination "$($destination_root)\$($_.Name)" }
}

if(-not (Test-Path $destination_root)) { New-Item $destination_root -ItemType Directory }

Copy-Files $cli_root
Copy-Files $gui_root

Remove-Item -Path "$($destination_root)\*.config"
Remove-Item -Path "$($destination_root)\*.zip"

Start-Process -FilePath 7z.exe -ArgumentList "a $($destination_root)\WineMsEvolution-HotFix.1.x-yyy.zip $($destination_root)\*.*" -NoNewWindow -Wait

