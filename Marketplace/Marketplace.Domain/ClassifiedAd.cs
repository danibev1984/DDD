using System;

namespace Marketplace.Domain
{
    public enum ClassifiedAdState
    {
        PendingReview,
        Active,
        Inactive,
        MarkedAsSold
    };

    public class ClassifiedAd
    {
        public ClassifiedAdId Id { get; }
        public UserId OwnerId { get; private set; }
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
        }


        public void SetTitle(ClassifiedAdTitle title)
        {
            Title = title;
            EnsureValidState();
        }

        public void UpdateText(ClassifiedAdText text)
        {
            Text = text;
            EnsureValidState();
        }

        public void UpdatePrice(Money price)
        {
            Price = price;
            EnsureValidState();
        }
         
        public void RequestToPublish()
        {
            State = ClassifiedAdState.PendingReview;
            EnsureValidState();
        }

        protected void EnsureValidState()
        {
            if (Title == null)
                throw new InvalidEntityStateException(this, "title cannot be empty");

            if (Text == null)
                throw new InvalidEntityStateException(this, "text cannot be empty");

            if (Price?.Amount == 0)
                throw new InvalidEntityStateException(this, "price cannot be zero");


        }
    }
}
