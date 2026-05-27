Write-Host "Resetting Trading Journal infrastructure..." -ForegroundColor Red
docker compose down -v
docker compose up -d
Write-Host "Reset complete. Fresh databases created." -ForegroundColor Green