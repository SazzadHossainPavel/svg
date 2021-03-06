import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DrawSvgModule } from './draw-svg/draw-svg.module';

const routes: Routes = [
  { path: '', loadChildren: () => DrawSvgModule },
  {
    path: '**',
    redirectTo: '',
    pathMatch: 'full',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
