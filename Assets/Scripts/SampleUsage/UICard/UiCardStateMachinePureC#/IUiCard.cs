using Patterns.StateMachine;

namespace Tools.UI.Card
{
    public interface IUiCard : IStateMachineHandler, IUiCardComponents, IUiCardTransform
    {
        bool IsDragging { get; }
        bool IsHovering { get; }
        void Play();
        void Disable();
        void Enable();
        void Select();
        void Unselect();
        void Hover();
        void Draw();
        void Discard();
    }
}