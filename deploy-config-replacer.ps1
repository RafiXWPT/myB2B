param($sqlconnection="")
 ls . appsettings.json -rec | %{ $f=$_; (gc $f.PSPath) | %{ $_ -replace "CONNECTION_STRING", $sqlconnection } | sc $f.PSPath }