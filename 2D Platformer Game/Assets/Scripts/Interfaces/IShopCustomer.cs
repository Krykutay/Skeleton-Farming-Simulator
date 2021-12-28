public interface IShopCustomer
{
    void BoughtItem(Items.ItemType itemType);
    bool TrySpendTokenAmount(int tokenAmount);
}
