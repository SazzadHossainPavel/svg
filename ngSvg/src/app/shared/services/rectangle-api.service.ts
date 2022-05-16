import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { RectangleMap } from '../models/rectangle-map.model';
import { Rectangle } from '../models/rectangle.model';

@Injectable({
  providedIn: 'root',
})
export class RectangleApiService {
  private baseUrl: string;

  constructor(private http: HttpClient) {
    this.baseUrl = environment.serverBaseUrl;
  }

  getStaticRectangles(): Observable<Rectangle[]> {
    return this.http.get<any>(`${this.baseUrl}/svg.json`).pipe(
      map((response) => response.rectangle),
      map((response: RectangleMap[]) =>
        response.filter((item) => item.svgId === 1)
      ),
      map((itemsMap) => itemsMap.map((item) => new Rectangle(item)))
    );
  }

  getRectangles(): Observable<Rectangle[]> {
    return this.http.get<RectangleMap[]>(`${this.baseUrl}/api/rectangle`).pipe(
      map((response) => response.filter((item) => item.svgId === 1)),
      map((itemsMap) => itemsMap.map((item) => new Rectangle(item)))
    );
  }

  createRectangle(rectangle: RectangleMap) {
    return this.http.post(`${this.baseUrl}/api/rectangle`, rectangle);
  }

  updateRectangle(rectangle: RectangleMap) {
    return this.http.put(
      `${this.baseUrl}/api/rectangle/${rectangle.rectId}`,
      rectangle
    );
  }
}
