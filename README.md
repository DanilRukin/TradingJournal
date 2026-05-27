# Trading Journal

Десктоп-ориентированное веб-приложение для журналирования, статистического анализа и визуализации торговых сделок.

**Целевая аудитория:** Профессиональные трейдеры, портфельные управляющие, трейдеры-самоучки (фондовый, срочный, криптовалютный рынки).

## Технологический стек

| Слой               | Технологии                                                     |
| :----------------- | :------------------------------------------------------------- |
| **Backend**        | .NET 8, ASP.NET Core, EF Core 8, PostgreSQL 16+, Redis         |
| **Frontend**       | React 18+, TypeScript, Material UI v6, Zustand, TanStack Query |
| **Графики**        | lightweight-charts (TradingView)                               |
| **Desktop**        | Electron                                                       |
| **Инфраструктура** | Docker, Docker Compose                                         |

## Документация

- [Техническое задание](docs/tz/)
- [Архитектурные решения (ADR)](docs/adr/)
- [Архитектура (диаграммы/описания)](docs/architecture/overview.md)
- [Спецификация API](docs/api/)

## Быстрый старт (будет дополнено)

### Предварительные требования

- [Docker Desktop](https://www.docker.com/products/docker-desktop/) 4.25+
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js 20 LTS](https://nodejs.org/)
- PowerShell 5.1+ (встроен в Windows)

### Запуск инфраструктуры

```bash
# Копируем переменные окружения
cp .env.example .env

# Поднимаем PostgreSQL и Redis
docker-compose up -d

# Проверяем статус
docker-compose ps

# Бэкенд
cd src/backend
dotnet run

# Фронтенд
cd src/frontend
npm run dev
```

### Запуск инфраструктуры (Windows)

```powershell
# Клонируем репозиторий
git clone <repo-url> trading-journal
cd trading-journal

# Запускаем PostgreSQL и Redis
.\scripts\infra-up.ps1

# Проверяем статус
docker compose ps
```

Оба контейнера должны быть в статусе healthy.

#### Остановка

```powershell
.\scripts\infra-down.ps1
```

#### Полный сброс данных

```powershell
.\scripts\infra-reset.ps1
```

#### Запуск бэкенда

```powershell
cd src\backend
dotnet restore
dotnet build
dotnet run --project TradingJournal.WebApi
```

API доступен на https://localhost:5001 (или http://localhost:5000).

#### Запуск фронтенда

```powershell
cd src\frontend
npm install
npm run dev
```

Открыть в браузере: http://localhost:5173

## Статус проекта

🟡 MVP в разработке (Спринт 1)
