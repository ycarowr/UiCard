using Patterns.StateMachine;

namespace Tools.UI.Card
{
    /// <summary>
    ///     A complete UI card.
    /// </summary>
    public interface IUiCard : IStateMachineHandler, IUiCardComponents, IUiCardTransform
    {
        IUiPlayerHand Hand { get; }
        bool IsDragging { get; }
        bool IsHovering { get; }
        bool IsDisabled { get; }
        bool IsPlayer { get; }
        void Disable();
        void Enable();
        void Select();
        void Unselect();
        void Hover();
        void Draw();
        void Discard();
    }
}