Param(
  [parameter(Mandatory=$false)][string]$registry,
  [parameter(Mandatory=$false)][string]$dockerOrg,
  [parameter(Mandatory=$false)][bool]$publish=$true,
  [parameter(Mandatory=$false)][string]$dockerUser,
  [parameter(Mandatory=$false)][string]$dockerPwd,
  [parameter(Mandatory=$false)][string]$imageTag,
  [parameter(Mandatory=$false)][bool]$buildImages=$true,
  [parameter(Mandatory=$false)][bool]$pushImages=$true
)

# Paths
$localFolder = (Get-Item -Path "./" -Verbose).FullName
$serviceFolder = Join-Path $localFolder "../services"

$services = (Get-Content "./Services")

$useDockerHub = [string]::IsNullOrEmpty($registry)


Set-Location $serviceFolder

function exitShell() {
  Set-Location $localFolder
  exit
}

if (-not $imageTag) {
  $imageTag = 'latest'
}

if($publish) {
  & dotnet restore Silky.Hero.sln
  & dotnet  build --no-restore -c Release Silky.Hero.sln
  foreach ($service in $services) {
    $publishServiceName = "{0}/src/{0}Host/{0}Host.csproj" -f $service
    & dotnet publish --no-restore -c Release $publishServiceName
  }
}
if ($buildImages) {
  Write-Host "Image tag is $imageTag" -ForegroundColor Yellow
  $env:TAG = $imageTag
  & docker-compose -f docker-compose.publish.yml build
}

if (-not [string]::IsNullOrEmpty($dockerUser)) {
  Write-Host "Use the account $dockerUser to log in to the docker hub $registry" -ForegroundColor Yellow
  & docker login -u $dockerUser -p $dockerPwd $registry
  if (-not $LastExitCode -eq 0) {
    Write-Host "Login docker hub failed" -ForegroundColor Red
    exitShell
  }
}

if ($pushImages) {
  Write-Host "Start pushing the image to the mirror repository ..." -ForegroundColor Yellow
  $images = & docker images | grep silky
  foreach($image in $images) {
    $imageName = ($image -Split " ")[0] 
    $imageFqdn = if ($useDockerHub) { "${imageName}" } elseif ($dockerOrg) { "$registry/$dockerOrg/${imageName}" } else { "$registry/${imageName}" }
    if ($useDockerHub) {
      docker tag ${imageName}:$imageTag ${imageFqdn}:$imageTag
    }
     docker push ${imageFqdn}:$imageTag 
  }
}

exitShell