import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-confirma',
  templateUrl: './confirma.component.html',
  styleUrls: ['./confirma.component.scss']
})
export class ConfirmaComponent {
  @Input() title?: string = 'Confirma!';
  @Input() messages?: string[] = ['Confirma?'];
  @Input() btnPrimary?: string = 'Sim';
  @Input() btnSecundary?: string = 'Não';
}
