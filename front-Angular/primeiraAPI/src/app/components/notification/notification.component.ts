import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-notification',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.scss'],
})
export class NotificationComponent {
  @Input() title: string = 'Notificação';
  @Input() message: string = '';
  @Input() type: 'success' | 'error' | 'warning' = 'success';
  isVisible: boolean = false;

  open(
    message: string,
    title: string = 'Notificação',
    type: 'success' | 'error' | 'warning' = 'success'
  ) {
    this.message = message;
    this.title = title;
    this.type = type;
    this.isVisible = true;
  }

  close() {
    this.isVisible = false;
  }
}
