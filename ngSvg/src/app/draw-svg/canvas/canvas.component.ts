import { Component, OnInit } from '@angular/core';
import { RectangleMap } from '../../shared/models/rectangle-map.model';
import { Rectangle } from '../../shared/models/rectangle.model';
import { RectangleApiService } from '../../shared/services/rectangle-api.service';

@Component({
  selector: 'app-canvas',
  styleUrls: ['./canvas.component.css'],
  templateUrl: './canvas.component.svg',
})
export class CanvasComponent implements OnInit {
  rectangles: Rectangle[] = [];
  selectedRectangle: Rectangle | null = null;

  constructor(private rectangleApiService: RectangleApiService) {}

  ngOnInit(): void {
    this.rectangleApiService.getStaticRectangles().subscribe(
      (rectangles) => {
        this.rectangles = rectangles;
      },
      () => {
        console.log('could not load the rectangles');
      }
    );
  }

  startDrawing(event: MouseEvent) {
    const rectangleIndex = this.rectangles.findIndex(
      (r) =>
        event.offsetX >= r.xCoord &&
        event.offsetX <= r.xCoord + r.width &&
        event.offsetY >= r.yCoord &&
        event.offsetY <= r.yCoord + r.height
    );

    if (rectangleIndex !== -1) {
      this.selectedRectangle = this.rectangles[rectangleIndex];
    } else {
      this.selectedRectangle = {
        id: this.rectangles.length + 1,
        height: 0,
        width: 0,
        xCoord: event.offsetX,
        yCoord: event.offsetY,
        textXCoordinate: 0,
        textYCoordinate: 0,
        perimeter: 0,
        isNew: true,
      };
    }
  }

  keepDrawing(event: MouseEvent) {
    if (this.selectedRectangle) {
      this.selectedRectangle.width = Math.abs(
        event.offsetX - this.selectedRectangle.xCoord
      );
      this.selectedRectangle.height = Math.abs(
        event.offsetY - this.selectedRectangle.yCoord
      );
      this.selectedRectangle.textXCoordinate =
        this.selectedRectangle.width / 2 + this.selectedRectangle.xCoord - 10;
      this.selectedRectangle.textYCoordinate =
        this.selectedRectangle.height + this.selectedRectangle.yCoord + 20;
      this.selectedRectangle.perimeter =
        2 * (this.selectedRectangle.height + this.selectedRectangle.width);

      const rectangleIndex = this.rectangles.findIndex(
        (r) => r.id === this.selectedRectangle?.id
      );

      if (rectangleIndex !== -1) {
        this.rectangles[rectangleIndex] = this.selectedRectangle;
      } else {
        this.rectangles.push(this.selectedRectangle);
      }
    }
  }

  stopDrawing() {
    if (!this.selectedRectangle || this.selectedRectangle.perimeter === 0) {
      return;
    }

    const rectangleMap: RectangleMap = {
      height: this.selectedRectangle.height,
      width: this.selectedRectangle.width,
      xCoordinate: this.selectedRectangle.xCoord,
      yCoordinate: this.selectedRectangle.yCoord,
      svgId: 1,
    };

    if (!this.selectedRectangle.isNew) {
      rectangleMap.rectId = this.selectedRectangle.id;

      this.rectangleApiService.updateRectangle(rectangleMap).subscribe(
        () => {
          console.log('rectangle is updated');
        },
        () => {
          console.log('error updating rectangle');
        }
      );
    } else {
      this.rectangleApiService.createRectangle(rectangleMap).subscribe(
        () => {
          console.log('rectangle is created');
        },
        () => {
          console.log('error creating rectangle');
        }
      );
    }

    this.selectedRectangle = null;
  }
}
