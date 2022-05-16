import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CanvasComponent } from './canvas/canvas.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    component: CanvasComponent,
  },
];

@NgModule({
  declarations: [CanvasComponent],
  imports: [CommonModule, RouterModule.forChild(routes)],
})
export class DrawSvgModule {}
