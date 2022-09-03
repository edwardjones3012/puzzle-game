using edw.Singletons;
using System;
using UnityEngine;

namespace edw.Events
{
    public class GameEvents : SingletonMonoBehaviour<GameEvents, Persisted>
    {
        protected override void Awake()
        {
            base.Awake();
        }

        public GameEvent<int> ExampleEvent = new GameEvent<int>();

        public GameEvent PillarMoved = new GameEvent();
    }

    [Serializable]
    public class GameEvent
    {
        public event Action Event;

        public void Invoke() => Event?.Invoke();

        public void AddDelegate(Action subscriber) => Event += subscriber;

        public void RemoveDelegate(Action subscriber) => Event -= subscriber;
    }

    [Serializable]
    public class GameEvent<T>
    {
        public event Action<T> Event;

        public void Invoke(T value) => Event?.Invoke(value);

        public void AddDelegate(Action<T> subscriber) => Event += subscriber;

        public void RemoveDelegate(Action<T> subscriber) => Event -= subscriber;
    }

    [Serializable]
    public class GameEvent<T, J>
    {
        public event Action<T, J> Event;

        public void Invoke(T value, J secondaryValue) => Event?.Invoke(value, secondaryValue);

        public void AddDelegate(Action<T, J> subscriber) => Event += subscriber;

        public void RemoveDelegate(Action<T, J> subscriber) => Event -= subscriber;
    }

    [Serializable]
    public class GameEvent<T, J, E>
    {
        public event Action<T, J, E> Event;

        public void Invoke(T value, J secondaryValue, E tertiaryValue) => Event?.Invoke(value, secondaryValue, tertiaryValue);

        public void AddDelegate(Action<T, J, E> subscriber) => Event += subscriber;

        public void RemoveDelegate(Action<T, J, E> subscriber) => Event -= subscriber;
    }
}