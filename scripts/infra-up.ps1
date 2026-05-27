# Запуск инфраструктуры
Write-Host "Starting Trading Journal infrastructure..." -ForegroundColor Cyan

# Копируем .env если нет
if (-not (Test-Path ".env")) {
    Copy-Item ".env.example" ".env"
    Write-Host "Created .env from .env.example" -ForegroundColor Yellow
}

# Запускаем контейнеры
docker compose up -d

# Ждём готовности
Write-Host "Waiting for services..." -ForegroundColor Yellow
Start-Sleep -Seconds 5

# Проверяем статус
docker compose ps

Write-Host "`nInfrastructure is ready!" -ForegroundColor Green
Write-Host "PostgreSQL: localhost:5432" -ForegroundColor Gray
Write-Host "Redis:      localhost:6379" -ForegroundColor Gray