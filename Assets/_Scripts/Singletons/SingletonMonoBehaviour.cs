using UnityEngine;

namespace edw.Singletons
{
    public class SingletonMonoBehaviour<T, U> : MonoBehaviour where T : SingletonMonoBehaviour<T, U>
    {
        public static T Instance = null;

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = (T)this;
                if (typeof(U) == typeof(Persisted))
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
            else
            {
                Destroy(this);
            }
        }
    }
}
