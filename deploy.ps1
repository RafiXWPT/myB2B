$configFiles = Get-ChildItem . appsettings.json -rec
foreach($file in $configFiles) {
  (Get-Content $file.PSPath) |
  Foreach-Object {$_ -replace "'ConnectionString': 'Server=localhost\\SQLEXPRESS;Database=MyB2B;Integrated Security=True;MultipleActiveResultSets=True;'", "$(sqlconnection)"} |
  Set-Content $file.PSPath
}