import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AccountService } from '../services/account.service';
import RegisterDTO from '../DTOs/account/RegisterDTO';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  @Output() cancelRegister = new EventEmitter();

  registerForm: FormGroup;

  constructor(private accountService: AccountService, private formBuilder: FormBuilder, private router: Router) {

  }

  ngOnInit(): void {
    this.initForm();
  }

  register() {
    this.accountService.register(this.registerForm.value).subscribe(respose => {
      this.router.navigateByUrl('/members');
    }, error => {
      console.log(error);
    })
  }

  initForm() {
    this.registerForm = this.formBuilder.group({
      gender: ['male'],
      knownAs: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(25)]],
      confirmPassword: ['', [Validators.required]]
    }, { validators: this.confirmPasswordValidator });
  }

  confirmPasswordValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
    return control?.value.password === control.value.confirmPassword ?
      null : { PasswordNoMatch: true };
  }


  cancel() {
    this.cancelRegister.emit(false);
  }

}
