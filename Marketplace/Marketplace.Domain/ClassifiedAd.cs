using Marketplace.Framework;

namespace Marketplace.Domain
{
    public enum ClassifiedAdState
    {
        PendingReview,
        Active,
        Inactive,
        MarkedAsSold
    };

    public class ClassifiedAd : Entity
    {
        public ClassifiedAdId Id { get; }
        public UserId OwnerId { get; private set; }
        public UserId ApprovedBy { get; private set; }
        public ClassifiedAdTitle Title { get; private set; }
        public ClassifiedAdText Text { get; private set; }
        public Money Price { get; private set; }
        public ClassifiedAdState State { get; private set; }
        
        public ClassifiedAd(ClassifiedAdId id, UserId ownerId)
        {
            Id = id;
            OwnerId = ownerId;
            State = ClassifiedAdState.Inactive;
            EnsureValidState();
            Raise(new Events.ClassifiedAdCreated { Id = id, OwnerId = ownerId });
        }


        public void SetTitle(ClassifiedAdTitle title)
        {
            Title = title;
            EnsureValidState();
            Raise(new Events.ClassifiedAdTitleChanged { Id = Id, Title = title });
        }

        public void UpdateText(ClassifiedAdText text)
        {
            Text = text;
            EnsureValidState();
            Raise(new Events.ClassifiedAdTextUpdated { Id = Id, AdText = Text });

        }

        public void UpdatePrice(Money price)
        {
            Price = price;
            EnsureValidState();
            Raise(new Events.ClassifiedPriceUpdated { Id = Id, CurrencyCode = price.Currency.CurrencyCode, Price = price.Amount });

        }

        public void RequestToPublish()
        {
            State = ClassifiedAdState.PendingReview;
            EnsureValidState();
            Raise(new Events.ClassifiedAdSentForReview { Id = Id });

        }

        protected void EnsureValidState()
        {
            bool stateValid = true;

            switch(State)
            {
                case ClassifiedAdState.PendingReview: stateValid = Title != null && Text != null && Price?.Amount > 0; break;
                case ClassifiedAdState.Active: stateValid = Title != null && Text != null && Price?.Amount > 0 && ApprovedBy != null; break;
            }

            var valid =
                Id != null &&
                OwnerId != null &&
                stateValid;
            
            if (!valid)
                throw new InvalidEntityStateException(this, $"Post-checks failed in state {State}");
        }
    }
}
