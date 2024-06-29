using ProjectContext.WindowsManager;
using UnityEngine;

namespace UI
{
    public abstract class Window : MonoBehaviour
    {
        [field: SerializeField] public EWindow EWindow { get; private set; }
        public abstract void Open();
        public abstract void Close();
    }
}