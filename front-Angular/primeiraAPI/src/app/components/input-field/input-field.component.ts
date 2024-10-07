import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-input-field',
  standalone: true,
  imports: [],
  templateUrl: './input-field.component.html',
  styleUrl: './input-field.component.scss',
})
export class InputFieldComponent {
  @Input() label: string = '';
  @Input() placeholder: string = '';
  @Input() type: string = 'text'; // Definir um tipo padrão
  private _value: string = '';

  @Input()
  set value(value: string) {
    this._value = value;
    this.valueChange.emit(this._value); // Emitir o valor quando for alterado
  }

  get value(): string {
    return this._value;
  }

  @Output() valueChange = new EventEmitter<string>(); // Adicionando o @Output

  onInputChange(event: Event) {
    const input = event.target as HTMLInputElement;
    this.value = input.value; // Isso acionará o setter e emitirá o valor
  }
}
