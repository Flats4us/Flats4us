# get-git-info.ps1
param($outputPath)
$commitHash = git rev-parse HEAD
$commitDate = git log -1 --format="%cd" --date=format:"%Y-%m-%d %H:%M:%S"
Set-Content -Path $outputPath -Value "$commitHash`n$commitDate"