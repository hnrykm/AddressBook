import { Component, OnInit } from '@angular/core';
import { NzModalService } from 'ng-zorro-antd/modal';
import { CreateDialogComponent } from './create-dialog/create-dialog.component';
import { Person } from './models';
import { PersonService } from './person.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  data: Person[] = [];

  constructor(
    private readonly api: PersonService,
    private readonly dialog: NzModalService,
  ) {}

  ngOnInit() {
    this.api.getAll().subscribe({
      next: (people: Person[]) => this.data = people,
      error: (error) => console.log(error),
    });
  }

  openCreateModal() {
    this.dialog.create({
      nzTitle: 'Create Employee',
      nzContent: CreateDialogComponent,
      nzOkText: 'Create',
    }).afterClose.subscribe(data => {
      this.api.create(data).subscribe({
        next: (person: Person) => this.data.push(person),
        error: (error) => console.log(error),
      });
    });
  }

  openEditModal(index: number) {
    this.dialog.create({
      nzTitle: 'Update Employee',
      nzContent: CreateDialogComponent,
      nzData: { person: this.data[index] },
      nzOkText: 'Update',
    }).afterClose.subscribe(data => {
      if (data) {
        this.api.update(data).subscribe({
          next: (person: Person) => this.data[index] = person,
          error: (error) => console.log(error),
        });
      }
    });
  }

  delete(index: number) {
    this.api.delete(this.data[index].id).subscribe({
        next: () => (this.data = this.data.filter(x => x.id !== this.data[index].id)),
        error: (error) => console.log(error),
      },
    );
  }
}
