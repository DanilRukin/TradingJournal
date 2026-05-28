# Модель данных (Database Schema)

Диаграмма базы данных Trading Journal.

## Содержимое

| Файл                 | Описание                             |
| :------------------- | :----------------------------------- |
| `TradingJournal.png` | ER-диаграмма. 20 таблиц. Версия MVP. |

## Инструмент

Схема создана в Toad Data Modeler.

## Актуальная версия

- **Дата:** 28.05.2026
- **Таблиц:** 20
- **Тип наследования:** Table Per Type (TPT) для InstrumentSpecs → FuturesSpecs / StockSpecs
- **СУБД:** PostgreSQL 16
- **Схема:** `trading`
