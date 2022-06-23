.\downloadtaxonomy.ps1

Write-Host "Start to translate"
.\ContentHubTaxsonomyUpdater.exe

Write-Host "Start to upload"
.\uploadtaxonomy.ps1

