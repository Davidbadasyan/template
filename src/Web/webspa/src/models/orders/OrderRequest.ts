interface IOrderRequest extends IOrder{
    paymentMethodId: number,
    shippingMethodId: number,
    weightUnitId: number,
}