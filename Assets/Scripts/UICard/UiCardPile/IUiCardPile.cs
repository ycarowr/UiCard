using System;

namespace Tools.UI.Card
{
    public interface IUiCardPile
    {
        Action<IUiCard[]> OnPileChanged { get; set; }
        void AddCard(IUiCard uiCard);
        void RemoveCard(IUiCard uiCard);
    }
}