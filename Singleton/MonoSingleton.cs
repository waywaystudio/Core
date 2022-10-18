using UnityEngine;
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
// ReSharper disable MemberCanBeProtected.Global

namespace Wayway.Engine.Singleton
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public bool ShowDebugMessage;

        private static T instance;
        private static object @lock = new ();
        private static bool isFirst = true;

        public static T Instance
        {
            get
            {
                lock (@lock)
                {
                    if (instance == null)
                    {
                        var type = typeof(T);

                        instance = (T)FindObjectOfType(type);

                        if (FindObjectsOfType(type).Length > 1)
                        {
                            Debug.LogError($"[Singleton] Same【{type.Name}】Duplication.");
                            return instance;
                        }
                        
                        if (instance == null)
                        {
                            Debug.LogWarning($"[Singleton] 【{type.Name}】Is Null. \n" +
                                $"MonoSingleton doesn't make Auto {type.Name} gameObject. \n" +
                                "return null;");

                            return null;
                        }
                    }
                }

                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (isFirst)
            {
                instance = this as T;
                isFirst = false;

                if (ShowDebugMessage)
                    if (instance != null)
                        Debug.Log($"[Singleton] Create instance at firstTime : 【{instance.GetType().Name}】");
            }
            else
            {
                if (instance != null)
                {
                    Debug.Log(
                        $"[Singleton] 【{instance.GetType().Name}】 Is already Exist. \n" +
                        $"Called From【{instance.gameObject.name}】 gameObject. \n");
                }
                else
                {
                    instance = this as T;

                    if (ShowDebugMessage)
                        if (instance != null)
                            Debug.Log($"[Singleton] Override instance : 【{instance.GetType().Name}】");
                }
            }
        }

        protected virtual void OnDestroy()
        {
            if (instance != null && instance.gameObject == gameObject)
            {
                if (ShowDebugMessage)
                    Debug.Log($"[Singleton] Destroy instance : 【{instance.GetType().Name}】");

                instance = null;
            }
        }
    }
}