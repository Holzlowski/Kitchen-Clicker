using UnityEngine;

namespace Singletons
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        protected static T Instance
        {
            get
            {
                if (instance != null)
                    return instance;
                Debug.LogWarning($"{typeof(T)} instance doesn't exist in scene (may cause a NullPointerException)");
                return null;
            }
        }

        private static T instance;

        protected virtual void Awake()
        {
            if (instance != null)
            {
                Debug.LogError($"There is more than one instance of {instance.GetType()} in the current scene.");
                Destroy(this);
            }

            instance = this as T;
        }
    }
}