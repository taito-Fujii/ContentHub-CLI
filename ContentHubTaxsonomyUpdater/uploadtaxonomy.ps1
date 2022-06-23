$importCmd = ch-cli system import -s import.zip

$id = $null
$hasId = $false
$imoprtCmdArray = $importCmd.Split(" ")
foreach ($currentItem in $imoprtCmdArray) {
    # Skip after items after finding ID for fetch 
    if ([int]::TryParse($currentItem, [ref]$id))
    {
        $hasId = $true
        break;
    }
}

if (!$hasId) {
    Write-Host "Failed to Upload."
    return
}

$cmdResult = ""
while (!$hasDownloadURL) {
    Write-Host "Status Checking.."

    $cmdResult =ch-cli jobs status --id $id

    if ($cmdResult.StartsWith("Success")) {
        break;
    } 
    Start-Sleep -Seconds 20
}

Write-Host "Sucessed to Upload."