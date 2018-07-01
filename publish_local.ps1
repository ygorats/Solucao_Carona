 cd  C:\Android\Projects\Solucao_Carona;
 iisreset /stop; dotnet publish -o C:\CaronaService\ -c Debug; iisreset /start;
 
 pause