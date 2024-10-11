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
  @Input() type: string = 'text';
  private _value: string = '';

  @Input()
  set value(value: string) {
    this._value = value;
    // Output the value when it changes
    this.valueChange.emit(this._value);
  }

  get value(): string {
    return this._value;
  }

  // Adding @Output
  @Output() valueChange = new EventEmitter<string>();

  onInputChange(event: Event) {
    const input = event.target as HTMLInputElement;
    // This will trigger the setter and output the value
    this.value = input.value;
  }
}
