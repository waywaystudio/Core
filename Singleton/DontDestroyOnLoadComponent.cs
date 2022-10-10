using UnityEngine;

namespace Wayway.Engine.Singleton
{
    public class DontDestroyOnLoadComponent : MonoBehaviour
    {
        public bool ShowDebugMessage;
        public MonoBehaviour TargetBehaviour;

        private void Awake()
        {
            if (!transform.root.gameObject.Equals(gameObject))
            {
                if (ShowDebugMessage)
                {
                    Debug.LogError($"Don't Destroyed Component must be in root gameObject! \n" +
                    $"Root Object is : {transform.root.gameObject} \n" +
                    $"Current Object is : {gameObject}");
                }

                return;
            }

            // Duplication Check        
            var components = FindObjectsOfType(TargetBehaviour.GetType());

            switch (components.Length)
            {
                case > 1:
                {
                    if (ShowDebugMessage)
                    {
                        Debug.Log($"{TargetBehaviour.GetType()} is came from another scene. \n" +
                                  $"In this scene {TargetBehaviour.GetType()} gameObject Destroy");
                    }

                    /* Annotation */
                    Destroy(gameObject);
                    break;
                }
                case 1:
                {
                    if (ShowDebugMessage)
                    {
                        Debug.Log($"Don't Destroyed On Load :: {TargetBehaviour.GetType()} registered");
                    }

                    DontDestroyOnLoad(gameObject);
                    break;
                }
                default:
                {
                    if (ShowDebugMessage)
                    {
                        Debug.LogError($"Can't Find {TargetBehaviour.GetType()}.");
                    }

                    break;
                }
            }
        }
    }
}

/* Annotation
if (gameObject.SetActive(false)) skipped. Awake, Start called twice; */
