﻿using System;

namespace Marketplace.Domain
{
    public class ClassifiedAd
    {
        private Guid _ownerId;
        private string _title;
        private string _text;
        private decimal _price;
        
        public Guid Id { get; }

        public ClassifiedAd(Guid id, Guid ownerId)
        {
            if (id == default)
                throw new ArgumentNullException("identity must be specified", nameof(id));

            if (ownerId == default)
                throw new ArgumentException("owner id must be specified", nameof(ownerId));

            Id = id;
            _ownerId = ownerId;
        }

        public void SetTitle(string title) => _title = title;
        public void UpdateText(string text) => _text = text;
        public void UpdatePrice(decimal price) => _price = price;
    }
}
