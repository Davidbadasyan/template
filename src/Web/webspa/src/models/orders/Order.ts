 interface IOrder{
    id: number;
    number: string;
    description?: string;
    weight: number;
    isDraft: boolean;
    address: IAddress;
    items: IOrderItem[];
}