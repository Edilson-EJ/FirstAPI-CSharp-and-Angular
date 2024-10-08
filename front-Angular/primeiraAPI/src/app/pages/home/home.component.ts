import { Component, inject, OnInit, ViewChild } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { Pessoa } from '../../Models/pessoas';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import {
  FormsModule,
  NonNullableFormBuilder,
  Validators,
} from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { InputFieldComponent } from '../../components/input-field/input-field.component';
import { NotificationComponent } from '../../components/notification/notification.component';
import { ModalUpdatePersonComponent } from '../../components/modal-update-person/modal-update-person.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    InputFieldComponent,
    NotificationComponent,
    ModalUpdatePersonComponent,
  ],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  @ViewChild('notificationPopup') notificationPopup!: NotificationComponent;

  apiService = inject(ApiService);
  private fb$ = inject(NonNullableFormBuilder);

  // List Person
  pessoas$?: Observable<Pessoa[]>;

  // Search Person
  searchForPerson$?: Observable<Pessoa[]>;
  valueForPerson = '';

  // Modal
  isModalVisible: boolean = false;
  selectedPerson!: Pessoa;

  // Variável de controle para busca
  searchAttempted: boolean = false;

  // Form
  protected personForm = this.fb$.group({
    name: ['', [Validators.required]],
    email: ['', [Validators.required, Validators.email]],
    age: ['', [Validators.required, Validators.min(0), Validators.max(150)]],
    birthDate: ['', Validators.required],
  });

  getPeople() {
    this.pessoas$ = this.apiService.getPeople();
  }

  getSpecificPerson() {
    this.searchAttempted = true;

    if (this.valueForPerson) {
      this.searchForPerson$ = this.apiService.getUpSpecificPerson(
        this.valueForPerson
      );
      this.searchForPerson$.subscribe(
        (persons: Pessoa[]) => {
          if (persons.length > 0) {
            this.notificationPopup.open(
              'Pessoa encontrada!',
              'Sucesso',
              'success'
            );
          } else {
            this.notificationPopup.open(
              'Nenhuma pessoa encontrada!',
              'Erro',
              'error'
            );
          }
        },
        (error) => {
          this.notificationPopup.open(
            'Nome incorreto digitado!',
            'Erro',
            'error'
          );
        }
      );
    } else {
      this.notificationPopup.open(
        'Por favor, insira um nome.',
        'Aviso',
        'warning'
      );
      this.searchForPerson$ = undefined;
    }
  }

  functionAddPerson() {
    if (this.personForm.invalid) {
      this.notificationPopup.open(
        'Preencha todos os campos obrigatórios!',
        'Erro',
        'error'
      );
      return;
    }

    this.apiService.postPerson(this.personForm.value).subscribe(
      (person) => {
        this.getPeople();
        this.notificationPopup.open(
          'Pessoa adicionada com sucesso!',
          'Sucesso',
          'success'
        );
      },
      (error) => {
        this.notificationPopup.open(
          'Erro ao adicionar pessoa!',
          'Erro',
          'error'
        );
      }
    );
  }

  prepareUpdate(pessoa: Pessoa) {
    this.selectedPerson = pessoa;
    this.isModalVisible = true;
  }

  handleUpdate(updatedPerson: Pessoa) {
    this.updatePerson(updatedPerson);
    this.isModalVisible = false;
  }

  closeModal() {
    this.isModalVisible = false;
  }

  updatePerson(updatedPerson: Pessoa) {
    this.apiService.updatePerson(updatedPerson.id, updatedPerson).subscribe(
      () => {
        this.getPeople();
        this.notificationPopup.open(
          'Pessoa atualizada com sucesso!',
          'Sucesso',
          'success'
        );
      },
      (error) => {
        this.notificationPopup.open(
          'Erro ao atualizar pessoa!',
          'Erro',
          'error'
        );
      }
    );
  }

  deletePerson(personId: any) {
    if (!personId) {
      this.notificationPopup.open(
        'O ID da pessoa é necessário para exclusão.',
        'Erro',
        'error'
      );
      return;
    }

    this.apiService.deletePerson(personId).subscribe(
      () => {
        this.getPeople();
        this.notificationPopup.open(
          'Pessoa deletada com sucesso!',
          'Sucesso',
          'success'
        );
      },
      (error) => {
        this.notificationPopup.open('Erro ao deletar pessoa!', 'Erro', 'error');
      }
    );
  }

  ngOnInit(): void {
    this.getPeople();
  }
}
