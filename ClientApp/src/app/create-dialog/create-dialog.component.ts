import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NZ_MODAL_DATA, NzModalRef } from 'ng-zorro-antd/modal';
import { Person } from '../models';

export interface ModalData {
  person: Person;
}

@Component({
  selector: 'app-create-dialog',
  templateUrl: './create-dialog.component.html',
  styleUrls: ['./create-dialog.component.scss'],
})
export class CreateDialogComponent implements OnInit {

  public form: FormGroup = this.fb.group({});

  constructor(
    @Inject(NZ_MODAL_DATA) public data: ModalData | undefined,
    private readonly modal: NzModalRef,
    private readonly fb: FormBuilder,
  ) {}

  ngOnInit() {
    this.form = this.createFormFromPerson(this.data?.person);
  }

  submit(): void {
    this.modal.destroy(this.form.getRawValue() as Person);
  }

  cancel(): void {
    this.modal.destroy();
  }

  private createFormFromPerson(person: Person | undefined): FormGroup {
    return this.fb.group({
      id: [person?.id || ''],
      firstName: [person?.firstName || '', Validators.required],
      lastName: [person?.lastName || '', Validators.required],
      dob: [person?.dob || '', Validators.required],
      address: this.fb.group({
        line1: [person?.address?.line1 || '', Validators.required],
        line2: [person?.address?.line2 || ''],
        city: [person?.address?.city || '', Validators.required],
        state: [person?.address?.state || '', Validators.required],
        zipCode: [person?.address?.zipCode || '', Validators.required],
      }),
    });
  }

}
