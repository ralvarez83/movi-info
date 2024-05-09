import { OrderBy, OrderType } from "./OrderTypes.d";

export class Order {
  orderBy : OrderBy
  orderType: OrderType
  
  constructor(orderBy: OrderBy, orderType: OrderType) {
    this.orderBy = orderBy
    this.orderType = orderType
  }

  public isNone (): boolean {
    return this.orderType === OrderType.NONE
  }
}