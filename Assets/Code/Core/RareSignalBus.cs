using R3;
using System;
using System.Collections.Generic;

public sealed class RareSignalBus
{
    // Словарь для хранения Subject'ов по типам сигналов
    private readonly Dictionary<Type, object> _subjects = new Dictionary<Type, object>();

    // Получение или создание Subject для конкретного типа сигнала
    private ISubject<T> GetSubject<T>()
    {
        Type signalType = typeof(T);
        if (!_subjects.ContainsKey(signalType))
        {
            _subjects[signalType] = new Subject<T>();
        }

        return (ISubject<T>)_subjects[signalType];
    }

    // Отправка сигнала
    public void Fire<T>(T signal)
    {
        GetSubject<T>().OnNext(signal);
    }

    // Подписка на сигнал с возвратом IDisposable для управления подпиской
    public IDisposable Subscribe<T>(Observer<T> handler)
    {
        return GetSubject<T>().Subscribe(handler);
    }


    // Очистка всех подписок (опционально, если нужно сбросить шину)
    public void Clear()
    {
        foreach (var subject in _subjects.Values)
        {
            if (subject is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        _subjects.Clear();
    }
}
