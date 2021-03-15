

namespace AuctionModel
{
    public class AuctionItem
    {
        public string ItemName { get; private set; }
        public float InitialValue { get; private set; }
        public float MinAditionalValue { get; private set; }
        public float CurrentValue { get; set; }
        public string CurrentOwner { get; set; }
        public int RemainingTime { get; set; }
        public bool IsAvailable { get; set; }

        public AuctionItem(string itemName, float initialValue, float minAditionalValue, float currentValue, string currentOwner, int remainingTime, bool isAvailable)
        {
            this.ItemName = itemName;
            this.InitialValue = initialValue;
            this.MinAditionalValue = minAditionalValue;
            this.CurrentValue = currentValue;
            this.CurrentOwner = currentOwner;
            this.RemainingTime = remainingTime;
            this.IsAvailable = isAvailable;
        }
    }
}
