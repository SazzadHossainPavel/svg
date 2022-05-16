import { RectangleMap } from './rectangle-map.model';

export class Rectangle {
  id: number;
  height: number;
  width: number;
  xCoord: number;
  yCoord: number;
  textXCoordinate: number;
  textYCoordinate: number;
  perimeter: number;
  isNew: boolean;

  constructor(rectanleMap: RectangleMap) {
    this.id = rectanleMap.rectId ? rectanleMap.rectId : 0;
    this.height = rectanleMap.height ? rectanleMap.height : 0;
    this.width = rectanleMap.width ? rectanleMap.width : 0;
    this.xCoord = rectanleMap.xCoordinate ? rectanleMap.xCoordinate : 0;
    this.yCoord = rectanleMap.yCoordinate ? rectanleMap.yCoordinate : 0;
    this.textXCoordinate = this.width / 2 + this.xCoord - 10;
    this.textYCoordinate = this.height + this.yCoord + 20;
    this.perimeter = 2 * (this.height + this.width);
    this.isNew = false;
  }
}
