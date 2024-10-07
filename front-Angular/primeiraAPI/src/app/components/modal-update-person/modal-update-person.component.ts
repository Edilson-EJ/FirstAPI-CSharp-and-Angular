import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Pessoa } from '../../Models/pessoas';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-modal-update-person',
  standalone: true,
  imports: [ReactiveFormsModule, FormsModule],
  templateUrl: './modal-update-person.component.html',
  styleUrl: './modal-update-person.component.scss',
})
export class ModalUpdatePersonComponent {
  @Input() person!: Pessoa;
  @Output() close = new EventEmitter<void>();
  @Output() update = new EventEmitter<Pessoa>();

  onSubmit() {
    this.update.emit(this.person);
  }

  closeModal() {
    this.close.emit();
  }
}
