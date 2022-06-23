$exportCmd = ch-cli system export -t taxonomies

$id = $null
$exportCmdArray = $exportCmd.Split(" ")
foreach ($currentItem in $exportCmdArray) {
    # Skip after items after finding ID for fetch 
    if ([int]::TryParse($currentItem, [ref]$id))
    {
        break;
    }
}

## Loop
$hasDownloadURL = $false
$cmdResult = ""
while (!$hasDownloadURL) {
    Write-Host "Fetching.."

    $cmdResult = ch-cli orders fetch --id $id

    if ($cmdResult.StartsWith("https")) {
        break;
    } 
    Start-Sleep -Seconds 5
}

Write-Host "Start to download export files.. "
Write-Host "The URI is as below "
Write-Host $cmdResult

$webclient = New-Object System.Net.WebClient
$uri = New-Object System.Uri($cmdResult)
$webclient.DownloadFile($uri, (Get-Location).Path + "\export.zip")

Write-Host "Sucessed to Download. "
