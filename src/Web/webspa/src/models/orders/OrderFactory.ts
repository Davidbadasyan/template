class OrderFactory {
    public static createDefaultOrder(): IOrderRequest {

        let orderData: IOrderRequest = {
            id: 0,
            number: '',
            weight: 0,
            paymentMethodId: 1,
            shippingMethodId: 1,
            weightUnitId: 1,
            description: '',
            items: [this.createDefaultOrderItem()],
            address: {
                street: '',
                city: '',
                state: '',
                country: '',
                zipCode: 0,
            },
            isDraft: false,
        };

        return orderData;
    }


    public static createDefaultOrderItem(): IOrderItem {
        let item: IOrderItem = {
            id: 0,
            productName: '',
            unitPrice: 0,
            discount: 0,
            units: 1,
        }

        return item;
    }
}

export { OrderFactory }