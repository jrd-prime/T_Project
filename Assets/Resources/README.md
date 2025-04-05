Что изменилось и почему?
Разделил "Core", "Game" и "Infrastructure" по чистой архитектуре

Core → Чистая бизнес-логика

Game → Реализация в Unity

Infrastructure → Сохранение, сеть, DI

Data → Данные (ScriptableObjects, конфиги)

Внёс UseCases (Interactors) как слой приложения

Они обрабатывают бизнес-логику и вызывают сервисы

GameManager теперь в "Managers" в Core

Потому что он управляет состоянием игры

Интерфейс IGameManager в Interfaces, реализация в Services

Scripts
- Core                      // Ядро приложения (бизнес-логика)
    - Characters             // Персонажи (игрок, враги, NPC)
        - Interfaces         // Контракты
        - Models             // Чистые модели данных
        - Services           // Логика работы с моделями
        - UseCases           // Бизнес-логика (приложение)
    - Inventory              // Инвентарь
        - Interfaces
        - Models
        - Services
        - UseCases
    - Crafting               // Крафт
        - Interfaces
        - Models
        - Services
        - UseCases
    - Gathering              // Добыча ресурсов
        - Interfaces
        - Models
        - Services
        - UseCases
    - Loot                   // Система лута
        - Interfaces
        - Models
        - Services
        - UseCases
    - Placement              // Установка объектов в мире
        - Interfaces
        - Models
        - Services
        - UseCases
    - Equipment              // Экипировка (оружие, броня, инструменты)
        - Interfaces
        - Models
        - Services
        - UseCases
    - Stats                  // Статы и прокачка
        - Interfaces
        - Models
        - Services
        - UseCases
    - Common                 // Общие сущности
        - Interfaces
        - Models
        - Services
    - Managers               // Глобальные менеджеры (координация модулей)
        - Interfaces
        - Models
        - Services
- Data                      // Данные (например, ScriptableObjects)
    - So                    // ScriptableObjects (конфигурации, рецепты)
- Game                      // Реализация в Unity (инфраструктура)
    - Presentation          // UI, контроллеры, визуал
        - UI
        - Controllers
        - Visuals
    - Systems               // Игровые системы
        - Gameplay
        - AI
        - Physics
    - Configs               // Конфигурационные файлы
- Infrastructure            // Техническая инфраструктура
    - Repositories          // Работа с данными
    - Networking            // Мультиплеер, синхронизация
    - SaveLoad              // Сохранение и загрузка
    - Installers            // DI-контейнер (Zenject)
