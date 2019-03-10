using UnityEngine;

namespace Patterns
{
    public abstract class StateMB<T> : MonoBehaviour where T : MonoBehaviour
    {
        /// <summary>
        ///     Reference for the parent finite state machine
        /// </summary>
        public T Fsm { get; private set; }

        /// <summary>
        ///     Called by the state machine's Awake
        /// </summary>
        public virtual void OnAwake()
        {
            Log("OnAwake!");
        }

        /// <summary>
        ///     Called by the state machine's Start
        /// </summary>
        public virtual void OnStart()
        {
            Log("OnStart!");
        }

        /// <summary>
        ///     Called by the state machine's Update when the state is on the top of the stack.
        /// </summary>
        public virtual void OnUpdate()
        {
            Log("OnUpdate! "+ name);
        }

        /// <summary>
        ///     Called right after enter the state.
        /// </summary>
        public virtual void OnEnterState()
        {
            Log("OnEnterState <---------", "green");
        }

        /// <summary>
        ///     Called right after left the state.
        /// </summary>
        public virtual void OnExitState()
        {
            Log("OnExitState <---------", "red");
        }


        /// <summary>
        ///     Setter to get a reference of the parent state machine.
        /// </summary>
        /// <param name="stateMachine"></param>
        public void InjectStateMachine(StateMachineMB<T> stateMachine)
        {
            Fsm = stateMachine as T;
            Log("StateMachine Assigned");
        }


        private void Log(string log, string colorName = "black")
        {
            (Fsm as StateMachineMB<T>).Log(string.Format("[" + GetType() + "]: <color={0}><b>" + log + "</b></color>", colorName));   
        }
    }
}