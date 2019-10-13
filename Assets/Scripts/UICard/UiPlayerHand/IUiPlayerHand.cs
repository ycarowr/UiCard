using System;
using System.Collections.Generic;

namespace Tools.UI.Card
{
    public interface IUiPlayerHand : IUiCardPile
    {
        List<IUiCard> Cards { get; }
        Action<IUiCard> OnCardPlayed { get; set; }
        Action<IUiCard> OnCardSelected { get; set; }
        void PlaySelected();
        void Unselect();
        void PlayCard(IUiCard uiCard);
        void SelectCard(IUiCard uiCard);
        void UnselectCard(IUiCard uiCard);
    }
}