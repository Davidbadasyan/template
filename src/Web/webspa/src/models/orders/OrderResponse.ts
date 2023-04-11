interface IOrderResponse extends IOrder {
    status: string,
    paymentMethod: string,
    shippingMethod: string,
    weightUnit: string,
    buyer: IBuyer
}