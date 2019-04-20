namespace Tools.UI.Card
{
    public interface IUiCardHand : IUiCardPile
    {
        void PlaySelected();
        void PlayCard(IUiCard uiCard);
        void SelectCard(IUiCard uiCard);
        void UnselectCard(IUiCard uiCard);
    }
}