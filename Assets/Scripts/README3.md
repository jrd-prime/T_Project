Реализация интерфейса игрока в чистой архитектуре
Дата: 20 марта 2025 года

Контекст: Обсуждение добавления интерфейса игрока с бафами и дебафами в иерархию проекта с использованием принципов чистой архитектуры.

Исходная иерархия проекта
text

Свернуть

Перенос

Копировать
Scripts
- Core
    - Domain
        - Interfaces
        - Models
        - Services
    - Application
        - UseCases
        - Managers
- Game
    - Presentation
        - UI
        - Controllers
        - Visuals
    - Systems
        - Gameplay
        - AI
        - Physics
- Infrastructure
    - Repositories
    - Networking
    - SaveLoad
    - Installers
- ThirdParty
    - SDKs
    - Plugins
      Задача
      Добавить интерфейс игрока (IPlayer) с поддержкой бафов (например, увеличение здоровья) и дебафов (например, яд), следуя чистой архитектуре и заданной иерархии.

Основные компоненты
1. Слой Domain (Scripts/Core/Domain)
   Этот слой содержит независимую бизнес-логику, интерфейсы и модели.

Интерфейсы (Interfaces)
IPlayer: Абстракция игрока.
csharp

Свернуть

Перенос

Копировать
namespace Core.Domain.Interfaces
{
public interface IPlayer
{
int Health { get; }
void TakeDamage(int amount);
void Heal(int amount);
void ApplyEffect(IEffect effect);
}
}
IEffect: Абстракция для бафов и дебафов.
csharp

Свернуть

Перенос

Копировать
namespace Core.Domain.Interfaces
{
public interface IEffect
{
void Apply(IPlayer player);
bool IsActive { get; }
void Update(float deltaTime);
}
}
Модели (Models)
PoisonEffect: Дебаф "Яд".
csharp

Свернуть

Перенос

Копировать
namespace Core.Domain.Models
{
public class PoisonEffect : IEffect
{
private float _duration;
private float _damagePerSecond;

        public PoisonEffect(float duration, float damagePerSecond)
        {
            _duration = duration;
            _damagePerSecond = damagePerSecond;
        }

        public bool IsActive => _duration > 0;

        public void Apply(IPlayer player) { }

        public void Update(float deltaTime)
        {
            if (IsActive) _duration -= deltaTime;
        }

        public int GetDamage(float deltaTime) => (int)(_damagePerSecond * deltaTime);
    }
}
HealthBoostEffect: Баф "Увеличение здоровья".
csharp

Свернуть

Перенос

Копировать
namespace Core.Domain.Models
{
public class HealthBoostEffect : IEffect
{
private readonly int _healthBonus;

        public HealthBoostEffect(int healthBonus)
        {
            _healthBonus = healthBonus;
        }

        public bool IsActive => true;

        public void Apply(IPlayer player)
        {
            player.Heal(_healthBonus);
        }

        public void Update(float deltaTime) { }
    }
}
Сервисы (Services)
IPlayerService и PlayerService: Управление логикой игрока и эффектов.
csharp

Свернуть

Перенос

Копировать
namespace Core.Domain.Services
{
public interface IPlayerService
{
void ApplyEffectToPlayer(IPlayer player, IEffect effect);
void UpdatePlayerEffects(IPlayer player, float deltaTime);
}

    public class PlayerService : IPlayerService
    {
        private readonly List<IEffect> _activeEffects = new List<IEffect>();

        public void ApplyEffectToPlayer(IPlayer player, IEffect effect)
        {
            effect.Apply(player);
            if (effect.IsActive) _activeEffects.Add(effect);
        }

        public void UpdatePlayerEffects(IPlayer player, float deltaTime)
        {
            foreach (var effect in _activeEffects.ToList())
            {
                effect.Update(deltaTime);
                if (effect is PoisonEffect poison)
                {
                    player.TakeDamage(poison.GetDamage(deltaTime));
                }
                if (!effect.IsActive) _activeEffects.Remove(effect);
            }
        }
    }
}
2. Слой Application (Scripts/Core/Application)
   Этот слой координирует взаимодействие между доменом и внешними системами.

Интеракторы (Interactors)
Решено использовать термин Interactor вместо UseCase для большей ясности.

PlayerInteractor: Единый интерактор для всех действий игрока.
csharp

Свернуть

Перенос

Копировать
namespace Core.Application.Interactors
{
public class PlayerInteractor
{
private readonly IPlayerService _playerService;
private readonly IPlayer _player;

        public PlayerInteractor(IPlayerService playerService, IPlayer player)
        {
            _playerService = playerService;
            _player = player;
        }

        public void ApplyEffect(string effectType, float value1, float value2 = 0)
        {
            IEffect effect = effectType switch
            {
                "Poison" => new PoisonEffect(value1, value2), // duration, damagePerSecond
                "HealthBoost" => new HealthBoostEffect((int)value1), // healthBonus
                _ => throw new ArgumentException("Unknown effect type")
            };
            _playerService.ApplyEffectToPlayer(_player, effect);
        }

        public void Update(float deltaTime)
        {
            _playerService.UpdatePlayerEffects(_player, deltaTime);
        }
    }
}
Возможные альтернативные интеракторы:

PlayerEffectInteractor — только для эффектов.
PlayerMovementInteractor — для движения.
PlayerCombatInteractor — для боя.
Решение: Пока используется один PlayerInteractor, так как проект небольшой. При росте функционала можно разделить.

3. Слой Game (Scripts/Game)
   Этот слой содержит конкретные реализации и игровую логику.

Системы (Systems/Gameplay)
Player: Реализация интерфейса IPlayer.
csharp

Свернуть

Перенос

Копировать
namespace Game.Systems.Gameplay
{
public class Player : Core.Domain.Interfaces.IPlayer
{
private int _health = 100;

        public int Health => _health;

        public void TakeDamage(int amount)
        {
            _health -= amount;
        }

        public void Heal(int amount)
        {
            _health += amount;
        }

        public void ApplyEffect(Core.Domain.Interfaces.IEffect effect)
        {
            effect.Apply(this);
        }
    }
}
Презентация (Presentation/Controllers)
PlayerController: Управление игроком через ввод.
csharp

Свернуть

Перенос

Копировать
namespace Game.Presentation.Controllers
{
public class PlayerController
{
private readonly Core.Application.Interactors.PlayerInteractor _playerInteractor;

        public PlayerController(PlayerInteractor playerInteractor)
        {
            _playerInteractor = playerInteractor;
        }

        public void OnPoisonTrigger() => _playerInteractor.ApplyEffect("Poison", 5f, 2f); // 5 сек, 2 урона/сек
        public void OnHealthPickup() => _playerInteractor.ApplyEffect("HealthBoost", 20f); // +20 hp
        public void Update(float deltaTime) => _playerInteractor.Update(deltaTime);
    }
}
4. Слой Infrastructure (опционально)
   Если нужно сохранять состояние игрока или эффектов, можно добавить:

PlayerRepository в Scripts/Infrastructure/Repositories.
Ответы на вопросы
Зачем нужен PlayerService?
Ответ: Для управления бизнес-логикой игрока (например, обработка бафов и дебафов). С бафами и дебафами он полезен, так как централизует правила (например, "яд наносит урон со временем") и делает код тестируемым.
Что такое PlayerUseCase/PlayerInteractor?
Ответ: Это класс в слое Application, который описывает сценарии использования (например, "применить яд", "обновить эффекты"). Альтернативное название — PlayerInteractor.
Какие могут быть названия для интеракторов?
Один: PlayerInteractor, PlayerHandler, PlayerCoordinator.
Специализированные: PlayerEffectInteractor, PlayerMovementInteractor, PlayerCombatInteractor.
Один интерактор или несколько?
Решение: Пока один PlayerInteractor, так как бафы и дебафы — основная механика. При добавлении новых систем (инвентарь, атаки) можно разделить.
Итог
Domain: Определены IPlayer, IEffect, модели (PoisonEffect, HealthBoostEffect) и сервис (PlayerService).
Application: Реализован PlayerInteractor для сценариев.
Game: Добавлены реализация Player и контроллер PlayerController.
Код следует чистой архитектуре: независимость слоев, инверсия зависимостей.