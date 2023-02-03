using IoneVectronConverter.Common;
using IoneVectronConverter.Ione.Mapper;
using IoneVectronConverter.Ione.Orders.Models;
using IoneVectronConverter.Vectron.Models;

namespace IoneVectronConverter.Vectron.Mapper;

public class ReceiptMapper
{
    public Receipt Map(OrderListData orderData)
    {
        string gcText = $"{orderData.IoneId}";
        if (!string.IsNullOrEmpty(orderData.CustomerNotes))
                gcText += $" - {orderData.CustomerNotes}";
        
            Receipt receipt = new Receipt
            {
                Gc = Convert.ToInt32(orderData.TableId),
                Operator = AppSettings.Default.Operator,
                OperatorCode = AppSettings.Default.OperatorCode,
                MediaNo = AppSettings.Default.ReceiptMediaNo,
                GcText = gcText
            };

            foreach (var orderItem in orderData.OrderItemList)
            {
                if (orderItem.ItemId != -10)
                {
                    Plu newPlu = new Plu
                    {
                        Number = Convert.ToInt32(orderItem.APIObjectId),
                        Quantity = orderItem.Quantity.GetDecimal(),
                        OverridePriceFactor = orderItem.Quantity.GetDecimal(),
                        OverridePriceValue = orderItem.Price.GetDecimal(),
                        ModifyPriceAbsoluteFactor = orderItem.DiscountUnit == 2 ? orderItem.DiscountedQuantity.GetDecimal() : 0,
                        ModifyPriceAbsoluteValue = orderItem.DiscountUnit == 2 ? -orderItem.Discount.GetDecimal() : 0,
                        ModifyPricePercentFactor = orderItem.DiscountUnit == 1 ? orderItem.DiscountedQuantity.GetDecimal() : 0,
                        ModifyPricePercentValue = orderItem.DiscountUnit == 1 ? -orderItem.Discount.GetDecimal() : 0
                    };

                    List<OrderItemListItem> additionalOrderItems = new List<OrderItemListItem>();
                    AddAdditionalItems(ref additionalOrderItems, orderItem.ItemList);

                    foreach (var additionalOrderItem in additionalOrderItems)
                    {
                        newPlu.AdditionalPlus.Add(new Plu
                        {
                            Number = Convert.ToInt32(additionalOrderItem.APIObjectId),
                            Quantity = additionalOrderItem.Quantity.GetDecimal(),
                            OverridePriceFactor = additionalOrderItem.Quantity.GetDecimal(),
                            OverridePriceValue = additionalOrderItem.Price.GetDecimal(),
                            ModifyPriceAbsoluteFactor = additionalOrderItem.DiscountUnit == 2 ? additionalOrderItem.DiscountedQuantity.GetDecimal() : 0,
                            ModifyPriceAbsoluteValue = additionalOrderItem.DiscountUnit == 2 ? additionalOrderItem.Discount.GetDecimal() : 0,
                            ModifyPricePercentFactor = additionalOrderItem.DiscountUnit == 1 ? additionalOrderItem.Discount.GetDecimal() : 0,
                            ModifyPricePercentValue = additionalOrderItem.DiscountUnit == 1 ? additionalOrderItem.Discount.GetDecimal() : 0
                        });
                    }
                    receipt.Plus.Add(newPlu);
                }
                else
                    receipt.Discounts.Add(new Discount { Number = AppSettings.Default.TipDiscountNumber, Value = orderItem.Total.GetDecimal() });
            }
            return receipt;
    }
    
    static void AddAdditionalItems(ref List<OrderItemListItem> orderItemList, OrderItemListItem[] orderItems)
    {
        if (orderItems != null && orderItems.Length > 0)
        {
            orderItemList.AddRange(orderItems);
            foreach (var subItem in orderItems)
            {
                AddAdditionalItems(ref orderItemList, subItem.ItemList);
            }
        }
    }
}